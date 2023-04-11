using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : CreatureController
{
    Vector2 _moveDir = Vector2.zero;
    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

	float EnvCollectDist { get; set; } = 1.0f;

	[SerializeField]
	Transform _indicator;
	[SerializeField]
	Transform _fireSocket;

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		_speed = 5.0f;

		Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
		Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;

		GetComponent<SpriteRenderer>().sortingOrder = (int)Define.SortOrder.Player;

		StartProjectile();
		StartEgoSword();
		StartPoisonField();

		return true;
	}

	void OnDestroy()
	{
        if (Managers.Game != null)
	    	Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
	}

	void HandleOnMoveDirChanged(Vector2 dir)
    {
        _moveDir = dir;
    }

	public override void UpdateController()
    {
		base.UpdateController();

        MovePlayer();
		CollectEnv();
	}

    void MovePlayer()
    {
		Vector3 dir = _moveDir * _speed *Time.deltaTime;
        transform.position += dir;

		if (_moveDir != Vector2.zero)
		{
			_indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-dir.x, dir.y) * 180 / Mathf.PI);
		}

		GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

	void CollectEnv()
	{
		float sqrCollectDist = EnvCollectDist * EnvCollectDist;

		List<GemController> gems = Managers.Object.Gems.ToList();
		foreach (GemController gem in gems)
		{
			Vector3 dir = gem.transform.position - transform.position;
			if (dir.sqrMagnitude <= sqrCollectDist)
			{
				Managers.Game.Gem += 1;
				Managers.Object.Despawn(gem);
			}
		}

		var findGems = GameObject.Find("@Grid").GetComponent<GridController>().GatherObjects(transform.position, EnvCollectDist + 0.5f);

		Debug.Log($"SearchGems({findGems.Count}) TotalGems({gems.Count})");
	}

	public override void OnDamaged(BaseController attacker, int damage)
	{
		base.OnDamaged(attacker, damage);

		Debug.Log($"OnDamaged ! {Hp}");

        // TEMP
		//CreatureController cc = attacker as CreatureController;
		//if (cc.IsValid() == false)
		//	return;

		//cc.OnDamaged(this, 10000);
	}

	// TEMP
	#region FireProjectile
	Coroutine _coFireProjectile;
	void StartProjectile()
	{
		if (_coFireProjectile != null)
			StopCoroutine(_coFireProjectile);

		_coFireProjectile = StartCoroutine(CoStartProjectile());
	}

	IEnumerator CoStartProjectile()
	{
		WaitForSeconds wait = new WaitForSeconds(0.5f);

		while (true)
		{
			ProjectileController pc = Managers.Object.Spawn<ProjectileController>(_fireSocket.position, 1);
			pc.SetInfo(1, this, (_fireSocket.position - _indicator.position).normalized);
			yield return wait;
		}
	}
	#endregion

	#region EgoSword
	EgoSwordController _egoSword;
	void StartEgoSword()
	{
		if (_egoSword.IsValid())
			return;

		_egoSword = Managers.Object.Spawn<EgoSwordController>(_indicator.position, Define.EGO_SWORD_ID);
		_egoSword.transform.SetParent(_indicator);

		_egoSword.ActivateSkill();
	}

	#endregion

	#region PoisonField
	Coroutine _coSpawnPoisonField;

	void StartPoisonField()
	{
		if (_coSpawnPoisonField != null)
			StopCoroutine(_coSpawnPoisonField);

		_coSpawnPoisonField = StartCoroutine(CoStartPoisonField());
	}

	IEnumerator CoStartPoisonField()
	{
		WaitForSeconds wait = new WaitForSeconds(30.0f);

		while (true)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector3 dir = new Vector3(Random.Range(0, 6), Random.Range(0, 6), 0);
				Managers.Object.Spawn<PoisonFieldController>(transform.position + dir, Define.POISON_FIELD_ID);
			}
			
			yield return wait;
		}
	}
	#endregion
}

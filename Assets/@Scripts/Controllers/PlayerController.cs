using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;
using UnityEngine.EventSystems;
using static Define;

public class PlayerController : CreatureController
{
    Vector2 _moveDir = Vector2.zero;
	float _speed = 5.0f;

	public Vector2 MoveDir
	{ 
		get { return _moveDir; }
		set { _moveDir = value.normalized; }
	}

	void Start()
    {
		Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;
		ObjectType = ObjectType.Player;

		_coSpawnIcebolt = StartCoroutine(CoSpawnIcebolt());
	}

	void OnDestroy()
	{
		if (Managers.Game != null)
			Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
	}

	// Update is called once per frame
	void Update()
    {
		//UpdateInput();
		MovePlayer();
	}

	void MovePlayer()
	{
		//Debug.Log(_moveDir);
		//Debug.Log(Managers.Instance.MoveDir);
		Vector3 dir = _moveDir * _speed * Time.deltaTime;
		transform.position += dir;
	}

	void HandleOnMoveDirChanged(Vector2 dir)
	{
		_moveDir = dir;
	}

	public override void OnDamaged(BaseController attacker, int damage)
	{
		base.OnDamaged(attacker, damage);

		Debug.Log($"OnDamaged ! {Hp}");

		CreatureController cc = attacker as CreatureController;
		cc?.OnDamaged(this, 10000);
	}

	Coroutine _coSpawnIcebolt;

	IEnumerator CoSpawnIcebolt()
	{
		while (true)
		{
			ProjectileController pc = Managers.Object.Spawn<ProjectileController>();
			pc.SetInfo(transform.position, MoveDir);

			yield return new WaitForSeconds(1.0f);
		}

		yield return null;
	}
}

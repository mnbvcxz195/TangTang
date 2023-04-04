using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : CreatureController
{
    public override bool Init()
	{
		if (base.Init() == false)
			return false;

		ObjectType = ObjectType.Monster;

		return true;
	}

	void FixedUpdate()
	{
		PlayerController pc = Managers.Game.Player;
		if (pc == null)
			return;

		Vector3 dir = pc.transform.position - transform.position;
		Vector3 newPos = transform.position + dir.normalized * Time.deltaTime * _speed;
		GetComponent<Rigidbody2D>().MovePosition(newPos);
		GetComponent<SpriteRenderer>().flipX = dir.x > 0;
	}

	protected override void OnDead()
	{
		if (_coDotDamage != null)
			StopCoroutine(_coDotDamage);

		// 아이템 스폰
		int itemGemID = Random.Range(0, 3);
		GemController gc = Managers.Object.Spawn<GemController>(itemGemID);
		gc.transform.position = transform.position;

		Managers.Object.Despawn(this);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		PlayerController target = collision.gameObject.GetComponent<PlayerController>();
		if (target == null)
			return;

		if (_coDotDamage != null)
			StopCoroutine(_coDotDamage);

		_coDotDamage = StartCoroutine(CoStartDotDamage(target));
	}

	public void OnCollisionExit2D(Collision2D collision)
	{
		PlayerController target = collision.gameObject.GetComponent<PlayerController>();
		if (target == null)
			return;

		if (_coDotDamage != null)
			StopCoroutine(_coDotDamage);
		_coDotDamage = null;
	}

	Coroutine _coDotDamage;
	public IEnumerator CoStartDotDamage(PlayerController target)
	{
		while (true)
		{
			target.OnDamaged(this, 2);
			yield return new WaitForSeconds(0.1f);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ProjectileController : BaseController
{
	// Owner?
	// Target?
	Vector2 _spawnPos;
	Vector3 _dir = Vector3.zero;
	float _speed = 10.0f;

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		ObjectType = ObjectType.Projectile;

		return true;
	}

	public void SetInfo(Vector2 position, Vector2 dir)
	{
		transform.position = position;
		_spawnPos =  position;
		_dir = dir.normalized;
		//GetComponent<Rigidbody2D>().velocity = _dir * _speed;
	}

	private void Update()
	{
		Vector3 pos = transform.position + _dir * _speed * Time.deltaTime;
		transform.position = pos;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		MonsterController monster = collision.transform.GetComponent<MonsterController>();
		if (monster == null)
			return;

		if (monster.isActiveAndEnabled == false)
		{
			int a = 3;
		}

		monster.OnDamaged(this, 10);

		Managers.Object.Despawn(this);
	}
}

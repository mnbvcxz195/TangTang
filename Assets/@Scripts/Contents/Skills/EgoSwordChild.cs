using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoSwordChild : MonoBehaviour
{
	BaseController _owner;
	int _damage;

	public void SetInfo(BaseController owner, int damage)
	{
		_owner = owner;
		_damage = damage;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		MonsterController monster = collision.transform.GetComponent<MonsterController>();
		if (monster.IsValid() == false)
			return;

		monster.OnDamaged(_owner, _damage);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : SkillController
{
	CreatureController _owner;
	Vector3 _moveDir;
	float _speed = 10.0f;
	float _lifeTime = 10.0f;

	public override bool Init()
	{
		base.Init();

		StartDestroy(_lifeTime);

		return true;
	}

	public void SetInfo(int templateID, CreatureController owner, Vector3 moveDir)
	{
		if (Managers.Data.SkillDic.TryGetValue(templateID, out Data.SkillData data) == false)
		{
			Debug.LogError("ProjectileController SetInfo Failed");
			return;
		}

		_owner = owner;
		_moveDir = moveDir;
		SkillData = data;
		// TODO : Data parsing
	}

	public override void UpdateController()
	{
		base.UpdateController();

		transform.position += _moveDir * _speed * Time.deltaTime;
	}

	// 데이터시트를 이용해서 조절할 수 있게 만들 것인가?
	// 상속 구조로 하드코딩 할 것인가?

	void OnTriggerEnter2D(Collider2D collision)
	{
		MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
		if (mc.IsValid() == false)
			return;
		if (this.IsValid() == false)
			return;

		mc.OnDamaged(_owner, SkillData.damage);

		// TODO : 몇 번 피격 가능한지?
		StopDestroy();
			
		Managers.Object.Despawn(this);
	}
}

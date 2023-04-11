using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PoisonFieldController : SkillController
{
	public override bool Init()
	{
		base.Init();

		StartDestroy(30.0f);
		StartApplyDamage();

		return true;
	}

	public void SetInfo()
	{
		// TODO Data
	}

	HashSet<MonsterController> _targets = new HashSet<MonsterController>();

	public void OnTriggerEnter2D(Collider2D collision)
	{
		MonsterController target = collision.transform.GetComponent<MonsterController>();
		if (target.IsValid() == false)
			return;

		_targets.Add(target);
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		MonsterController target = collision.transform.GetComponent<MonsterController>();
		if (target == null)
			return;

		if (target.IsValid() == false)
		{
			int a = 3;
		}

		_targets.Remove(target);
	}

	#region Damage
	Coroutine _coApplyDamage;

	void StartApplyDamage()
	{
		StopApplyDamage();
		_coApplyDamage = StartCoroutine(CoApplyDamage());
	}

	void StopApplyDamage()
	{
		if (_coApplyDamage != null)
			StopCoroutine(_coApplyDamage);
		_coApplyDamage = null;
	}

	IEnumerator CoApplyDamage()
	{
		while (true)
		{
			var targets = _targets.ToList();

			foreach (var target in targets)
			{
				if (target.IsValid() == false)
				{
					_targets.Remove(target);
					continue;
				}

				target.OnDamaged(this, 100);
			}

			yield return new WaitForSeconds(1.0f);
		}
	}

	public override void OnDestroyController()
	{
		StopApplyDamage();
	}
	#endregion
}

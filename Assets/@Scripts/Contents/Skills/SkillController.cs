using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EgoSword : ÆòÅ¸
// WindCutter : ºÎ¸Þ¶û?
// FireProjectile : Åõ»çÃ¼
// PoisonField_AOE_01 : ¹Ù´Ú
public class SkillController : BaseController
{
	public Define.SkillType SkillType { get; set; }
	public Data.SkillData SkillData { get; protected set; }

	public override bool Init()
	{
		base.Init();

		return true;
	}

	#region Destroy
	Coroutine _coDestroy;

	public void StartDestroy(float delaySeconds)
	{
		StopDestroy();
		_coDestroy = StartCoroutine(CoDestroy(delaySeconds));
	}

	public void StopDestroy()
	{
		if (_coDestroy != null)
		{
			StopCoroutine(_coDestroy);
			_coDestroy = null;
		}
	}

	IEnumerator CoDestroy(float delaySeconds)
	{
		yield return new WaitForSeconds(delaySeconds);
	
		if (this.IsValid())
		{
			OnDestroyController();
			Managers.Object.Despawn(this);
		}	
	}

	public virtual void OnDestroyController()
	{

	}
	#endregion
}

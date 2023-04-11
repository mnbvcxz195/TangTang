using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : BaseController
{
	public override bool Init()
	{
		base.Init();

		GetComponent<SpriteRenderer>().sortingOrder = (int)Define.SortOrder.Env;

		return true;
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GemController : BaseController
{
	int _gemID = 0;

	public override bool Init()
	{
		if (base.Init() == false)
			return false;


		return true;
	}

	public void SetInfo(int gemID)
	{
		_gemID = gemID;

		// TEMP
		string spriteName;
		if (gemID == 0)
			spriteName = "EXPJam_01.png";
		else if (gemID == 1)
			spriteName = "EXPJam_02.png";
		else
			spriteName = "EXPJam_03.png";

		Sprite sprite = Managers.Resource.Load<Sprite>(spriteName);
		GetComponent<SpriteRenderer>().sprite = sprite;
	}

}

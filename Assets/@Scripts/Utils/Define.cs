using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
	public enum Scene
	{
		Unknown,
		DevScene,
		GameScene,
	}

	public enum ObjectState
	{
		Idle,
		Moving,
		Dead
	}

	public enum ObjectType
	{
		Player,			//플레이어
		Monster,		//몬스터
		Projectile,		//투사체
		Env				//환경물
	}

	public enum Sound
	{
		Bgm,
		Effect,
		MaxCount,
	}
}

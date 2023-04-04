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
		Player,			//�÷��̾�
		Monster,		//����
		Projectile,		//����ü
		Env				//ȯ�湰
	}

	public enum Sound
	{
		Bgm,
		Effect,
		MaxCount,
	}
}

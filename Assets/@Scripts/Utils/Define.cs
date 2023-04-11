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

	public enum Sound
	{
		Bgm,
		Effect,
	}

	public enum ObjectType
	{
		Player,
		Monster,
		Projectile,
		Env
	}

	public enum SortOrder
	{
		Env = 105,
		Player = 200,
		Monster = 200,
	}

	// 플레이어 스킬
	public enum SkillType
	{
		None,
		Melee,
		Projectile,
		Etc,
	}

	public const int EGO_SWORD_ID = 10;
	public const int POISON_FIELD_ID = 20;
}

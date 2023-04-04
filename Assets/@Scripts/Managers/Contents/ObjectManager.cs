using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
	public PlayerController Player { get; private set; }
	public HashSet<MonsterController> Monsters { get; } = new HashSet<MonsterController>();
	public HashSet<GemController> Gems { get; } = new HashSet<GemController>();
	public HashSet<ProjectileController> Projectiles { get; } = new HashSet<ProjectileController>();

	public Transform MonsterRoot
	{
		get
		{
			GameObject root = GameObject.Find("@Monster");
			if (root == null)
				root = new GameObject { name = "@Monster" };
			return root.transform;
		}
	}

	public T Spawn<T>(int templateID = 0) where T : BaseController              //T는 BaseController를 상속 받은 것만 만들어준다.
    {
		System.Type type = typeof(T);

		if (type == typeof(PlayerController))
		{
			// TODO : Data
			GameObject go = Managers.Resource.Instantiate("Slime_01.prefab");	//원래는 데이터 시트에 있는 ID값을 찾아서 생성되게 설정
			go.name = "Player";

			PlayerController pc = go.GetOrAddComponent<PlayerController>();
			Player = pc;

			return pc as T;
		}
		else if (type == typeof(MonsterController))
		{
			string name = (templateID == 0 ? "Goblin_01" : "Snake_01");
			GameObject go = Managers.Resource.Instantiate(name + ".prefab", pooling: true);
			//go.transform.parent = MonsterRoot;

			MonsterController mc = go.GetOrAddComponent<MonsterController>();
			Monsters.Add(mc);

			return mc as T;
		}
		else if (type == typeof(GemController))
		{
			GameObject go = Managers.Resource.Instantiate("ExpGem.prefab", pooling: true);
			GemController gc = go.GetOrAddComponent<GemController>();
			gc.SetInfo(templateID);
			Gems.Add(gc);

			return gc as T;
		}
		else if (type == typeof(ProjectileController))
		{
			GameObject go = Managers.Resource.Instantiate("Icebolt.prefab", pooling: true);
			ProjectileController pc = go.GetOrAddComponent<ProjectileController>();
			//pc.SetInfo();
			Projectiles.Add(pc);

			return pc as T;
		}

		return null;
	}

	public void Despawn<T>(T obj) where T : BaseController						//코드가 실행되면 삭제하고자 하는 몬스터들이 사라짐
	{
		System.Type type = typeof(T);

		if (type == typeof(PlayerController))
		{
			// ?
		}
		else if (type == typeof(MonsterController))
		{
			Monsters.Remove(obj as MonsterController);
			Managers.Resource.Destroy(obj.gameObject);
		}
		else if (type == typeof(GemController)) 
		{
			Gems.Remove(obj as GemController);
			Managers.Resource.Destroy(obj.gameObject);
		}
		else if (type == typeof(ProjectileController))
		{
			Projectiles.Remove(obj as ProjectileController);
			Managers.Resource.Destroy(obj.gameObject);
		}
	}
}

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

	public T Spawn<T>(int templateID = 0) where T : BaseController              //T�� BaseController�� ��� ���� �͸� ������ش�.
    {
		System.Type type = typeof(T);

		if (type == typeof(PlayerController))
		{
			// TODO : Data
			GameObject go = Managers.Resource.Instantiate("Slime_01.prefab");	//������ ������ ��Ʈ�� �ִ� ID���� ã�Ƽ� �����ǰ� ����
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

	public void Despawn<T>(T obj) where T : BaseController						//�ڵ尡 ����Ǹ� �����ϰ��� �ϴ� ���͵��� �����
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

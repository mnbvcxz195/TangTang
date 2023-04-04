using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameScene : MonoBehaviour
{
    void Start()
    {
		Managers.Resource.LoadAllAsync<GameObject>("Prefabs", (key, count, totalCount) =>
		{
			//Debug.Log($"{key}, {count}/{totalCount}");
			if (count == totalCount)
			{
				//StartLoaded();
				//StartLoaded2();

				Managers.Resource.LoadAllAsync<Sprite>("Sprites", (key2, count2, totalCount2) =>
				{
					//Debug.Log($"{key}, {count}/{totalCount}");
					if (count2 == totalCount2)
					{
						Managers.Resource.LoadAllAsync<TextAsset>("Data", (key3, count3, totalCount3) =>
						{
							//Debug.Log($"{key}, {count}/{totalCount}");
							if (count3 == totalCount3)
							{
								//StartLoaded();
								StartLoaded2();
							}
						});
						//StartLoaded();
						//StartLoaded2();
					}
				});
			}
		});

		
	}

	void StartLoaded()
	{
		var player = Managers.Resource.Instantiate("Slime_01.prefab");
		player.GetOrAddComponent<PlayerController>(); 		
		var snake = Managers.Resource.Instantiate("Snake_01.prefab");
		var goblin = Managers.Resource.Instantiate("Goblin_01.prefab");
		var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
		joystick.name = "@UI_Joystick";

		var map = Managers.Resource.Instantiate("Map.prefab");
		map.name = "@Map";
		Camera.main.GetComponent<CameraController>().Target = player;
	}

	SpawningPool _spawningPool;

	void StartLoaded2()
	{
		_spawningPool = gameObject.AddComponent<SpawningPool>();

		var player = Managers.Object.Spawn<PlayerController>();

		for (int i = 0; i < 10; i++)
		{
			MonsterController mc = Managers.Object.Spawn<MonsterController>(Random.Range(0, 2));
			mc.transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
		}

		Managers.Resource.Instantiate("UI_Joystick.prefab").name = "@UI_Joystick";
		Managers.Resource.Instantiate("Map.prefab").name = "@Map";

		Camera.main.GetComponent<CameraController>().Target = player.gameObject;

		// Data Test
		Managers.Data.Init();

		foreach (var playerData in Managers.Data.PlayerDic.Values)
		{
			Debug.Log($"Lvl : {playerData.level}, Hp{playerData.maxHp}");
		}

	}

	// Update is called once per frame
	void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    void Start()
    {
		Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
		{
			Debug.Log($"{key} {count}/{totalCount}");

			if (count == totalCount)
			{
				StartLoaded();
			}
		});
	}

	SpawningPool _spawningPool;

	[SerializeField]
	GridController _grid;

	void StartLoaded()
	{
		// Data Test
		Managers.Data.Init();

		_spawningPool = gameObject.AddComponent<SpawningPool>();

		var player = Managers.Object.Spawn<PlayerController>(Vector3.zero);

		for (int i = 0; i < 10; i++)
		{
			Vector3 randPos = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)); 
			MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos, Random.Range(0, 2));			
		}
		
		var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
		joystick.name = "@UI_Joystick";

		var map = Managers.Resource.Instantiate("Map.prefab");
		map.name = "@Map";

		Camera.main.GetComponent<CameraController>().Target = player.gameObject;
	}

	void Update()
    {
        
    }
}

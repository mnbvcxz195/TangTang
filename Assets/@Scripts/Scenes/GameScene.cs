using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public GameObject _mapPrefab;    
    public GameObject _snakePrefab;
    public GameObject _slimePrefab;
    public GameObject _goblinPrefab;
    public GameObject _joystickPrefab;

    GameObject _map;
    GameObject _monsters;
	GameObject _snake;
	GameObject _player;
	GameObject _goblin;
	GameObject _joystick;

    // Start is called before the first frame update
    void Start()
    {
        _map = GameObject.Instantiate(_mapPrefab);
        _snake = GameObject.Instantiate(_snakePrefab);
        _player = GameObject.Instantiate(_slimePrefab);
        _goblin = GameObject.Instantiate(_goblinPrefab);
        _joystick = GameObject.Instantiate(_joystickPrefab);

        _map.name = "@Map";

        _monsters = new GameObject() { name = "@Monsters" };
		_snake.name = "Snake";
        _player.name = "Player";
        _goblin.name = "Goblin";

        _snake.transform.parent = _monsters.transform;
		_goblin.transform.parent = _monsters.transform;

		_player.AddComponent<PlayerController>();

        Camera.main.GetComponent<CameraController>().Target = _player;

		_joystick.name = "@UI_Joystick";

        //Managers.Scene.LoadScene(Define.Scene.DevScene);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}

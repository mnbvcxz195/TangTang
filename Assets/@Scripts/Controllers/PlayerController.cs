using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    Vector2 _moveDir = Vector2.zero;
	float _speed = 5.0f;

	public Vector2 MoveDir 
	{ 
		get { return _moveDir; }
		set { _moveDir = value.normalized; }
	}

	// Start is called before the first frame update
	void Start()
    {
		Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;
	}

	void HandleOnMoveDirChanged(Vector2 dir)
	{
		_moveDir = dir;
	}

	private void OnDestroy()
	{
		if (Managers.Game != null)
			Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
	}

	// Update is called once per frame
	void Update()
    {
		//UpdateInput();
		MovePlayer();
	}

	// Device Simulator에선 먹통!
    void UpdateInput()  //키보드를 입력해서 MoveDir의 방향을 바꿔주는 함수
    {
		Vector2 moveDir = Vector2.zero;

		if (Input.GetKey(KeyCode.W))
			moveDir.y += 1;
		if (Input.GetKey(KeyCode.S))
			moveDir.y -= 1;
		if (Input.GetKey(KeyCode.A))
			moveDir.x -= 1;
		if (Input.GetKey(KeyCode.D))
			moveDir.x += 1;

		_moveDir = moveDir.normalized; //단위벡터로 만들어줌
	}

	void MovePlayer()
	{
		//Debug.Log(_moveDir);
		//Debug.Log(Managers.Instance.MoveDir);

		Vector3 dir = _moveDir * _speed * Time.deltaTime;
		transform.position += dir;
	}
}

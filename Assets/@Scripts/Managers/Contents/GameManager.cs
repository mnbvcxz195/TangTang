using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
	public PlayerController Player {  get { return Managers.Object?.Player; } }

	public event Action<Vector2> OnMoveDirChanged;

	Vector2 _moveDir;
	public Vector2 MoveDir
	{
		get { return _moveDir; }
		set
		{
			_moveDir = value;
			OnMoveDirChanged?.Invoke(_moveDir);
		}
	}
}

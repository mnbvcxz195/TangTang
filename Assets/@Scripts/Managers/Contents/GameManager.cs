using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
	// delegate, Func, Action, event
	public event Action<Vector2> OnMoveDirChanged;

	static Vector2 _moveDir;
	public Vector2 MoveDir
	{
		get { return _moveDir; }
		set
		{
			_moveDir = value;
			// TEMP3
			OnMoveDirChanged?.Invoke(_moveDir);
		}
	}
}

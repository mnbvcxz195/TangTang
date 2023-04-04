using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BaseController : MonoBehaviour
{
	public ObjectType ObjectType { get; protected set; }	//오브젝트 타입을 가져올 수만 있게 set은 protect로 설정

	bool _init = false;

	void Awake()
	{
		Init();
	}

	public virtual bool Init()
	{
		if (_init)
			return false;

		_init = true;
		return true;
	}
}

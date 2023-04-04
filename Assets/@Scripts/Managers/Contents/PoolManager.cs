using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

class Pool
{
	GameObject _prefab; 
	IObjectPool<GameObject> _pool;      //UnityEngine에서 지원해주는 pool방식

    Transform _root;
	Transform Root
	{
		get 
		{ 
			if (_root == null)
			{
				GameObject go = new GameObject() { name = $"@{_prefab.name}Pool" };
				_root = go.transform;
			}

			return _root;
		}
	}

	public Pool(GameObject prefab)
	{
		_prefab = prefab; 
		_pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);      //새로 만들경우, 만들어진 것은 가져올 때, 사용한 것을 해제할 때, 삭제 할 때의 함수(사실 풀링할 때에는 OnDestroy의 의미가 없음 따라서 있어도 되고 없어도 됨)
    }

	public void Push(GameObject go)     //오브젝트를 잠시 꺼둘때
    {
		_pool.Release(go);
	}

	public GameObject Pop()		//오브젝트를 가져올 때
	{
		return _pool.Get();
	}

	#region Funcs
	GameObject OnCreate()           //원본 물체를 Instantiate로 프리펩의 이름으로 지정해둔 Root의 위치로 만들어줌.
    {
		GameObject go = GameObject.Instantiate(_prefab);
		go.transform.parent = Root;
		go.name = _prefab.name;
		return go;
	}

	void OnGet(GameObject go)
	{
		go.SetActive(true);
	}

	void OnRelease(GameObject go)
	{
		go.SetActive(false);
	}

	void OnDestroy(GameObject go)
	{
		GameObject.Destroy(go);
	}
	#endregion
}

public class PoolManager
{
	Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();           //풀을 많이 사용하기 때문에 Dictionary를 이용해서 키값과 벨류를 저장해준 뒤 풀을 사용할 때 키값을 받아 해당 풀의 오브젝트를 가져온다.

    public GameObject Pop(GameObject prefab)        //풀을 만들기 위해서 먼저 해당 풀이 있는지 확인 후, 없다면 프리펩의 키값을 받아와 Dictionary에서 풀을 가져온다.
    {
		if (_pools.ContainsKey(prefab.name) == false)
			CreatePool(prefab);

		return _pools[prefab.name].Pop();
	}

	public bool Push(GameObject go)				//풀을 반납할 때 풀이 있는지 확인 후, 해당 키값의 풀을 반납한다.
	{
		if (_pools.ContainsKey(go.name) == false)
			return false;

		_pools[go.name].Push(go);
		return true;
	}

	public void Clear()
	{
		_pools.Clear();
	}

	void CreatePool(GameObject original)
	{
		Pool pool = new Pool(original);
		_pools.Add(original.name, pool);
	}
}

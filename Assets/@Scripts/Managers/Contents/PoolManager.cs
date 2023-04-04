using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

class Pool
{
	GameObject _prefab; 
	IObjectPool<GameObject> _pool;      //UnityEngine���� �������ִ� pool���

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
		_pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);      //���� ������, ������� ���� ������ ��, ����� ���� ������ ��, ���� �� ���� �Լ�(��� Ǯ���� ������ OnDestroy�� �ǹ̰� ���� ���� �־ �ǰ� ��� ��)
    }

	public void Push(GameObject go)     //������Ʈ�� ��� ���Ѷ�
    {
		_pool.Release(go);
	}

	public GameObject Pop()		//������Ʈ�� ������ ��
	{
		return _pool.Get();
	}

	#region Funcs
	GameObject OnCreate()           //���� ��ü�� Instantiate�� �������� �̸����� �����ص� Root�� ��ġ�� �������.
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
	Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();           //Ǯ�� ���� ����ϱ� ������ Dictionary�� �̿��ؼ� Ű���� ������ �������� �� Ǯ�� ����� �� Ű���� �޾� �ش� Ǯ�� ������Ʈ�� �����´�.

    public GameObject Pop(GameObject prefab)        //Ǯ�� ����� ���ؼ� ���� �ش� Ǯ�� �ִ��� Ȯ�� ��, ���ٸ� �������� Ű���� �޾ƿ� Dictionary���� Ǯ�� �����´�.
    {
		if (_pools.ContainsKey(prefab.name) == false)
			CreatePool(prefab);

		return _pools[prefab.name].Pop();
	}

	public bool Push(GameObject go)				//Ǯ�� �ݳ��� �� Ǯ�� �ִ��� Ȯ�� ��, �ش� Ű���� Ǯ�� �ݳ��Ѵ�.
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

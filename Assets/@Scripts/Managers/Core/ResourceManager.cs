using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using Object = UnityEngine.Object;
using UnityEngine.Pool;

public class ResourceManager
{
	Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();

	public T Load<T>(string key) where T : Object                               //�ּ��� �� ���� AddressableAssets�� ������ �� �� ���Ŀ��� �Ϲ����� �ε� ������� ������ �ε�
    {
		if (_resources.TryGetValue(key, out Object resource))
			return resource as T;

		return null;
	}

	public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
	{
		GameObject prefab = Load<GameObject>($"{key}");
		if (prefab == null)
		{
			Debug.Log($"Failed to load prefab : {key}");
			return null;
		}

		//�̹� ������ ������Ʈ�� ������ �ʰ� ��� ���� ����(pooling���� �̸� ���������� ������Ʈ�� �ʿ����� �ʴ� ���� ��� ���δ� ������ ���� ���)
		if (pooling)
			return Managers.Pool.Pop(prefab);

		GameObject go = Object.Instantiate(prefab, parent);
		go.name = prefab.name;
		return go;
	}

	public void Destroy(GameObject go)
	{
		if (go == null)
			return;

		if (Managers.Pool.Push(go))
			return;

		Object.Destroy(go);
	}

	#region ��巹����
	public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object            //�ݵ�� T�� UnityEngine.Object�� ������ ��
    {
		// ĳ�� Ȯ��.
		if (_resources.TryGetValue(key, out Object resource))												//Ű ���� �̿��� �������� ������
		{
			callback?.Invoke(resource as T);
			return;
		}		

		// ���ҽ� �񵿱� �ε� ����.
		var asyncOperation = Addressables.LoadAssetAsync<T>(key);                                           //LoadAssetAsync�� �ݵ�� �ε尡 ���� ������ ���� �ݹ��� �޾ƾ� ���� 
        asyncOperation.Completed += (op) =>
		{
			_resources.Add(key, op.Result);
			callback?.Invoke(op.Result);
		};
	}

	public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object	//���ϴ� label�� �Է����ָ� �ش� label�� ���� �ִ� ��� �͵��� �������ش�.
	{
		var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
		opHandle.Completed += (op) =>
		{
			int loadCount = 0; 
			int totalCount = op.Result.Count;		

			foreach (var result in op.Result)
			{
				LoadAsync<T>(result.PrimaryKey, (obj) =>
				{
					loadCount++;
					callback?.Invoke(result.PrimaryKey, loadCount, totalCount);									//�ε��� ���� �̸�, �� ��°, �� ������ �ݹ�
				});
			}
		};
	}
	#endregion
}

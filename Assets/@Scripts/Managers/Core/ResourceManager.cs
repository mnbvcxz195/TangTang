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

	public T Load<T>(string key) where T : Object                               //최소의 한 번만 AddressableAssets로 가져와 준 뒤 추후에는 일반적인 로드 방식으로 데이터 로드
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

		//이미 삭제한 오브젝트를 날리지 않고 계속 갖고 있음(pooling으로 미리 생성시켜진 오브젝트를 필요하지 않는 것은 잠시 꺼두는 식으로 성능 향상)
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

	#region 어드레서블
	public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object            //반드시 T는 UnityEngine.Object를 보유한 것
    {
		// 캐시 확인.
		if (_resources.TryGetValue(key, out Object resource))												//키 값을 이용해 벨류값을 가져옴
		{
			callback?.Invoke(resource as T);
			return;
		}		

		// 리소스 비동기 로딩 시작.
		var asyncOperation = Addressables.LoadAssetAsync<T>(key);                                           //LoadAssetAsync가 반드시 로드가 끝난 다음에 따로 콜백을 받아야 실행 
        asyncOperation.Completed += (op) =>
		{
			_resources.Add(key, op.Result);
			callback?.Invoke(op.Result);
		};
	}

	public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object	//원하는 label을 입력해주면 해당 label을 갖고 있는 모든 것들을 생성해준다.
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
					callback?.Invoke(result.PrimaryKey, loadCount, totalCount);									//로드한 파일 이름, 몇 번째, 총 개수를 콜백
				});
			}
		};
	}
	#endregion
}

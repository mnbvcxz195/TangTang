using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraController : MonoBehaviour
{
    public GameObject Target { get; set; }

	void Start()
    {
	}

    void Update()
    {
        
    }

	void LateUpdate()
	{
		if (Target == null)
			return;

		transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, -10);
	}
}

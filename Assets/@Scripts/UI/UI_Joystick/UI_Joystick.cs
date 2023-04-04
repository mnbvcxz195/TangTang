using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Joystick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public Image _background;
	public Image _handler;

	Vector2 _touchPosition;
	Vector2 _moveDir;	
	float _joystickRadius;

	void Start()
    {
		_joystickRadius = _background.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
	}

    void Update()
    {
        
    }

	public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		Debug.Log("OnPointerClick");		
	}

	public void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
	{
		Debug.Log("OnPointerDown");
		_background.transform.position = eventData.position;
		_handler.transform.position = eventData.position;
		_touchPosition = eventData.position;
	}

	public void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
	{
		Debug.Log("OnPointerUp");		
		_handler.transform.position = _touchPosition;

		_moveDir = Vector2.zero;
		Managers.Game.MoveDir = _moveDir;
	}

	public void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
	{
		Debug.Log("OnDrag");

		Vector2 touchDir = (eventData.position - _touchPosition);
		float moveDist = Mathf.Min(touchDir.magnitude, _joystickRadius);
		Vector2 newPosition = _touchPosition + _moveDir * moveDist;
		_handler.transform.position = newPosition;

		_moveDir = touchDir.normalized;
		Managers.Game.MoveDir = _moveDir;
	}
}

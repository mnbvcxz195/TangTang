using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Joystick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler //Ž���ϰ� ���� �̺�Ʈ (��ġ ����, ��ġ ��, ��ġ �� ������ ��)
{
	public Image _background;
	public Image _handler;

	Vector2 _touchPosition;
	Vector2 _moveDir; 
	
	float _joystickRadius;  // ���̽�ƽ�� ������ �� �ִ� �ִ� �Ÿ�
	PlayerController _player;

	public event Action<Vector2> OnDirectionChanged;

	void Start()
    {
		_joystickRadius = _background.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    void Update()
    {
        
    }

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("OnPointerClick");		
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("OnPointerDown");
		_background.transform.position = eventData.position;
		_handler.transform.position = eventData.position;
		_touchPosition = eventData.position;
		//���� ���� �������� ����(��ġ �� �巡�׸� �� �� �ֱ� ������ ���� ��Ŵ)
	}

	public void OnPointerUp(PointerEventData eventData)   //��ġ ���� �� ���̽�ƽ ����ġ�� ���ư�
	{
		Debug.Log("OnPointerUp");		
		_handler.transform.position = _touchPosition;

		// TEMP 
		_moveDir = Vector2.zero;
		_player.MoveDir = _moveDir;
		// TEMP2
		//Managers.Game.MoveDir = _moveDir;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log("OnDrag");

		Vector2 touchDir = (eventData.position - _touchPosition);
		float moveDist = Mathf.Min(touchDir.magnitude, _joystickRadius); //�ִ� ���� �̻��̸� �ִ� ������, �� ���ϸ� ������ �Ÿ���ŭ
		Vector2 newPosition = _touchPosition + _moveDir * moveDist;      //��ġ �� �������� �̵� ���� ��ŭ ���̽�ƽ �̵�
		_handler.transform.position = newPosition;

		// TEMP
		_moveDir = touchDir.normalized; //��ġ ���� ����
		_player.MoveDir = _moveDir;
		// TEMP2
		//Managers.Game.MoveDir = _moveDir;
	}
}

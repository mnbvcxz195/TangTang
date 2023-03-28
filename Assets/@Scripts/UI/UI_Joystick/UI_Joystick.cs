using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Joystick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler //탐지하고 싶은 이벤트 (터치 순간, 터치 끝, 터치 후 움직일 때)
{
	public Image _background;
	public Image _handler;

	Vector2 _touchPosition;
	Vector2 _moveDir; 
	
	float _joystickRadius;  // 조이스틱이 움직일 수 있는 최대 거리
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
		//내가 누른 포지션을 저장(터치 후 드래그를 할 수 있기 때문에 저장 시킴)
	}

	public void OnPointerUp(PointerEventData eventData)   //터치 종료 후 조이스틱 원위치로 돌아감
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
		float moveDist = Mathf.Min(touchDir.magnitude, _joystickRadius); //최대 범위 이상이면 최대 범위로, 그 이하면 움직인 거리만큼
		Vector2 newPosition = _touchPosition + _moveDir * moveDist;      //터치 한 방향으로 이동 범위 만큼 조이스틱 이동
		_handler.transform.position = newPosition;

		// TEMP
		_moveDir = touchDir.normalized; //터치 방향 설정
		_player.MoveDir = _moveDir;
		// TEMP2
		//Managers.Game.MoveDir = _moveDir;
	}
}

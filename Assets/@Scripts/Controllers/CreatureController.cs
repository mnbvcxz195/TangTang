using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CreatureController : BaseController
{
	protected float _speed = 1.0f;

	public float Hp { get; set; } = 100;
	public float MaxHp { get; set; } = 100;

    public ObjectState State { get; set; } = ObjectState.Moving;

	void Awake()
    {
        Init();
	}

    void Update()
    {
		UpdateController();
	}

	protected virtual void UpdateController()
    {
		switch (State)
		{
			case ObjectState.Idle:
				UpdateIdle();
				break;
			case ObjectState.Moving:
				UpdateMoving();
				break;
			case ObjectState.Dead:
				UpdateDead();
				break;
		}
	}

	protected virtual void UpdateIdle() { }
	protected virtual void UpdateMoving() { }
	protected virtual void UpdateDead() { }

    public virtual void OnDamaged(BaseController attacker, int damage) 
	{
		Hp -= damage;
		if (Hp <= 0)
		{
			Hp = 0;
			OnDead();
		}
    }

	protected virtual void OnDead()
    {
        
    }
}

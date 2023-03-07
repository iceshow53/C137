using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// ** 움직이는 속도
	private float Speed;
	private Vector3 movement;

	private Animator animator;

	private bool onAttack;
	private bool onHit;
	private bool onJump;
	private bool onRoll;
	private bool died;
	private bool onStatus;

	// ** 유니티 기본 제공 함수
	// 첫 번째 프레임이 업데이트되기 전 시작되는 함수
	void Start()
	{
		// ** 속도를 초기화
		Speed = 5.0f;
		// this << 생략가능.
		// ** player 의 Animator를 받아온다.
		animator = this.GetComponent<Animator>();

		onAttack = false;
		onHit = false;
		onJump = false;
		onRoll = false;
		died = false;
		onStatus = false;
	}

	// 매 프레임마다 업데이트되는 함수
	void Update()
	{
		// 실수 연산은 IEEE 754 확인
		// ** Input.GetAxis = -1 ~ 1 사이의 값을 반환함.
		// Input.GetAxisRaw = -1 또는 0 또는 1 중 하나를 반환함.
		float Hor;
		float Ver;

		if (!died)
		{
			Hor = Input.GetAxisRaw("Horizontal");
			Ver = Input.GetAxisRaw("Vertical");
			if (!onStatus)
			{
				if (Input.GetKey(KeyCode.LeftControl))
					OnAttack();

				if (Input.GetKey(KeyCode.LeftShift))
					OnHit();

				if (Input.GetKey(KeyCode.Space))
					OnJump();

				if (Input.GetKey(KeyCode.C))
					OnRoll();
			}

			if (Input.GetKey(KeyCode.R))
			{
				animator.SetTrigger("Die");
				died = true;
			}
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
				animator.SetBool("Climb", true);
			else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
				animator.SetBool("Climb", false);

			animator.SetFloat("Speed", Hor);
			transform.position += new Vector3(Hor * Time.deltaTime * Speed, Ver * Time.deltaTime * Speed, 0.0f);

		}
	}

	private void OnAttack()
	{
		if (onAttack)
			return;

		onAttack = true;
		animator.SetTrigger("Attack");
	}

	private void SetAttack()
	{
		onAttack = false;
	}

	private void OnHit()
	{
		if (onHit)
			return;

		onHit = true;
		animator.SetTrigger("Hit");
	}

	private void SetHit()
	{
		onHit = false;
	}

	private void OnJump()
	{
		if (onJump)
			return;

		onJump = true;
		onStatus = true;
		animator.SetTrigger("Jump");
	}

	private void JumpDown()
	{
		animator.SetTrigger("DownJump");
	}

	private void SetJump()
	{
		onStatus = false;
		onJump = false;
	}

	private void OnRoll()
	{
		if (onRoll)
			return;

		onRoll = true;
		animator.SetTrigger("Roll");
	}

	private void SetRoll()
	{
		onRoll = false;
	}

	
}

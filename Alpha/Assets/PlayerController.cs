using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// ** �����̴� �ӵ�
	private float Speed;
	private Vector3 movement;

	private Animator animator;

	private bool onAttack;
	private bool onHit;
	private bool onJump;
	private bool onRoll;
	private bool died;
	private bool onStatus;

	// ** ����Ƽ �⺻ ���� �Լ�
	// ù ��° �������� ������Ʈ�Ǳ� �� ���۵Ǵ� �Լ�
	void Start()
	{
		// ** �ӵ��� �ʱ�ȭ
		Speed = 5.0f;
		// this << ��������.
		// ** player �� Animator�� �޾ƿ´�.
		animator = this.GetComponent<Animator>();

		onAttack = false;
		onHit = false;
		onJump = false;
		onRoll = false;
		died = false;
		onStatus = false;
	}

	// �� �����Ӹ��� ������Ʈ�Ǵ� �Լ�
	void Update()
	{
		// �Ǽ� ������ IEEE 754 Ȯ��
		// ** Input.GetAxis = -1 ~ 1 ������ ���� ��ȯ��.
		// Input.GetAxisRaw = -1 �Ǵ� 0 �Ǵ� 1 �� �ϳ��� ��ȯ��.
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

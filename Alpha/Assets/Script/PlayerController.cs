using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{

	// ** �����̴� �ӵ�
	private float Speed;

	// ** �������� �����ϴ� ����
	private Vector3 movement;

	private float Hor;
	private float Ver;

	// ** �÷��̾��� Animator ������Ҹ� �޾ƿ��� ����
	private Animator animator;
	// ** �÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ��� ����
	private SpriteRenderer spriteRenderer;

	// ** [����üũ]
	private int a; // ����������������
	private bool onAttack; // ����������
	private bool onHit; // �¾Ҵ���
	private bool onJump; // ����������
	private bool onRoll; // ������ ������
	private bool died; // �׾�����
	private bool onStatus; // �ൿ������

	// ** �÷��̾ ���������� �ٶ� ����
	private float Direction;

	// ** ������ �Ѿ� ����
	private GameObject BulletPrefab;
	// ** ������ FX ����
	private GameObject fxPrefab;

	// ** ���Ŀ� ����Ʈ�� ����
	//public GameObject[] stageBack = new GameObject[7];

	/*
	Dictionary<string,GameObject>	 
	 */
	[Header("����")]

	// ** ������ �Ѿ��� �������
	private List<GameObject> Bullets = new List<GameObject>();

	// ** �����ϸ� ������Ʈ�� Awake���� ����.
	private void Awake()
	{
		// this << ��������.
		// ** player �� Animator�� �޾ƿ´�.
		animator = this.GetComponent<Animator>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();

		// ** [Resources] �������� ����� ���ҽ��� ���´�.
		BulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
		//fxPrefab = Resources.Load("Prefabs/FX/Smoke") as GameObject;
		fxPrefab = Resources.Load("Prefabs/FX/Hit") as GameObject;
	}

	// ** ����Ƽ �⺻ ���� �Լ�
	// ù ��° �������� ������Ʈ�Ǳ� �� ���۵Ǵ� �Լ�
	void Start()
	{
		a = 0;
		onAttack = false;
		onHit = false;
		onJump = false;
		onRoll = false;
		died = false;
		onStatus = false;

		Direction = 1.0f;

		//for (int i = 0; i < 7; i++)
		//	stageBack[i] = GameObject.Find(i.ToString());
	}

	// �� �����Ӹ��� ������Ʈ�Ǵ� �Լ�
	void Update()
	{
		// �Ǽ� ������ IEEE 754 Ȯ��
		// ** Input.GetAxis = -1 ~ 1 ������ ���� ��ȯ��.
		// Input.GetAxisRaw = -1 �Ǵ� 0 �Ǵ� 1 �� �ϳ��� ��ȯ��.
		Hor = Input.GetAxisRaw("Horizontal");
		Ver = Input.GetAxisRaw("Vertical");

		Speed = ControllerManager.GetInstance().CharacterSpeed;

		movement = new Vector3(Hor * Time.deltaTime * Speed, Ver * Time.deltaTime * Speed * 0.5f, 0.0f);


		// ** Hor�� 0�̶�� �����ִ� �����̹Ƿ� ����ó���� ���ش�.
		if (Hor != 0)
			Direction = Hor;

		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			if (transform.position.x < 0)
			{
				transform.position += new Vector3(movement.x, 0.0f, 0.0f);
				spriteRenderer.flipX = false;
			}
			else
			{
				ControllerManager.GetInstance().DirRight = true;
				ControllerManager.GetInstance().DirLeft = false;
				spriteRenderer.flipX = false;
			}
		}
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{ 
			ControllerManager.GetInstance().DirRight = false;
			ControllerManager.GetInstance().DirLeft = true;
			spriteRenderer.flipX = true;
			if (transform.position.x > -10)
			{
				// ** �Է¹��� ������ �÷��̾ �����δ�.
				transform.position += new Vector3(movement.x, 0.0f, 0.0f);
			}
		}		
		if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
		{
			ControllerManager.GetInstance().DirRight = false;
			ControllerManager.GetInstance().DirLeft = false;
		}

		transform.position += new Vector3(0.0f, movement.y, 0.0f);


		// ** �׾����� ������ ����
		if (!died)
		{
			// ** �ൿ������ ������ ����
			if (!onStatus)
			{
				// ** ���� ��Ʈ�� �Է�
				if (Input.GetKey(KeyCode.LeftControl))
					OnAttack(); // ** ����
				// ** ���� ����Ʈ �Է�
				if (Input.GetKey(KeyCode.LeftShift))
					OnHit(); // ** �ǰ�
				// ** �����̽� �Է�
				if (Input.GetKey(KeyCode.Space))
					OnJump(); // ** ����
				// ** CŰ �Է�
				if (Input.GetKey(KeyCode.C))
					OnRoll(); // ** ������
			}

			// ** RŰ �Է�
			if (Input.GetKey(KeyCode.R))
			{
				// ** ���
				animator.SetTrigger("Die");
				died = true;
			}
			// ����Ű ��Ű�� �Ʒ�Ű �Է½�
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
			{
				++a;
				animator.SetInteger("climb", a);
			}
			else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
			{
				--a;
				animator.SetInteger("climb", a);
			}
			// ���������� �ִϸ��̼� ����

			// ** �÷��̾��� �����ӿ� ���� �̵� ����� �����Ѵ�.
			animator.SetFloat("Speed", Hor);

			//// ** �÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� �̹��� ���� ����
			//if(Direction < 0)
			//{
				//spriteRenderer.flipX = DirLeft = true;
				//// ** �Է¹��� ������ �÷��̾ �����δ�.
				//transform.position += new Vector3(Hor * Time.deltaTime * Speed, Ver * Time.deltaTime * Speed, 0.0f);
			//}
			//else if(Direction > 0)
			//{
				//DirRight = true;
				//spriteRenderer.flipX = false;
			//}
			
			// ** �Է¹��� ������ �÷��̾ �����δ�.
			//transform.position += new Vector3(Hor * Time.deltaTime * Speed, Ver * Time.deltaTime * Speed, 0.0f);
			//Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);

		}
	}

	private void OnAttack()
	{
		// ** �̹� ���ݸ���� �������̸�
		if (onAttack)
			return; // �Լ� ����

		// ** ���ݻ��� Ȱ��ȭ
		onAttack = true;
		// ** �Ѿ��� �����Ѵ�.
		GameObject Obj = Instantiate(BulletPrefab);
		// ** ������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�.
		Obj.transform.position = transform.position;
		
		// ** �Ѿ��� BulletController ��ũ��Ʈ�� �޾ƿ´�.
		BulletController controller = Obj.AddComponent<BulletController>();
		
		// ** �Ѿ� ��ũ��Ʈ ������ ���� ������ ���� �÷��̾��� ���� ������ �����Ѵ�.
		controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

		// ** �Ѿ� ��ũ��Ʈ ���ο�
		controller.fxPrefab = fxPrefab;

		// ** ��� ������ ����Ǿ��ٸ� ����ҿ� �����Ѵ�.
		Bullets.Add(Obj);

		// ** ���� ��� ����
		animator.SetTrigger("Attack");
	}

	private void SetAttack()
	{
		// ** �Լ��� ����Ǹ� ���ݸ���� ��Ȱ��ȭ �ȴ�.
		// ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
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
		// ** �Լ��� ����Ǹ� �ǰݸ���� ��Ȱ��ȭ �ȴ�.
		// ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
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
	}

	private void SetJump()
	{
		// ** �Լ��� ����Ǹ� ��������� ��Ȱ��ȭ �ȴ�.
		// ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
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
		// ** �Լ��� ����Ǹ� ������ ����� ��Ȱ��ȭ �ȴ�.
		// ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
		onRoll = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		print(collision.name);
    }
}

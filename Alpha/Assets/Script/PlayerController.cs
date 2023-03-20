using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{

	// ** 움직이는 속도
	private float Speed;

	// ** 움직임을 저장하는 벡터
	private Vector3 movement;

	private float Hor;
	private float Ver;

	// ** 플레이어의 Animator 구성요소를 받아오기 위해
	private Animator animator;
	// ** 플레이어의 SpriteRenderer 구성요소를 받아오기 위해
	private SpriteRenderer spriteRenderer;

	// ** [상태체크]
	private int a; // 오르내리는중인지
	private bool onAttack; // 공격중인지
	private bool onHit; // 맞았는지
	private bool onJump; // 점프중인지
	private bool onRoll; // 구르는 중인지
	private bool died; // 죽었는지
	private bool onStatus; // 행동중인지

	// ** 플레이어가 마지막으로 바라본 방향
	private float Direction;

	// ** 복사할 총알 원본
	private GameObject BulletPrefab;
	// ** 복사할 FX 원본
	private GameObject fxPrefab;

	// ** 추후에 리스트로 변경
	//public GameObject[] stageBack = new GameObject[7];

	/*
	Dictionary<string,GameObject>	 
	 */
	[Header("방향")]

	// ** 복제된 총알의 저장공간
	private List<GameObject> Bullets = new List<GameObject>();

	// ** 웬만하면 컴포넌트는 Awake에서 권장.
	private void Awake()
	{
		// this << 생략가능.
		// ** player 의 Animator를 받아온다.
		animator = this.GetComponent<Animator>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();

		// ** [Resources] 폴더에서 사용할 리소스를 들고온다.
		BulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
		//fxPrefab = Resources.Load("Prefabs/FX/Smoke") as GameObject;
		fxPrefab = Resources.Load("Prefabs/FX/Hit") as GameObject;
	}

	// ** 유니티 기본 제공 함수
	// 첫 번째 프레임이 업데이트되기 전 시작되는 함수
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

	// 매 프레임마다 업데이트되는 함수
	void Update()
	{
		// 실수 연산은 IEEE 754 확인
		// ** Input.GetAxis = -1 ~ 1 사이의 값을 반환함.
		// Input.GetAxisRaw = -1 또는 0 또는 1 중 하나를 반환함.
		Hor = Input.GetAxisRaw("Horizontal");
		Ver = Input.GetAxisRaw("Vertical");

		Speed = ControllerManager.GetInstance().CharacterSpeed;

		movement = new Vector3(Hor * Time.deltaTime * Speed, Ver * Time.deltaTime * Speed * 0.5f, 0.0f);


		// ** Hor이 0이라면 멈춰있는 상태이므로 예외처리를 해준다.
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
				// ** 입력받은 값으로 플레이어를 움직인다.
				transform.position += new Vector3(movement.x, 0.0f, 0.0f);
			}
		}		
		if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
		{
			ControllerManager.GetInstance().DirRight = false;
			ControllerManager.GetInstance().DirLeft = false;
		}

		transform.position += new Vector3(0.0f, movement.y, 0.0f);


		// ** 죽어있지 않으면 실행
		if (!died)
		{
			// ** 행동중이지 않으면 실행
			if (!onStatus)
			{
				// ** 좌측 컨트롤 입력
				if (Input.GetKey(KeyCode.LeftControl))
					OnAttack(); // ** 공격
				// ** 좌측 쉬프트 입력
				if (Input.GetKey(KeyCode.LeftShift))
					OnHit(); // ** 피격
				// ** 스페이스 입력
				if (Input.GetKey(KeyCode.Space))
					OnJump(); // ** 점프
				// ** C키 입력
				if (Input.GetKey(KeyCode.C))
					OnRoll(); // ** 구르기
			}

			// ** R키 입력
			if (Input.GetKey(KeyCode.R))
			{
				// ** 사망
				animator.SetTrigger("Die");
				died = true;
			}
			// 방향키 위키랑 아래키 입력시
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
			// 오르내리는 애니메이션 실행

			// ** 플레이어의 움직임에 따라 이동 모션을 실행한다.
			animator.SetFloat("Speed", Hor);

			//// ** 플레이어가 바라보고 있는 방향에 따라 이미지 반전 설정
			//if(Direction < 0)
			//{
				//spriteRenderer.flipX = DirLeft = true;
				//// ** 입력받은 값으로 플레이어를 움직인다.
				//transform.position += new Vector3(Hor * Time.deltaTime * Speed, Ver * Time.deltaTime * Speed, 0.0f);
			//}
			//else if(Direction > 0)
			//{
				//DirRight = true;
				//spriteRenderer.flipX = false;
			//}
			
			// ** 입력받은 값으로 플레이어를 움직인다.
			//transform.position += new Vector3(Hor * Time.deltaTime * Speed, Ver * Time.deltaTime * Speed, 0.0f);
			//Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);

		}
	}

	private void OnAttack()
	{
		// ** 이미 공격모션이 진행중이면
		if (onAttack)
			return; // 함수 종료

		// ** 공격상태 활성화
		onAttack = true;
		// ** 총알을 복제한다.
		GameObject Obj = Instantiate(BulletPrefab);
		// ** 복제된 총알의 위치를 현재 플레이어의 위치로 초기화한다.
		Obj.transform.position = transform.position;
		
		// ** 총알의 BulletController 스크립트를 받아온다.
		BulletController controller = Obj.AddComponent<BulletController>();
		
		// ** 총알 스크립트 내부의 방향 변수를 현재 플레이어의 방향 변수로 설정한다.
		controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

		// ** 총알 스크립트 내부에
		controller.fxPrefab = fxPrefab;

		// ** 모든 설정이 종료되었다면 저장소에 보관한다.
		Bullets.Add(Obj);

		// ** 공격 모션 실행
		animator.SetTrigger("Attack");
	}

	private void SetAttack()
	{
		// ** 함수가 실행되면 공격모션이 비활성화 된다.
		// ** 함수는 애니메이션 클립의 이벤트 프레임으로 삽입됨.
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
		// ** 함수가 실행되면 피격모션이 비활성화 된다.
		// ** 함수는 애니메이션 클립의 이벤트 프레임으로 삽입됨.
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
		// ** 함수가 실행되면 점프모션이 비활성화 된다.
		// ** 함수는 애니메이션 클립의 이벤트 프레임으로 삽입됨.
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
		// ** 함수가 실행되면 구르는 모션이 비활성화 된다.
		// ** 함수는 애니메이션 클립의 이벤트 프레임으로 삽입됨.
		onRoll = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		print(collision.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
	// ** 총알의 속도
	public float Speed;
	public GameObject Target;

	public bool Option;
	public bool Option2;
	public float gostrait;
	public float Angle;
	public float i;
	

	private SpriteRenderer spriteRenderer;
	// ** 총알이 충돌한 횟수
	private int HP = 3;

	// ** 이펙트효과 원본
	public GameObject fxPrefab;

	//public Vector3 Direction
	//{
	//    get
	//    {
	//        return Direction;
	//    }
	//    set
	//    {
	//        Direction = value;
	//    }
	//}

	// * 위 내용과 다를바 없음
	// ** 총알이 날아갈 방향
	public Vector3 Direction { get; set; }

	private void Start()
	{

		// ** 속도 초기값
		Speed = ControllerManager.GetInstance().BulletSpeed;
		// Speed = Option ? 0.35f : 1.0f;
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		gostrait = 1.0f;

		// ** 벡터의 정규화
		// Direction = (Target.transform.position - transform.position).normalized;
		Direction.Normalize();
	}

	// Update is called once per frame
	void Update()
	{
		// ** 실시간으로 타겟의 위치를 확인하고 방향을 갱신한다.
		if (Option && Target)
		{
			Direction = (Target.transform.position - transform.position).normalized;
			Angle = Mathf.Atan2(Target.transform.position.y - transform.position.y, Target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(Angle + 90f, Vector3.forward);
		}
		else
		{
			transform.position += Direction * Speed * Time.deltaTime;
		}
		if (Option2)
		{
			Speed = 1f;
			transform.position += Direction * Speed * Time.deltaTime;
			i += Time.deltaTime;
			if (i > gostrait)
			{
				Angle = Mathf.Atan2(Target.transform.position.y - transform.position.y, Target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
				Direction = (Target.transform.position - transform.position).normalized;
				transform.rotation = Quaternion.AngleAxis(Angle + 90f, Vector3.forward);
				Speed = 4.0f;
				Option2 = false;
			}
		}
		transform.position += Direction * Speed * Time.deltaTime;
	}

	// ** 충돌체와 물리엔진이 포함된 오브젝트가 다른 충돌체와 충돌한다면 실행되는 함수.
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ** "Wall"이라는 태그를 가진 대상과 충돌하거나 HP가 0이 되면 총알 삭제
		if (collision.tag == "Wall" || HP == 0)
			Destroy(this.gameObject, 0.016f);
		else if (collision.tag == "Enemy")
		{
			// ** 충돌횟수 차감.
			// --HP;

			// ** 폭발효과 복제
			GameObject Obj = Instantiate(fxPrefab);

			// ** 진동효과를 생성할 관리자
			GameObject camera = new GameObject("Camera Test");

			// ** 진동 효과 컨트롤러 생성
			camera.AddComponent<CameraController>();

			// ** 이펙트효과의 위치를 지정
			Obj.transform.position = transform.position;

			// ** collision = 충돌한 대상.
		}
	}
	/*
    private void OnTriggerStay2D(Collider2D collision)
    {
		print("BBBBB");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
		print("c");
    }
	*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	// ** 총알의 속도
	private float Speed;
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
		Speed = 10.0f;
		spriteRenderer = this.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		// ** 방향으로 속도만큼 위치를 변경
		transform.position += Direction * Speed * Time.deltaTime;
		if(Direction.x < 0)
			spriteRenderer.flipY = true;
	}

	// ** 충돌체와 물리엔진이 포함된 오브젝트가 다른 충돌체와 충돌한다면 실행되는 함수.
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ** "Wall"이라는 태그를 가진 대상과 충돌하거나 HP가 0이 되면 총알 삭제
		if (collision.tag == "Wall" || HP == 0)
			Destroy(this.gameObject);
		else
        {
			// ** 충돌횟수 차감.
			--HP;

			// ** 폭발효과 복제
			GameObject Obj = Instantiate(fxPrefab);

			// ** 진동효과를 생성할 관리자
			GameObject camera = new GameObject("Camera Test");

			// ** 진동 효과 컨트롤러 생성
			camera.AddComponent<CameraController>();

			// ** 이펙트효과의 위치를 지정
			Obj.transform.position = transform.position;

			// ** collision = 충돌한 대상.
			// ** 충돌한 물체 제거
			Destroy(collision.gameObject);
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

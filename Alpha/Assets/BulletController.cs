using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	// ** 총알의 속도
	private float Speed;
	private SpriteRenderer spriteRenderer;
	private int HP = 3;

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
		--HP;

		GameObject Obj = Instantiate(fxPrefab);

		Obj.transform.position = transform.position;

		GameObject camera = new GameObject("Camera Test");
		camera.AddComponent<CameraController>();

		if(collision.tag == "Wall")
			Destroy(this.gameObject);

		//Destroy(collision.gameObject);

		if (HP == 0)
			DestroyObject(this.gameObject);
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

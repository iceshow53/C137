using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	// ** �Ѿ��� �ӵ�
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

	// * �� ����� �ٸ��� ����
	// ** �Ѿ��� ���ư� ����
	public Vector3 Direction { get; set; }

	private void Start()
	{
		// ** �ӵ� �ʱⰪ
		Speed = 10.0f;
		spriteRenderer = this.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		// ** �������� �ӵ���ŭ ��ġ�� ����
		transform.position += Direction * Speed * Time.deltaTime;
		if(Direction.x < 0)
			spriteRenderer.flipY = true;
	}

	// ** �浹ü�� ���������� ���Ե� ������Ʈ�� �ٸ� �浹ü�� �浹�Ѵٸ� ����Ǵ� �Լ�.
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

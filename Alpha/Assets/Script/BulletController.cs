using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	// ** �Ѿ��� �ӵ�
	private float Speed;
	private SpriteRenderer spriteRenderer;
	// ** �Ѿ��� �浹�� Ƚ��
	private int HP = 3;

	// ** ����Ʈȿ�� ����
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
		// ** "Wall"�̶�� �±׸� ���� ���� �浹�ϰų� HP�� 0�� �Ǹ� �Ѿ� ����
		if (collision.tag == "Wall" || HP == 0)
			Destroy(this.gameObject);
		else
        {
			// ** �浹Ƚ�� ����.
			--HP;

			// ** ����ȿ�� ����
			GameObject Obj = Instantiate(fxPrefab);

			// ** ����ȿ���� ������ ������
			GameObject camera = new GameObject("Camera Test");

			// ** ���� ȿ�� ��Ʈ�ѷ� ����
			camera.AddComponent<CameraController>();

			// ** ����Ʈȿ���� ��ġ�� ����
			Obj.transform.position = transform.position;

			// ** collision = �浹�� ���.
			// ** �浹�� ��ü ����
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

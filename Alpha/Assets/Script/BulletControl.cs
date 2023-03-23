using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
	// ** �Ѿ��� �ӵ�
	public float Speed;
	public GameObject Target;

	public bool Option;
	public bool Option2;
	public float gostrait;
	public float Angle;
	public float i;
	

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
		Speed = ControllerManager.GetInstance().BulletSpeed;
		// Speed = Option ? 0.35f : 1.0f;
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		gostrait = 1.0f;

		// ** ������ ����ȭ
		// Direction = (Target.transform.position - transform.position).normalized;
		Direction.Normalize();
	}

	// Update is called once per frame
	void Update()
	{
		// ** �ǽð����� Ÿ���� ��ġ�� Ȯ���ϰ� ������ �����Ѵ�.
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

	// ** �浹ü�� ���������� ���Ե� ������Ʈ�� �ٸ� �浹ü�� �浹�Ѵٸ� ����Ǵ� �Լ�.
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ** "Wall"�̶�� �±׸� ���� ���� �浹�ϰų� HP�� 0�� �Ǹ� �Ѿ� ����
		if (collision.tag == "Wall" || HP == 0)
			Destroy(this.gameObject, 0.016f);
		else if (collision.tag == "Enemy")
		{
			// ** �浹Ƚ�� ����.
			// --HP;

			// ** ����ȿ�� ����
			GameObject Obj = Instantiate(fxPrefab);

			// ** ����ȿ���� ������ ������
			GameObject camera = new GameObject("Camera Test");

			// ** ���� ȿ�� ��Ʈ�ѷ� ����
			camera.AddComponent<CameraController>();

			// ** ����Ʈȿ���� ��ġ�� ����
			Obj.transform.position = transform.position;

			// ** collision = �浹�� ���.
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
	// ** BackGround�� ���ִ� ���������� �ֻ��� ��ü(�θ�)
	private Transform parent;

	// ** Sprite�� �����ϰ� �ִ� �������
	private SpriteRenderer spriteRenderer;

	// ** �̹���
	private Sprite sprite;

	// ** �̹��� �̵��ӵ�
	public float Speed;

	// ** ��������
	private float endPoint;

	// ** ��������.
	private float exitPoint;

	// ** �÷��̾� ����
	private GameObject player;
	private SpriteRenderer playerRenderer;

	// ** ������ ����
	private Vector3 movement;

	// ** �̹����� �߾� ��ġ�� ���������� ����� �� �ֵ��� �ϱ� ���� ���濪��.
	private Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);

	private void Awake()
	{
		// ** �̹����� ����ִ� ������Ҹ� �޾ƿ´�.
		spriteRenderer = GetComponent<SpriteRenderer>();

		// ** �÷��̾��� �⺻������ �޾ƿ´�.
		player = GameObject.Find("player").gameObject;
		// ** �θ�ü�� �޾ƿ´�.
		parent = GameObject.Find("BackGround").transform;
		// ** �÷��̾� �̹����� ����ִ� ������Ҹ� �޾ƿ´�.
		playerRenderer = player.GetComponent<SpriteRenderer>();
	}


	// Start is called before the first frame update
	void Start()
	{
		// ** ������ҿ� ���Ե� �̹����� �޾ƿ´�.
		sprite = spriteRenderer.sprite;
		// ** ���������� ����
		endPoint = sprite.bounds.size.x * 0.5f + transform.position.x;
		// ** ���������� ����
		exitPoint = player.transform.position.x - sprite.bounds.size.x * 0.5f;
	}

	// Update is called once per frame
	void Update()
	{
		// ** �÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� �б��.
		if(playerRenderer.flipX) // ** ���� �̵�
        {

        }
		else // ** ���� �̵�
        {
			// ** �̵����� ����
			movement = new Vector3(((Input.GetAxisRaw("Horizontal") + offset.x) * Time.deltaTime * Speed * 2.5f), 0.0f, 0.0f);
			// ** �̵����� ����
			transform.position -= movement;
			endPoint -= movement.x;
		}
		

		// ** ������ �̹��� ����
		if(player.transform.position.x + (sprite.bounds.size.x * 0.5f) > endPoint)
		{
			// ** �̹����� �����Ѵ�.
			GameObject Obj = Instantiate(this.gameObject);

			// ** ������ �̹����� �θ� �����Ѵ�.
			Obj.transform.parent = parent.transform;
			// ** ������ �̹����� �̸��� �����Ѵ�.
			Obj.transform.name = transform.name;
			// ** ������ �̹����� ��ġ�� �����Ѵ�.
			Obj.transform.position = new Vector3(
				transform.position.x + endPoint + 25.0f,
				0.0f, 0.0f);
			// ** ���� ������ �����Ѵ�.
			endPoint += endPoint + 25.0f;
		}

		// ** ���������� �����ϸ� �����Ѵ�.
		if(player.transform.position.x - (sprite.bounds.size.x) + 3 > transform.position.x)
			Destroy(this.gameObject);
	}
}

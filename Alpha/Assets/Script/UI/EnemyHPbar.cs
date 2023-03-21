using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPbar : MonoBehaviour
{
	// ** ����ٴ� ��ü
	public GameObject Target;
	public EnemyController controller;
	private Slider HPBar;

	// ** ������ġ ����
	private Vector3 offset;
	private void Awake()
	{
		HPBar = GetComponent<Slider>();
	}

	private void Start()
	{
		controller = Target.GetComponent<EnemyController>();
		// ** ��ġ ����
		offset = new Vector3(0.3f, 0.5f, 0.0f);
		HPBar.maxValue = controller.HP;
		HPBar.value = HPBar.maxValue;
	}

	private void Update()
	{
		if (controller.HP <= 0)
			Destroy(gameObject);
		// ** WorldToScreenPoint = ������ǥ�� ī�޶� ��ǥ�� ��ȯ.
		// ** ����� �ִ� Ÿ���� ��ǥ�� ī�޶� ��ǥ�� ��ȯ�Ͽ� UI�� �����Ѵ�.
		transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + offset);
		HPBar.value = controller.HP;
	}
}

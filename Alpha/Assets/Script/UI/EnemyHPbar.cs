using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPbar : MonoBehaviour
{
	// ** 따라다닐 객체
	public GameObject Target;
	public EnemyController controller;
	private Slider HPBar;

	// ** 세부위치 조정
	private Vector3 offset;
	private void Awake()
	{
		HPBar = GetComponent<Slider>();
	}

	private void Start()
	{
		controller = Target.GetComponent<EnemyController>();
		// ** 위치 세팅
		offset = new Vector3(0.3f, 0.5f, 0.0f);
		HPBar.maxValue = controller.HP;
		HPBar.value = HPBar.maxValue;
	}

	private void Update()
	{
		if (controller.HP <= 0)
			Destroy(gameObject);
		// ** WorldToScreenPoint = 월드좌표를 카메라 좌표로 변환.
		// ** 월드상에 있는 타겟의 좌표를 카메라 좌표로 변환하여 UI에 셋팅한다.
		transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + offset);
		HPBar.value = controller.HP;
	}
}

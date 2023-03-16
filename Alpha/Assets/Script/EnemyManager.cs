using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private EnemyManager() { }
	private static EnemyManager Instance = null;

	public static EnemyManager GetInstance
	{
		get
		{
			if (Instance == null)
				return null;
			return Instance;
		}
	}

	// ** Enemy�� �θ� ��ü
	private GameObject Parent;
	// ** Enemy ��ü
	private GameObject Prefab,Bullet;
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;

			// ** ���� ����Ǿ ��� ������ �� �ְ� ���ش�.
			DontDestroyOnLoad(gameObject);

			// ** �����Ǵ� Enemy�� ��Ƶ� ���� ��ü
			Parent = new GameObject("EnemyList");

			// ** Enemy�� ����� ���� ��ü
			Prefab = Resources.Load("Prefabs/Enemy") as GameObject;
			Bullet = Resources.Load("Prefabs/EnemyBullet") as GameObject;
		}
	}
	
	// ** �������ڸ��� Start �Լ��� �ڷ�ƾ �Լ��� ����
	private IEnumerator Start()
	{
		while (true)
		{
			// ** Enemy ������ü�� �����Ѵ�.
			GameObject Obj = Instantiate(Prefab);

			Obj.GetComponent<EnemyController>().bullet = Bullet;

			// ** Ŭ���� ��ġ�� �ʱ�ȭ
			Obj.transform.position = new Vector3(18.0f, Random.Range(-8.2f, -5.2f), 0.0f);
			

			// ** Ŭ���� �̸� �ʱ�ȭ
			Obj.transform.name = "TEST";

			// ** Ŭ���� �������� ����.
			Obj.transform.parent = Parent.transform;

			// ** 1.5�� �޽�
			yield return new WaitForSeconds(1.5f);
		}
	}
}

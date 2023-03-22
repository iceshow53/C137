using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
	private Text ui;
	public GameObject test, player;
	public float distance;
 
	// Start is called before the first frame update
	void Start()
	{
		ui = GetComponent<Text>();

		distance = 0.0f;
	}

		
	// Update is called once per frame
	void Update()
	{

		// ** 방향 구하는 방식
		Vector3 bob = (player.transform.position - test.transform.position).normalized;
		// ** 방향 구하는 방식2
		//bob.Normalize();

		transform.position += bob * Time.deltaTime * 2.0f;

		//distance = Mathf.Sqrt(Mathf.Pow(bob.x, 2) + Mathf.Pow(bob.y, 2));

		distance = Vector3.Distance(player.transform.position, test.transform.position);

		// ui.text = distance.ToString();

		if(distance < 5.0f)
        {
			test.GetComponent<MyGizmo>().color = Color.green;
        }
		else
        {
			test.GetComponent<MyGizmo>().color = Color.red;
        }

	}
}

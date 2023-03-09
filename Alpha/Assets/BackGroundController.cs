using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
	public float Speed;

	private GameObject player;
	private Vector3 movement;
	private Vector3 offset = new Vector3(0.0f, 2.0f, 0.0f);


	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.Find("player").gameObject;
	}

	// Update is called once per frame
	void Update()
	{
		movement = new Vector3(Input.GetAxisRaw("Horizontal") + offset.x, player.transform.position.y + offset.y, 0.0f);
		transform.position -= movement * Time.deltaTime * Speed;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float Speed;
	public int HP;
	private Animator Anim;
	private Vector3 movement;
	

	private void Awake()
	{
		Anim = GetComponent<Animator>();
	}

	// Start is called before the first frame update
	void Start()
	{
		Speed = 0.3f;
		movement = new Vector3(1.0f, 0.0f, 0.0f);
		HP = 3;
	}

	// Update is called once per frame
	void Update()
	{
		//if (ControllerManager.GetInstance().DirRight)
  //      {
		//	movement = new Vector3(Input.GetAxisRaw("Horizontal") + 3.0f, 0.0f, 0.0f);
  //      }
		//else
  //      {
		//	movement = new Vector3(1.0f, 0.0f, 0.0f);
  //      }

		movement = ControllerManager.GetInstance().DirRight ? new Vector3(Speed + 4.0f, 0.0f, 0.0f) : new Vector3(Speed + 1.0f, 0.0f, 0.0f);

		transform.position -= movement * Time.deltaTime * Speed;
		Anim.SetFloat("Speed", movement.x);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
			--HP;
			if(HP <= 0)
            {
				Anim.SetBool("Dead", true);
				GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
    }

	private void DestroyEnemy()
    {
		Destroy(gameObject, 0.016f);
    }
}

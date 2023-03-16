using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float Speed;
	private float distance;
	public int HP;
	private Animator Anim;
	private GameObject player;
	public GameObject bullet;
	private Vector3 movement;
	private bool is_spell,is_attack;
	private int is_move = 0;
	

	private void Awake()
	{
		Anim = GetComponent<Animator>();
		player = GameObject.Find("player");
	}

	// Start is called before the first frame update
	void Start()
	{
		Speed = 0.8f;
		movement = new Vector3(1.0f, 0.0f, 0.0f);
		HP = 3;
		is_move = 0;
		is_spell = is_attack = false;
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

		movement = ControllerManager.GetInstance().DirRight ? new Vector3(Speed + 3.0f, 0.0f, 0.0f) : new Vector3(Speed + 1.0f, 0.0f, 0.0f);

		if (is_move == 0)
		{
			transform.position -= movement * Time.deltaTime * Speed;
			Anim.SetFloat("Speed", movement.x);
		}
		else if(is_move > 0)
        {
			movement = ControllerManager.GetInstance().DirRight ? new Vector3(1.4f, 0.0f, 0.0f) : new Vector3(0.0f, 0.0f, 0.0f);
			transform.position -= movement * Time.deltaTime * Speed;
		}

		distance = Vector3.Distance(player.transform.position, transform.position);

		if(distance <= 5)
        {
			if (!is_spell)
			{
				StartCoroutine(Spell_Cool(5.0f));
			}
        }
		if(distance <= 0.8f)
        {
			if (!is_attack)
				StartCoroutine(Attack_Cool(2.0f));
        }
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

	IEnumerator Spell_Cool(float CoolDown)
    {
		Anim.SetTrigger("Cast");
		is_move += 1;
		is_spell = true;

		GameObject Bullets = Instantiate(bullet);
		Bullets.transform.position = player.transform.position + new Vector3(0.0f, 0.3f, 0.0f);
		Destroy(Bullets, 2.4f);

		while (CoolDown > 0.0f)
        {
			CoolDown -= Time.deltaTime;
			yield return null;
        }
		is_spell = false;
    }

	IEnumerator Attack_Cool(float CoolDown)
    {
		Anim.SetTrigger("Attack");
		is_attack = true;
		is_move += 1;

		while (CoolDown > 0.0f)
        {
			CoolDown -= Time.deltaTime;
			yield return null;
        }
		is_attack = false;
    }

	private void DestroyEnemy()
    {
		Destroy(gameObject, 0.016f);
    }

    public void keepmove()
    {
		is_move -= 1;
    }
}

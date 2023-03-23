using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collisions()
    {
        if (gameObject.GetComponent<CapsuleCollider2D>().enabled == true)
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        else if (gameObject.GetComponent<CapsuleCollider2D>().enabled == false)
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
    }
}

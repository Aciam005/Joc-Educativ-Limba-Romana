using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Control;

public class EnemyHead : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] float knockBackForce;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FeetSmashySmash")
        {
            checkDirection();
            GetComponentInParent<EnemyBrain>().enemyDie(); 
        }
    }

    private void checkDirection()
    {
         Vector3 header = player.transform.position - transform.position;
        float distance = header.magnitude;
        Vector3 direction = header / distance;


        if (!player.GetComponent<Player>().isGrounded)
        {
            if (direction.x < 0)
            {
                //LEFT
            }
            else
            {
                //RIGHT 
            }

            if (direction.y <= 0)
            {
                //UP
            }

            else
            {
                player.GetComponent<Player>().playerKnockBack(KnockBackDirection.up, knockBackForce);
                //DOWN
            }
        }
    }
}

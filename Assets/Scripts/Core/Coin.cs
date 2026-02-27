using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float value;

    ScoreKeeper keeper;
    // Start is called before the first frame update
    void Start()
    {
        keeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            keeper.keeperAddScore(value);
            Destroy(this.gameObject);
        }
    }
}

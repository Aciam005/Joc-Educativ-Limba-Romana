using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatformer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject head;

    GameObject feetSmashySmash;
    void Start()
    {
        feetSmashySmash = GameObject.FindGameObjectWithTag("FeetSmashySmash");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

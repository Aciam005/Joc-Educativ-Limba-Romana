using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Control;

public class EnemyStealther : MonoBehaviour
{
    public float viewRange;
    EnemyBrain brain;
    [SerializeField] LayerMask IgnoreMe;

    public float timeToSpotPlayer;
    float actTimeToSpotPlayer;

    // Start is called before the first frame update
    void Start()
    {
        brain = GetComponent<EnemyBrain>();
        actTimeToSpotPlayer = timeToSpotPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        Vision();
    }

    void Vision()
    {
        if (Physics2D.Raycast(transform.position, transform.right, viewRange, ~IgnoreMe))
        {
            actTimeToSpotPlayer -= Time.deltaTime;
            brain.StopMove();
            if(actTimeToSpotPlayer <= 0) { Debug.Log("GOT YOU!"); GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().die(); }
        }
        else { brain.StartMove(); actTimeToSpotPlayer += Time.deltaTime; actTimeToSpotPlayer = Mathf.Clamp(actTimeToSpotPlayer, 0, timeToSpotPlayer); }
    }
}

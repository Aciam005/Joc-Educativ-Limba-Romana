using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Control
{
    public enum EnemyTypes
    {
        stealther,
        platformer,
    }
    public class EnemyBrain : MonoBehaviour
    {
        [Header("STATS")]
        [SerializeField] EnemyTypes enemyType;
        public float EnemySpeed;

        [Header("AI")]
        public bool isStopped;

        [Header("PATHFINDING")]
        public Transform[] Waypoints;
        public float enemyStopDistance;
        int waypointCount;
        Vector3 targetPosition;

        EnemyPlatformer platformer;
        EnemyStealther stealther;
        void Start()
        {
            waypointCount = 0;
            targetPosition = Waypoints[waypointCount].position; //sets first destination
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, EnemySpeed * Time.deltaTime);

            isStopped = false;

            determineEnemyType();
        }

        private void determineEnemyType()
        {
            switch (enemyType)
            {
                case EnemyTypes.platformer:
                    platformer = GetComponent<EnemyPlatformer>();
                    break;

                case EnemyTypes.stealther:
                    stealther = GetComponent<EnemyStealther>();
                    break;
            }
        }

        void Update()
        {
            if (!isStopped) { manageMovement(); }
        }

        void manageMovement()
        {
            if (Vector3.Distance(transform.position, targetPosition) <= enemyStopDistance) { targetPosition = chooseNextWaypoint(); }
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, EnemySpeed * Time.deltaTime);
        }

        Vector3 chooseNextWaypoint()
        {
            if(waypointCount < Waypoints.Length - 1) waypointCount++;

            else { waypointCount = 0; }

            checkDirection();

            return Waypoints[waypointCount].position;
        }

        public void enemyDie()
        {
            Destroy(this.gameObject);
        }

        private void checkDirection()
        {
            Vector3 header = Waypoints[waypointCount].position - transform.position;
            float distance = header.magnitude;
            Vector3 direction = header / distance;

            if(direction.x < 0)
            {
                FlipLeft(); return;// left
            }
            else  if(direction.x > 0)
            {
                FlipRight(); return;//right
            }

            if (direction.y <= 0) 
            {
                //up
            }
            else 
            {
                 //down
            }
        }

        void FlipLeft()
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }

        void FlipRight()
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        public void StopMove() { isStopped = true; }


        public void StartMove() { isStopped = false; }
    }
}

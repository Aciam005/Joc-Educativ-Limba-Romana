using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMaster : MonoBehaviour
{
    [SerializeField] int health;

    [SerializeField] Heart[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        hearts = GetComponentsInChildren<Heart>(true);
    }

    private void Update()
    {
        
    }

    public void SetMaxHealth(int amount)
    {
        health = amount;

        foreach(Heart heart in hearts)
        {
            heart.gameObject.SetActive(true);
        }
    }

    public void IncreaseHealth()
    {
        health ++;
        hearts[health].gameObject.SetActive(true);
    }

    public void DecreaseHealth()
    {
        health--;
        hearts[health].gameObject.SetActive(false);
    }
}

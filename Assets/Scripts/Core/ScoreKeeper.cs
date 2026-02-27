using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public Text keeperText;
    public float playerScore;
    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        keeperText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        keeperText.text = playerScore.ToString();
    }

    public void keeperAddScore(float amount)
    {
        playerScore = playerScore + amount;
    }
}

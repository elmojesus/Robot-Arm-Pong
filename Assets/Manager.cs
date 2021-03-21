using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject ball;

    public GameObject Player1;
    public GameObject LGoal;

    public GameObject Player2;
    public GameObject RGoal;

    public GameObject Player1T;
    public GameObject Player2T;

    public int Player1Score;
    public int Player2Score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void P1Score()
    {
        Player1Score++;
        Player1T.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString();
        resetBall();
    }

    public void P2Score()
    {
        Player2Score++;
        Player2T.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();
        resetBall();
    }

    void resetBall()
    {
        ball.GetComponent<Ball>().Reset();
    }
}

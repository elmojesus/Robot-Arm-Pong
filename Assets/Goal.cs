using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isLeft;

    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            if(isLeft)
            {
                manager.GetComponent<Manager>().P2Score();
            }
            else
            {
                manager.GetComponent<Manager>().P1Score();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OtherPlayer : MonoBehaviour
{
    public Rigidbody2D ball;
    public Rigidbody2D rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float d = ball.position.y - rb.position.y;
        if(d > 0){
            Debug.Log("UP");
            rb.velocity = new Vector2(rb.velocity.x, speed * Mathf.Min(d, 1.0f)); 
            //rb.velocity = new Vector2(rb.velocity.x, speed);   
        }
        if(d < 0){
            Debug.Log("DOWN");
            rb.velocity = new Vector2(rb.velocity.x, -(speed * Mathf.Min(-d, 1.0f)));
            //rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
    }

    void FixedUpdate()
    {
        // var gamepad = Gamepad.current;
        // if (gamepad == null)
        //     return; // No gamepad connected.

        // Vector2 left = gamepad.leftStick.ReadValue();

        // rb.velocity = new Vector2(rb.velocity.x, speed * left[1]);

    }
}

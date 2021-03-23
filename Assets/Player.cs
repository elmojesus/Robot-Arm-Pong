using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System.IO.Ports;


public class Player : Agent
{
    public Rigidbody2D ball;
    public Rigidbody2D rb;
    public float speed;

    SerialPort Serial_port = new SerialPort("COM3", 38400);

    void OnApplicationQuit(){
        Serial_port.Close();
    }

    public override void Initialize()
    {
        Serial_port.Open();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        rb.velocity = new Vector2(rb.velocity.x, speed * vectorAction[1]);
    }

    public override void OnEpisodeBegin()
    {
       
    } 

    public void Reset()
    {

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(ball.position.x);
        sensor.AddObservation(ball.position.y);
        sensor.AddObservation(rb.position.x);
        sensor.AddObservation(rb.position.y);
        sensor.AddObservation(ball.velocity);
    }

    public override void Heuristic(float[] actionsOut)
    {
       var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        Vector2 right = gamepad.rightStick.ReadValue();

        actionsOut[0] = right[0];
        actionsOut[1] = right[1];
    }

    public void FixedUpdate()
    {
       
    }

    public void sendToArduino(float vertical, float horizontal){
        byte[] message = new byte[6];
        message[0] = 254;
        message[5] = 255;

        //Debug.Log((int)(vertical*253));
        int converted_vertical_byte = Mathf.Abs((int)(vertical * 253));
        byte vertical_byte = (byte)converted_vertical_byte;

        int converted_horizontal_byte = Mathf.Abs((int)(horizontal * 253));
        byte horizontal_byte = (byte)converted_horizontal_byte;
        //Debug.Log((int)(horizontal*253));
        
        message[1] = (byte)(vertical < 0 ? 0 : 1);
        message[2] = vertical_byte;
        message[3] = (byte)(horizontal < 0 ? 1 : 0);
        message[4] = horizontal_byte;

        //Serial_port.Open();
        Serial_port.Write(message, 0, 6);
        //Serial_port.Close();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.IO.Ports;


public class Player : Agent
{
    public GameObject Ball;
    public Rigidbody2D ball;
    public Rigidbody2D rb;
    public float speed;

    SerialPort Serial_port = new SerialPort("COM3", 38400);

    void OnApplicationQuit(){
        Serial_port.Close();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            AddReward(0.1f);
        }
    }

    public override void Initialize()
    {
        Serial_port.Open();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(ball.position.x);
        sensor.AddObservation(ball.position.y);
        sensor.AddObservation(rb.position.x);
        sensor.AddObservation(rb.position.y);
        sensor.AddObservation(ball.velocity);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var continuousActions = actionBuffers.ContinuousActions;
        //Debug.Log(continuousActions[0]);
        rb.velocity = new Vector2(rb.velocity.x, speed * continuousActions[0]);
        sendToArduino(continuousActions[0]);
    }

    public override void OnEpisodeBegin()
    {
       Ball.GetComponent<Ball>().kaboom();
    } 

    public void Reset()
    {

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        Vector2 right = gamepad.rightStick.ReadValue();

        continuousActionsOut[0] = right[1]; //vert
        //actionsOut.ContinuousActionsOut[1] = right[1];
    }

    public void end()
    {
        EndEpisode();
    }

    public void Setreward(float amount)
    {
        SetReward(amount);
    }

    public void Addreward(float amount)
    {
        AddReward(amount);
    }

    public void sendToArduino(float vertical){
        byte[] message = new byte[4];
        message[0] = 254;
        message[3] = 255;

        //Debug.Log((int)(vertical*253));
        int converted_vertical_byte = Mathf.Abs((int)(vertical * 253));
        byte vertical_byte = (byte)converted_vertical_byte;
        
        message[1] = (byte)(vertical < 0 ? 0 : 1);
        message[2] = vertical_byte;

        //Serial_port.Open();
        Serial_port.Write(message, 0, 4);
        //Serial_port.Close();
    }
}

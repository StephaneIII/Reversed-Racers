using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Mouvement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [field: SerializeField]
    public double CurrentDirection { get; set; }
    [field: SerializeField]
    public float Acceleration { get; set; }
    [field: SerializeField]
    public float Speed { get; set; }
    private Rigidbody2D rb { get; set; }

    [field: SerializeField]
    public int TopSpeed { get; set; }
    [field: SerializeField]
    public double TurnRateAdjustement { get; set; }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentDirection = Math.PI;
        Acceleration = 0.3f;
        Speed = 0;
        TopSpeed = 100;
        TurnRateAdjustement = 1E-05;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v2 = new Vector2((float)Math.Cos(CurrentDirection) * Speed * 0.007f, (float)Math.Sin(CurrentDirection) * Speed *0.007f) + rb.position;
        rb.MovePositionAndRotation(v2, (float) ((CurrentDirection * (180 / Math.PI))- 90 ));

        if (Input.GetKey(KeyCode.A))
        {
            //CurrentDirection += Math.PI/45;
            CurrentDirection += TurnRate(Speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //CurrentDirection -= Math.PI/45;
            CurrentDirection -= TurnRate(Speed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (Speed + Acceleration >= TopSpeed)
                Speed = TopSpeed;
            else Speed += Acceleration;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (Speed - Acceleration <= -10)
                Speed = -10;
            else Speed -= Acceleration;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            HandBreak();
        }
    }



    public void HandBreak()
    {
        if (Math.Abs(Speed) < 5)
            Speed = 0;
        else
        {
            if (Speed > 0)
                Speed -= Acceleration * 3;
            else
                Speed += Acceleration * 3;
        }
    }

    public void Friction()
    {
        throw new NotImplementedException();
    }

    public double TurnRate(double speed)
    {
        if(speed <= 30)
        {
            return 4* speed* (180/math.PI)* TurnRateAdjustement;
        }
        return (90- speed) * (180 / Math.PI)* TurnRateAdjustement;

    }


}

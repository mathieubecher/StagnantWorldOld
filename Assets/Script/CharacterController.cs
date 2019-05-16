using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : SimpleController
{
    public bool joystick;
    public float activeX = 0;
    public float activeY = 0;
    public float activeA = 0;
    public float activeB = 0;

    public Texture2D spritebase;
    
    public Equipment helmet;
    public Equipment pants;
    public Equipment mitt;
    public Equipment torso;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        DetectMoveInput();
        CurrentState.Move();
        CurrentState.UpdateAnim();
        CurrentState.Update();

        // Detect Hit
        if (Input.GetKey(InputConfig.Hit) || Input.GetKey(KeyCode.JoystickButton2))
        {
            left = false;
            activeX += Time.deltaTime;
            if (activeX > 0.2) CurrentState.ChargeHit();
        }
        else
        {
            if (activeX > 0)
            {
                CurrentState.Hit();
            }
            activeX = 0;
        }

        // Detect Dash
        if (Input.GetKey(InputConfig.Dash) || Input.GetKey(KeyCode.JoystickButton0))
        {
            activeA += Time.deltaTime;
            if (activeA > 0.2) CurrentState.ChargeDash();
        }
        else
        {
            if (Math.Abs(move.x) + Math.Abs(move.y) > 0 && activeA>0) {
                Debug.Log("Dash");
                CurrentState.Dash();
            }
            activeA = 0;
        }
        // Detect Hit Left
        if (Input.GetKey(InputConfig.HitLeft) || Input.GetKey(KeyCode.JoystickButton3))
        {
            left = true;
            activeY += Time.deltaTime;
            if (activeY > 0.2) CurrentState.ChargeHit();
        }
        else
        {
            if (activeY > 0)
            {
                CurrentState.Hit();
            }
            activeY = 0;
        }


        
    }

    // Détection du déplacement
    protected override void DetectMoveInput()
    {
        if (joystick)
        {
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                move.x = Input.GetAxis("Horizontal");
                move.y = Input.GetAxis("Vertical");
            }
            else joystick = false;
        }
        else
        {
            if (Input.GetKeyDown(InputConfig.Top) || (Input.GetKey(InputConfig.Top))) move.y = 1;
            else if (Input.GetKeyDown(InputConfig.Bottom) || (Input.GetKey(InputConfig.Bottom))) move.y = -1;
            else if ((!Input.GetKey(InputConfig.Bottom) && Move.y < 0) || (!Input.GetKey(InputConfig.Top) && move.y > 0)) move.y = 0;

            if (Input.GetKeyDown(InputConfig.Right) || (Input.GetKey(InputConfig.Right))) move.x = 1;
            else if (Input.GetKeyDown(InputConfig.Left) || (Input.GetKey(InputConfig.Left))) move.x = -1;
            else if ((!Input.GetKey(InputConfig.Left) && move.x < 0) || (!Input.GetKey(InputConfig.Right) && move.x > 0)) move.x = 0;
        }
        float actualspeed = (float)Math.Sqrt(Math.Pow(Move.x, 2) + Math.Pow(Move.y, 2));

        if (actualspeed > 1)
        {
            move.x = move.x / actualspeed;
            move.y = move.y / actualspeed;
        }
    }

    
}

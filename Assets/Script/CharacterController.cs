using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Direction{ LEFT, RIGHT, TOP, BOTTOM, TOPLEFT, TOPRIGHT, BOTTOMLEFT, BOTTOMRIGHT }

public class CharacterController : MonoBehaviour
{
    private State currentState;
    private Animator anim;

    private Vector2 move;
    public Vector2 Move { get => move; set => move = value; }

    public bool joystick;

    // Start is called before the first frame update
    void Start()
    {
        move = new Vector2(0, 0);
        currentState = new State(this);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()   
    {
        DetectMoveInput();
        currentState.Move();
        currentState.Hit();

    }

    private void DetectMoveInput()
    {
        if (joystick)
        {
            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) {
                move.x = Input.GetAxis("Horizontal");
                move.y = Input.GetAxis("Vertical");
            }
        }
        else
        {
            if (Input.GetKeyDown(InputConfig.Top) || (Input.GetKey(InputConfig.Top) && move.y == 0)) move.y = 1;
            else if (Input.GetKeyDown(InputConfig.Bottom) || (Input.GetKey(InputConfig.Bottom) && move.y == 0)) move.y = -1;
            else if ((!Input.GetKey(InputConfig.Bottom) && Move.y < 0) || (!Input.GetKey(InputConfig.Top) && move.y > 0)) move.y = 0;

            if (Input.GetKeyDown(InputConfig.Right) || (Input.GetKey(InputConfig.Right) && move.x == 0)) move.x = 1;
            else if (Input.GetKeyDown(InputConfig.Left) || (Input.GetKey(InputConfig.Left) && move.x == 0)) move.x = -1;
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

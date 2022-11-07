using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Joystick joystick_movement;

    public PlayerAnimation scr_playerAnimation;
    private Transform trans_bottomPart;
    private Rigidbody2D rb_player;
    public ParticleSystem particleSystem_dust;


    private float MOVE_SPEED = 10f;
    [System.NonSerialized]
    public float lastBottomPartAngle = 0f;
    [System.NonSerialized]
    public float lastTopPartAngle = 0f;
    public bool backWalking = false;
    public bool forwardWalking = false;

    Vector2 movement;

    public void Start()
    {
        trans_bottomPart = transform.GetChild(1).GetComponent<Transform>();
        scr_playerAnimation = GetComponent<PlayerAnimation>();
        rb_player = GetComponent<Rigidbody2D>();
        particleSystem_dust = GetComponent<Transform>().GetChild(3).GetChild(0).GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        MovementInput();
    }

    public void FixedUpdate()
    {
        Move();
    }

    public void MovementInput()
    {
        movement.x = joystick_movement.Horizontal;
        movement.y = joystick_movement.Vertical;

        //Burda ya hep ya hiç yapıyorum hareketi.
        if (movement.x >= .2f)
        {
            movement.x = 1f;
        }
        else if (movement.x <= -.2f)
        {
            movement.x = -1f;
        }
        else
        {
            movement.x = 0f;
        }


        if (movement.y >= .2f)
        {
            movement.y = 1f;
        }
        else if (movement.y <= -.2f)
        {
            movement.y = -1f;
        }
        else
        {
            movement.y = 0f;
        }
    }


    public void Move()
    {
        float angle = CommonCalculateFunctions.FromDirectionToAngle(movement);
        if (Math.Abs(movement.x) > 0 || Math.Abs(movement.y) > 0)
        {
            rb_player.MovePosition(rb_player.position + movement * MOVE_SPEED * Time.fixedDeltaTime);
            lastBottomPartAngle = angle;

            backWalking = IsAngleGreaterThanNinenty();
            forwardWalking = !backWalking;

            RotateBottomPart(angle);
            CreateDust(); // Particle burda olusturuluyor.
        }
        else
        {
            backWalking = false;
            forwardWalking = false;
        }

        scr_playerAnimation.Move(backWalking, forwardWalking);

    }

    public void RotateBottomPart(float angle)
    {
        if (backWalking)  // Eger geri geri yuruyorsa biraktiginda idle da geri kalicak o yuzden guncelledik.
            angle += angle < 180 ? 180 : -180;

        trans_bottomPart.eulerAngles = Vector3.forward * (angle);
    }

    public bool IsAngleGreaterThanNinenty() // Top ve bot part arasindaki aci 90 dan buyukmu
    {
        float topPart = CommonCalculateFunctions.PositiveAngleConverter(lastTopPartAngle);
        float bottomPart = CommonCalculateFunctions.PositiveAngleConverter(lastBottomPartAngle);
        float leftPath = CommonCalculateFunctions.LeftPathCalculator(bottomPart, topPart);

        if (leftPath <= 180)
        {
            return leftPath > 90;
        }
        else
        {
            float rightPath = 360 - leftPath;
            return rightPath > 90;
        }
    }

    private void CreateDust()
    {
        particleSystem_dust.Play();
    }
}

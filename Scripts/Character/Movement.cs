using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Joystick _movementJoystick;

    public PlayerAnimation _playerAnimation;
    private Transform _transformBottomPart;
    private Rigidbody2D _rb;
    public ParticleSystem _dust;


    private float moveSpeed = 10f;
    [System.NonSerialized]
    public float lastBottomPartAngle = 0f;
    [System.NonSerialized]
    public float lastTopPartAngle = 0f;
    public bool backWalking = false;
    public bool forwardWalking = false;

    Vector2 movement;

    public void Start()
    {
        _transformBottomPart = transform.GetChild(1).GetComponent<Transform>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _rb = GetComponent<Rigidbody2D>();
        _dust = GetComponent<Transform>().GetChild(3).GetChild(0).GetComponent<ParticleSystem>();
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
        movement.x = _movementJoystick.Horizontal;
        movement.y = _movementJoystick.Vertical;

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
            _rb.MovePosition(_rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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

        _playerAnimation.Move(backWalking, forwardWalking);

    }

    public void RotateBottomPart(float angle)
    {
        if (backWalking)  // Eger geri geri yuruyorsa biraktiginda idle da geri kalicak o yuzden guncelledik.
            angle += angle < 180 ? 180 : -180;

        _transformBottomPart.eulerAngles = Vector3.forward * (angle); 
    }

    public bool IsAngleGreaterThanNinenty() // Top ve bot part arasindaki aci 90 dan buyukmu
    {
        float topPart = CommonCalculateFunctions.PositiveAngleConverter(lastTopPartAngle);
        float bottomPart = CommonCalculateFunctions.PositiveAngleConverter(lastBottomPartAngle);
        float leftPath = CommonCalculateFunctions.LeftPathCalculator(bottomPart , topPart);
        
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
        _dust.Play();
    }
}

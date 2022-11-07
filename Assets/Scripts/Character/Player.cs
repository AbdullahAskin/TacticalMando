using System;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : Movement, IAlive // Burayı mono behaviour'dan çıkarıp alive abstract yapılıcak ve alt fonksyonda rigidbody çekilicek firefunction'danda açılar çekilicek. 
{
    private float hp;

    private Transform _transformTopPart;
    public static Joystick joystick_fire;
    public Gun _gun;
    private GameObject _canvas;
    private Slider _healthSlider;
    public GameObject _tryAgainScreen;

    new void Start()
    {
        base.Start();
        _transformTopPart = transform.GetChild(0).GetComponent<Transform>();
        _gun = gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Gun>();
        joystick_movement = GameObject.Find("MovementJoystick").GetComponent<Joystick>();
        joystick_fire = GameObject.Find("FireJoystick").GetComponent<Joystick>();
        _canvas = transform.GetChild(2).gameObject;
        _healthSlider = GameObject.Find("HealthBarBg").GetComponent<Slider>();
    }

    new void Update()
    {
        backWalking = IsAngleGreaterThanNinenty(); // Her atak ve hareketten sonra kontrol edilmek zorunda . 
        base.Update();
    }

    new void FixedUpdate()
    {
        FireTarget();
        base.FixedUpdate();
    }



    public void FireTarget()
    {
        Vector2 direction = joystick_fire.Direction;  // Vektörel cinsten direction döndürür.
        Attack(direction);
    }

    private void Attack(Vector2 direction)
    {
        if (Math.Abs(direction.magnitude) > 0)
        {
            float angle = CommonCalculateFunctions.FromDirectionToAngle(direction);
            lastTopPartAngle = angle;
            RotateTopPart(angle);
            ControlBottomRotate(angle);
            _gun.Fire(angle);
        }
    }

    private void ControlBottomRotate(float angle)
    {
        if (IsAngleGreaterThanNinenty())
        {
            lastBottomPartAngle += lastBottomPartAngle < 180 ? 180 : -180;
            RotateBottomPart(lastBottomPartAngle);
        }
        else
        {
            RotateBottomPart(lastBottomPartAngle);
        }

    }

    private void RotateTopPart(float angle)
    {
        _transformTopPart.eulerAngles = Vector3.forward * (angle); // resim düz olduğu için 90 derece ileriden gidiyor.
    }


    public void CalculateDamage(float damage)
    {
        FloatingTextController.CreateFloatingDamageTextForCharacter(_canvas, damage.ToString(), transform);
        _healthSlider.value -= damage;
        if (_healthSlider.value <= 0)
        {
            _tryAgainScreen.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    public void Heal(float heal)
    {
        // Daha sonra ayarlanicak.
    }

    public void Dead()
    {
        throw new NotImplementedException();
    }
}

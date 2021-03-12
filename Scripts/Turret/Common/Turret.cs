using System;
using System.Collections.Generic;
using UnityEngine;

public class Turret : TurretRotate, ITurretUpgrade
{

    public GunAnimation _animationScript;
    public Joystick _fireJoystick;
    public GameObject _turretRange;
    private SpriteRenderer _turretRangeSpriteRen;
    public List<Gun> _guns;
    public GameObject _gunGameObj;


    private Vector2 target;
    private bool isActive = false;
    public float distance;
    public bool turretControl = false;

    //Daha sonra classi degistirilicek.
    public float turretLevel = 0;

    public void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .4f);
        _transform = gameObject.transform.GetChild(0).GetComponent<Transform>();
        _turretRange = gameObject.transform.GetChild(2).gameObject;
        _turretRangeSpriteRen = _turretRange.GetComponent<SpriteRenderer>();
        _guns = new List<Gun>();
        _guns.Add(gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Gun>());
        _gunGameObj = _guns[0].gameObject;
    }

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public void ControlAttack()
    {
        Vector2 direction = _fireJoystick.Direction;  // Vektörel cinsten direction döndürür.
        Attack(direction);
    }


    public void FireTarget() // Menzil ekle .
    {
        if (target.magnitude != 0)
        {
            Vector2 direction = CommonCalculateFunctions.FromVectorsToDirection(target, transform.position);
            Attack(direction);
        }
    }
    public void Attack(Vector2 direction)
    {
        if (Math.Abs(direction.magnitude) > 0)    //direction boyutunu ölçmek şuan için anlamsız.
        {
            float angle = CommonCalculateFunctions.FromDirectionToAngle(direction);
            RotateTurret(angle);
            if (turretRotateIsComplete)
            {
                Fire(angle);
            }
        }
    }

    public void Fire(float angle)
    {
        foreach (var gun in _guns)
        {
            gun.Fire(angle);
        }
    }

    public void UpdateTarget()
    {
        Transform targetTemp = Find.FindNearestEnemy(transform, distance);
        if (targetTemp)
        {
            target = targetTemp.position;
            EnemyInRange(true);

        }
        else
        {
            target = new Vector2(0, 0);
            EnemyInRange(false);
        }
    }

    public void EnemyInRange(bool enemyInRange)
    {
        if (_turretRangeSpriteRen.color.g > 0f && enemyInRange)  // greenin 0 dan buyuk olmasi demek suanda ekranin yesilde olmasi demektir.
        {

            _turretRangeSpriteRen.color = new Color(1f, 0f, 0f, 150 / 255f);
        }
        else if (!(_turretRangeSpriteRen.color.g > 0f) && !enemyInRange)  // kirmizi ise 0 dan buyuk olamamasi demek bu yuzden kirmizidan yesile donucek.
        {
            _turretRangeSpriteRen.color = new Color(1f, 1f, 1f, 130 / 255f);
        }

    }

    public void UpgradeTurret()
    {
        GameObject currentGun = Instantiate(_gunGameObj);
        currentGun.transform.SetParent(transform.GetChild(0).GetChild(2), false);
        _guns.Add(currentGun.GetComponent<Gun>());
        turretLevel++;
    }

    private void OnDestroy()
    {
        CommonData.RemoveTurret(gameObject);
    }
}

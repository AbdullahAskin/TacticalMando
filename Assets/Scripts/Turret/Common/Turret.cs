using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : TurretRotate, ITurretUpgrade
{

    [HideInInspector] protected GunAnimation scr_basicTurretAnimation;
    private TurretDestroy scr_turretDestroy;
    [HideInInspector] public Joystick joystick_fire;
    public GameObject GO_turretRange;
    public SpriteRenderer spriteRenderer_turretRange;
    public List<Gun> scr_guns;


    private Vector2 target;
    private bool isActive = false;
    public float distance;
    [SerializeField] float m_destroyDuration = 5f;
    [HideInInspector] public bool isTurretUnderControl = false;

    private float turretLevel = 0;

    public void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, .4f);
        scr_turretDestroy = new TurretDestroy(GetComponentInChildren<Canvas>().GetComponentInChildren<Slider>(), this);
        trans_gfx = gameObject.transform.GetChild(0).GetComponent<Transform>();
        m_destroyDuration += UnityEngine.Random.Range(0, m_destroyDuration / 2);
    }

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; StartCoroutine(scr_turretDestroy.SwitchGunTimer(m_destroyDuration)); }
    }

    public void ControlAttack()
    {
        Vector2 direction = joystick_fire.Direction;
        Attack(direction);
    }

    public void FireToTarget()
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
                Fire(angle);
        }
    }

    public void Fire(float angle)
    {
        foreach (var gun in scr_guns)
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

    public void EnemyInRange(bool isEnemyInRange)
    {
        if (!isEnemyInRange)
            return;

        if (spriteRenderer_turretRange.color.g > 0f)
            spriteRenderer_turretRange.DOColor(new Color(1f, 0f, 0f, 150 / 255f), .5f);
        else if (!(spriteRenderer_turretRange.color.g > 0f))
            spriteRenderer_turretRange.DOColor(new Color(1f, 1f, 1f, 130 / 255f), .5f);
    }

    public void UpgradeTurret()
    {
        GameObject currentGun = Instantiate(scr_guns[0].gameObject);
        currentGun.transform.SetParent(transform.GetChild(0).GetChild(2), false);
        scr_guns.Add(currentGun.GetComponent<Gun>());
        turretLevel++;
        scr_turretDestroy.elapsedTime = 0f;
    }

    private void OnDestroy()
    {
        CommonData.RemoveTurret(gameObject);
    }

    public void DestroyTheTurret()
    {
        if (TurretButtonsListeners.turretControlModeOn)
            GameObject.FindGameObjectWithTag("CommonScripts").GetComponent<TurretButtonsListeners>().GoBackToHumanButtonClicked();
        Destroy(gameObject);
    }
}

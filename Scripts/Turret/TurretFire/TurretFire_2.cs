using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire_2 : Turret
{

    new void Start()
    {
        base.Start();

        _animationScript = transform.GetComponent<MediumTurretAnimation>();

        distance = 15f;


        _turretRange.transform.localScale = new Vector3(distance, distance, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            if (!turretControl)
            {
                FireTarget();
            }
            else
            {
                ControlAttack();
            }
        }

    }


}

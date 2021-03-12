using UnityEngine;

public class TurretFire_1 : Turret
{
    new void Start()
    {
        base.Start();
        _animationScript = transform.GetComponent<BasicTurretAnimation>();

        distance = 10f;

        _turretRange.transform.localScale = new Vector3(distance, distance, 0);

    }

    void Update()
    {
        if (IsActive)
        {
            if (!turretControl)
            {
                FireTarget(); // Pozisyon kaydirma burda yok.
            }
            else
            {
                ControlAttack();
            }
        }
    }
}

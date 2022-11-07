using UnityEngine;

public class TurretFire_1 : Turret
{
    new void Start()
    {
        base.Start();
        scr_basicTurretAnimation = transform.GetComponent<BasicTurretAnimation>();
        distance = 10f;
        GO_turretRange.transform.localScale = new Vector3(distance, distance, 0);
    }

    void Update()
    {
        if (IsActive)
        {
            if (!isTurretUnderControl)
            {
                FireToTarget(); // Pozisyon kaydirma burda yok.
            }
            else
            {
                ControlAttack();
            }
        }
    }
}

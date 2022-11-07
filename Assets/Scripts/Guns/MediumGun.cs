using UnityEngine;

public class MediumGun : Gun
{
    new void Start()
    {
        base.Start();
        _defaultFire = Resources.Load<GameObject>("Projectiles/TurretProjectile/MediumTurretFire");
        fireRate = .25f;
    }

    public override void Fire(float angle)
    {
        if (Time.time > nextFire)
        {
            Shoot(angle);
            _fireAnim.Fire(); // Daha sonra default gun icinde eklenicek.
        }
    }


}

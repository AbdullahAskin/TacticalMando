using UnityEngine;

public class BasicGun : Gun
{
    new void Start()
    {
        base.Start();
        _defaultFire = Resources.Load<GameObject>("Projectiles/TurretProjectile/BasicTurretFire");
        fireRate = .5f;
        //_default fire'i onload ile al.
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

using UnityEngine;

public class HeavyGun : Gun  // Random sekilde ates etmeyi ekle.
{
    new void Start()
    {
        base.Start();
        _defaultFire = Resources.Load<GameObject>("Projectiles/TurretProjectile/HeavyTurretFire");
        fireRate = .2f;
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

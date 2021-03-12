using UnityEngine;

public class TurretProjectile : Projectile
{
    new void Start()
    {
        base.Start();
        damage = 30f;
        _particleCollisionWithTarget = Resources.Load<GameObject>("Particles/TurretProjectileCollisionWithTarget");
    }

}

using UnityEngine;

public class DefaultGun : Gun
{
    PlayerAnimation _playerAnimation;
    ParticleSystem _muzzleParticle;
    new void Start()
    {
        base.Start();
        fireRate = .5f;
        _defaultFire = Resources.Load<GameObject>("Projectiles/CharacterProjectile/DefaultFire");
        _playerAnimation = transform.parent.parent.parent.GetComponent<PlayerAnimation>();
        _muzzleParticle = transform.GetChild(2).GetChild(0).GetComponent<ParticleSystem>();

    }

    public override void Fire(float angle)
    {
        if (Time.time > nextFire)
        {
            Shoot(angle);
            _muzzleParticle.Play();
            _fireAnim.Fire(); // Silahin uzerindeki animasyon kayip.
            _playerAnimation.Fire();
        }
    }
}

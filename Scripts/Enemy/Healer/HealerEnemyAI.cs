using System;
using UnityEngine;

public class HealerEnemyAI : EnemyAI
{

    private float enemyFindingDistance = 50f;
    private float distance = 6f;
    private float smokeSpeed = 10f;

    private ParticleSystem _healingSmokeParticle;
    private ParticleSystem.VelocityOverLifetimeModule _velocity;


    private new void Start()
    {
        base.Start();
        _healingSmokeParticle = transform.GetChild(1).GetChild(1).GetComponent<ParticleSystem>();
        _velocity = _healingSmokeParticle.velocityOverLifetime;
        InvokeRepeating("FireTarget", 0f, 0.5f);

    }

    private new void FixedUpdate()
    {
        AttackParticleControl();
        if (!PathCheck()) // false donerse yolda bi sorun vardir bu yuzden hareket edemezsin.
        {
            return;
        }
        if (FireCheck(distance))
        {
            Attack();
            return;
        }
        Movement();
    }

    private void Attack()
    {
        bool rotateControl = _animationScript.Attack();
        _healingSmokeParticle.Play();
        if (rotateControl)
            RotateEnemy(CommonCalculateFunctions.FromVectorsToDirection(_target.position, transform.position));
    }

    private void AttackParticleControl()
    {
        if (_enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            float radian = _enemy.eulerAngles.z * Mathf.Deg2Rad;
            float horizontalMove = (float)Math.Cos(radian) * smokeSpeed;
            float verticalMove = (float)Math.Sin(radian) * smokeSpeed;
            _velocity.xMultiplier = horizontalMove;
            _velocity.yMultiplier = verticalMove;
            if (!_healingSmokeParticle.isPlaying)
                _healingSmokeParticle.Play();

        }
        else if (_healingSmokeParticle.isPlaying && (_enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walking") || _enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("DamageTaking")))
        {
            _healingSmokeParticle.Stop();
        }

    }


    private new void Movement()
    {
        if (_enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attacking") || _enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("DamageTaking"))
        {
            _animationScript.Move(false);
            return;
        }
        else
        {
            _healingSmokeParticle.Stop();
            base.Movement();
        }
    }

    private void FireTarget()
    {
        Transform currentTargetPosition = Find.FindAppropriateTargetForHealer(transform, enemyFindingDistance);
        _target = currentTargetPosition;
    }

}

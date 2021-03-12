using UnityEngine;

public class MeeleFighterEnemyAI : EnemyAI
{
    ParticleSystem _rocketParticle;
    private float distance = 2.5f;


    private new void Start()
    {
        base.Start();
        _target = GameObject.FindWithTag("Player").GetComponent<Transform>();  // Bu daha sonra her ai da ayri alinicak.
        _rocketParticle = transform.GetChild(1).GetChild(1).GetComponent<ParticleSystem>();
    }

    private new void FixedUpdate()
    {
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
        _animationScript.Attack();
        _animationScript.Move(false);
        bool rotateControl = _animationScript.Attack();
        if (rotateControl)
            RotateEnemy(CommonCalculateFunctions.FromVectorsToDirection(_target.position, transform.position));
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
            base.Movement();
            _rocketParticle.Play();
        }

    }



}

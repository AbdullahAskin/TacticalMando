using UnityEngine;

public class AvatarEnemyAI : EnemyAI
{
    float passingTimeSinceLastCollision = 0f;
    float damage = 10f;
    float collisionCooldown = .6f;


    private new void Start()
    {
        base.Start();
        _target = GameObject.FindWithTag("Player").GetComponent<Transform>();  // Bu daha sonra her ai da ayri alinicak.
    }

    private new void FixedUpdate()
    {
        if (!PathCheck() || !_target) // false donerse yolda bi sorun vardir bu yuzden hareket edemezsin.
        {
            return;
        }

        Movement();
    }



    private new void Movement()
    {
        if (_enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("DamageTaking"))
        {
            _animationScript.Move(false);
            return;
        }
        else
        {
            base.Movement();
        }
    }

    void OnCollisionStay2D(Collision2D _collision) // Bu ozellik simdilik sadece basic monsterda var.
    {
        passingTimeSinceLastCollision += Time.deltaTime;
        if (_collision.collider.tag.Equals("Player") && passingTimeSinceLastCollision > collisionCooldown)
        {
            IAlive _aliveScript = _collision.gameObject.GetComponent<IAlive>();
            _aliveScript.DamageTakingCalculations(damage);
            passingTimeSinceLastCollision = 0;
        }
    }



}

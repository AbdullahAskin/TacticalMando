using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float angle;
    private float speed = 40f;
    private float distance = 40f;
    public float force = 200f; // iktirme gucu.
    [NonSerialized]
    public float damage = 30f;
    private Vector2 startingPosition;

    [NonSerialized]
    public static GameObject _particleCollisionWithObstacle;// Bunlar defalarca yukleniyor daha sonra burayi duzenle.
    [NonSerialized]
    public static GameObject _particleCollisionWithTarget;
    private Rigidbody2D _rb;


    public float Angle
    {
        get { return angle; }
        set { angle = value; }
    }


    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;

        _particleCollisionWithObstacle = Resources.Load<GameObject>("Particles/ProjectileCollisionWithObstacle");
        _particleCollisionWithTarget = Resources.Load<GameObject>("Particles/DefaultProjectileCollisionWithTarget");
    }

    void FixedUpdate()
    {
        float distanceTraveled = Vector2.Distance(transform.position, startingPosition);

        if (distance > distanceTraveled)
        {
            Move();
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private void Move()
    {
        float radian = angle * Mathf.Deg2Rad;
        float horizontalMove = (float)Math.Cos(radian) * speed * Time.deltaTime;
        float verticalMove = (float)Math.Sin(radian) * speed * Time.deltaTime;
        _rb.position += new Vector2(horizontalMove, verticalMove);
    }

    public void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.tag.Equals("Enemy"))
        {
            IAlive _aliveScript = _collider.GetComponent<IAlive>();
            IDamageableParticle _particleScript = _collider.GetComponent<IDamageableParticle>();
            IEnemyAI _enemyAIScript = _collider.GetComponent<IEnemyAI>();

            _aliveScript.CalculateDamage(damage); // Bura hedef oldugunde son hasari bastirmiyor.
            _enemyAIScript.DamageTakingAnimation(angle, force);
            _particleScript.InitializeParticle(_particleCollisionWithTarget);
            Destroy(this.gameObject);
        }
        else if (_collider.tag.Equals("Ground"))
        {
            InitializeParticle(_particleCollisionWithObstacle);
            Destroy(this.gameObject);
        }
    }


    public void InitializeParticle(GameObject _particleOrigin) // Daha sonra statige cevirilebilir.
    {
        GameObject _particle = Instantiate(_particleOrigin);
        _particle.transform.position = gameObject.transform.position;

        ParticleSystem _partSys = _particle.GetComponent<ParticleSystem>();
        _partSys.Play();
        Destroy(_particle, _partSys.main.duration + _partSys.main.startLifetime.constantMax); // Arti bir sure vermezsek direk kayboluyor.
    }

}

using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IEnemyAI
{
    public Transform _target;

    public float speed = 20f;
    public float nextWayPointDistance = 0.2f;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;
    private Vector2 _movementDirection;

    private Seeker _seeker;
    private Rigidbody2D _rb;
    public Transform _enemy;
    public Animator _enemyAnimator;
    public EnemyAnimation _animationScript;



    public void Start()
    {
        _enemy = transform.GetChild(1).GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _seeker = GetComponent<Seeker>();
        _animationScript = GetComponent<EnemyAnimation>();
        _enemyAnimator = _animationScript._anim;
        InvokeRepeating("UpdatePath", 0f, .3f);
    }

    public void UpdatePath()
    {
        if (!_target || _target.position.magnitude == 0)  // magnitude = 0 dusman atamasi yapilmadi demek.
            return;

        if (_seeker.IsDone())// Yol hesaplaması bittiyse yenisini hesapla.
            _seeker.StartPath(_rb.position, _target.position, OnPathComplete); // Sonda girilen metoda bir path döndürür.
    }
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    public void FixedUpdate()
    {
        if (!PathCheck()) // false donerse yolda bi sorun vardir bu yuzden hareket edemezsin.
        {
            //Kosmayi burda false yap.
            return;
        }

        Movement();
    }

    public bool PathCheck() // Yol bittiyse veya herhangi bir sorun varsa false yolda sorun yok ise true doner.
    {
        if (path == null)
            return false;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return false;
        }
        else
        {
            reachedEndOfPath = false;
        }
        return true;
    }


    public bool FireCheck(float distance) // Ates edilebilicek menzildemi kontrolu yapar.
    {
        float currentDistance;
        if (_target)
        {
            currentDistance = Vector2.Distance(transform.position, _target.position);
        }
        else
        {
            return false;
        }

        if (currentDistance <= distance)
        {
            return true;
        }
        return false;
    }


    public void Movement()
    {
        if (!_target || reachedEndOfPath)
        {
            return;
        }
        _animationScript.Move(true);

        float distanceFromPath = Vector2.Distance(_rb.position, path.vectorPath[currentWayPoint]);

        if ((distanceFromPath <= nextWayPointDistance)) // burda eger yolu tamamladiysak devam etmeye calisma diyoruz.
        {
            currentWayPoint++;
            _movementDirection = DirectionCalculator();
            RotateEnemy(_movementDirection);
        }
        _movementDirection = DirectionCalculator();

        _rb.MovePosition(_rb.position + _movementDirection * speed * Time.fixedDeltaTime);
    }

    public Vector2 DirectionCalculator()
    {
        if (!reachedEndOfPath && !(currentWayPoint >= path.vectorPath.Count))
        {
            _movementDirection = ((Vector2)path.vectorPath[currentWayPoint] - _rb.position).normalized;
        }
        return _movementDirection;
    }

    public void RotateEnemy(Vector2 _movementDirection)
    {
        float angle = CommonCalculateFunctions.FromDirectionToAngle(_movementDirection);
        _enemy.eulerAngles = Vector3.forward * (angle);
    }

    public void DamageTakingAnimation(float angle, float force) // alinan hasarin gorsel sekli
    {
        Vector2 direction = CommonCalculateFunctions.FromAngleToDirection(angle);
        _rb.AddForce(direction * 100);
        _animationScript.DamageTaking();
    }

}

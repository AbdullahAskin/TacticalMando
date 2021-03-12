public abstract class Gun : Shooting
{
    public IFireAnimation _fireAnim;

    public void Start()
    {
        _gunPointer = gameObject.transform.GetChild(1);
        _fireAnim = GetComponent<IFireAnimation>();
    }

    abstract public void Fire(float angle);

}

using UnityEngine;

public class GunAnimation : MonoBehaviour, IFireAnimation
{

    public Animator _animator;

    void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void Fire()
    {
        _animator.SetTrigger("Fire");
    }

}

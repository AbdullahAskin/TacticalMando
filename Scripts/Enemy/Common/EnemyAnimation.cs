using UnityEngine;
public class EnemyAnimation : MonoBehaviour
{
    [HideInInspector]
    public Animator _anim;

    public void Awake()
    {
        _anim = transform.GetChild(1).GetChild(0).GetComponent<Animator>();
    }

    public void Move(bool walking)
    {
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
            _anim.SetBool("Walking", walking);
    }

    public void DamageTaking()
    {
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("DamageTaking"))
            _anim.SetTrigger("DamageTaking");
    }

    public bool Attack()
    {
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            _anim.SetTrigger("Attacking");
            return true;
        }
        return false;
    }

}

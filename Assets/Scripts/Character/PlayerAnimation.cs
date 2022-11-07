using UnityEngine;

public class PlayerAnimation : MonoBehaviour,IFireAnimation
{
    private Animator _bottomPartAnim, _topPartAnim,_gunAnim;

    // Start is called before the first frame update
    void Start()
    {
        _bottomPartAnim = transform.GetChild(1).GetComponent<Transform>().GetChild(0).GetComponent<Animator>();
        _topPartAnim = transform.GetChild(0).GetComponent<Transform>().GetChild(0).GetComponent<Animator>();
    }


    public void Move(bool backWalking, bool forwardWalking)
    {
        _bottomPartAnim.SetBool("BackWalking", backWalking);
        _bottomPartAnim.SetBool("ForwardWalking", forwardWalking);

    }

    public void SwitchGun(bool switchGun)
    {
        _topPartAnim.SetBool("SwitchGun", switchGun);
    }

    public void Fire()
    {
        _topPartAnim.SetTrigger("Fire");
    }
}

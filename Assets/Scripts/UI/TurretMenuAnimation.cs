using UnityEngine;

public class TurretMenuAnimation : MonoBehaviour
{
    private Animator _animator;
    private float lastClickTime = 0f;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ButtonClicked()
    {
        if ((Time.time - lastClickTime) > _animator.GetCurrentAnimatorStateInfo(0).length)
        {
            lastClickTime = Time.time;
            _animator.SetTrigger("ButtonClicked");
        }
    }
}

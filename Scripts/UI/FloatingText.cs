using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Animator _animator;
    private Text _damageText;


    void OnEnable()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _damageText = _animator.GetComponent<Text>();
        AnimatorClipInfo[] clipInfos = _animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfos[0].clip.length);
    }

    public void SetText(string text)
    {
        _damageText.text = text;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButtonAnimation : MonoBehaviour
{
    public Animator _controlButtonAnimator;

    void Start()
    {
        _controlButtonAnimator = transform.GetChild(1).GetComponent<Transform>().GetChild(0).GetComponent<Animator>();
    }


    public void ControlButtonAnim()
    {
        _controlButtonAnimator.SetTrigger("ControlButton");
    }
}

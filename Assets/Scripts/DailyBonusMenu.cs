using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusMenu : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("MenuIn");
    }

    public void StartCloseAnimation()
    {
        animator.Play("MenuOut");
    }

    public void AnimationEnd()
    { 
        gameObject.SetActive(false);
    }
}

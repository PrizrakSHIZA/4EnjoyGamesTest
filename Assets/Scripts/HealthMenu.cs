using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMenu : MonoBehaviour
{
    Animator animator;

    private void OnEnable()
    {
        if(!animator)
            animator = GetComponent<Animator>();
        animator.Play("HealthMenuIn");
    }

    public void StartCloseAnimation()
    {
        animator.Play("HealthMenuOut");
    }

    public void AnimationEnd()
    {
        gameObject.SetActive(false);
    }
}

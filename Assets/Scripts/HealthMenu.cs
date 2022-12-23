using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMenu : MonoBehaviour
{
    Animator animator;

    [Header("References")]
    [SerializeField] GameObject buttonUseLife;
    [SerializeField] GameObject buttonRefillLives;
    [SerializeField] GameObject timer;

    private void OnEnable()
    {
        if(!animator)
            animator = GetComponent<Animator>();
        animator.Play("HealthMenuIn");
        HealthSystem.OnHealthUpdate += OnHealthUpdate;
        OnHealthUpdate(HealthSystem.Singleton.CurrentHealth);
    }

    void OnHealthUpdate(int health)
    {
        if (health <= 0)
        {
            buttonUseLife.SetActive(false);
            buttonRefillLives.SetActive(true);
            timer.SetActive(true);
        }
        else if (health == HealthSystem.Singleton.MaximumHealth)
        {
            buttonUseLife.SetActive(true);
            buttonRefillLives.SetActive(false);
            timer.SetActive(false);
        }
        else
        {
            buttonUseLife.SetActive(true);
            buttonRefillLives.SetActive(true);
            timer.SetActive(true);
        }
    }

    public void UseLife()
    {
        HealthSystem.Singleton.RemoveHealth();
    }

    public void RefillLives()
    {
        HealthSystem.Singleton.AddHealth(HealthSystem.Singleton.MaximumHealth);
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

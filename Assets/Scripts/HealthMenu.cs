using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMenu : MonoBehaviour
{
    Animator animator;

    [Header("References")]
    [SerializeField] GameObject buttonUseLife;
    [SerializeField] GameObject buttonRefillLives;
    [SerializeField] GameObject timer;
    [SerializeField] VerticalLayoutGroup verticalLayoutGroup;

    private void OnEnable()
    {
        if(!animator)
            animator = GetComponent<Animator>();
        animator.Play("MenuIn");
        HealthSystem.OnHealthUpdate += OnHealthUpdate;
        OnHealthUpdate(HealthSystem.Singleton.CurrentHealth);
    }

    void OnHealthUpdate(int health)
    {
        if (health <= 0)
        {
            buttonUseLife.SetActive(false);
            buttonRefillLives.SetActive(true);
            verticalLayoutGroup.padding = new RectOffset(0, 0, 0, 0);
            timer.SetActive(true);
        }
        else if (health == HealthSystem.Singleton.MaximumHealth)
        {
            buttonUseLife.SetActive(true);
            buttonRefillLives.SetActive(false);
            verticalLayoutGroup.padding = new RectOffset(0, 0, 0, 0);
            timer.SetActive(false);
        }
        else
        {
            buttonUseLife.SetActive(true);
            buttonRefillLives.SetActive(true);
            verticalLayoutGroup.padding = new RectOffset(50, 50, 0, 0);
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
        animator.Play("MenuOut");
    }

    public void AnimationEnd()
    {
        gameObject.SetActive(false);
    }
}

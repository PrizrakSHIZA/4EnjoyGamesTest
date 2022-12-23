using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI healthText;

    void Awake()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        HealthSystem.OnHealthUpdate += OnHealthUpdate;
    }

    private void OnEnable()
    {
        OnHealthUpdate(HealthSystem.Singleton.CurrentHealth);
    }

    void OnHealthUpdate(int health) 
    {
        healthText.text = health.ToString();
    }
}

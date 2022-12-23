using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthTimer : MonoBehaviour
{
    TextMeshProUGUI timerText;

    void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        HealthSystem.OnTimeUntilUpdate += OnTimeUntilUpdate;
    }

    void OnTimeUntilUpdate(string timeUntil)
    {
        timerText.text = timeUntil;
    }
}

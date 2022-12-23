using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HealthSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int maxHP = 5;
    [SerializeField, Tooltip("In seconds")] float recoverInterval = 20f;
    //only for testing
    [SerializeField] int startHP = 0;

    [Header("References")]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI timerText;

    int currentHP;
    DateTime startTime, endTime; //using DateTime for posibility of saving endTime to Prefs or elsewhere and calculate new hp

    public void Start()
    {
        currentHP = startHP; //Here might be loading last hp from serialized PlayerPrefs or remote database
        healthText.text = currentHP.ToString();
        CheckHealth();
    }

    public void CheckHealth()
    {
        if (currentHP < maxHP)
            StartTimer();
        else if (currentHP == maxHP)
            timerText.text = "Full";
    }

    public void StartTimer()
    { 
        startTime = DateTime.Now;
        endTime = startTime.AddSeconds(recoverInterval);
        // For better performance and preventing changing time difference every frame in update (cuz we dont need ms and etc.)
        // we`re using coroutine, which will update diff every .2f(or any other) of a second of realtime 
        StartCoroutine(HealthTimer());
    }

    IEnumerator HealthTimer()
    {
        while (DateTime.Now < endTime)
        {
            timerText.text = (endTime - DateTime.Now).ToString(@"mm\:ss");
            yield return new WaitForSecondsRealtime(.2f);
        }
        currentHP++;
        healthText.text = currentHP.ToString();
        CheckHealth();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem Singleton;

    public static Action<int> OnHealthUpdate;
    public static Action<string> OnTimeUntilUpdate;

    public int MaximumHealth => maxHP;
    public int CurrentHealth => currentHP;

    [Header("Settings")]
    [SerializeField] int maxHP = 5;
    [SerializeField, Tooltip("In seconds")] float recoverInterval = 20f;
    //only for testing
    [SerializeField] int startHP = 0;

    bool coroStarted = false;
    string timeUntil;
    int currentHP;
    DateTime startTime, endTime; //using DateTime for posibility of saving endTime to Prefs or elsewhere and calculate new hp

    private void Awake()
    {
        #region Singleton
        if (!Singleton) Singleton = this;
        else
        {
            Debug.LogWarning($"There is more than 1 script on scene of type {typeof(HealthSystem)}");
            Destroy(this); //preventing from being more than 1 script on scene
        }
        #endregion

        currentHP = startHP; //Here might be loading last hp from serialized PlayerPrefs or remote database
        OnHealthUpdate?.Invoke(currentHP);
        CheckHealth();
    }

    public void CheckHealth()
    {
        if (currentHP < maxHP && !coroStarted)
            StartTimer();
        else if (currentHP == maxHP)
        {
            StopAllCoroutines();
            coroStarted = false;
            OnTimeUntilUpdate?.Invoke("Full");
        }
    }

    public void StartTimer()
    { 
        startTime = DateTime.Now;
        endTime = startTime.AddSeconds(recoverInterval);
        OnTimeUntilUpdate?.Invoke(timeUntil);
        // For better performance and preventing changing time difference every frame in update (cuz we dont need ms and etc.)
        // we`re using coroutine, which will update diff every .2f(or any other) of a second of realtime 
        StartCoroutine(HealthTimer());
    }

    public void RemoveHealth(int amount = 1)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        OnHealthUpdate?.Invoke(currentHP);
        CheckHealth();
    }

    public void AddHealth(int amount = 1)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;

        OnHealthUpdate?.Invoke(currentHP);
        CheckHealth();
    }

    IEnumerator HealthTimer()
    {
        coroStarted = true;
        while (DateTime.Now < endTime)
        {
            timeUntil = (endTime - DateTime.Now).ToString(@"mm\:ss");
            OnTimeUntilUpdate?.Invoke(timeUntil);
            yield return new WaitForSecondsRealtime(.2f);
        }
        coroStarted = false;
        AddHealth();
        CheckHealth();
    }
}

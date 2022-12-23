using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyBonusSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rewardText;
    DateTime today;
    [SerializeField] List<float> rewardList = new List<float> { 2, 3 };

    // here might be var like 'LastLogIn' which will prevent from collecting dailt bonus more than once a day,
    // but this is test so I dont implemetn it

    void Start()
    {
        CalculateDailyReward();
    }

    void CalculateDailyReward()
    {
        today = DateTime.Today;
        int firstMonth = (int)GetFirstMonthOfSeason(today);
        DateTime startDate = new DateTime(DateTime.Today.Year, firstMonth, 1);
        if (startDate > today)
            startDate.AddYears(-1);
        int numberOfDays = (today - startDate).Days + 1;
        int temp;

        for (int i = 2; i < numberOfDays; i++)
        {
            temp = (int)(rewardList[i - 2] + rewardList[i - 1] / 100 * 60);
            rewardList.Add(temp);
        }
        rewardText.text = rewardList[numberOfDays - 1].ToString();
    }

    Seasons GetFirstMonthOfSeason(DateTime date)
    {
        switch (date.Month)
        {
            case 12:
            case 1:
            case 2:
                return Seasons.winter;
            case 3:
            case 4:
            case 5:
                return Seasons.spring;
            case 6:
            case 7:
            case 8:
                return Seasons.summer;
            case 9:
            case 10:
            case 11:
                return Seasons.autumn;
            default:
                return Seasons.winter;
        }
    }

    enum Seasons
    { 
        winter = 12,
        spring = 3,
        summer = 6,
        autumn = 9
    }
}

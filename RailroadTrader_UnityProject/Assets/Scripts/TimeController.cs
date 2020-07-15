﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    [Tooltip("How many minutes in real time is 1 ingame hour")]
    public float realTimeMinPerHour = 2.0f;
    [Tooltip("Order: paused, normal, fast, super fast")]
    public float[] ingameSpeed = new float[4];

    private IngameTime curTime;
    private float passedTime = 0.0f;

    public static event System.Action<IngameTime> OnHourChange = delegate { };
    public static event System.Action OnHourEnd = delegate { };
    public static event System.Action OnDayEnd = delegate { };
    public static event System.Action OnMonthEnd = delegate { };
    public static event System.Action OnYearEnd = delegate { };
    

    private void Start()
    {
        curTime = new IngameTime(0, 1, 1, 1);
        OnHourChange(curTime);
    }

    private void Update()
    {
        if(GameManager.Instance.State == GameState.RUNNING)
        {
            CountMinutes();
        }
    }

    private void CountMinutes()
    {
        passedTime += Time.deltaTime;
        if (passedTime >= (realTimeMinPerHour * 60))
        {
            passedTime = 0.0f;
            AddHours();           
        }
    }
    
    private void AddHours(int hoursToAdd = 1)
    {
        if (curTime.ChangeDay())
        {
            OnDayEnd();
            if (curTime.ChangeMonth())
            {
                OnMonthEnd();
                if (curTime.ChangeYear())
                {
                    OnYearEnd();
                }
            }
        }
        OnHourChange(curTime);
    }

    private int StartNextDay()
    {
        return 0;
    }

    private int StartNextMonth()
    {
        return 0;
    }

    private int StartNextYear()
    {
        return 0;
    }

    /// <summary>
    /// Values are taken from ingameSpeed array: 0 = Pause, 1 = normal, 2 = fast, 3 = super fast
    /// </summary>
    /// <param name="speed"></param>
    public void SetGameSpeed(int speed)
    {
        //https://docs.unity3d.com/ScriptReference/Time-timeScale.html
        //https://docs.unity3d.com/ScriptReference/Time-fixedDeltaTime.html change this too?

    }

    public IngameTime CurTime()
    {
        return curTime;
    }
}

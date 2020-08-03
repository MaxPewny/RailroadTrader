using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public GenericValues values;

    [Tooltip("How many secs in real time is 1 ingame hour")]
    public float realTimePerHour = 120.0f;
    [Tooltip("Order: paused, normal, fast, super fast")]
    public float[] ingameSpeed = new float[4];
    
    public IngameTime CurTime { get; protected set; }
    private float passedTime = 0.0f;

    public static event System.Action<IngameTime> OnHourChange = delegate { };
    public static event System.Action OnHourEnd = delegate { };
    public static event System.Action OnDayEnd = delegate { };
    public static event System.Action OnMonthEnd = delegate { };
    public static event System.Action OnYearEnd = delegate { };


    private void Start()
    {
        CurTime = new IngameTime(0, 1, 1, 1);
        OnHourChange(CurTime);
        realTimePerHour = values.RealTimeSecsPerHour;

        FinanceController.GetCurTime += CurrentTime;
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
        if (passedTime >= (realTimePerHour))
        {
            passedTime = 0.0f;
            AddHours();           
        }
    }
    
    private void AddHours(int hoursToAdd = 1)
    {
        if (CurTime.ChangeDay())
        {
            OnDayEnd();
            if (CurTime.ChangeMonth())
            {
                OnMonthEnd();
                if (CurTime.ChangeYear())
                {
                    OnYearEnd();
                }
            }
        }
        OnHourChange(CurTime);
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

    public IngameTime CurrentTime()
    {
        return CurTime;
    }
}

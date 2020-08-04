using core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected GameManager() { }
    public bool UIElementOpen = false;
    public bool BuildModeActive = false;
    
    public GameState State { get; protected set; }

    private void Awake()
    {
        //StartCoroutine(PrepareGameForStart());
    }

    private void Update()
    {
        //TestTimeScale();
    }

    public IEnumerator PrepareGameForStart()
    {
        SetGameState(GameState.LOADING);
        yield return new WaitForSeconds(1.0f);
        FinanceController FC = FindObjectOfType<FinanceController>();
        FC.AddCurrency(FC.Values.StartMoney);
        yield return new WaitForSeconds(1.0f);
        print("Game ready");
        SetGameState(GameState.RUNNING);
    }

    public void SetGameState(GameState newState)
    {
        State = newState;
    }

    private void TestTimeScale()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Time.timeScale < 0.1f)
                Time.timeScale = 1.0f;
            else
                Time.timeScale = 0.0f;
        }
    }
}

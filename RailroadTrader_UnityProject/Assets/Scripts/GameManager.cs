using core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected GameManager() { }

    public GameState State { get; protected set; }
    private int startMoney = 1000;

    private void Awake()
    {
        StartCoroutine(PrepareGameForStart());
    }

    private IEnumerator PrepareGameForStart()
    {
        SetGameState(GameState.LOADING);
        yield return new WaitForSeconds(1.0f);
        StartForGate2();
        print("Game ready");
        SetGameState(GameState.RUNNING);
    }

    private void StartForGate2()
    {
        FinanceController FC = FindObjectOfType<FinanceController>();
        FC.AddCurrency(startMoney);
        //FC.UpdateMonthlyIncome(startMoney);
    }

    public void SetGameState(GameState newState)
    {
        State = newState;
    }

}

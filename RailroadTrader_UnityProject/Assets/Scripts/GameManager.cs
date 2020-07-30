using core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected GameManager() { }
    
    public GameState State { get; protected set; }
    public bool BuildModeActive = false;

    private void Awake()
    {
        StartCoroutine(PrepareGameForStart());
    }

    private IEnumerator PrepareGameForStart()
    {
        SetGameState(GameState.LOADING);
        yield return new WaitForSeconds(1.0f);
        FinanceController FC = FindObjectOfType<FinanceController>();
        FC.AddCurrency(FC.Values.StartMoney);
        print("Game ready");
        SetGameState(GameState.RUNNING);
    }

    public void SetGameState(GameState newState)
    {
        State = newState;
    }

}

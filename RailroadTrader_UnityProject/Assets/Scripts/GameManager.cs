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

    public IEnumerator PrepareGameForStart()
    {
        SetGameState(GameState.LOADING);
        yield return new WaitForSeconds(1.5f);
        FinanceController FC = FindObjectOfType<FinanceController>();
        FC.AddCurrency(FC.Values.StartMoney);
        yield return new WaitForSeconds(0.5f);
        print("Game ready");
        SetGameState(GameState.RUNNING);
    }

    public void SetGameState(GameState newState)
    {
        State = newState;
    }
}

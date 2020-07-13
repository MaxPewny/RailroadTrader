using core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected GameManager() { }

    public GameState State { get; protected set; }

    private void Awake()
    {
        StartCoroutine(PrepareGameForStart());
    }

    private IEnumerator PrepareGameForStart()
    {
        SetGameState(GameState.LOADING);
        yield return new WaitForSeconds(1.0f);
        print("Game ready");
        SetGameState(GameState.RUNNING);
    }

    public void SetGameState(GameState newState)
    {
        State = newState;
    }

}

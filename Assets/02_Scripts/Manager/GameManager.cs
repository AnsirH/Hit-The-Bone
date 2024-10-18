using StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameManagerState
    {
        Idle = 0,
        TowerInstallation
    }
    
    private StateMachine<GameManager> stateMachine;
    private StateBase<GameManager>[] states;

    protected override void Awake()
    {
        base.Awake();
        states = new StateBase<GameManager>[2];
        states[(int)GameManagerState.Idle] = new GameManagerIdleState();
        states[(int)GameManagerState.TowerInstallation] = new GameManagerTowerInstallationState();

        stateMachine = new StateMachine<GameManager>();
        stateMachine.SetUp(this, states[(int)GameManagerState.Idle]);


    }

    private void Update()
    {
        stateMachine.Updated();
    }
}

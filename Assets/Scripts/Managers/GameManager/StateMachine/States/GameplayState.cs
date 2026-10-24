using System.Collections.Generic;
using UnityEngine;

public class GameplayState : GameState
{
    public GameplayState(GameManager gameManager, GameStateMachine gameStateMachine) : base(gameManager, gameStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entering Gameplay");
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

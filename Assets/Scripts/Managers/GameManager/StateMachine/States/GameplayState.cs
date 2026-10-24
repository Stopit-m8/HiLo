using UnityEngine;

public class GameplayState : GameState
{
    public GameplayState(GameManager gameManager, GameStateMachine gameStateMachine) : base(gameManager, gameStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        gameManager.StartGame();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

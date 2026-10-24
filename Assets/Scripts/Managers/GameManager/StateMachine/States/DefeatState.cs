using UnityEngine;

public class DefeatState : GameState
{
    public DefeatState(GameManager gameManager, GameStateMachine gameStateMachine) : base(gameManager, gameStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0f;
        gameManager.EndGame();
    }

    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1f;
    }
}

using UnityEngine;

public class GameStateMachine
{
    public GameState CurrentGameState {  get; private set; }

    public void Initialize(GameState startingState)
    {
        CurrentGameState = startingState;
        CurrentGameState.EnterState();
    }

    public void ChangeState(GameState newState)
    {
        CurrentGameState.ExitState();
        CurrentGameState = newState;
        CurrentGameState.EnterState();
    }
}

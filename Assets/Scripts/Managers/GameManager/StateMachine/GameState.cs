using Unity.VisualScripting;
using UnityEngine;

public class GameState
{
    protected GameManager gameManager;
    protected GameStateMachine gameStateMachine;

    public GameState(GameManager gameManager, GameStateMachine gameStateMachine)
    {
        this.gameManager = gameManager;
        this.gameStateMachine = gameStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
}

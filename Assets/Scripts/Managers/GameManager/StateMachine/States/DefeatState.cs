using UnityEngine;

public class DefeatState : GameState
{
    private CanvasGroup _defeatPanel;
    public DefeatState(GameManager gameManager, GameStateMachine gameStateMachine) : base(gameManager, gameStateMachine)
    {
        _defeatPanel = GameObject.FindGameObjectWithTag("DefeatPanel").GetComponent<CanvasGroup>();
    }

    public override void EnterState()
    {
        base.EnterState();
        _defeatPanel.alpha = 1.0f;
        _defeatPanel.interactable = true;
        _defeatPanel.blocksRaycasts = true;
        Time.timeScale = 0f;
        Debug.Log("Entering Defeat");
    }

    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1f;
        _defeatPanel.alpha = 0f;
        _defeatPanel.interactable = false;
        _defeatPanel.blocksRaycasts = false;
    }
}

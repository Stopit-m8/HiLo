using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region State Machine Variables

    public GameStateMachine StateMachine {  get; set; }
    public DefeatState DefeatState { get; set; }
    public GameplayState GameplayState { get; set; }
    #endregion

    private void Awake()
    {
        StateMachine = new GameStateMachine();
        DefeatState = new DefeatState(this, StateMachine);
        GameplayState = new GameplayState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(GameplayState);
    }

    public void StartGame()
    {

    }

    public void EndGame()
    {

    }

    #region CardManager
    //shuffle card
    //draw card
    //update card count
    #endregion

    #region UIManager
    //update score 
    //update life
    //update card count
    #endregion

    #region ScoreManager
    //update score 
    #endregion

    #region LifeManager
    //update life
    #endregion

    #region GameManager
    //get button input
    //check HiLo
    //restart
    #endregion
}

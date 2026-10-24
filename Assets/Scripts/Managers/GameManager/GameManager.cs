using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private CardManager cardManager;
    public event Action OnPlayerWrong;
    public event Action OnPlayerRight;
    public event Action OnValueChecked;
    private event Action<currentSelected> OnButtonPressed;
    [SerializeField] private float checkTime = 1f;
    private enum currentSelected
    {
        Low = 0,
        High = 1
    }

    private currentSelected _currentSelected;
    #region State Machine Variables

    public GameStateMachine StateMachine {  get; set; }
    public DefeatState DefeatState { get; set; }
    public GameplayState GameplayState { get; set; }
    #endregion

    private void OnEnable()
    {
        OnButtonPressed += CheckHiLo;
        cardManager.OnCardsCalledBack += DrawCard;
    }

    private void OnDisable()
    {
        OnButtonPressed -= CheckHiLo;
        cardManager.OnCardsCalledBack -= DrawCard;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        StateMachine = new GameStateMachine();
        DefeatState = new DefeatState(this, StateMachine);
        GameplayState = new GameplayState(this, StateMachine);

    }

    private void Start()
    {
        StateMachine.Initialize(GameplayState);
        StartGame();
    }

    private void DrawCard()
    {
        cardManager.DrawRandomCard();
        Debug.Log("Draw");
    }

    private void StartGame()
    {
        StateMachine.ChangeState(GameplayState);
        DrawCard();
    }

    private void EndGame()
    {
        StateMachine.ChangeState(DefeatState);
    }

    #region Buttons
    public void HighSelected()
    {
        Debug.Log("High Selected");
        _currentSelected = currentSelected.High;
        DrawCard();
        OnButtonPressed?.Invoke(_currentSelected);
    }

    public void LowSelected()
    {
        Debug.Log("Low Selected");
        _currentSelected = currentSelected.Low;
        DrawCard();
        OnButtonPressed?.Invoke(_currentSelected);
    }

    public void Restart()
    {
        //Restart everything
    }
    #endregion

    private void CheckHiLo(currentSelected _currentSelected)
    {
        Debug.Log("Check HiLo");
        int higher;
        (int _prevValue, int _nextValue) = cardManager.GetCardsValue();
        
        if (_prevValue < _nextValue)
        {
            higher = 1;
            
        }
        else
        {
            higher = 0;
        }

        Debug.Log($"Higher = {higher}");

        if (higher == (int)_currentSelected)
        {
            Debug.Log("Player Right");
            OnPlayerRight?.Invoke();
        }
        else
        {
            Debug.Log("Player Wrong");
            OnPlayerWrong?.Invoke();
        }
        StartCoroutine(WaitToCheck());
    }

    IEnumerator WaitToCheck()
    {
        yield return new WaitForSeconds(checkTime);
        OnValueChecked?.Invoke();
    }

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
    //restart
    #endregion
}

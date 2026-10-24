using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private CardManager cardManager;
    [SerializeField] private LifeManager lifeManager;
    [SerializeField] private float checkTime = 1f;
    private bool _isChecking;
    private Coroutine _checkCoroutine;
    private Coroutine _defeatCoroutine;

    public event Action OnPlayerWrong;
    public event Action OnPlayerRight;
    public event Action OnValueChecked;
    public event Action OnRestarted;
    private event Action<CurrentSelected> OnButtonPressed;

    
    private enum CurrentSelected
    {
        Low = 0,
        High = 1
    }

    private CurrentSelected _currentSelected;
    #region State Machine Variables

    public GameStateMachine StateMachine {  get; set; }
    public DefeatState DefeatState { get; set; }
    public GameplayState GameplayState { get; set; }
    #endregion

    private void OnEnable()
    {
        OnButtonPressed += CheckHiLo;
        lifeManager.OnDeath += EndGame;
        cardManager.OnCardsCalledBack += DrawCard;
    }

    private void OnDisable()
    {
        OnButtonPressed -= CheckHiLo;
        lifeManager.OnDeath -= EndGame;
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

    }

    private void EndGame()
    {
        if (_defeatCoroutine != null)
        {
            return;
        }

        _defeatCoroutine = StartCoroutine(WaitForDefeat());
    }

    #region Buttons
    public void HighSelected()
    {
        if (_isChecking)
        {
            return;
        }
        Debug.Log("High Selected");
        _currentSelected = CurrentSelected.High;
        DrawCard();
        OnButtonPressed?.Invoke(_currentSelected);
    }

    public void LowSelected()
    {
        if (_isChecking)
        {
            return;
        }
        Debug.Log("Low Selected");
        _currentSelected = CurrentSelected.Low;
        DrawCard();
        OnButtonPressed?.Invoke(_currentSelected);
    }

    public void Restart()
    {
        SkipCheckTime();
        StateMachine.ChangeState(GameplayState);
        OnRestarted?.Invoke();
        
    }
    #endregion

    private void CheckHiLo(CurrentSelected _currentSelected)
    {
        Debug.Log("Check HiLo");
        int higher;
        (int _prevValue, int _nextValue, int _cardCount) = cardManager.GetCardsValue();
        
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
            Debug.Log($"Subscribers: {OnPlayerRight?.GetInvocationList().Length ?? 0}");
            OnPlayerRight?.Invoke();
        }
        else
        {
            Debug.Log("Player Wrong");
            OnPlayerWrong?.Invoke();
        }
        _checkCoroutine = StartCoroutine(WaitToCheck());
        if (_cardCount <= 0)
        {
            EndGame();
        }
            
    }

    private void SkipCheckTime()
    {
        if (_checkCoroutine != null)
        {
            StopCoroutine(_checkCoroutine);
            _checkCoroutine = null;
            OnValueChecked?.Invoke();
            _isChecking = false;
        }

        if (_defeatCoroutine != null)
        {
            StopCoroutine(_defeatCoroutine);
            _defeatCoroutine = null;
        }
    }

    IEnumerator WaitForDefeat()
    {
        yield return StartCoroutine(WaitToCheck());
        Debug.Log("End game");
        StateMachine.ChangeState(DefeatState);

        _defeatCoroutine = null;
    }

    IEnumerator WaitToCheck()
    {
        _isChecking = true;
        yield return new WaitForSecondsRealtime(checkTime);
        OnValueChecked?.Invoke();
        _isChecking = false;
    }
}

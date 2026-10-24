using System;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private CardPooling cardPooling;
    [SerializeField] private List<CardSO> cardList;
    [SerializeField] private Transform prevCardArea;
    [SerializeField] private Transform nextCardArea;

    private List<CardSO> _currCardList;

    public event Action OnDrawCard;
    public event Action OnCardsCalledBack;

    private GameObject _prevCard;
    private GameObject _nextCard;

    private CardSO _prevCardData;
    private CardSO _nextCardData;

    private void OnEnable()
    {
        GameManager.Instance.OnValueChecked += CallBackCards;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnValueChecked -= CallBackCards;
    }

    private void Awake()
    {
        _currCardList = new List<CardSO>();
        
        foreach (CardSO card in cardList)
        {
            _currCardList.Add(card);
        }
    }

    public void DrawRandomCard()
    {
        int random = UnityEngine.Random.Range(0, _currCardList.Count);
        Debug.Log(_currCardList[random]);
        DrawCard(_currCardList[random]);
        _currCardList.RemoveAt(random);
    }

    private void DrawCard(CardSO cardData)
    {
        if (_prevCard == null)
        {
            _prevCardData = cardData;
            _prevCard = cardPooling.GetObject();
            _prevCard.GetComponent<Card>().Intialize(cardData, prevCardArea);
            OnDrawCard?.Invoke();
        }
        else
        {
            _nextCardData = cardData;
            _nextCard = cardPooling.GetObject();
            _nextCard.GetComponent<Card>().Intialize(cardData, nextCardArea);
            OnDrawCard?.Invoke();
        }
    }

    private void CallBackCards()
    {
        cardPooling.ReturnObject(_prevCard);
        cardPooling.ReturnObject(_nextCard);
        _prevCard = null;
        _prevCardData = null;
        _nextCard = null;
        _nextCardData = null;
        OnCardsCalledBack?.Invoke();
    }

    public (int, int) GetCardsValue()
    {
        int prevValue = _prevCardData.cardValue;
        int nextValue = _nextCardData.cardValue;
        if (prevValue == nextValue)
        {
            if (_prevCardData.cardSymbol > _nextCardData.cardSymbol)
            {
                prevValue++;
            }
            else
            {
                nextValue++;
            }
        }
        return (prevValue, nextValue);
    }


}

using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private int _value;

    public void Intialize(CardSO cardData, Transform placeArea)
    {
        _value = cardData.cardValue;
        _spriteRenderer.sprite = cardData.cardSprite;
        transform.position = placeArea.position;
    }
}

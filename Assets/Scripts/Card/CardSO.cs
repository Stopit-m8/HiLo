using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Create Card SO")]
public class CardSO : ScriptableObject
{
    public int cardValue;
    public Sprite cardSprite;
    public enum CardSymbol
    {
        Diamond = 4,
        Club = 3,
        Heart = 2,
        Spade = 1
    }
    public CardSymbol cardSymbol;
}

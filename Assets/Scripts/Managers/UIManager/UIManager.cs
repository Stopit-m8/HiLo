using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text scoreValueText;
    [SerializeField] private TMP_Text defeatScoreValueText;
    [SerializeField] private TMP_Text lifeValueText;
    [SerializeField] private TMP_Text cardCountText;

    [Header("Manager")]
    [SerializeField] private LifeManager lifeManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private CardManager cardManager;

    private void OnEnable()
    {
        lifeManager.OnLifeChanged += UpdateLife;
        scoreManager.OnScoreChanged += UpdateScore;
        cardManager.OnCountChanged += UpdateCardCount;
    }

    private void OnDisable()
    {
        lifeManager.OnLifeChanged -= UpdateLife;
        scoreManager.OnScoreChanged -= UpdateScore;
        cardManager.OnCountChanged -= UpdateCardCount;
    }

    private void UpdateScore(float currScore)
    {
        scoreValueText.text = currScore.ToString();
        defeatScoreValueText.text = currScore.ToString();
    }

    private void UpdateLife(int currLife)
    {
        lifeValueText.text = currLife.ToString();
    }

    private void UpdateCardCount(int currCount)
    {
        cardCountText.text = currCount.ToString();
    }

}

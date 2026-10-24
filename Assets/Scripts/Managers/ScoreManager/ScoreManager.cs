using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float scorePerRight;
    private float _currScore = 0f;
    public event Action<float> OnScoreChanged;

    private void OnEnable()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null!");
            return;
        }
        GameManager.Instance.OnPlayerRight += ChangeScore;
        GameManager.Instance.OnRestarted += RestartScore;
    }

    private void OnDisable()
    {
        Debug.Log("Score disabled; unsubscribing");
        GameManager.Instance.OnPlayerRight -= ChangeScore;
        GameManager.Instance.OnRestarted -= RestartScore;
    }

    private void ChangeScore()
    {
        _currScore += scorePerRight;
        Debug.Log($"Current Score = {_currScore}");
        OnScoreChanged?.Invoke(_currScore);
    }

    private void RestartScore()
    {
        _currScore = 0;
        Debug.Log("Score Reset");
        OnScoreChanged?.Invoke(_currScore);
    }
}

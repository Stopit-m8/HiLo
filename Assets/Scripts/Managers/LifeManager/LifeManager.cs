using System;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int maxLife;
    private int _currLife;

    public event Action OnDeath;

    private void Awake()
    {
        _currLife = maxLife;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerWrong += ReduceLife;
        GameManager.Instance.OnRestarted += RestartLife;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerWrong -= ReduceLife;
        GameManager.Instance.OnRestarted -= RestartLife;
    }

    private void ReduceLife()
    {
        _currLife--;
        Debug.Log($"Current life = {_currLife}");
        if (_currLife <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    private void RestartLife()
    {
        _currLife = maxLife;
    }
}

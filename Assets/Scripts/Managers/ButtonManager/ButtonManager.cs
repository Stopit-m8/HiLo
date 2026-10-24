using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void LowSelected()
    {
        GameManager.Instance.LowSelected();
    }

    public void HighSelected()
    {
        GameManager.Instance.HighSelected();
    }

    public void Restart()
    {
        GameManager.Instance.Restart();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject endGamePanel;
    
    public bool isCanClick = false;
    
    public void ClosePanel()
    {
        endGamePanel.SetActive(false);
        GameController.Instance.StartGame();
    }

    public void ReloadGame()
    {
        if (isCanClick)
            GameController.Instance.ReloadGame();
    }
}

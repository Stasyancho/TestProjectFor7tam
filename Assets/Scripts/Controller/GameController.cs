using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]private FigureGenerator figureGenerator;
    [SerializeField]private InputManager inputManager;
    [SerializeField]private PanelManager panelManager;
    [SerializeField]private UIViewer uiViewer;
    [SerializeField]private UIController uiController;
    
    private List<Figure> activeFigures;
    
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameController>();
            }
            return instance;
        }
    }
    
    void Start()
    {
        figureGenerator.CreteFigures();
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void ReloadGame()
    {
        StartCoroutine(StartGameCoroutine(activeFigures));
    }

    private IEnumerator StartGameCoroutine(List<Figure> figures = null)
    {
        yield return figureGenerator.StartGeneartion(figures);
        activeFigures = figureGenerator.activeFigures;
        inputManager.isCanClick = true;
        uiController.isCanClick = true;
    }

    public void AddFigureToPanel(Figure figure)
    {
        activeFigures.Remove(figure);
        panelManager.AddFigure(figure);
        if (activeFigures.Count == 0)
        {
            inputManager.isCanClick = false;
            uiController.isCanClick = false;
            uiViewer.ResultPanel(true);
        }
    }

    public void GameLost()
    {
        inputManager.isCanClick = false;
        uiController.isCanClick = false;
        uiViewer.ResultPanel(false);
    }
}

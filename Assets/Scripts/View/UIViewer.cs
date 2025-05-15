using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewer : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text text;
    
    public void ResultPanel(bool resultValue)
    {
        panel.SetActive(true);
        text.text = resultValue ? "Победа" : "Проигрыш";
    }
}

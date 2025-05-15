using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
   [SerializeField] private List<Transform> panelPositions;
   
   private List<Figure> figures = new List<Figure>();

   public void AddFigure(Figure figure)
   {
      figures.Add(figure);
      figure.transform.rotation = Quaternion.Euler(0, 0, 0);
      figure.rb.bodyType = RigidbodyType2D.Static;
      
      CheckTrios();
      UpdatePanel();
      CheckLose();
   }

   void CheckTrios()
   {
      //Просто тройки убираем
      foreach (var figure in figures)
      {
         var duplicates = figures.Where(x =>
            x.color == figure.color && 
            x.shape == figure.shape && 
            x.animal == figure.animal).ToList();
         if (duplicates.Count() == 3)
         {
            for (int i = 2; i >= 0; i--)
            {
               duplicates[i].gameObject.SetActive(false);
               duplicates[i].rb.bodyType = RigidbodyType2D.Dynamic;
               figures.Remove(duplicates[i]);
            }  
            return;
         }
      }
   }

   void UpdatePanel()
   {
      //Если совпала тройка, то смещаемся 
      for (int i = 0; i < figures.Count; i++)
      {
         figures[i].transform.position = panelPositions[i].position;
      }
   }

   void CheckLose()
   {
      if (figures.Count == 7)
      {
         figures.ForEach(x =>
         {
            x.gameObject.SetActive(false);
            x.rb.bodyType = RigidbodyType2D.Dynamic;
         });
         figures.Clear();
         GameController.Instance.GameLost();
      }
   }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FigureGenerator : MonoBehaviour
{
    public List<Figure> activeFigures = new List<Figure>();
    
    [SerializeField] private int numberOfFigures = 24;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] figures;
    [SerializeField] private Color[] colors;
    //Chicken - Yellow color
    //Cow - White color
    //Dog - Brown color
    //Cat - Grey color 
    [SerializeField] private Color[] animals;
    
    private List<List<Figure>> fullFiguresList = new List<List<Figure>> ();
    
    //Просто заранее создаем все возможные тройки,чтобы лишний раз не нагружать методом "Instantiate"
    public void CreteFigures()
    {
        foreach (var figure in figures)
        {
            foreach (var color in colors)
            {
                foreach (var animal in animals)
                {
                    var obj = Instantiate(figure).GetComponent<Figure>();
                    obj.SetColor(color);
                    obj.SetAnimal(animal);
                    fullFiguresList.Add(new List<Figure>()
                    {
                        obj,
                        Instantiate(obj).GetComponent<Figure>(),
                        Instantiate(obj).GetComponent<Figure>() 
                    });
                }
            }
        }
    }

    public IEnumerator StartGeneartion(List<Figure> remainingFigures = null)
    {
        fullFiguresList.ForEach(x => x.ForEach(
            y =>
            {
                if (y.rb.bodyType == RigidbodyType2D.Dynamic)
                    y.gameObject.SetActive(false);
            }));
        
        var choosedFigures = new List<Figure> ();
        List<int> addedFigures = new List<int>();
        var maxFigures = numberOfFigures;

        //Если что-то пришло в currentFigures, значит это перезапуск. Иначе пропускаем
        //Запоминаем все элементы, которые уже добавлены в панель, чтобы всегда можно было пройти игру
        if (remainingFigures != null)
        {
            maxFigures = remainingFigures.Count;
            foreach (var figure in remainingFigures)
            {
                var duplicates = remainingFigures.Where(x =>
                    x.color == figure.color && 
                    x.shape == figure.shape && 
                    x.animal == figure.animal).ToList();
                if (duplicates.Count() != 3)
                {
                    choosedFigures.Add(figure);
                    for (var i = 0; i < fullFiguresList.Count; i++)
                    {
                        if(fullFiguresList[i].Contains(figure) && !addedFigures.Contains(i))
                            addedFigures.Add(i);
                    }
                }
            }   
        }
        
        //Выбираем рандомные "тройки"
        while (choosedFigures.Count < maxFigures)
        {
            var randomIndex = Random.Range(0, fullFiguresList.Count);
            if (!addedFigures.Contains(randomIndex))
            {
                fullFiguresList[randomIndex].ForEach(x => choosedFigures.Add(x));   
                addedFigures.Add(randomIndex);
            }
        }
        
        activeFigures.Clear();
        choosedFigures.ForEach(x => activeFigures.Add(x));
        
        //Генерируем в случайном порядке
        while (choosedFigures.Count > 0)
        {
            var currentFigure = choosedFigures[Random.Range(0, choosedFigures.Count)];
            if (currentFigure.rb.bodyType == RigidbodyType2D.Dynamic)
                currentFigure.transform.position = spawnPoint.position + new Vector3((Random.value - 0.5f) / 30, 0, 0);
            currentFigure.gameObject.SetActive(true);
            choosedFigures.Remove(currentFigure);
            yield return new WaitForSeconds(0.2f);
        }
    }
}

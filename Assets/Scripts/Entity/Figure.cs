using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public Shape shape;
    public Color color;
    public Color animal;
    
    public Rigidbody2D rb;
    
    [SerializeField] private SpriteRenderer main;
    [SerializeField] private SpriteRenderer sprite;
    
    public void SetColor(Color color)
    {
        main.color = color;
        this.color = color;
    }

    public void SetAnimal(Color animal)
    {
        this.animal = animal;
        this.sprite.color = animal;
    }
}

public enum Shape
{
    Circle,
    Polygon
}

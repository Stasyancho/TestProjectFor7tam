using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool isCanClick = false;

    void Update()
    {
        if (Input.touchCount > 0 && isCanClick)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    var target = Physics2D.Raycast(ray.origin, ray.direction);
                    if (target.collider != null)
                    {
                        GameController.Instance.AddFigureToPanel(target.collider.gameObject.GetComponentInParent<Figure>());
                    }
                }
            }
        }
    }
}

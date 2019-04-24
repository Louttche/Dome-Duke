using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public GameObject img;
    public GameObject imgHover;
   
    void Start()
    {
        imgHover.SetActive(false);
    }

    public void Hover()
    {
        imgHover.SetActive(true);
        img.SetActive(false);
    }
    public void Release()
    {
        imgHover.SetActive(false);
        img.SetActive(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.Translate(0, (Input.mousePosition.y / 15), 0);
      
        if (transform.position.y >= 170)
        {
            eventData.pointerDrag = null;
            
            transform.localPosition = Vector3.zero;

            imgHover.SetActive(false);
            img.SetActive(true);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;

    }


}

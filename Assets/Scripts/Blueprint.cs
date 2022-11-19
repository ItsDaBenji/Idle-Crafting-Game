using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Blueprint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] GameObject recipeImage;


    public void OnPointerEnter(PointerEventData eventData)
    {
        recipeImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        recipeImage.SetActive(false);
    }

}

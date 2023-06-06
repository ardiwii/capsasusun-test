using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardPicker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Card pickedCard;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.cardController.PickCard(pickedCard);
    }
}

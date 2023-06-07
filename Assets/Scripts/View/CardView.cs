using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image cardImage;
    [SerializeField] private GameObject pickedOutline;
    private Card viewedCard;

    public delegate void cardClicked(Card clickedCard);
    public event cardClicked OnCardClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCardClicked?.Invoke(viewedCard);
    }

    public void SetView(Card card, Sprite cardSprite)
    {
        viewedCard = card;
        cardImage.sprite = cardSprite;
    }

    public void TogglePicked(bool active)
    {
        pickedOutline.SetActive(active);
    }

    public Sprite GetCardSprite()
    {
        return cardImage.sprite;
    }
}

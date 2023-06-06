using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private GameObject pickedOutline;

    public void SetSprite(Sprite cardSprite)
    {
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

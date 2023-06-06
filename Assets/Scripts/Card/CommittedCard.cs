using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this class only shows the card that has been set up and will be compared to other hands, the comparation logic itself will still going on in Hand class
/// </summary>
public class CommittedCard : MonoBehaviour
{
    private Sprite cardSprite;
    [SerializeField] private Image cardImage;

    public void SetCard(Card card)
    {
        cardSprite = card.cardView.GetCardSprite();
        Reveal();
    }

    public void Reveal()
    {
        cardImage.sprite = cardSprite;
    }
}

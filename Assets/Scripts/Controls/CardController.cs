using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private Card pickedCard;

    public void PickCard(Card card)
    {
        if (pickedCard != null)
        {
            if(pickedCard == card)
            {
                Debug.Log("same card repicked");
                card.cardView.TogglePicked(false);
                pickedCard = null;
            }
            else
            {
                card.SwapCard(pickedCard);
                pickedCard.cardView.TogglePicked(false);
                card.cardView.TogglePicked(false);
                pickedCard = null;
            }
        }
        else
        {
            card.cardView.TogglePicked(true);
            pickedCard = card;
        }
    }
}

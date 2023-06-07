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
                card.CardView.TogglePicked(false);
                pickedCard = null;
            }
            else
            {
                card.SwapCard(pickedCard);
                pickedCard.CardView.TogglePicked(false);
                card.CardView.TogglePicked(false);
                pickedCard = null;
            }
        }
        else
        {
            card.CardView.TogglePicked(true);
            pickedCard = card;
        }
    }
}

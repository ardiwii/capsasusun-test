using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> cardsInDeck;
    private Shuffler shuffler;

    public void InitDeck(CardPool cardPool)
    {
        shuffler = new Shuffler();
        cardsInDeck = new List<Card>();
        List<Card> unshuffledCards = cardPool.PopAllPooledCards();
        List<int> shuffledOrder = shuffler.ShuffleCard(unshuffledCards.Count);
        for (int i = 0; i < unshuffledCards.Count; i++)
        {
            cardsInDeck.Add(unshuffledCards[shuffledOrder[i]]);
            unshuffledCards[shuffledOrder[i]].gameObject.transform.SetParent(transform);
        }
    }

    public void ReshuffleDeck()
    {
        List<Card> unshuffledCards = new List<Card>(cardsInDeck);
        cardsInDeck.Clear();
        List<int> shuffledOrder = shuffler.ShuffleCard(unshuffledCards.Count);
        for (int i = 0; i < unshuffledCards.Count; i++)
        {
            cardsInDeck.Add(unshuffledCards[shuffledOrder[i]]);
        }
    }

    /// <summary>
    /// pop the card from deck, return the card on top of the deck and remove it from the deck
    /// </summary>
    /// <returns></returns>
    public Card DrawCard()
    {
        Card retval = null;
        if (cardsInDeck.Count > 0)
        {
            retval = cardsInDeck[0];
            cardsInDeck.RemoveAt(0);
        }
        return retval;
    }
}

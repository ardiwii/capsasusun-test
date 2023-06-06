using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private CardDatabaseSO cardDatabase;
    private List<GameObject> pooledCards;

    public void InitPool()
    {
        pooledCards = new List<GameObject>();
        for (int i = 0; i < cardDatabase.cards.Count; i++)
        {
            GameObject newCard = Instantiate(cardPrefab.gameObject, transform);
            newCard.name = cardDatabase.cards[i].cardImage.name;
            newCard.GetComponent<Card>().SetCard(cardDatabase.cards[i], this);
            newCard.SetActive(false);
            pooledCards.Add(newCard);
        }
    }

    public List<Card> PopAllPooledCards()
    {
        List<Card> poppedCards = new List<Card>();
        for (int i = pooledCards.Count-1; i>=0; i--)
        {
            poppedCards.Add(pooledCards[i].GetComponent<Card>());
            pooledCards.RemoveAt(i);
        }
        return poppedCards;
    }

    public void ReturnCardToPool(Card returnedCard)
    {
        returnedCard.gameObject.transform.SetParent(transform);
        pooledCards.Add(returnedCard.gameObject);
        returnedCard.gameObject.SetActive(false);
    }
}

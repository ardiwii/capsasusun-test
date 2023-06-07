using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHand : Hand
{
    public override void DrawFirstHand(Deck deck)
    {
        cardsInHand = new List<Card>();
        topSet.InitSet();
        midSet.InitSet();
        bottomSet.InitSet();
        for (int i = 0; i < 13; i++)
        {
            Card drawnCard = deck.DrawCard();
            cardsInHand.Add(drawnCard);
        }

        Debug.Log(gameObject.name);
        DetermineHand();
    }

    private void DetermineHand()
    {
        //determine the best set to use for bottom set
        List<Card> remainingCards = new List<Card>(cardsInHand);

        remainingCards = DetermineSet(remainingCards, bottomSet, 5);
        bottomSet.EvaluateSet();
        remainingCards = DetermineSet(remainingCards, midSet, 5);
        midSet.EvaluateSet();
        DetermineSet(remainingCards, topSet, 3);
        topSet.EvaluateSet();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="setToFill"></param>
    /// <param name="setSize"></param>
    /// <returns></returns>
    private List<Card> DetermineSet(List<Card> cards, CardSet setToFill, int setSize)
    {
        List<Card> remainingCards = new List<Card>(cards);

        CardComparer comparer = new CardComparer();
        comparer.compareField = CardComparer.SortBy.Value;
        remainingCards.Sort(comparer);

        List<Card> bestSet = GetBestSet(remainingCards);
        if (bestSet != null)
        {
            for (int i = 0; i < bestSet.Count; i++)
            {
                setToFill.AddCardToSet(bestSet[i]);
                remainingCards.Remove(bestSet[i]);
            }
            if (bestSet.Count < setSize && cards.Count >= setSize) //the set is not a 5 card set (4 of a kind, 3 of a kind, two pair, or pair), fill it with the lowest value cards
            {
                for (int i = bestSet.Count; i < setSize; i++)
                {
                    setToFill.AddCardToSet(remainingCards[0]);
                    remainingCards.RemoveAt(0);
                }
            }
        }
        else //no set exist, make high card set
        {
            setToFill.AddCardToSet(remainingCards[remainingCards.Count - 1]);
            remainingCards.RemoveAt(remainingCards.Count - 1);
            for (int i = 1; i < setSize; i++)
            {
                setToFill.AddCardToSet(remainingCards[0]);
                remainingCards.RemoveAt(0);
            }
        }
        return remainingCards;
    }

    private List<Card> GetBestSet(List<Card> cards)
    {
        //check for straight flush and flush
        CardComparer comparer = new CardComparer();
        List<Card> suiteSortedCards = new List<Card>(cards);

        comparer.compareField = CardComparer.SortBy.Suite;
        suiteSortedCards.Sort(comparer);

        List<Card> bestSet = GetBestStraight(suiteSortedCards);
        if (bestSet != null)
        {
            return bestSet;
        }

        List<Card> valueSortedCards = new List<Card>(cards);
        comparer.compareField = CardComparer.SortBy.Value;
        valueSortedCards.Sort(comparer);

        List <Card> bestSameCardSet = GetBestSameCardSet(valueSortedCards);
        if (bestSameCardSet != null && bestSameCardSet.Count == 5)
        {
            return bestSameCardSet;
        }

        bestSet = GetBestFlush(suiteSortedCards);
        if (bestSet != null)
        {
            return bestSet;
        }

        bestSet = GetBestStraight(valueSortedCards);
        if (bestSet != null)
        {
            return bestSet;
        }

        if (bestSameCardSet.Count > 0)
        {
            return bestSameCardSet;
        }
        else return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cards">list of cards, ordered by suite</param>
    /// <returns></returns>
    private List<Card> GetBestFlush(List<Card> cards)
    {
        List<Card> bestFlushCards = null;
        List<Card> flushCards = new List<Card>();
        flushCards.Add(cards[0]);
        for (int i = 1; i < cards.Count; i++)
        {
            if (flushCards[flushCards.Count - 1].CardValue.suite == cards[i].CardValue.suite)
            {
                flushCards.Add(cards[i]);

                if(flushCards.Count >= 5)
                {
                    if (bestFlushCards == null)
                    {
                        bestFlushCards = new List<Card>(flushCards);
                    }
                    else
                    {
                        if (flushCards.Count > 5)
                        {
                            flushCards.RemoveAt(0);
                        }
                        if (flushCards[flushCards.Count-1].CardValue.CompareTo(bestFlushCards[bestFlushCards.Count-1].CardValue) > 0)
                        {
                            bestFlushCards = new List<Card>(flushCards);
                        }
                    }
                }
            }
            else
            {
                flushCards.Clear();
                flushCards.Add(cards[i]);
            }
        }
        return bestFlushCards;
    }
    
    /// <summary>
    /// get the best set of four of a kind, full house, three of a kinds, two pair, or pair
    /// </summary>
    /// <param name="cards">list of cards, ordered by value</param>
    /// <returns>sets maybe returned with less than 5 cards</returns>
    private List<Card> GetBestSameCardSet(List<Card> cards)
    {
        List<Card> bestSet = new List<Card>();
        List<List<Card>> sameCardSets = new List<List<Card>>();
        List<Card> lastSameCard = new List<Card>();
        lastSameCard.Add(cards[0]);
        for (int i = 1; i < cards.Count; i++)
        {
            if(cards[i].CardValue.numberValue == lastSameCard[0].CardValue.numberValue)
            {
                lastSameCard.Add(cards[i]);
                if(i == cards.Count - 1 && lastSameCard.Count >= 2)
                {
                    sameCardSets.Add(lastSameCard);
                }
            }
            else
            {
                if (lastSameCard.Count >= 2) {
                    sameCardSets.Add(lastSameCard);
                }
                lastSameCard = new List<Card>();
                lastSameCard.Add(cards[i]);
            }
        }
        //look for four of a kind
        for (int i = 0; i < sameCardSets.Count; i++)
        {
            if(sameCardSets[i].Count == 4)
            {
                if (bestSet.Count > 0)
                {
                    bestSet.Clear();
                }
                bestSet.AddRange(sameCardSets[i]);
                for (int j = 0; j < cards.Count; j++)
                {
                    if(cards[j].CardValue.numberValue != bestSet[0].CardValue.numberValue) //add the filler card
                    {
                        bestSet.Insert(0, cards[j]);
                        break;
                    }
                }
            }
        }
        if (bestSet.Count > 0) return bestSet;
        //look for full house or three of a kind
        for (int i = 0; i < sameCardSets.Count; i++)
        {
            if (sameCardSets[i].Count == 3)
            {
                if (bestSet.Count > 0)
                {
                    bestSet.Clear();
                }
                bestSet.AddRange(sameCardSets[i]);
                //check if there is another pair that can be used as filler, if not then it's only three of a kind
                if (sameCardSets.Count > 1) 
                {
                    //add filler pair, search from the lowest value pair, there is a chance that the filler pair can only be get from a three of a kind
                    for (int j = 0; j < sameCardSets.Count; j++)
                    {
                        if (sameCardSets[j].Count >= 2 && i != j)
                        {
                            for (int k = 0; k < 2; k++)
                            {
                                bestSet.Insert(0, sameCardSets[j][k]);
                            }
                            break;
                        }
                    }
                }
            }
        }
        if (bestSet.Count > 0) return bestSet;
        //look for two pair or pair
        if(sameCardSets.Count > 1)
        {
            Debug.Log("two pair, high pair: " + sameCardSets[sameCardSets.Count - 1][0].CardValue.numberValue);
            //add the best pair and the lowest one, best pair is the one at the last index
            bestSet.AddRange(sameCardSets[0]);
            bestSet.AddRange(sameCardSets[sameCardSets.Count - 1]);
        }
        else if(sameCardSets.Count == 1)
        {
            bestSet.AddRange(sameCardSets[0]);
        }
        return bestSet;
    }

    /// <summary>
    /// get a list of card that is the highest straight set from a bigger list of cards
    /// </summary>
    /// <param name="cards">list of cards, if ordered by suite, this will return a straight flush set, if not the a regular straight</param>
    /// <returns>will return null if no straight is found</returns>
    private List<Card> GetBestStraight(List<Card> cards)
    {
        List<Card> straightCards = new List<Card>();
        straightCards.Add(cards[0]);
        for (int i = 1; i < cards.Count; i++)
        {
            if (straightCards[straightCards.Count - 1].IsOneBefore(cards[i]))
            {
                straightCards.Add(cards[i]);
            }
            else if(cards[i].CardValue.numberValue == straightCards[straightCards.Count - 1].CardValue.numberValue)
            {
                //do nothing
            }
            else
            {
                straightCards.Clear();
                straightCards.Add(cards[i]);
            }
        }
        if(straightCards.Count > 5) //trim the lower number
        {
            straightCards.RemoveRange(0, straightCards.Count - 5);
        }
        if (straightCards.Count == 5)
        {
            return straightCards;
        }
        else
        {
            return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffler
{
    public List<int> ShuffleCard(int deckSize)
    {
        List<int> shuffledOrder = new List<int>();

        for (int i = 0; i < deckSize; i++)
        {
            shuffledOrder.Add(i);
        }

        for (int i = 0; i < shuffledOrder.Count; i++)
        {
            // Random for remaining positions.
            int r = i + (Random.Range(0, shuffledOrder.Count - i));

            int temp = shuffledOrder[i];
            shuffledOrder[i] = shuffledOrder[r];
            shuffledOrder[r] = temp;
        }

        return shuffledOrder;
    }
}

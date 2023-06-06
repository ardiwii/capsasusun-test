using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataGenerator : MonoBehaviour
{
    public CardDatabaseSO cardDatabase;

    private void Start()
    {
        for (int i = 0; i < cardDatabase.cards.Count; i++)
        {
            string cardImageName = cardDatabase.cards[i].cardImage.name;
            string suiteName = cardImageName.Substring(0, cardImageName.Length - 2);
            string numberValue = cardImageName.Substring(cardImageName.Length - 2, 2);

            SuiteEnum suite = SuiteEnum.Club;
            switch(suiteName)
            {
                case "Diamond": suite = SuiteEnum.Diamond; break;
                case "Club": suite = SuiteEnum.Club; break;
                case "Heart": suite = SuiteEnum.Heart; break;
                case "Spade": suite = SuiteEnum.Spade; break;
            }

            int intNumberValue = int.Parse(numberValue);
            CardData cardData = new CardData() { cardImage = cardDatabase.cards[i].cardImage, value = new CardValue() { suite = suite, numberValue = intNumberValue } };
            cardDatabase.cards[i] = cardData;
        } 
    }
}

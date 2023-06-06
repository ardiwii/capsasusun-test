using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CardController cardController;
    [SerializeField] private CardPool cardPool;
    [SerializeField] private Deck deck;
    [SerializeField] private AvatarSelection avatarSelection;
    [SerializeField] private Player player;
    [SerializeField] private List<Player> opponents;

    [SerializeField] GameObject avatarSelectUI;
    [SerializeField] GameObject PlayerSetupHandUI;
    [SerializeField] GameObject GameBoardUI;

    public static GameManager instance;

    private void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }

    // Start is called before the first frame update
    void StartGame()
    {
        cardPool.InitPool();
        deck.InitDeck(cardPool);
        PlayerSetupHandUI.SetActive(true);
        player.Hand.DrawFirstHand(deck);
        for (int i = 0; i < opponents.Count; i++)
        {
            opponents[i].Hand.DrawFirstHand(deck);
        }
    }

    public void ConfirmCardSetup()
    {
        player.FinalizeHand();
        for (int i = 0; i < opponents.Count; i++)
        {
            opponents[i].FinalizeHand();
        }
        PlayerSetupHandUI.SetActive(false);
        GameBoardUI.SetActive(true);
        ComparePlayerHands();
    }

    public void ComparePlayerHands()
    {
        player.CompareHandAndScoreRound(opponents);

        List<Player> allPlayers = new List<Player>(opponents);
        allPlayers.Add(player);

        for (int i = 0; i < opponents.Count; i++)
        {
            allPlayers.Remove(opponents[i]);
            opponents[i].CompareHandAndScoreRound(allPlayers);
            allPlayers.Add(opponents[i]);
        }
    }

    public void NextRound()
    {
        GameBoardUI.SetActive(false);
        player.Hand.ReturnCards();
        for (int i = 0; i < opponents.Count; i++)
        {
            opponents[i].Hand.ReturnCards();
        }
        PlayerSetupHandUI.SetActive(true);
        deck.InitDeck(cardPool);
        player.Hand.DrawFirstHand(deck);
        for (int i = 0; i < opponents.Count; i++)
        {
            opponents[i].Hand.DrawFirstHand(deck);
        }
    }

    public void SetPlayerAvatar(int avatarIdx)
    {
        avatarSelection.SetPlayerAvatar(avatarIdx, player, opponents);
        avatarSelectUI.SetActive(false);
        StartGame();
    }
}

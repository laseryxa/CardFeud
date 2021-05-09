using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public Card cardPrefab;
    [SerializeField] public HandLayout handlayoutPrefab;
    [SerializeField] public Deck deckPrefab;
    [SerializeField] public ActivatedCardsArea playerActivatedCardsAreaPrefab;
    [SerializeField] public Player playerPrefab;

    public HandLayout playerHand;
    public Deck playerDeck;
    public ActivatedCardsArea playerActivatedCardsArea;
    public Player player;

    public int turn;

    void Awake()
    {
        turn = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GlobalState.selectedPlayer) {
            player = Instantiate(GlobalState.selectedPlayer, transform);
        } else {
            Debug.Log("Since we didn't choose a character, use default prefab for testing purposes.");
            player = Instantiate(playerPrefab, transform);
        }

        playerActivatedCardsArea = Instantiate(playerActivatedCardsAreaPrefab, transform);
        playerHand = Instantiate(handlayoutPrefab, transform);
        playerDeck = Instantiate(deckPrefab, transform);                

        playerActivatedCardsArea.owningPlayer = player;

        RectTransform rectTransform = player.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(400, -200, 0);
        playerDeck.CardPrefab = cardPrefab;
        playerHand.deck = playerDeck;

        playerDeck.AddCard("Best card", 0, 10, 10);
        playerDeck.AddCard("Silly card", 3, 0, 3);
        playerDeck.AddCard("Not so good card", 7, 1, 1);
        for (int i = 0; i < 100; i++) {
            playerDeck.AddCard("Card " + i.ToString(), i % 10, i % 5 + 1, i % 7);
        }

        DrawCardsFromDeck(5);
    }

    void DrawCardsFromDeck(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Card currentCard = playerDeck.drawCard();
            playerHand.AddCard(currentCard);
        }
    }

    public void EndTurn()
    {
        Debug.Log("End turn");
        turn += 1;
        DrawCardsFromDeck(1);
        player.AddGold(turn);
    }

    // Update is called once per frame
    void Update()
    {
    }
}

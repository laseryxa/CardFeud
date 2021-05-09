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
    int step;

    // Start is called before the first frame update
    void Start()
    {
        step = 0;

        playerActivatedCardsArea = Instantiate(playerActivatedCardsAreaPrefab, transform);
        playerHand = Instantiate(handlayoutPrefab, transform);
        playerDeck = Instantiate(deckPrefab, transform);                

        //player = Instantiate(playerPrefab, transform);
        player = Instantiate(GlobalState.selectedPlayer, transform);
        //player = GlobalState.selectedPlayer;

        playerActivatedCardsArea.owningPlayer = player;

        RectTransform rectTransform = player.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(400, -200, 0);
        playerDeck.CardPrefab = cardPrefab;
        playerHand.deck = playerDeck;
    }

    void DrawCardsFromDeck(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Card currentCard = playerDeck.drawCard();
            playerHand.AddCard(currentCard);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (step == 0) {
            playerDeck.AddCard("Best card", 0, 10, 10);
            playerDeck.AddCard("Silly card", 3, 0, 3);
            playerDeck.AddCard("Not so good card", 7, 1, 1);
            for (int i = 0; i < 100; i++) {
                playerDeck.AddCard("Card " + i.ToString(), i % 10, i % 5 + 1, i % 7);
            }

            DrawCardsFromDeck(5);
        }
        step = step + 1;
    }
}

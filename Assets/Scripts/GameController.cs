using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public Card cardPrefab;
    [SerializeField] public HandLayout handlayoutPrefab;
    [SerializeField] public Deck deckPrefab;

    [SerializeField] public ActivatedCardsArea playerActivatedCardsAreaPrefab;

    public HandLayout playerHand;
    public Deck playerDeck;

    public ActivatedCardsArea playerActivatedCardsArea;
    int step;

    // Start is called before the first frame update
    void Start()
    {
        step = 0;

        playerActivatedCardsArea = Instantiate(playerActivatedCardsAreaPrefab, transform);
        playerHand = Instantiate(handlayoutPrefab, transform);
        playerDeck = Instantiate(deckPrefab, transform);
        playerDeck.CardPrefab = cardPrefab;
        playerHand.deck = playerDeck;
    }

    // Update is called once per frame
    void Update()
    {
        if (step == 0) {
            playerDeck.AddCard("Best card");
            playerDeck.AddCard("Silly card");
            playerDeck.AddCard("Not so good card");

            Card currentCard = playerDeck.drawCard();
            playerHand.AddCard(currentCard);

            currentCard = playerDeck.drawCard();
            playerHand.AddCard(currentCard);

            currentCard = playerDeck.drawCard();
            playerHand.AddCard(currentCard);
        }
        step = step + 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public Card cardPrefab;
    [SerializeField] public HandLayout handlayoutPrefab;

    [SerializeField] public Deck deckPrefab;

    public HandLayout playerHand;
    public Deck playerDeck;

    int step;

    // Start is called before the first frame update
    void Start()
    {
        step = 0;

        playerDeck = Instantiate(deckPrefab, transform);
        playerDeck.CardPrefab = cardPrefab;

        playerHand = Instantiate(handlayoutPrefab, transform);
        playerHand.deck = playerDeck;
    }

    // Update is called once per frame
    void Update()
    {
        if (step == 0) {
            playerDeck.AddCard("Best card");
            Card currentCard = playerDeck.drawCard();
            playerHand.AddCard(currentCard);

            playerDeck.AddCard("Silly card");
            currentCard = playerDeck.drawCard();
            playerHand.AddCard(currentCard);

            playerDeck.AddCard("Not so good card");
            currentCard = playerDeck.drawCard();
            playerHand.AddCard(currentCard);
        }
        step = step + 1;
    }
}

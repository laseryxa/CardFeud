using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Ability
{
    private string name;
    public Ability(string name)
    {
        this.name = name;
    }
}

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

    public HandLayout opponentHand;
    public Deck opponentDeck;
    public ActivatedCardsArea opponentActivatedCardsArea;
    public Player opponent;

    public int turn;

    public List<Ability> abilities;

    public Player currentPlayer;

    public void AddAbility(string name) {
        abilities.Add(new Ability(name));
    }

    void Awake()
    {
        turn = 1;
    }

    void CreateStartDeck(Deck deck)
    {
        deck.AddCard("Dogdas", 6, 3, 7); // beast
        deck.AddCard("Wolf", 2, 2, 2); // beast
        deck.AddCard("Spirit Wolf", 5, 3, 4); // beast
        deck.AddCard("Sharp Tooth", 7, 3, 4); // beast
        deck.AddCard("Uldrurth", 8, 4, 6); // dragon
        deck.AddCard("Rhinium", 9, 2, 12); // beast
        deck.AddCard("Rexossum", 7, 6, 9); // beast
        deck.AddCard("Aarbrok", 3, 2, 4); // beast
        deck.AddCard("Aluxious", 2, 3, 2); // beast
        deck.AddCard("Gereon", 7, 6, 8); // dragon
        deck.AddCard("Best card", 0, 10, 10);
        deck.AddCard("Silly card", 3, 0, 3);
        deck.AddCard("Not so good card", 7, 1, 1);
        for (int i = 0; i < 100; i++) {
            deck.AddCard("Card " + i.ToString(), i % 10, i % 5 + 1, i % 7);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        const int cardsAtFirstDraw = 3;

        if (GlobalState.selectedPlayer) {
            player = Instantiate(GlobalState.selectedPlayer, transform);
        } else {
            Debug.Log("Since we didn't choose a character, use default prefab for testing purposes.");
            player = Instantiate(playerPrefab, transform);
        }

        currentPlayer = player;

        playerActivatedCardsArea = Instantiate(playerActivatedCardsAreaPrefab, transform);
        RectTransform rectTransform = playerActivatedCardsArea.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0, -50, 0);

        playerHand = Instantiate(handlayoutPrefab, transform);
        rectTransform = playerHand.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0, -280, 0);

        playerDeck = Instantiate(deckPrefab, transform);                
        playerDeck.owner = player;

        playerActivatedCardsArea.owningPlayer = player;
        rectTransform = player.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(400, -200, 0);
        playerDeck.CardPrefab = cardPrefab;
        playerHand.deck = playerDeck;

        CreateStartDeck(playerHand.deck);
        DrawCardsFromDeck(playerHand, cardsAtFirstDraw);

        opponentActivatedCardsArea.owningPlayer = opponent;
        opponentHand.deck = opponentDeck;
        opponentDeck.owner = opponent;
        CreateStartDeck(opponentHand.deck);
        DrawCardsFromDeck(opponentHand, cardsAtFirstDraw);

    }

    void DrawCardsFromDeck(HandLayout hand, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Card currentCard = hand.deck.drawCard();
            hand.AddCard(currentCard);
        }
    }

    public void EndTurn()
    {
        Debug.Log("End turn");
        playerActivatedCardsArea.Tick();
        opponentActivatedCardsArea.Tick();
        if (currentPlayer == player) {
            turn += 1;
            currentPlayer = opponent;
            DrawCardsFromDeck(opponentHand, 1);
            opponent.AddGold(turn);
        } else {
            currentPlayer = player;
            DrawCardsFromDeck(playerHand, 1);
            player.AddGold(turn);
        }
    }

    // Return true if a card was played
    bool TryPlayMostExpensiveCard()
    {
        int mostExpensiveCardIndex = -1;
        int mostExpensiveCardCost = -1;
        
        for (int i = 0; i < opponentHand.transform.childCount; i++)
        {
            GameObject cardObject = opponentHand.transform.GetChild(i).gameObject;
            Card card = cardObject.GetComponent<Card>();
            if (card.GetCost() > mostExpensiveCardCost && card.GetCost() <= opponent.GetGold()) 
            {
                mostExpensiveCardCost = card.GetCost();
                mostExpensiveCardIndex = i;
            }            
        }

        if (mostExpensiveCardIndex >= 0)
        {
            GameObject cardObject = opponentHand.transform.GetChild(mostExpensiveCardIndex).gameObject;
            Card card = cardObject.GetComponent<Card>();
            Debug.Log("Want to play " + card.GetLabel());
            opponentActivatedCardsArea.PlayCard(cardObject);
        }

        return mostExpensiveCardCost >= 0;
    }

    bool TryAttackPlayerCards()
    {
        bool hasAttackedCard = false;
        if (playerActivatedCardsArea.transform.childCount > 0)
        {
            for (int i = 0; i < opponentActivatedCardsArea.transform.childCount; i++)
            {
                GameObject cardObject = opponentActivatedCardsArea.transform.GetChild(i).gameObject;
                Card card = cardObject.GetComponent<Card>();
                if (!card.hasStatus(Card.Status.Activated) && !card.hasStatus(Card.Status.Sleeping))
                {
                    GameObject playerCardObject = playerActivatedCardsArea.transform.GetChild(0).gameObject;
                    Card playerCard = playerCardObject.GetComponent<Card>();
                    hasAttackedCard = true;
                    Debug.Log("Attacking " + playerCard.GetLabel() + " with " + card.GetLabel());
                    card.AttackCard(playerCard);
                }
            }
        }

        return hasAttackedCard;
    }

    bool TryAttackPlayer()
    {
        bool hasAttackedPlayer = false;
        if (playerActivatedCardsArea.transform.childCount == 0)
        {
            for (int i = 0; i < opponentActivatedCardsArea.transform.childCount; i++)
            {
                GameObject cardObject = opponentActivatedCardsArea.transform.GetChild(i).gameObject;
                Card card = cardObject.GetComponent<Card>();
                if (!card.hasStatus(Card.Status.Activated) && !card.hasStatus(Card.Status.Sleeping))
                {
                    hasAttackedPlayer = true;
                    Debug.Log("Attacking player with " + card.GetLabel());
                    player.AttackedByCard(card);
                }

            }
        }
        
        return hasAttackedPlayer;
    }

    void LoadMainMenu()
    {
        DOTween.KillAll(true);            
        SceneManager.LoadScene("MainMenu");
    }

    // Check if anyone has won
    void CheckVictory()
    {
        if (player.GetHealth() <= 0)
        {
            LoadMainMenu();
        }

        if (opponent.GetHealth() <= 0)
        {
            LoadMainMenu();
        }
    }

    void UpdateAIPlayer()
    {
        // First try playing all cards, most expensive first, until we don't have enough gold.
        if (!TryPlayMostExpensiveCard())
        {
            // Then try to attack other players cards until all cards have attacked, or there 
            // are no more cards to attack.
            if (!TryAttackPlayerCards())
            {
                // Then try to attack the other player
                if (!TryAttackPlayer())
                {
                    EndTurn();
                }
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer == opponent)
        {
            UpdateAIPlayer();
        }

        // Check if any player is dead, if so, return to main menu for now.
        CheckVictory();
    }
}

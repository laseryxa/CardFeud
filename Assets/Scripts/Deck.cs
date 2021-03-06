using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [System.Serializable]
    public class CardData
    {
        public string name;
        public int cost;
        public int attack;
        public int defence;
        public CardData(string name, int cost, int attack, int defence)
        {
            this.name = name;
            this.cost = cost;
            this.attack = attack;
            this.defence = defence;
        }
    };  
    
    [SerializeField]
    public Card CardPrefab;
    public List<CardData> cards; 
    public Player owner;
    
    void Awake()
    {
        cards = new List<CardData>();
    }

    public void AddCard(string name, int cost, int attack, int defence)
    {        
        cards.Add(new CardData(name, cost, attack, defence));
    }

    public Card drawCard()
    {
        if (cards.Count > 0) {
            CardData c = cards[0];
            cards.RemoveAt(0);
            Debug.Log("Card drawn was " + c.name);
            Card card = Instantiate(CardPrefab, transform);
            var cardComponent = card.GetComponent<Card>();
            cardComponent.SetLabel("<color=#005500>" + c.name);
            cardComponent.SetCost(c.cost);
            cardComponent.SetAttack(c.attack);
            cardComponent.SetDefence(c.defence);
            cardComponent.owner = owner;
            cardComponent.addStatus(Card.Status.InHand);

            return card;
        } else {
            Debug.Log("Out of cards in deck!");
            return null;
        }
    }
}

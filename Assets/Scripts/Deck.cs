using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
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
    List<CardData> cards;

    // Start is called before the first frame update
    void Start()
    {
        cards = new List<CardData>();
    }

    public void AddCard(string name)
    {        
        cards.Add(new CardData(name, 1, 1, 1));
    }

    public Card drawCard()
    {
        if (cards.Count > 0) {
            CardData c = cards[0];
            cards.RemoveAt(0);
            Card card = Instantiate(CardPrefab, transform);
            var cardComponent = card.GetComponent<Card>();
            cardComponent.SetLabel(c.name);

            return card;
        } else {
            return null;
        }
    }
}

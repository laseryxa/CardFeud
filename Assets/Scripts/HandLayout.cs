using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLayout : MonoBehaviour
{
    [SerializeField] public Deck deck;

    List<Card> cards;
    int step;

    private void Awake()
    {
        cards = new List<Card>();
        step = 0;
    }

    public void AddCard(Card card)
    {
        card.transform.SetParent(this.transform);
        cards.Add(card);
    }

    public void RemoveCard(Card card)
    {

    }

    public void Rearrange()    
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(130 * i, 100, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (step == 0) {
            Rearrange();
        }
        step = step + 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandLayout : MonoBehaviour, IDropHandler
{
    [SerializeField] public Deck deck;

    int step;

    private void Awake()
    {
        step = 0;
    }

    public void AddCard(Card card)
    {
        card.transform.SetParent(this.transform);

        Rearrange();
    }

    public void RemoveCard(Card card)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Card card = eventData.pointerDrag.GetComponent<Card>();
              
            //card.transform.SetParent(this.transform);           
            AddCard(card);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("Dropped " + card.GetLabel());

            card.DropAccepted();

            Rearrange();
        }
    }

    public void Rearrange()    
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject cardObject = transform.GetChild(i).gameObject;
            //Card card = cardObject.GetComponent<Card>();
            cardObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(- transform.childCount * 130 / 2 + 130 * i, 0, 0);
        }
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

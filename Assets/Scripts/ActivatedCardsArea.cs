using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivatedCardsArea : MonoBehaviour, IDropHandler
{   
    [SerializeField] public Player owningPlayer;


    public void PlayCard(GameObject cardObject)
    {
        cardObject.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        Card card = cardObject.GetComponent<Card>();
        Debug.Log("Played " + card.GetLabel());
        card.transform.SetParent(this.transform);
        card.DropAccepted();
        Rearrange();
        owningPlayer.AddGold(-card.GetCost());
    }

    private bool CardCanBeDraggedByPlayer(Card card)
    {
        return card.owner == owningPlayer;
    }

    private bool CardIsAlreadyInActivatedCardsArea(Card card)
    {
        return card.transform.parent == this.transform;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Card card = eventData.pointerDrag.GetComponent<Card>();

            if (CardCanBeDraggedByPlayer(card))
            {
                if (!CardIsAlreadyInActivatedCardsArea(card)) 
                {
                    Debug.Log("Card cost is " + card.GetCost().ToString() + " and player has " + owningPlayer.GetGold().ToString());
                    if (card.GetCost() <= owningPlayer.GetGold())
                    {
                        PlayCard(eventData.pointerDrag);
                    } else {
                        Debug.Log(card.GetLabel() + " is to expensive!");
                    }
                }
            } else {
                Debug.Log("Tried to drop card in opponents area!");
            }
        }
    }

    public void Tick()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject cardObject = transform.GetChild(i).gameObject;
            cardObject.GetComponent<Card>().removeStatus(Card.Status.Sleeping);
            cardObject.GetComponent<Card>().removeStatus(Card.Status.Activated);
        }        
    }

    private void Rearrange()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject cardObject = transform.GetChild(i).gameObject;
            cardObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-transform.childCount * 130 / 2 + 130 * i, 0, 0);
        }
    }
}

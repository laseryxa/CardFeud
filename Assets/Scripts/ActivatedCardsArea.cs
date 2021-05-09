using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivatedCardsArea : MonoBehaviour, IDropHandler
{   
    [SerializeField] public Player owningPlayer;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Card card = eventData.pointerDrag.GetComponent<Card>();
            Debug.Log("Card cost is " + card.GetCost().ToString() + " and player has " + owningPlayer.GetGold().ToString());
            if (card.GetCost() <= owningPlayer.GetGold())
            {
                Debug.Log("Dropped " + card.GetLabel());
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                card.transform.SetParent(this.transform);
                card.DropAccepted();
                Rearrange();
                owningPlayer.AddGold(-card.GetCost());
            } else {
                Debug.Log(card.GetLabel() + " is to expensive!");
            }
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

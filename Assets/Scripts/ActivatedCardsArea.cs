using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivatedCardsArea : MonoBehaviour, IDropHandler
{   
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Card card = eventData.pointerDrag.GetComponent<Card>();
            card.transform.SetParent(this.transform);
            
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("Dropped " + card.GetLabel());
            card.DropAccepted();
            Rearrange();
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

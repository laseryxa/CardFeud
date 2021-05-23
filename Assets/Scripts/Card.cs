using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Card : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public enum Status {
        Activated,
        Sleeping,
        Frozen,
        Burning,
        Haste,
        Taunt
    };
    [SerializeField] TextMeshProUGUI Label;
    [SerializeField] TextMeshProUGUI Cost;
    [SerializeField] TextMeshProUGUI Attack;
    [SerializeField] TextMeshProUGUI Defence;
    [SerializeField] Image sleepingImage;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private HandLayout startHand;
    private bool isDragged;
    private bool dropAccepted;
    public List<Status> statuses;
    public Player owner;
    public void addStatus(Status status)
    {
        if (! hasStatus(status)) {
            statuses.Add(status);
        }
    }
    public void removeStatus(Status status)
    {
        statuses.RemoveAll(itemStatus => itemStatus == status);
    }
    public bool hasStatus(Status status)
    {
        for (int i = 0; i < statuses.Count; i++) {
            if (statuses[i] == status) return true;
        }
        return false;
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        isDragged = false;
        dropAccepted = false;
    }

    public void DropAccepted()
    {
        dropAccepted = true;
        addStatus(Status.Sleeping);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down on '" + Label.text.ToString() + "'");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down on '" + Label.text.ToString() + "'");
        rectTransform.DOComplete();
        rectTransform.DOScale(Vector3.one * 1.5f, 0.3f).SetEase(Ease.OutQuad);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down on '" + Label.text.ToString() + "'");
        if (!isDragged) {
            rectTransform.DOComplete();
            rectTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (hasStatus(Status.Sleeping) || hasStatus(Status.Activated))
        {
            eventData.pointerDrag = null;                        
            return;
        }
        dropAccepted = false;
        //Debug.Log("Begin drag '" + Label.text.ToString() + "'");
        startPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
        isDragged = true;

        HandLayout hand = this.transform.parent.GetComponent<HandLayout>();
        if (hand)
        {
            //Debug.Log("Calling rearrange when started dragging");
            //hand.Rearrange();
            startHand = hand;
        }

        rectTransform.DOComplete();
        rectTransform.DOScale(Vector3.one * 1.5f, 0.3f).SetEase(Ease.OutQuad);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End drag '" + Label.text.ToString() + "'");
        if (!dropAccepted) {
            rectTransform.anchoredPosition = startPosition;
        }
        canvasGroup.alpha = 1.0f;
        isDragged = false;
        canvasGroup.blocksRaycasts = true;
        rectTransform.DOComplete();
        rectTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad);
        if (startHand) {
            startHand.Rearrange();
            Debug.Log("Calling rearrange hand");
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void AttackCard(Card cardToAttack)
    {
        this.SetDefence(GetDefence() - cardToAttack.GetAttack());
        cardToAttack.SetDefence(cardToAttack.GetDefence() - GetAttack());
        this.addStatus(Status.Activated);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Card card = eventData.pointerDrag.GetComponent<Card>();
        if (card.owner == owner) {
            Debug.Log("Trying to stack own cards, just ignore");
        } else {
            Debug.Log("Fight!");    
            SetDefence(GetDefence() - card.GetAttack());
            card.SetDefence(card.GetDefence() - GetAttack());
            card.addStatus(Status.Activated);
        }
    }

    public void SetLabel(string text)
    {
        Label.text = text;
    }

    public string GetLabel()
    {
        return Label.text;
    }
    public void SetCost(int cost)
    {
        Cost.text = cost.ToString();
    }

    public int GetCost()
    {
        return int.Parse(Cost.text);
    }
    public void SetAttack(int attack)
    {
        Attack.text = attack.ToString();
    }

    public int GetAttack()
    {
        return int.Parse(Attack.text);
    }
    public void SetDefence(int defence)
    {
        Defence.text = defence.ToString();
    }

    public int GetDefence()
    {
        return int.Parse(Defence.text);
    }

    private void Die()
    {
        rectTransform.DOComplete();
        Destroy(this.gameObject);
    }

    public void Update()
    {
        if (sleepingImage)
        {
            sleepingImage.enabled = hasStatus(Status.Sleeping);
        }
        if (GetDefence() <= 0)
        {
            Die();    
        }
    }

}

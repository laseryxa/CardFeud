using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Card : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] TextMeshProUGUI Label;
    [SerializeField] TextMeshProUGUI Cost;
    [SerializeField] TextMeshProUGUI Attack;
    [SerializeField] TextMeshProUGUI Defence;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private bool isDragged;
    private bool dropAccepted;
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
        dropAccepted = false;
        //Debug.Log("Begin drag '" + Label.text.ToString() + "'");
        startPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
        isDragged = true;
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
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Card : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    TextMeshProUGUI Label;

    private RectTransform rectTransform;
    private Vector3 startPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down on '" + Label.text.ToString() + "'");
        startPosition = rectTransform.anchoredPosition;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down on '" + Label.text.ToString() + "'");
        startPosition = rectTransform.anchoredPosition;
        rectTransform.DOComplete();
        rectTransform.DOScale(Vector3.one * 1.5f, 0.3f).SetEase(Ease.OutQuad);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down on '" + Label.text.ToString() + "'");
        startPosition = rectTransform.anchoredPosition;
        rectTransform.DOComplete();
        rectTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin drag '" + Label.text.ToString() + "'");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End drag '" + Label.text.ToString() + "'");
        rectTransform.anchoredPosition = startPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void SetLabel(string text)
    {
        Label.text = text;
    }
}

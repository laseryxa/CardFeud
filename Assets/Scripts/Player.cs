using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Player : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;
    [SerializeField] public int health;
    [SerializeField] public int gold;
    [SerializeField] public TextMeshProUGUI goldText;
    [SerializeField] public TextMeshProUGUI healthText;
    [SerializeField] public Image image;
    public void AddGold(int amount) 
    {
        gold += amount;        
    }

    public int GetGold()
    {
        return gold;
    }
    public void SetHealth(int health)
    {
        this.health = health;
    }
    public int GetHealth()
    {
        return health;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "Gold: " + gold.ToString();
        healthText.text = "Health: " + health.ToString();
    }

    public void AttackedByCard(Card card)
    {
        SetHealth(GetHealth() - card.GetAttack());
        card.addStatus(Card.Status.Activated);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Card card = eventData.pointerDrag.GetComponent<Card>();

            if (card.owner != this)
            {
                Debug.Log("Trying to attack other player ");
                if (!card.hasStatus(Card.Status.InHand)) 
                {
                    AttackedByCard(card);
                }
            } else {
                Debug.Log("Tried to drop card on yourself!");
            }
        }
    }
}

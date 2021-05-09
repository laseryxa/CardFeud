using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField] public int gold;
    [SerializeField] public TextMeshProUGUI goldText;
    [SerializeField] public Image image;
    public void AddGold(int amount) 
    {
        gold += amount;        
    }

    public int GetGold()
    {
        return gold;
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
    }
}

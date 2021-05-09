using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public SelectorItem selectorPrefab;

    [SerializeField] List<Player> availableCharacters;

    void Awake()
    {
        const int itemWidth = 130;
        int centerOffset = - (availableCharacters.Count * itemWidth / 2) + itemWidth / 2;
        for (int i = 0; i < availableCharacters.Count; i++)
        {
            Player p = availableCharacters[i];
            SelectorItem item = Instantiate(selectorPrefab, transform);
            item.character = p;
            Image image = item.GetComponent<Image>();
            image.sprite = availableCharacters[i].image.sprite;
            image.color = availableCharacters[i].image.color;

            item.GetComponent<RectTransform>().anchoredPosition = new Vector3(centerOffset + (itemWidth * i), 0, 0);
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

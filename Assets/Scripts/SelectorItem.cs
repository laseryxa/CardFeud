using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorItem : MonoBehaviour
{
    public Player character;
    public void handleClick()
    {
        Debug.Log("Clicked!");
        GlobalState.selectedPlayer = character;
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

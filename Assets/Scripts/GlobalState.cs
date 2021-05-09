using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    public static Player selectedPlayer;
    public static GameObject root;
    
    void Awake()
    {
        root = new GameObject();
        
        //Let the gameobject persist over the scenes
        DontDestroyOnLoad(this.gameObject);

    }

    void Update()
    {
    }
}

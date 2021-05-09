using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button startButton;

    public void SelectPlayer(Player selectedPlayer)
    {
        GlobalState.selectedPlayer = selectedPlayer;
        Debug.Log("Selecting character that has " + GlobalState.selectedPlayer.GetGold().ToString() + " gold");
    }

    public void LoadGame()
    {
        if (!GlobalState.selectedPlayer) {
            Debug.Log("GlobalState.selectedPlayer is null!");
        }
        Player p = Instantiate(GlobalState.selectedPlayer, GlobalState.root.transform);

        Debug.Log("Trying to load scene with character that has " + p.GetGold().ToString() + " gold");

        SceneManager.LoadScene("GameScene");
    }

    public void Update()
    {
        startButton.GetComponent<Button>().interactable = (GlobalState.selectedPlayer != null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject defeatScreen;



    private void Awake()
    {
        // Enables the Victory and Defeat Event Listeners and disables the Victory and Defeat Screens.
        // GameManager.Instance.OnPlayerVictory += ...
        // GameManager.Instance.OnPlayerDefeat += ...

        // victoryScreen.SetActive(false);
        // defeatScreen.SetActive(false);
    }

    private void OnPlayerWon()
    {
        // Enables the Victory Screen if the player has won the game.
        // victoryScreen.SetActive(true);
    }

    private void OnPlayerLost()
    {
        // Enables the Defeat Screen if the player has lost the game.
        // defeatScreen.SetActive(true);
    }



}

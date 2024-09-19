using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{


    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject defeatScreen;

    [SerializeField] private Button mainMenuButton1;
    [SerializeField] private Button mainMenuButton2;


    private void Awake()
    {
        // Disable Victory and Defeat screen
        victoryScreen.SetActive(false);
        defeatScreen.SetActive(false);

        // Go back to Main Menu when Buttons are clicked
        mainMenuButton1.onClick.AddListener(() => SceneManager.LoadScene("MainMenuScene"));
        mainMenuButton2.onClick.AddListener(() => SceneManager.LoadScene("MainMenuScene"));
    }

    private void Update()
    {
        // Enable the Victory screen if the player won, and the Defeat screen if the player lost
        if (GameManager.Instance.playerPile.Count == 0)
        { 
            defeatScreen.SetActive(true);
        }
        if (GameManager.Instance.computerPile.Count == 0)
        {
            victoryScreen.SetActive(true);
        }
    }


}

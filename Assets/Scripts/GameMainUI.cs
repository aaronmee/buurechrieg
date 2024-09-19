using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMainUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI playerCardCounterText;
    [SerializeField] private TextMeshProUGUI computerCardCounterText;

    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;



    private void Awake()
    {
        // Set starting value
        playerCardCounterText.text = "16 Karten";
        computerCardCounterText.text = "16 Karten";

        // Exit the Application or go back to the Main Menu when Buttons are clicked
        exitButton.onClick.AddListener(() => Application.Quit());
        backButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenuScene"));

    }

    void Update()
    {
        // Update the card counters
        playerCardCounterText.text = GameManager.Instance.playerPile.Count + " Karten";
        computerCardCounterText.text = GameManager.Instance.computerPile.Count + " Karten";

        // Disable the back button if someone won
        if (GameManager.Instance.playerPile.Count == 0 || GameManager.Instance.computerPile.Count == 0)
        {
            backButton.gameObject.SetActive(false);
        }

    }


}

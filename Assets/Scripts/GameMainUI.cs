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
        playerCardCounterText.text = "16 Karten";
        computerCardCounterText.text = "16 Karten";

        exitButton.onClick.AddListener(() => Application.Quit());
        backButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenuScene"));

        // GameManager.Instance.OnPlayerVictory += ...
        // GameManager.Instance.OnPlayerDefeat += ...
    }

    void Update()
    {
        //playerCardCounterText.text = GameManager.Instance.playerPile.Count + " Karten";
        //computerCardCounterText.text = GameManager.Instance.computerPile.Count + " Karten";
    }


    private void OnPlayerVictory()
    {
        backButton.gameObject.SetActive(false);
    }

    private void OnPlayerDefeat()
    {
        backButton.gameObject.SetActive(false);
    }


}

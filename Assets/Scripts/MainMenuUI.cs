using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private Button singlePlayerButton;
    [SerializeField] private Button multiPlayerButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button exitButton;




    private void Awake()
    {
        singlePlayerButton.onClick.AddListener(() => {
            SceneManager.LoadScene("GameScene");
        });

        multiPlayerButton.onClick.AddListener(() =>
        {
            Debug.Log("Coming soon!");
        });

        tutorialButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("TutorialScene");
        });

        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        Application.targetFrameRate = 60;
    }




}

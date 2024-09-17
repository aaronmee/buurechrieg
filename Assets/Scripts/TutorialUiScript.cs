using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class  TutorialUI: MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;



    private void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenuScene");
        });
        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }




}

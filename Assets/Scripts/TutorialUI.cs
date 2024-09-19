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
        // Exit the Application or go back to the Main Menu when Buttons are clicked
        backButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenuScene"));
        exitButton.onClick.AddListener(() => Application.Quit());
    }

}

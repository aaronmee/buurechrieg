using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//all comments form chatgpt
public class MainMenuUI : MonoBehaviour
{
    // Serialized fields for the buttons that can be set in the Unity Inspector
    [SerializeField] private Button singlePlayerButton;  // Button for single player mode
    [SerializeField] private Button multiPlayerButton;   // Button for multiplayer mode
    [SerializeField] private Button tutorialButton;      // Button for tutorial mode
    [SerializeField] private Button exitButton;          // Button to exit the game

    // Function: Awake
    // Purpose: Initializes the button listeners and sets the target frame rate.
    // Arguments: None
    // Output: None
    private void Awake()
    {
        // Adds listener for the single player button to load the "GameScene".
        singlePlayerButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));

        // Adds listener for the multiplayer button, but the functionality is not implemented yet.
        multiPlayerButton.onClick.AddListener(() => Debug.Log("Coming soon!"));

        // Adds listener for the tutorial button to load the "TutorialScene".
        tutorialButton.onClick.AddListener(() => SceneManager.LoadScene("TutorialScene"));

        // Adds listener for the exit button to quit the game application.
        exitButton.onClick.AddListener(() => Application.Quit());

        // Sets the target frame rate to 60 frames per second.
        Application.targetFrameRate = 60;
    }
}

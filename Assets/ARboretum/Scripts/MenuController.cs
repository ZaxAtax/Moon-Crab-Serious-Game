using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject howToPlayCanvas;
    public Button playButton;
    public Button howToPlayButton;
    public Button mainMenuButton;
    public Button quitButton;

    void Start()
    {
        // Hide the how to play canvas on start
        howToPlayCanvas.SetActive(false);

        // Set up event listeners for each button
        playButton.onClick.AddListener(OnPlayButtonPressed);
        howToPlayButton.onClick.AddListener(OnHowToPlayButtonPressed);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonPressed);
        quitButton.onClick.AddListener(OnQuitButtonPressed);
    }

    // Called when the play button is pressed
    void OnPlayButtonPressed()
    {
        // Load the Arboretum scene
        SceneManager.LoadScene("Arboretum");
    }

    // Called when the how to play button is pressed
    void OnHowToPlayButtonPressed()
    {
        // Hide the menu canvas and show the how to play canvas
        menuCanvas.SetActive(false);
        howToPlayCanvas.SetActive(true);
    }

    // Called when the main menu button is pressed on the how to play canvas
    void OnMainMenuButtonPressed()
    {
        // Hide the how to play canvas and show the menu canvas
        howToPlayCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    // Called when the quit button is pressed on the menu canvas
    void OnQuitButtonPressed()
    {
        // Quit the game
        Application.Quit();
    }
}

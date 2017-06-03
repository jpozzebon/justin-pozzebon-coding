using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [Header("Current Open Scene Name")]
    public string _sceneName; // scene name string
    [Header("UI Panels")]
    public GameObject _confirmQuitPanel;
    public GameObject _mainMenuPanel;
    public GameObject _optionsPanel;
    public GameObject _creditsPanel;
    [Header("Game Title Sign")]
    public GameObject _titleSign;

    void Start()
    {
        Scene _currentScene = SceneManager.GetActiveScene(); // Finds the name of the scene
        _sceneName = _currentScene.name; //assign scene name string

        Time.timeScale = 1;

        _confirmQuitPanel.SetActive(false);
        _optionsPanel.SetActive(false);
        _creditsPanel.SetActive(false);

        if (_sceneName == "IntroWarningScene")
        {
            _mainMenuPanel = null;
            _optionsPanel = null;
            _creditsPanel = null;
            _confirmQuitPanel = null;
            _titleSign = null;
        }

        if (_sceneName == "InGameScene")
        {
            _mainMenuPanel = null;
            _optionsPanel = null;
            _creditsPanel = null;
            _titleSign = null;
        }
    }

    void Update ()
    {
        OpenConfirmQuit();
    }

    public void ChangeScene (string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad); // This is how to load scene. Add to an empty game object, which is added to a button, and the scene string name added
    }

    public void OpenConfirmQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_sceneName == "MainMenuScene") // Show Confirm Quit Panel
            {
                _confirmQuitPanel.SetActive(true);
            }

            if (_sceneName == "IntroWarningScene") // Immediately Quit The Game
            {
                Application.Quit();
            }
        }
    }

    public void CancelQuit() // Cancel the quit function - Assigned to a button
    {
        _confirmQuitPanel.SetActive(false);
    }

    public void QuitGame() // Confirm quit - Assign to a button
    {
        Application.Quit();
        Debug.Log("THE GAME QUIT");
    }

    public void ToggleAudio()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void ToggleTutorial()
    {
        if(PlayerPrefs.GetFloat("TutOn") == 0)
        {
            PlayerPrefs.SetFloat("TutOn", 1);
            PlayerPrefs.Save();
        }
        else if (PlayerPrefs.GetFloat("TutOn") == 1)
        {
            PlayerPrefs.SetFloat("TutOn", 0);
            PlayerPrefs.Save();
        }
    }

    public void OpenMainMenuPanel()
    {
        _mainMenuPanel.SetActive(true);
        _titleSign.SetActive(true);
        _optionsPanel.SetActive(false);
        _creditsPanel.SetActive(false);
    }

    public void OpenOptionsPanel()
    {
        _optionsPanel.SetActive(true);
        _mainMenuPanel.SetActive(false);
        _creditsPanel.SetActive(false);
        _titleSign.SetActive(false);
    }

    public void OpenCreditsPanel()
    {
        _creditsPanel.SetActive(true);
        _mainMenuPanel.SetActive(false);
        _optionsPanel.SetActive(false);
    }
}

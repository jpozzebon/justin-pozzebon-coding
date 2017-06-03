using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region HOW TO APPLY THIS SCRIPT
// 1. Attach script to a Pause Canvas, a separate canvas to the in-game UI canvas
// 2. When assigning to a button, drag Pause canvas from Heirachy onto button "On click()" section
// 3. Scroll to "PauseManager" then select "PauseGame()"
#endregion

public class PauseManager : MonoBehaviour
{
    Canvas _canvas; // Declaration of the pause canvas component
    [Header("Resume Counter")]
    public float _resumeCounter; // Countdown ready timer
    public Text _restartTimerText; // Countdown ready timer text for UI
    [Header("Pause Button")]
    public Button _pauseButton; // The pause button
    bool _toggleActive; // Check to see if pause is engaged - this check is only for the pause on and off
    bool _timerResetBool; // bool to turn on the resume counter
    bool _isPauseEnabled; // Check to see if pause is engaged - this check is for the overall use of the pause
    [Header("Return To Menu Panel")]
    public GameObject _returnToMenuPanel; // The return to menu panel

	void Start ()
    {
        Time.timeScale = 1; // Set the timeScale to 1
        _canvas = GetComponent<Canvas>(); // Call the canvas component
        _canvas.enabled = false; // Turn off the pause canvas at the start of the level
        _restartTimerText.enabled = false; // Turn off the resume timer text at the start of the level
        _isPauseEnabled = true; // Turn on the ability to pause the game
        _toggleActive = true; // Turn on the ability to pause the game
        _timerResetBool = false; // Turn off the reset timer bool
        _resumeCounter = 3f; // set the resume counter to 3.
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(_timerResetBool == true) // If the timer reset bool is true, turn off the ability to pause and turn off the pause button.
        {
            _isPauseEnabled = false;
            _pauseButton.enabled = false;
        }
        if(_timerResetBool == false) // If the timer reset bool is false, turn on the ability to pause and turn on the puase button.
        {
            _isPauseEnabled = true;
            _pauseButton.enabled = true;
        }
        if(_isPauseEnabled == true) // If the is pause enabled is true...
        { 
            if(Input.GetKeyDown(KeyCode.Escape)) // If the escape key (or back button on the phone) is pressed, run the "PauseGame()" method.
            {
                PauseGame();
            }
        }
	}

    public void PauseGame() // The pause game method
    {
        _canvas.enabled = !_canvas.enabled; // Toggle between enabled and not enabled
        _toggleActive = !_toggleActive; // Toggle between true and false
        Time.timeScale = Time.timeScale == 0 ? 1 : 0; // Toggle the timeScale between 0 when paused and 1 when not paused

        if(_toggleActive == true) // If the toggle active is true, set the TimeScale really low, set the reset timer bool to true and start the Resume game coroutine
        {
            Time.timeScale = 0.000001f;
            _timerResetBool = true;
            StartCoroutine(ResumeGame());
        }
    }

    IEnumerator ResumeGame() // The resume game coroutine
    {
        _resumeCounter = 3.49f; // Set the resume counter to 3.49 seconds.
        while (_resumeCounter > 0.51f) // while the resume counter is greater than 0.51 seconds
        {
            _resumeCounter -= Time.unscaledDeltaTime; // countdown the resume counter with unscaled delta time
            _restartTimerText.enabled = true; // Turn on the reset timer text
            _restartTimerText.text = _resumeCounter.ToString("F0"); // Show the resume counter as a string
            //Yield until the next frame
            yield return null;
        }

        Time.timeScale = 1; // When the timer is finished, reset the timeScale to 1
        _restartTimerText.enabled = false; // Turn off the reset timer
        _timerResetBool = false; // Turn off the reset bool
        Debug.Log("TEST OF THE TIMER"); // Run a console message if the Coroutine works.
    }

    public void OpenReturnToMenu()
    {
        _returnToMenuPanel.SetActive(true);
    }

    public void CancelReturnToMenu()
    {
        _returnToMenuPanel.SetActive(false);
    }
}

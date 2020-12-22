using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourChange : MonoBehaviour
{
    Renderer m_Renderer; // this object's renderer
    private Color color = Color.red; // sets the default starting colour to red

    public Color colorMedium; // colour change from medium to well done
    public Color colorWellDone;

    public AudioSource alert; // a digital alert sound

    public Text timerText; // the UI text that shows the timer
    public GameObject flipText; // text to indicate when to flip the steak
    public GameObject startText; // text to indicate how to start the timer
    public GameObject finishText; // text to indicate when cooking is finished
    public Text selectionText; // text to show what cooking length was selected

    public float timer = 0; // sets the timer start value
    public bool timerIsRunning = false; // sets the timer to stop by default 

    // references to the different cooking times
    public float timeToCook; // how long it takes to cook
    public float flipTime; // how long until the steak should be turned over
    private float mediumTexture = 300; // interpolation of the timer to texture change 

    public enum CookingState { Blue, Rare, MediumRare, Medium, MediumWell, WellDone } // the different steak cooking states
    public CookingState currentCookingState; // the current state the steak will be in


    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>(); // gets the object's renderer
        m_Renderer.material.color = color; // assigns the object's colour
        flipText.SetActive(false); // sets the help text to off
        startText.SetActive(true); // sets the start text to on
        finishText.SetActive(false); // sets the finish text to off
    }

    // Update is called once per frame
    void Update()
    {
        SteakStateSelector();
        ChangeColour();
        StartTimer();
        DisplayTime(timer);
        HelpText();
    }

    /// <summary>
    /// Changes the texture according to the cooking state
    /// </summary>
    public void ChangeColour() // changes the albedo colour of the texture over time
    {
        color.r = 1; // set the default RGB channel values
        color.g = 0;
        color.b = 0;

        if (timer <= timeToCook) // if the timer goes past the selected cooking time
        {
            if (timer <= 300)
            {
                color.g += timer / mediumTexture; // changes the texture's colour value according to the timer
                color.b += timer / mediumTexture;

                Debug.Log(color.b);
            }
            if (currentCookingState == CookingState.MediumWell || currentCookingState == CookingState.WellDone)
            {
                if (timer > 300)
                {
                    color = Color.Lerp(colorMedium, colorWellDone, timer / timeToCook);
                }
            }
        }

        m_Renderer.material.color = color; // updates the renderer
    }

    /// <summary>
    /// Starts, runs and stops the timer
    /// </summary>
    public void StartTimer()
    {
        if (timerIsRunning == true)
        {
            timer += Time.deltaTime;
            if (timer > timeToCook) // time to cook is set by the cooking state enum
            {
                // Debug.Log("Time has run out!");
                timer = timeToCook; // displays the end time when the cooking state is reached
                timerIsRunning = false; // stops the timer
            }
        }
    }

    /// <summary>
    /// Displays the timers in minutes and seconds, counting up
    /// </summary>
    /// <param name="timeToDisplay"></param>
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 0;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    /// <summary>
    /// Starts the Timer when a button is pressed
    /// </summary>
    public void StartButton()
    {
        timerIsRunning = true;
        startText.SetActive(false); // turns off the start text
        alert.Play(0); // plays the alert sound
    }

    /// <summary>
    /// Handles the current cooking state
    /// </summary>
    public CookingState CurrentState // handles the current cooking state
    {
        get
        {
            return currentCookingState; // gets the current cooking state
        }
        set
        {
            currentCookingState = value; // set the cooking value
        }
    }

    /// <summary>
    ///  The different cooking states and their timer lengths
    /// </summary>
    private void SteakStateSelector()
    {
        switch (currentCookingState)
        {
            case CookingState.Blue:
                {
                    timeToCook = 120; // 2 minutes both sides
                    flipTime = 60;
                    break;
                }
            case CookingState.Rare: 
                {
                    timeToCook = 180; // 3 minutes
                    flipTime = 90;
                    break;
                }
            case CookingState.MediumRare: 
                {
                    timeToCook = 240; // 4 minutes
                    flipTime = 120;
                    break;
                }
            case CookingState.Medium: 
                {
                    timeToCook = 300; // 5 minutes
                    flipTime = 150;
                    break;
                }
            case CookingState.MediumWell: // 6 minutes
                {
                    timeToCook = 360;
                    flipTime = 180;
                    break;
                }
            case CookingState.WellDone: // 8 minutes
                {
                    timeToCook = 480;
                    flipTime = 240;
                    break;
                }
        }
    }

    /// <summary>
    /// Updates the UI help text elements
    /// </summary>
    public void HelpText()
    {
        // sets the text to the selected cooking method a display the cooking time in minutes
        selectionText.text = "A " + currentCookingState + " steak will cook in " + (timeToCook / 60) + " minutes";

        if(timer > flipTime) // once the time reaches half way
        {
            flipText.SetActive(true);

            if (alert.isPlaying == false)
            {
                alert.Play(); // plays the alert sound
            }

            if (timer > flipTime + 10) // displays the flip text for 20 seconds
            {
                flipText.SetActive(false); // then turns it off again
                if (alert.isPlaying == true)
                {
                    alert.Pause(); // plays the alert sound
                }
            }
        }
        if(timer == timeToCook) // once the timer is complete
        {
            finishText.SetActive(true); // turns on the text prompt that the cooking is complete
            if (alert.isPlaying == false)
            {
                alert.Play(); // plays the alert sound
            }
        }
    }
}

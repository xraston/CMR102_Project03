using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourChange : MonoBehaviour
{
    Renderer m_Renderer; // this object's renderer
    private Color color = Color.red; // sets the default starting colour to red

    public Color colorMedium;
    public Color colorWellDone;

    public Text timerText; // the UI text that shows the timer

    public float timer = 0; // sets the timer start value
    public bool timerIsRunning = false; // sets the timer to stop by default 

    // references to the different cooking times
    public float timeToCook; // how long it takes to cook
    public float flipTime; // how long until the steak should be turned over
    private float mediumTexture = 540; // interpolation of the timer to texture change 

    public enum CookingState { Blue, Rare, MediumRare, Medium, MediumWell, WellDone } // the different steak cooking states
    public CookingState currentCookingState; // the current state the steak will be in


    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>(); // gets the object's renderer
        m_Renderer.material.color = color; // assigns the object's colour
    }

    // Update is called once per frame
    void Update()
    {
        SteakStateSelector();
        ChangeColour();
        StartTimer();
        DisplayTime(timer);
    }

    /// <summary>
    /// Changes the texture according to the cooking state
    /// </summary>
    public void ChangeColour() // changes the albedo colour of the texture over time
    {
        color.r = 1; // set the default RGB channel values
        color.g = 0;
        color.b = 0;

        if (timer < timeToCook) // if the timer goes past the selected cooking time
        {
            if (timer <= 540)
            {
                color.g += timer / mediumTexture; // changes the texture's colour value according to the timer
                color.b += timer / mediumTexture;

                Debug.Log(color.b);
            }
            if (currentCookingState == CookingState.MediumWell || currentCookingState == CookingState.WellDone)
            {
                if (timer > 540)
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
            if(timer > flipTime)
            {
                // Debug.Log("Turn over the Steak now!");
                // could add an animation and wait for touch input the flip the steak?
            }
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
    ///  Update the states to match the selected cooking length
    /// </summary>
    private void SteakStateSelector()
    {
        switch (currentCookingState)
        {
            case CookingState.Blue:
                {
                    timeToCook = 180; // 3 minutes both sides
                    flipTime = 90;
                    break;
                }
            case CookingState.Rare: 
                {
                    timeToCook = 300; // 5 minutes
                    flipTime = 150;
                    break;
                }
            case CookingState.MediumRare: 
                {
                    timeToCook = 420; // 7 minutes
                    flipTime = 210;
                    break;
                }
            case CookingState.Medium: 
                {
                    timeToCook = 540; // 9 minutes
                    flipTime = 270;
                    break;
                }
            case CookingState.MediumWell: // 11 minutes
                {
                    timeToCook = 660;
                    flipTime = 330;
                    break;
                }
            case CookingState.WellDone: // 13 minutes
                {
                    timeToCook = 780;
                    flipTime =390;
                    break;
                }
        }
    }
}

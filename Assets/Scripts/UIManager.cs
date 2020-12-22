using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ColourChange steakScriptInside; // references to both steak objects
    public ColourChange steakScriptOutside;

    public GameObject cookingSelectionCanvas; // a refernce to the buttons canvas
    public GameObject timerCanvas; // a reference to the Timer UI elements
    public GameObject selectionText; // text to show what cooking length was selected

    public AudioSource meatSlap; // a reference to the meatslap audio

    // Start is called before the first frame update
    void Start()
    {
        cookingSelectionCanvas.SetActive(true);
        selectionText.SetActive(false);
        timerCanvas.SetActive(false);
    }

    public void SteakButtonBlue() // sets the cooking state to Blue
    {
        steakScriptInside.currentCookingState = ColourChange.CookingState.Blue;
        steakScriptOutside.currentCookingState = ColourChange.CookingState.Blue;
        SwitchCanvas();
    }
    public void SteakButtonRare() // sets the cooking state to Rare
    {
        steakScriptInside.currentCookingState = ColourChange.CookingState.Rare;
        steakScriptOutside.currentCookingState = ColourChange.CookingState.Rare;
        SwitchCanvas();
    }
    public void SteakButtonMediumRare() // sets the cooking state to Medium Rare
    {
        steakScriptInside.currentCookingState = ColourChange.CookingState.MediumRare;
        steakScriptOutside.currentCookingState = ColourChange.CookingState.MediumRare;
        SwitchCanvas();
    }
    public void SteakButtonMedium() // sets the cooking state to Medium
    {
        steakScriptInside.currentCookingState = ColourChange.CookingState.Medium;
        steakScriptOutside.currentCookingState = ColourChange.CookingState.Medium;
        SwitchCanvas();
    }
    public void SteakButtonMediumWell() // sets the cooking state to Medium Well
    {
        steakScriptInside.currentCookingState = ColourChange.CookingState.MediumWell;
        steakScriptOutside.currentCookingState = ColourChange.CookingState.MediumWell;
        SwitchCanvas();
    }
    public void SteakButtonWellDone() // sets the cooking state to Well Done
    {
        steakScriptInside.currentCookingState = ColourChange.CookingState.WellDone;
        steakScriptOutside.currentCookingState = ColourChange.CookingState.WellDone;
        SwitchCanvas();
    }

    public void SwitchCanvas()
    {
        cookingSelectionCanvas.SetActive(false);
        selectionText.SetActive(true);
        timerCanvas.SetActive(true);
        meatSlap.Play(0);
    }
}

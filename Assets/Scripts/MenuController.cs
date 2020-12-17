using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public string optionOne; //First steak option choice
    public string optionTwo; //Two steak option choice
    public string optionThree; //Three steak option choice
    public string optionFour; //Four steak option choice
    public Button steakOne;
    public TMP_Text steakOneText;
    public bool rumpTrue;
    public float timerOutcome;
    
    

    public string weightOne; //Size of Steak - Option One
    public string weightTwo; //Size of Steak - Option Two
    public string weightThree; //Size of Steak - Option Three
    public string weightFour; //Size of Steak - Option Four

    public float selectionOne = 0;
    public float selectionTwo = 0;




    // Start is called before the first frame update
    void Start()
    {
        steakOneText = steakOne.GetComponentInChildren<TextMeshProUGUI>();
        MainMenu();


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MainMenu()
    {
        steakOneText.text = "Rump Steak";
      
    }

    public void RumpSteak()
    {
        if (steakOneText.text == "Rump Steak")
            selectionOne = 1;
            steakOneText.text = "500g";
      
    }
    public void FiveHundredGrams()
    {
        if (steakOneText.text == "500g")
            selectionTwo = 1;
        steakOneText.text = "Thankyou!";
    }
   
    public float TimerOutput()
    {
        timerOutcome = (selectionOne + selectionTwo);
        return timerOutcome;
        Debug.Log("The Outcome is" + timerOutcome);




    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Problems[] problems;      // array list of all problems
    public int curProblem;          // current problem the player needs to solve 
    public float timePerProblem;    // time allowed to answer each problem
    public float remainingTime;     // time remaining for the current problem
    //public PlayerController player; // player object

    //create instance of this whole script, to access the script by just going GameManager.instance.[…] without needing to reference it in another script if needed.
    public static GameManager instance;
    void Awake()
    {
        // set instance to this script.
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // set the initial problem
        SetProblem(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Since we’re having a timer for each problem, we need to check if it’s ran out. 
        /* remainingTime -= Time.deltaTime;
         // has the remaining time ran out?
         if (remainingTime <= 0.0f)
         {
             Lose();
         } */

    }

    // called when the player answers all the problems
    void Win()
    {
        Time.timeScale = 0.0f;
        // set UI text. This is refering to MathUI script 
        //MathUI.instance.SetEndText(true);
    }
    // called if the remaining time on a problem reaches 0 or all questions answered wrong
    void Lose()
    {
        Time.timeScale = 0.0f;
        // set UI text
        //MathUI.instance.SetEndText(false);
    }

    // sets the current problem. This function will carry over an index number for the problem array and set that as the current problem.
    void SetProblem(int problem)
    {
        curProblem = problem;
        remainingTime = timePerProblem;
        // set UI text to show problem and answers. this is refering to MathUI script 
        MathUI.instance.SetProblemText(problems[curProblem]);
    }

    // called when the ball enters the correct Basket
    void CorrectAnswer()
    {
        // is this the last problem?
        if (problems.Length - 1 == curProblem)
            Win();
        else
            SetProblem(curProblem + 1);
    }

    // called when thhe ball enters the incorrect Basket
    void IncorrectAnswer()
    {
       // player.Stun();
    }


    // called when the Ball enters a Basket. It carrying over the id of the Basket, which correlates back to the “answers” array in the “Problems” class.
    public void OnPlayerEnterTube(int tube)
    {
        // did they enter the correct tube?
        if (tube == problems[curProblem].correctTube)
            CorrectAnswer();
        else
            IncorrectAnswer();
    }
}

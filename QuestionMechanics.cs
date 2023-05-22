using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MathOperation
{
    Addition,
    Subtraction, 
    Multiplication, 
    Division
}
/*
public class Question
{
    public int FirstNumber;
    public int SecondNumber;
    public int? MissingNumber; // nullable int to represent a missing number
    public int TotalAnswer;
    public Operator Operator;
} */

public class QuestionMechanics : MonoBehaviour
{
    public Text questionText;
    public Text[] answerTexts;
    public int[] firstNumbers;
    public int[] secondNumbers;

    private int currentQuestionIndex;
    private int correctAnswer;
    public int correctChoice;

    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button button4;

    [SerializeField] GameObject desk1;
    [SerializeField] GameObject desk2;
    [SerializeField] GameObject desk3;
    [SerializeField] GameObject desk4;
    [SerializeField] Sprite orangeSprite;

    [SerializeField] AudioSource correctSound;
    [SerializeField] AudioSource wrongSound;
     [SerializeField] AudioSource crowdSound;

    [SerializeField] Animator crowdCheer;
    [SerializeField] Animator kozuAnimations;

  //  public int missingNumber; // nullable int to represent a missing number
    private int equalNumber;
    public MathOperation selectedMathOperation;
    // public Operator Operator;
    //  private Question currentQuestion;
    //this Array will be used for the Billboard score display
    public static bool[] answerStatuses = new bool[8];

    /* // these three are for Respawing the Ball prefab and making it a child of BallParent 
     public GameObject ballPrefab;
     public GameObject originalBallPosition;
     public GameObject ballParent; // This is the Ballparent object */
    public GameObject ballImageHolder;
   

    private void DisableButtons()
    {
        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;
        button4.interactable = false;
    }
    private void EnableButtons()
    {
        button1.interactable = true;
        button2.interactable = true;
        button3.interactable = true;
        button4.interactable = true;
    }

    private void NormalDeskSprite()
    {
        desk1.GetComponent<SpriteRenderer>().sprite = orangeSprite;
        desk2.GetComponent<SpriteRenderer>().sprite = orangeSprite;
        desk3.GetComponent<SpriteRenderer>().sprite = orangeSprite;
        desk4.GetComponent<SpriteRenderer>().sprite = orangeSprite;
    }

    void Start()
    {
        currentQuestionIndex = 0;
        GenerateQuestion();
    }
    //to delay the Generation of a new question
    public void GenerateQuestion(float delayTime)
    {
        Invoke("GenerateQuestion", delayTime);
    }

    void GenerateQuestion()
    {
        switch (selectedMathOperation)
        {
            /*
            case MathOperation.Addition:
                firstNumbers = new int[] { 1, 2, 3, 4, 5 };
                secondNumbers = new int[] { 1, 2, 3, 4, 5 };
                break;*/
            case MathOperation.Subtraction:
                firstNumbers = new int[] { 6, 7, 8, 9, 10, 16, 14, 17};
                secondNumbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 3};
                break;
            case MathOperation.Multiplication:
                firstNumbers = new int[] { 2, 3, 4, 5, 6 };
                secondNumbers = new int[] { 2, 3, 4, 5, 6 };
                break;
            case MathOperation.Division:
                firstNumbers = new int[] { 12, 15, 16, 20, 24 };
                secondNumbers = new int[] { 2, 3, 4, 5, 6 };
                break;
        }



        int firstNumber = firstNumbers[currentQuestionIndex];
        int secondNumber = secondNumbers[currentQuestionIndex];
        int questionFormat = Random.Range(0, 3); // choose a question format randomly

        switch (selectedMathOperation)
        {
            case MathOperation.Addition:
                switch (questionFormat)
                {
                    case 0:
                        equalNumber = firstNumber + secondNumber;
                        questionText.text = firstNumber + " + " + secondNumber + " = ?";
                        correctAnswer = equalNumber;
                        break;
                    case 1:
                        correctAnswer = secondNumber;
                        questionText.text = firstNumber + " + ? = " + (firstNumber + secondNumber);
                        break;
                    case 2:
                        correctAnswer = firstNumber;
                        questionText.text = "? + " + secondNumber + " = " + (firstNumber + secondNumber);
                        break;
                }
                break;

            case MathOperation.Subtraction:
                switch (questionFormat)
                {
                    case 0:
                        equalNumber = firstNumber - secondNumber;
                        questionText.text = firstNumber + " - " + secondNumber + " = ?";
                        correctAnswer = equalNumber;
                        break;
                    case 1:
                        correctAnswer = secondNumber;
                        questionText.text = firstNumber + " - ? = " + (firstNumber - secondNumber);
                        break;
                    case 2:
                        correctAnswer = firstNumber;
                        questionText.text = "? - " + secondNumber + " = " + (firstNumber - secondNumber);
                        break;
                }
                break;
              
            case MathOperation.Multiplication:
                switch (questionFormat)
                {
                    case 0:
                        equalNumber = firstNumber * secondNumber;
                        questionText.text = firstNumber + " × " + secondNumber + " = ?";
                        correctAnswer = equalNumber;
                        break;
                    case 1:
                        correctAnswer = secondNumber;
                        questionText.text = firstNumber + "× ? = " + (firstNumber * secondNumber);
                        break;
                    case 2:
                        correctAnswer = firstNumber;
                        questionText.text = "? × " + secondNumber + " = " + (firstNumber * secondNumber);
                        break;
                }
                break;
        
            case MathOperation.Division:
                switch (questionFormat)
                {
                    case 0:
                        equalNumber = firstNumber / secondNumber;
                        questionText.text = firstNumber + " ÷ " + secondNumber + " = ?";
                        correctAnswer = equalNumber;
                        break;
                    case 1:
                        correctAnswer = secondNumber;
                        questionText.text = firstNumber + "÷ ? = " + (firstNumber / secondNumber);
                        break;
                    case 2:
                        correctAnswer = firstNumber;
                        questionText.text = "? ÷ " + secondNumber + " = " + (firstNumber / secondNumber);
                        break;
                }
                break;
        }

       
        correctChoice = correctAnswer;

        List<int> possibleAnswers = new List<int>();
        possibleAnswers.Add(correctAnswer);

        while (possibleAnswers.Count < answerTexts.Length)
        {
            int randomAnswer = Random.Range(0, 20);
            if (!possibleAnswers.Contains(randomAnswer))
            {
                possibleAnswers.Add(randomAnswer);
            }
        }

        ShuffleList(possibleAnswers);

        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = possibleAnswers[i].ToString();
        }


        ballImageHolder.SetActive(true);
        EnableButtons();
        NormalDeskSprite();
    }


    /*  //Actual method for genearting a new question
     void GenerateQuestion()
     {
         int firstNumber = firstNumbers[currentQuestionIndex];
         int secondNumber = secondNumbers[currentQuestionIndex];

         correctAnswer = firstNumber + secondNumber;
         questionText.text = firstNumber + " + " + secondNumber + " = ?";

         correctChoice = correctAnswer;

         List<int> possibleAnswers = new List<int>();
         possibleAnswers.Add(correctAnswer);

         while (possibleAnswers.Count < answerTexts.Length)
         {
             int randomAnswer = Random.Range(0, 20);
             if (!possibleAnswers.Contains(randomAnswer))
             {
                 possibleAnswers.Add(randomAnswer);
             }
         }

         ShuffleList(possibleAnswers);

         for (int i = 0; i < answerTexts.Length; i++)
         {
             answerTexts[i].text = possibleAnswers[i].ToString();
         }


         ballImageHolder.SetActive(true);
         EnableButtons();
         NormalDeskSprite();
     } */

    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

   

    public void CheckAnswer(int answerIndex)
    {
        
        int firstNumber = firstNumbers[currentQuestionIndex];
        int secondNumber = secondNumbers[currentQuestionIndex];

       


        if (int.Parse(answerTexts[answerIndex].text) == correctAnswer)
        {
         
            Debug.Log("Correct!");
           
            correctSound.Play();
            crowdSound.Play();
            crowdCheer.SetTrigger("isCrowdCheer");
            kozuAnimations.SetTrigger("IsButtonPressed");

            //will be calling method in BillboardScore scrip and passing a bool parameter
            answerStatuses[currentQuestionIndex] = true;

           // questionText.text = firstNumber + " + " + secondNumber + " = " + correctAnswer;
        }
        else
        {
         
            Debug.Log("Wrong!");
            wrongSound.Play();
            kozuAnimations.SetTrigger("IsKozuFailed");

            //will be calling method in BillboardScore scrip and passing a bool parameter
            answerStatuses[currentQuestionIndex] = false;
        }
        // Call OnAnsweredQuestion method in QuizUI script to trigger a proccess of Score Billboard.
        FindObjectOfType<BillboardScore>().OnAnsweredQuestion(answerStatuses[currentQuestionIndex]);


        currentQuestionIndex++;

        if (currentQuestionIndex >= firstNumbers.Length)
        {
            Debug.Log("Quiz finished!");
            return;
        }
        DisableButtons();

    }

    //Called by the Ball when it hits the ring To generate a new question with a delay
    public void GenerateQuestionDelay()
    {
        GenerateQuestion(2f);
    }
}

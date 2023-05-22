using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillboardScore : MonoBehaviour
{
    public Image[] circles;
    private int currentQuestionIndex = 0;

    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < circles.Length; i++)
        {
            if (i < currentQuestionIndex)
            {
                // Question has been answered
                if (QuestionMechanics.answerStatuses[i])
                {
                    // Correct answer
                    circles[i].enabled = true;
                    circles[i].color = Color.white;
                  
                }
                else
                {
                    // Wrong answer
                    circles[i].enabled = true;
                     circles[i].color = Color.red;
                   
                }
            }
    
            else
            {
               // circles[i].enabled = true;
                // Question has not been answered yet
               // circles[i].color = Color.blue;
                Debug.Log("Not Pressed!");

            }
        }
    }
   // thi method will be called from CheckAnswer method of QUestionMechanics script that will pass a bool parameter
    public void OnAnsweredQuestion(bool isCorrect)
    {
        QuestionMechanics.answerStatuses[currentQuestionIndex] = isCorrect;
        currentQuestionIndex++;
        UpdateUI();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    // public bool isCorrect;
    // public GameObject questionBoard;

    public Sprite greenSprite;
    public Sprite redSprite;
    public GameManager gameManager;
    public Text[] answerTexts;
    [SerializeField] GameObject desk;
    public void ChangeSprite(int answerIndex)
    {

        int correctNumber = gameManager.GetComponent<QuestionMechanics>().correctChoice;
       // int textNumber= int.Parse(answerText.text);
        if (int.Parse(answerTexts[answerIndex].text) == correctNumber)
        {
            // Set the sprite to green
            desk.GetComponent<SpriteRenderer>().sprite = greenSprite;
        }
        else
        {
            // Set the sprite to red
            desk.GetComponent<SpriteRenderer>().sprite = redSprite;
        }
       

    }
}

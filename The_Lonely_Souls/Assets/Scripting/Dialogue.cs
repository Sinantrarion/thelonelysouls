using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour  
{
    public enum typeOfDialogue { dialogue, description, question }
    public typeOfDialogue currentType;
    

    //Dialogue
    [Space]
    public string characterNameToDisplay;
    public int characterImage;
    public string dialogText;
    public int additionalEffects = 0;
    public int position = 0;
    //Position- 0 on the left, 1 on the right
    public float speedOfLetters = 0.05f;

    //Question
    [Space]
    public string answerOne;
    public int nextDialogueIDOne;
    [Space]
    public string answerTwo;
    public int nextDialogueIDTwo;
    [Space]
    public string answerThree;
    public int nextDialogueIDThree;

    //Always Show
    [Space]
    public int nextDialogueID;
    public void PassDataToFiller()
    {
        switch (currentType)
        {
            case typeOfDialogue.description:
                ChatBox_Filler.Instance.PassedDescription(dialogText, speedOfLetters, nextDialogueID);
                break;
            case typeOfDialogue.dialogue:
                if (dialogText.Contains("@MyName"))
                {
                    string charNameMine = gameObject.GetComponent<CharacterHolder>().yourName;
                    dialogText = dialogText.Replace("@MyName", characterNameToDisplay);
                }
                ChatBox_Filler.Instance.PassedDialogue(characterNameToDisplay, characterImage, dialogText, additionalEffects, position, speedOfLetters, nextDialogueID);
                break;
            case typeOfDialogue.question:
                ChatBox_Filler.Instance.Questionaire(answerOne, answerTwo, answerThree, nextDialogueIDOne, nextDialogueIDTwo, nextDialogueIDThree);
                break;

        }
    }
}
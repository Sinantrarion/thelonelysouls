using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour  
{
    public enum typeOfDialogue { dialogue, description, question, split, enterName, finishing, stop }
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

    [Space]
    //Split
    public int clauseAndReason;
    public int clauseTrueID, clauseFalseID;

    //Always Show
    [Space]
    public int nextDialogueID;
    public int consequenceID;
    public int bonusRep;
    public int bonusRepID;



    public void PassDataToFiller()
    {
        if (consequenceID > 0 || bonusRep != 0)
        {
            ChatBox_Filler.Instance.PassRelationshipAndConsequences(consequenceID, bonusRep, bonusRepID);
        }
        switch (currentType)
        {
            case typeOfDialogue.description:
                ChatBox_Filler.Instance.PassedDescription(dialogText, speedOfLetters, nextDialogueID);
                break;
            case typeOfDialogue.dialogue:
                ChatBox_Filler.Instance.PassedDialogue(characterNameToDisplay, characterImage, dialogText, additionalEffects, position, speedOfLetters, nextDialogueID);
                break;
            case typeOfDialogue.question:
                ChatBox_Filler.Instance.Questionaire(answerOne, answerTwo, answerThree, nextDialogueIDOne, nextDialogueIDTwo, nextDialogueIDThree);
                break;
            case typeOfDialogue.split:
                ChatBox_Filler.Instance.Split(clauseAndReason, clauseTrueID, clauseFalseID);
                break;
            case typeOfDialogue.enterName:
                ChatBox_Filler.Instance.InputNameStart(nextDialogueID);
                break;
            case typeOfDialogue.finishing:
                ChatBox_Filler.Instance.Finishing();
                break;
            case typeOfDialogue.stop:

                break;
        }


    }
}
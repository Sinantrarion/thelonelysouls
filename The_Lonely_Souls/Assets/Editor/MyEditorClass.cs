using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Dialogue))]
public class MyEditorClass : Editor
{
    public override void OnInspectorGUI()
    {
        // If we call base the default inspector will get drawn too.
        // Remove this line if you don't want that to happen.
        //base.OnInspectorGUI();

        Dialogue myBehaviour = target as Dialogue;

        myBehaviour.currentType = (Dialogue.typeOfDialogue)EditorGUILayout.EnumPopup("Current type", myBehaviour.currentType);

        switch (myBehaviour.currentType)
        {
            case Dialogue.typeOfDialogue.dialogue:
                myBehaviour.characterNameToDisplay = EditorGUILayout.TextField("Character Name to Display:", myBehaviour.characterNameToDisplay);
                myBehaviour.characterImage = EditorGUILayout.IntField("Image:", myBehaviour.characterImage);
                myBehaviour.dialogText = EditorGUILayout.TextField("Dialogue Text:", myBehaviour.dialogText);
                myBehaviour.additionalEffects = EditorGUILayout.IntField("Additional Effect:", myBehaviour.additionalEffects);
                myBehaviour.position = EditorGUILayout.IntSlider("Position:", myBehaviour.position, 0, 1);
                myBehaviour.speedOfLetters = EditorGUILayout.FloatField("Speed of Letters:", myBehaviour.speedOfLetters);
                EditorGUILayout.Space();
                myBehaviour.nextDialogueID = EditorGUILayout.IntField("Next Dialogue ID:", myBehaviour.nextDialogueID);
                break;

            case Dialogue.typeOfDialogue.description:
                myBehaviour.dialogText = EditorGUILayout.TextField("Dialogue Text:", myBehaviour.dialogText);
                myBehaviour.speedOfLetters = EditorGUILayout.FloatField("Speed of Letters:", myBehaviour.speedOfLetters);
                EditorGUILayout.Space();
                myBehaviour.nextDialogueID = EditorGUILayout.IntField("Next Dialogue ID:", myBehaviour.nextDialogueID);
                break;

            case Dialogue.typeOfDialogue.question:
                myBehaviour.answerOne = EditorGUILayout.TextField("First Answer:", myBehaviour.answerOne);
                myBehaviour.nextDialogueIDOne = EditorGUILayout.IntField("Next ID First:", myBehaviour.nextDialogueIDOne);
                EditorGUILayout.Space();
                myBehaviour.answerTwo = EditorGUILayout.TextField("Second Answer:", myBehaviour.answerTwo);
                myBehaviour.nextDialogueIDTwo = EditorGUILayout.IntField("Next ID Second:", myBehaviour.nextDialogueIDTwo);
                EditorGUILayout.Space();
                myBehaviour.answerThree = EditorGUILayout.TextField("Third Answer:", myBehaviour.answerThree);
                myBehaviour.nextDialogueIDThree = EditorGUILayout.IntField("Next ID Third:", myBehaviour.nextDialogueIDThree);
                break;
        }
    }
}

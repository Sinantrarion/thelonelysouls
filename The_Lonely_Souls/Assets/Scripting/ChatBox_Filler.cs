using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBox_Filler : MonoBehaviour
{
    public static ChatBox_Filler Instance;
    public TextMeshProUGUI main_textMeshPro;
    public GameObject leftCharacterNameBox;
    public GameObject rightCharacterNameBox;

    private TextMeshProUGUI leftCharacterName;
    private TextMeshProUGUI rightCharacterName;

    bool finishedProcess = true;
    [Space]
    [HideInInspector]
    public GameObject buttonAnswerOne, buttonAnswerTwo, buttonAnswerThree;
    [HideInInspector]
    public Text buttonTextOne, buttonTextTwo, buttonTextThree;

    [Space]
    public List<CharacterHolder> characters;
    int nextIdSave;
    [HideInInspector]
    public List<int> questionId;


    private void Awake()
    {
        Instance = this;
        leftCharacterName = leftCharacterNameBox.GetComponentInChildren<TextMeshProUGUI>();
        rightCharacterName = rightCharacterNameBox.GetComponentInChildren<TextMeshProUGUI>();

    }

    private void Start()
    {
        characters[0].characterDialogue[0].PassDataToFiller();
    }

    public void NextMessage(string name, string text, float speed, int pos)
    {
        if (pos == 0)
        {
            leftCharacterName.text = name;
            leftCharacterNameBox.SetActive(true);
            rightCharacterNameBox.SetActive(false);
        }
        else
        {
            rightCharacterName.text = name;
            rightCharacterNameBox.SetActive(true);
            leftCharacterNameBox.SetActive(false);
        }
        StartCoroutine(Continue(text, speed));
    }

    public void NextDescription(string text, float speed)
    {
        leftCharacterNameBox.SetActive(false);
        rightCharacterNameBox.SetActive(false);
        StartCoroutine(Continue(text, speed));
    }

    public void PassedDialogue(string name, int image, string text, int effects, int pos, float speed, int nextID)
    {
        nextIdSave = nextID;
        NextMessage(name, text, speed, pos);
    }

    public void PassedDescription(string text, float speed, int nextID)
    {
        nextIdSave = nextID;
        NextDescription(text, speed);
    }

    public void Questionaire(string textOne, string textTwo, string textThree, int idOne, int idTwo, int idThree)
    {

        questionId[0] = idOne;
        questionId[1] = idTwo;
        questionId[2] = idThree;

        if(idThree > 0)
        {
            buttonTextOne.text = textOne;
            buttonTextTwo.text = textTwo;
            buttonTextThree.text = textThree;
            buttonAnswerOne.SetActive(true);
            buttonAnswerTwo.SetActive(true);
            buttonAnswerThree.SetActive(true);
        } else
        {
            buttonTextOne.text = textOne;
            buttonTextTwo.text = textTwo;
            buttonAnswerOne.SetActive(true);
            buttonAnswerTwo.SetActive(true);
        }
    }

    public void DefaultKeyPress()
    {
        if (finishedProcess)
        {
            int tempIdLink = nextIdSave / 1000;
            int tempIdText = nextIdSave - (tempIdLink * 1000);

            characters[tempIdLink].characterDialogue[tempIdText].PassDataToFiller();
        }
        else
        {
            finishedProcess = true;
        }
        
    }


    public void AnswerKeyPress(int number)
    {
        int tempIdLink = questionId[number] / 1000;
        int tempIdText = questionId[number] - (tempIdLink * 1000);

        characters[tempIdLink].characterDialogue[tempIdText].PassDataToFiller();

        questionId[0] = 0;
        questionId[1] = 0;
        questionId[2] = 0;
        buttonAnswerOne.SetActive(false);
        buttonAnswerTwo.SetActive(false);
        buttonAnswerThree.SetActive(false);
    }


    IEnumerator Continue(string dialogText, float speedOfLetters)
    {
        finishedProcess = false;
        main_textMeshPro.text = dialogText;
        int totalVisibleCharacters = dialogText.Length;
        int counter = 0;

        while (!finishedProcess)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            main_textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                finishedProcess = true;
            }

            counter += 1;

            yield return new WaitForSeconds(speedOfLetters);
        }
        main_textMeshPro.maxVisibleCharacters = totalVisibleCharacters;
        finishedProcess = true;
    }
}

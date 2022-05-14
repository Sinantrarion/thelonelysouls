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
    [HideInInspector]
    public GameObject mainWindow;
    [HideInInspector]
    public Image bgImage, chrImage;

    bool finishedProcess = true;
    [Space]
    [HideInInspector]
    public GameObject buttonAnswerOne, buttonAnswerTwo, buttonAnswerThree;
    [HideInInspector]
    public Text buttonTextOne, buttonTextTwo, buttonTextThree;

    [HideInInspector]
    public GameObject inputNameBox;
    [HideInInspector]
    public GameObject submitChoiceButton;
    [HideInInspector]
    public Text nameInputField;
    [HideInInspector]
    public Button continueButton;
    [Space]
    public string characterNameToDisplay;
    public string spiderName;
    public string buffName;
    public string deerName;

    [Space]
    public List<CharacterHolder> characters;
    int nextIdSave;
    int ending;

    [HideInInspector]
    public List<int> questionId;
    public List<bool> causesList;
    public List<int> reputation;
    public List<int> finishResults;

    public List<Sprite> bcgImageSprite;
    public List<Sprite> spiderImageSprite;
    public List<Sprite> buffImageSprite;
    public List<Sprite> deerImageSprite;

    public bool newGame;
    public int savedStage;

    private void Awake()
    {
        Instance = this;
        leftCharacterName = leftCharacterNameBox.GetComponentInChildren<TextMeshProUGUI>();
        rightCharacterName = rightCharacterNameBox.GetComponentInChildren<TextMeshProUGUI>();

    }
    
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        newGame = data.secondGame;
        causesList = data.causesList;
        reputation = data.reputation;
        savedStage = data.savedStage;
    }

    private void Start()
    {
        if (!newGame)
        {
            characters[0].characterDialogue[0].PassDataToFiller();
        } else
        {
            PassData(savedStage);
        }
    }

    //Inputting Name Zone
    //InputNameStart gets called to activate name entering window, as well as disables button to continue.
    public void InputNameStart(int nextID)
    {
        nextIdSave = nextID;
        continueButton.interactable = false;
        inputNameBox.SetActive(true);
        submitChoiceButton.SetActive(true);
    }
    //InputNameFinish gets called to deactivate name entering window, as well as enable button to continue and continues.
    public void InputNameFinish()
    {
        inputNameBox.SetActive(false);
        submitChoiceButton.SetActive(false);
        characterNameToDisplay = nameInputField.text;
        continueButton.interactable = true;
        DefaultKeyPress();
    }

    //Call this to pass dialogue to the Chatbox filler. 
    public void PassedDialogue(string name, int image, string text, int effects, int pos, float speed, int nextID)
    {
        nextIdSave = nextID;
        if (text.Contains("@"))
        {
            if (text.Contains("@MyName"))
            {
                text = text.Replace("@MyName", characterNameToDisplay);
            }
            if (text.Contains("@SpiderName"))
            {
                text = text.Replace("@SpiderName", spiderName);
            }
            if (text.Contains("@DeerName"))
            {
                text = text.Replace("@DeerName", deerName);
            }
            if (text.Contains("@BuffName"))
            {
                text = text.Replace("@BuffName", buffName);
            }
        }
        switch (name)
        {
            case "@MyName":
                name = characterNameToDisplay;
                break;
            case "@SpiderName":
                name = spiderName;
                break;
            case "@BuffName":
                name = buffName;
                break;
            case "@DeerName":
                name = deerName;
                break;
            case null:
                break;
        }
            


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

    public void PassData(int numb)
    {
        int tempIdLink = numb / 100;
        int tempIdText;
        if (tempIdLink > 0)
        {
            //Adjustement due to the bug in calculations- From 100 numbers are correct, but before 100 there is a difference in 1. Instead of changing 100 entries I decided to do it this way. 
            tempIdText = (numb - (tempIdLink * 100));
        }
        else
        {
            tempIdText = (numb - (tempIdLink * 100)) - 1;
        }
        SavePlayer();
        characters[tempIdLink].characterDialogue[tempIdText].PassDataToFiller();
    }

    public void PassedDescription(string text, float speed, int nextID)
    {
        if (text.Contains("@MyName"))
        {
            text = text.Replace("@MyName", characterNameToDisplay);
        }
        if (text.Contains("@SpiderName"))
        {
            text = text.Replace("@SpiderName", spiderName);
        }
        if (name.Contains("@DeerName"))
        {
            text = text.Replace("@DeerName", deerName);
        }
        if (name.Contains("@BuffName"))
        {
            text = text.Replace("@BuffName", buffName);
        }
        nextIdSave = nextID;
        leftCharacterNameBox.SetActive(false);
        rightCharacterNameBox.SetActive(false);
        StartCoroutine(Continue(text, speed));
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

    public void Split(int clause, int idOne, int idTwo)
    {
        Debug.Log("Split Before");
        if(causesList[clause] == true)
        {
            PassData(idOne);
        } else
        {
            PassData(idTwo);
        }
    }

    public void DefaultKeyPress()
    {
        if (finishedProcess)
        {
            PassData(nextIdSave);
        }
        else
        {
            finishedProcess = true;
        }
        
    }

    public void PassRelationshipAndConsequences(int consequenceID, int bonusRep, int bonusRepID)
    {
        if (consequenceID > 0)
        {
            causesList[consequenceID] = true;
        }
        if (bonusRep != 0)
        {
            reputation[bonusRepID] += bonusRep;
        }
    }


    public void AnswerKeyPress(int number)
    {
        PassData(questionId[number]);
        questionId[0] = 0;
        questionId[1] = 0;
        questionId[2] = 0;
        buttonAnswerOne.SetActive(false);
        buttonAnswerTwo.SetActive(false);
        buttonAnswerThree.SetActive(false);
    }

    public void Finishing()
    {
        //Finale- Found first Love
        if(reputation[0] >= 5 || reputation[1] >= 5 || reputation[2] >= 5)
        {
            ending = 0;
            nextIdSave = finishResults[0];
        } 
        //Finale- Found a friend
        else if (reputation[0] >= 3 || reputation[1] >= 3 || reputation[2] >= 3)
        {
            ending = 1;
            nextIdSave = finishResults[1];
        } 
        //Finale- Met some people
        else if (reputation[0] >= 1 && reputation[1] >= 1 && reputation[2] >= 1)
        {
            ending = 2;
            nextIdSave = finishResults[2];
        }
        //Finale- Somebody
        else if (reputation[0] >= 1 || reputation[1] >= 1 || reputation[2] >= 1)
        {
            ending = 3;
            nextIdSave = finishResults[3];
        }
        //Finale- Alone
        else
        {
            ending = 4;
            nextIdSave = finishResults[4];
        }
        PassData(nextIdSave);

    }

    public void Stop()
    {
        mainWindow.SetActive(false);
    }

    public void ChangeBackground(int newB)
    {
        if (newB != 0)
        {
            bgImage.sprite = bcgImageSprite[newB];
        }
    }

    public void ChangeImage(int chr, int newI)
    {
        if (newI != 0)
        {
            switch (chr)
            {
                case 0:
                    break;
                case 1:
                    chrImage.sprite = spiderImageSprite[newI];
                    break;
                case 2:
                    chrImage.sprite = buffImageSprite[newI];
                    break;
                case 3:
                    chrImage.sprite = deerImageSprite[newI];
                    break;
            }
        }
    }

    IEnumerator Continue(string dialogText, float speedOfLetters)
    {
        if(speedOfLetters != 0.05f)
        {
            speedOfLetters = 0.01f;
        }
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

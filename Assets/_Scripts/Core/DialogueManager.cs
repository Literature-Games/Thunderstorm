using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager dm;

    [HideInInspector] public bool finished = false;

    //Ink Variables
    public bool dialogueIsPlaying {get; private set;}

    //private variables
    [Header("Dialogue Background")]
    [SerializeField] Sprite left;
    [SerializeField] Sprite right;
    [SerializeField] Sprite center; //thought bubble
    [SerializeField] Sprite middle; //speech bubble

    [Header("Dialogue Portraits")]
    [SerializeField] Sprite annoyed;
    [SerializeField] Sprite neutral;
    [SerializeField] Sprite angry;
    [SerializeField] Sprite happy;
    [SerializeField] Sprite upset;
    [SerializeField] Sprite thinking;
    [SerializeField] Sprite speaking;

    [Header("Dialogue Sprites")]
    [SerializeField] Image leftSprite; //Jake
    [SerializeField] Image middleSprite; //Player
    [SerializeField] Image rightSprite; //Rufford OR Nova

    [Header("Dialogue UI")]
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] Image dialogueLayout;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI displayNameText;

    [Header("Choices UI")]
    [SerializeField] GameObject[] choices;
    TextMeshProUGUI[] choicesText;

    ///Ink variables
    Story currentStory;
    string npcName = "";
    const string SPEAKER_TAG = "speaker";
    const string PORTRAIT_TAG = "portrait";
    const string LAYOUT_TAG = "layout";
    bool giveHeart = false;
    bool giveTear = false;
    bool calculating = false;
    int actorNum = 0;
    Inventory _inventory;

    void Awake()
    {
        if(dm != null)
        {
            Debug.LogWarning("More than one Dialogue Manager in scene");
        }
        if(dm == null)
            dm = this.GetComponent<DialogueManager>();
    }

    void Start()
    {
        finished = false;
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //get all the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        _inventory = Inventory.instance;
    }

    void Update()
    {
        //return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }

        // handle continueing to the next line in the dialogue when is pressed
        if (InputManager.im.GetSubmitPressed())
        {
            ContinueStory();
        }

        if(finished)
        {
            Debug.Log("Dialogue Finished...");
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        //Setup story for all files involved
        currentStory = new Story(inkJSON.text);
        InventoryPrefab.ip.currentStory = currentStory;
        InventoryPrefab.ip.setUpScene();
        npcName = (string)currentStory.variablesState["char1"]; //get name
        DisplayActors();
        InkInventory.ii.inkJSON = inkJSON;
        InkInventory.ii.checkVariables();

        //Starting dialogue
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        //reset speaker
        displayNameText.text = "???";

        //Set path
        if(npcName == "bridge")
        {
            if(_inventory.items.Count > 0)
            {
                currentStory.ChoosePathString("full");
            }
            else currentStory.ChoosePathString("empty");
        }
        else 
            InventoryPrefab.ip.setInkPath();

        ContinueStory();
    }

    IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    void ContinueStory()
    {

        if (currentStory.canContinue)
        {
            //set text for the current story
            dialogueText.text = currentStory.Continue();

            //display choices, if any, for this dialogue line
            DisplayChoices();

            //handle Tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            if(npcName != "bridge")
            {
                Debug.Log("NPC is not bridge");
                //Check if the player has reached one of the endings
                giveHeart = (bool)currentStory.variablesState["heart"];
                giveTear = (bool)currentStory.variablesState["tear"];

                if(giveHeart)
                {
                    InkInventory.ii.GetHeart();
                    finished = true;
                }
                    
                if(giveTear)
                {
                    InkInventory.ii.GetTear();
                    finished = true;
                }
            }
            else{
                Debug.Log("npc is a bridge");
                //Check if the player has reached one of the endings
                calculating = (bool)currentStory.variablesState["calculate"];
                if(calculating)
                {
                    Bridge.instance.Exchange();
                }
            }
            StartCoroutine(ExitDialogueMode());
        }
    }

    void HandleTags(List<string> currentTags)
    {
        //loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            //parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag coould not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue= splitTag[1].Trim();

            //handle the tag
            switch(tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;

                case PORTRAIT_TAG:
                    // set the correct portrait image
                    if(displayNameText.text == "Jake")
                    {
                        switch(tagValue)
                        {
                            case "annoyed":
                                leftSprite.sprite = annoyed;
                                break;
                            case "neutral":
                                leftSprite.sprite = neutral;
                                break;
                            case "angry":
                                dialogueLayout.sprite = angry;
                                break;
                            case "happy":
                                leftSprite.sprite = happy;
                                break;
                            case "upset":
                                leftSprite.sprite = upset;
                                break;
                            default:
                                Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                                break;
                        }
                    }
                    if(displayNameText.text == "Rufford" || displayNameText.text == "Nova")
                    {
                        switch(tagValue)
                        {
                            case "annoyed":
                                rightSprite.sprite = annoyed;
                                break;
                            case "neutral":
                                rightSprite.sprite = neutral;
                                break;
                            case "angry":
                                rightSprite.sprite = angry;
                                break;
                            case "happy":
                                rightSprite.sprite = happy;
                                break;
                            case "upset":
                                rightSprite.sprite = upset;
                                break;
                            default:
                                Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                                break;
                        }
                    }
                    if(displayNameText.text == "you")
                    {
                        switch(tagValue)
                        {
                            case "thinking":
                                middleSprite.sprite = thinking;
                                break;
                            case "speaking":
                                middleSprite.sprite = speaking;
                                break;
                            default:
                                Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                                break;
                        }
                    }
                    break;

                case LAYOUT_TAG:
                    //set the correct layout
                    switch(tagValue)
                    {
                        case "left":
                            dialogueLayout.sprite = left;
                            break;
                        case "right":
                            dialogueLayout.sprite = right;
                            break;
                        case "center": //thought bubble
                            dialogueLayout.sprite = center;
                            break;
                        case "middle": //speech bubble
                            dialogueLayout.sprite = middle;
                            break;
                        default:
                            Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                            break;
                    }
                    break;

                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            } //close tag switch
        } //close foreach
    }

    /*
    *   if actorNum == 1, the only actor is the player
    *   if actorNum == 2, the actors are Jake and the player
    *   if actorNum >= 3, the actors are Jake, player, and several others
    */

    void DisplayActors()
    {
        actorNum = (int)currentStory.variablesState["actorNum"]; //get actor number
        switch(actorNum)
        {
            case 1:
                leftSprite.enabled = false;
                middleSprite.enabled = true;
                rightSprite.enabled = false;
                break;
            case 2:
                leftSprite.enabled = true;
                middleSprite.enabled = true;
                rightSprite.enabled = false;
                break;
            case 3:
                leftSprite.enabled = true;
                middleSprite.enabled = true;
                rightSprite.enabled = true;
                break;
            default:
                Debug.LogWarning("Actor Number came in but is not currently being handled: " + actorNum);
                break;
        }
    }

    void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defemsive check to make sure our UI can support the number of choices in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of the choices given: " 
            + currentChoices.Count);
        }
        
        int index = 0;
        //enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        //go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }

    IEnumerator SelectFirstChoice()
    {
        // cleared first, then wait
        //for at least one frame befroe setting current object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}

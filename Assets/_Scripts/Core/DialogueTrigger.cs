using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //private variables below
    [Header("Visual Cue")]
    [SerializeField] GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] TextAsset inkJSON;

    bool playerInRange;
    //Collider2D _collider2D;

    void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        //this._collider2D = this.GetComponent<Collider2D>();
    }

    void Update()
    {
        if (playerInRange && !DialogueManager.dm.dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (InputManager.im.GetInteractPressed())
            {
                DialogueManager.dm.EnterDialogueMode(inkJSON);
            }
        }
        else visualCue.SetActive(false);

        //deactivate the trigger for this object
        /*
        if(DialogueManager.dm.finished)
        {
            this._collider2D.enabled = false;
        }
        */
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}

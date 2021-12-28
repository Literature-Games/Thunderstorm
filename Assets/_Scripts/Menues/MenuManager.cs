using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/*
* This file is the manager for the main menu
*/

public class MenuManager : MonoBehaviour
{

    [SerializeField] Animator transition;

    [Header("Menu Panels")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject poemMenu;
    [SerializeField] GameObject creditMenu;

    // First buttons selected in the menus attached to the main menu
    [Header("Selection Buttons")]
    [SerializeField] GameObject mainFirstSelected;
    [SerializeField] GameObject poemFirstSelected;
    [SerializeField] GameObject poemCloseSelected;
    [SerializeField] GameObject creditFirstSelected;
    [SerializeField] GameObject creditCloseSelected;


    // Start is called before the first frame update
    void Start()
    {
        OpenMain();
    }

    public void OpenMain()
    {
        mainMenu.SetActive(true);
        // cleared first, then wait
        //for at least one frame befroe setting current object
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainFirstSelected);
    }

    public void StartGame()
    {
        SceneTransition.st.LoadLevel("Scene01");
    }

    public void CloseMain()
    {
        mainMenu.SetActive(false);
        // cleared
        EventSystem.current.SetSelectedGameObject(null);

    }

    public void OpenPoem()
    {
        poemMenu.SetActive(true);
        // cleared first, then wait
        //for at least one frame befroe setting current object
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(poemFirstSelected);
    }

    public void ClosePoem()
    {
        poemMenu.SetActive(false);
        // cleared first, then wait
        //for at least one frame befroe setting current object
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(poemCloseSelected);
    }

    public void OpenCredit()
    {
        creditMenu.SetActive(true);
        // cleared first, then wait
        //for at least one frame befroe setting current object
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditFirstSelected);
    }

    public void CloseCredit()
    {
        creditMenu.SetActive(false);
        // cleared first, then wait
        //for at least one frame befroe setting current object
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditCloseSelected);
    }
}
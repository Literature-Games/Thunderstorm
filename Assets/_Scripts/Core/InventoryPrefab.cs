using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Ink.Runtime;

public class InventoryPrefab : MonoBehaviour
{
    public static InventoryPrefab ip;
    [HideInInspector] public Story currentStory;
    static string _ending01;
    static string _ending02;
    static string _ending03;
    static string _ending04;


    //Endings listed for scene03
    //can be found in WorryingStory.ink
    bool ruffordRain;
    bool novaRain;

    //Ending listed for scene04
    //can be found in LeavingStory.ink
    bool ruffordDenialRain;
    bool ruffordAcceptanceRain;
    bool novaDenialRain;
    bool novaAcceptanceRain;

    //Endings found in both scenes
    Inventory _inventory;
    bool rainEnding;
    bool sunEnding;
    bool regularStart;
    string storyName;


    void Awake()
    {
        if(ip == null)
            ip = this.GetComponent<InventoryPrefab>();
    }

    void Start()
    {
        _inventory = Inventory.instance;
    }

    public void setUpScene()
    {
        if (GameManager.gm)
        {
            Debug.Log("Checking scenes...");  
            storyName = (string)currentStory.variablesState["storyName"];
            if (GameManager.gm.currentLevel == "Scene01" || GameManager.gm.currentLevel == "Scene02")
            {
                Debug.Log("currently scene 1 or 2. Skipping scene check.");  
                regularStart = true;
                return;
            }

            Debug.Log("Not scene 1 or 2. Calling scene check for 3, 4, or 5.");             
            if (GameManager.gm.currentLevel == "Scene03") scene3Check();
            if (GameManager.gm.currentLevel == "Scene04") scene4Check();
            if (GameManager.gm.currentLevel == "Scene05")
            {
                resetEndings();
                if( storyName == "RuffordReflection") scene05RuffordCheck();
                else scene05NovaCheck();
            }
        }
        return;
    }


    /*
    *   Called at the end of every scene.
    *   Stores the players inventory for later use.
    */
    public string StoreValue (string sceneName, string value)
    {
        switch(sceneName)
        {
            case "Scene01":
                _ending01 = value;
                Debug.Log("Ending 01 is " + _ending01);
                break;
            case "Scene02":
                _ending02 = value;
                Debug.Log("Ending 02 is " + _ending02);
                break;
            case "Scene03":
                _ending03 = value;
                Debug.Log("Ending 03 is " + _ending03);
                break;
            case "Scene04":
                _ending04 = value;
                Debug.Log("Ending 04 is " + _ending04);
                break;
            default:
                Debug.Log("There is no maching level.");
                break;
        }
        return value;
    }


    /*
    *   Called at the start of Scene05.
    *   Fills the players inventory with all of the items they collected so far.
    */
    public void SetValue()
    {
        if (_ending01 == "heart") InkInventory.ii.GetHeart();
        else InkInventory.ii.GetTear();

        if (_ending02 == "heart") InkInventory.ii.GetHeart();
        else InkInventory.ii.GetTear();

        if (_ending03 == "heart") InkInventory.ii.GetHeart();
        else InkInventory.ii.GetTear();

        if (_ending04 == "heart") InkInventory.ii.GetHeart();
        else InkInventory.ii.GetTear();
    }

    /*
    *   Story path Checks for each scene
    */
    void scene3Check()
    {
        Debug.Log("In Scene 03. _ending01 = " + _ending01 + " and _ending02 = " + _ending02);
        
        if(_ending01 == "tear" && _ending02 == "tear")
            rainEnding = true;

        else if(_ending01 == "tear" && _ending02 == "heart")
            ruffordRain = true;

        else if(_ending01 == "heart" && _ending02 == "tear")
            novaRain = true;

        else if(_ending01 == "heart" && _ending02 == "heart")
            sunEnding = true;

        return;
    }

    void scene4Check()
    {
        Debug.Log("In Scene 04. _ending01 = " + _ending01 + ", _ending02 = " + _ending02 + ", and _ending03 = " + _ending03);
        
        if((_ending01 == "tear") && (_ending02 == "tear") && (_ending03 == "tear"))
            rainEnding = true;

        else if(_ending01 == "tear" && _ending02 == "heart" && _ending03 == "tear")
            ruffordDenialRain = true;

        else if(_ending01 == "tear" && _ending02 == "heart" && _ending03 == "heart")
            ruffordAcceptanceRain = true;

        else if(_ending01 == "heart" && _ending02 == "tear" && _ending03 == "tear")
            novaDenialRain = true;

        else if(_ending01 == "heart" && _ending02 == "tear" && _ending03 == "heart")
            novaAcceptanceRain = true;

        else if(_ending01 == "heart" && _ending02 == "heart" && _ending03 == "heart")
            sunEnding = true;
        return;
    }

    void scene05RuffordCheck()
    {
        Debug.Log("In Scene 05. _ending01 = " + _ending01 + ", _ending03 = " + _ending03 + ", _ending04 = " + _ending04);
        
        if((_ending01 == "tear") && (_ending03 == "tear") && (_ending04 == "tear"))
            rainEnding = true;

        else if(_ending01 == "tear" && _ending03 == "heart" && _ending04 == "tear")
            ruffordDenialRain = true;

        else if(_ending01 == "tear" && _ending03 == "heart" && _ending04 == "heart")
            ruffordAcceptanceRain = true;

        else if(_ending01 == "heart" && _ending03 == "heart" && _ending04 == "heart")
            sunEnding = true;
        return;

    }

    void scene05NovaCheck()
    {
        Debug.Log("In Scene 05. _ending02 = " + _ending02 + ", _ending03 = " + _ending03 + ", _ending04 = " + _ending04);
        
        if((_ending02 == "tear") && (_ending03 == "tear") && (_ending04 == "tear"))
            rainEnding = true;

        else if(_ending02 == "tear" && _ending03 == "heart" && _ending04 == "tear")
            novaDenialRain = true;

        else if(_ending02 == "tear" && _ending03 == "heart" && _ending04 == "heart")
            novaAcceptanceRain = true;

        else if(_ending02 == "heart" && _ending03 == "heart" && _ending04 == "heart")
            sunEnding = true;
        return;
    }

    //Inventory has been checked, time to set the path
    public void setInkPath()
    {
        if (GameManager.gm)
        {
            Debug.Log("Checking scenes...");  
            if (GameManager.gm.currentLevel == "Scene01" || GameManager.gm.currentLevel == "Scene02")
                setPathReg();

            Debug.Log("Not scene 1 or 2. Calling story paths for 3");             
            if (GameManager.gm.currentLevel == "Scene03")
                setPath3();

            
            Debug.Log("Not scene 1 or 2 or 3. Calling story paths 4.");             
            if (GameManager.gm.currentLevel == "Scene04")
                setPath4();

            Debug.Log("Not scene 1 or 2 or 3 or 4. Calling story paths 5.");             
            if (GameManager.gm.currentLevel == "Scene05")
                setPath5();
        }
    }

    //Called on scene01 and scene02 automatically (only path available)
    void setPathReg()
    {
        currentStory.ChoosePathString("start");
        return;
    }

    void setPath3()
    {
        Debug.Log("Setting Path.");
        if(ruffordRain) { currentStory.ChoosePathString("RuffordRain"); return; }
        if(novaRain) { currentStory.ChoosePathString("NovaRain"); return; }
        if(rainEnding)  { currentStory.ChoosePathString("Rain"); return; }
        if(sunEnding) { currentStory.ChoosePathString("Sun"); return; }
    }

    void setPath4()
    {
        if(ruffordDenialRain) { currentStory.ChoosePathString("RuffordDenialRain"); return; }
        if(ruffordAcceptanceRain) { currentStory.ChoosePathString("RuffordAcceptanceRain"); return; }
        if(novaDenialRain) { currentStory.ChoosePathString("NovaDenialRain"); return; }
        if(novaAcceptanceRain) { currentStory.ChoosePathString("NovaAcceptanceRain"); return; }
        if(rainEnding) { currentStory.ChoosePathString("Rain"); return; }
        if(sunEnding) { currentStory.ChoosePathString("Sun"); return; }
    }

    void setPath5()
    {
        if(ruffordDenialRain) { currentStory.ChoosePathString("RuffordDenialRain"); return; }
        if(ruffordAcceptanceRain) { currentStory.ChoosePathString("RuffordAcceptanceRain"); return; }
        if(novaDenialRain) { currentStory.ChoosePathString("NovaDenialRain"); return; }
        if(novaAcceptanceRain) { currentStory.ChoosePathString("NovaAcceptanceRain"); return; }
        if(rainEnding) { currentStory.ChoosePathString("Rain"); return; }
        if(sunEnding) { currentStory.ChoosePathString("Sun"); return; }
    }

    //Reset all endings before setting path for Scene05
    public void resetEndings()
    {
        ruffordRain = false;
        novaRain = false;
        ruffordDenialRain = false;
        ruffordAcceptanceRain = false;
        novaDenialRain = false;
        novaAcceptanceRain = false;
        rainEnding = false;
        sunEnding = false;
    }
}

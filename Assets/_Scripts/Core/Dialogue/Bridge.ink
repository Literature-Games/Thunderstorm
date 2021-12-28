// Inventory Variables
LIST inventory = heart, tear

VAR MyInv = ()

VAR invCount = 0

VAR calculate = false

VAR char1 = "bridge"

VAR actorNum = 1 //player

// DEBUG MODE shortcuts
VAR DEBUG = false
{DEBUG:
    IN DEBUG MODE!
    *   [Beginning...] -> start
    *   [Empty...] -> empty
    *   [Full...] -> full
    
- else:
    //First diversion: where do we begin?
 -> start
}

/*-----------------------------------------------------------------------------
    funtions
-----------------------------------------------------------------------------*/
//check the items in the player's inventory first
EXTERNAL ItemCheck()

=== function ItemCheck() ===
    ~ invCount = 0
    


/*-----------------------------------------------------------------------------
    Start dialogue
-----------------------------------------------------------------------------*/

=== start ===
    { invCount == 0:
        -> empty
        
        -else:
            -> full
    }
    

    
=== empty ===
    I have nothing to stop the rain... #speaker: you #portrait: speaking #layout: middle
    ~ calculate = false
    -> END
    
=== full ===
    I can finally change the rain... #speaker: you #portrait: speaking #layout: middle
    //tell unity to start calculations after dialogue finishes
    ~ calculate = true
    -> END

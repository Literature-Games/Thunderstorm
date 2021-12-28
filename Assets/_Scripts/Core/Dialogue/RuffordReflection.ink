//Tags = [ Speaker, Portrait, Layout(background)]
//Layout types = [Left(Jake), Right(), Speaking(You Speaking), Thought(You Thinking)]
//Scene Decorations = [Phone]
//Emotions = [Annoyed, Neutral, Angry, Happy, Upset]

//Character variables
VAR char1 = "You"

//Inventory Items
VAR heart = false
VAR tear = false

/*
* Plays into which options are shown to the player
*/
//Full List of the endings so far
LIST ending = null 

VAR ending01 = null //Ending from Scene 1
VAR ending03 = null //Ending from Scene 3
VAR ending04 = null //Ending from Scene 4

//Story states:
VAR Done = false
VAR delima01Done = false
VAR delima02Done = false
VAR total = 0
VAR actorNum = 1 //Player
VAR stroyName = "RuffordReflection"

// DEBUG MODE shortcuts
VAR DEBUG = false
{DEBUG:
    IN DEBUG MODE!
    *   [Beginning...] -> Rain
    
- else:
    //First diversion: where do we begin?
 -> Rain
}


/*-----------------------------------------------------------------------------
    functions
-----------------------------------------------------------------------------*/

=== function setEndingValues(value1, value3, value4) ===
    ~ ending01 = value1
    ~ ending03 = value3
    ~ ending04 = value4

=== function resetDelimas() ===
    ~ delima01Done = false
    ~ delima02Done = false
    
=== function calculateTotal(int) ===
    ~ total += int  


/*-----------------------------------------------------------------------------
    Start dialogue
-----------------------------------------------------------------------------*/
=== Rain === 
~setEndingValues("tear", "tear", "tear")
    -> start

=== RuffordDenialRain === 
~setEndingValues("tear", "heart", "tear")
    -> start

=== RuffordAcceptanceRain === 
~setEndingValues("tear", "heart", "heart")
    -> start

=== Sun === 
~setEndingValues("heart", "heart", "heart")
    -> start
    
=== start ===
//Greeting
    {ending01 == "heart":
        Rufford was right that I shouldn't have pretended to be someone I'm not. #speaker: you #portrait: speaking #layout: center
        I was wrong and mistook the changes he was trying to point out that made everyone uncomfortable.
        
        -else:
        Rufford was just being a good friend and I pushed him away the moment he tried to help.
        I felt like I was so right about this situation until it was too late.
        }
        
    {ending03 == "heart":
        I confessed that I messed up.
        -else:
        I denied that it was my fault. I kept shutting him down and pointing fingers.
        }
        
    {ending04 == "heart":
        But I don't think it will fix our friendship enough if he doesn't even want to hang out like we used too. 
            ->END
        -else:
            Even after all of that, I still refused to appologize to him.
            -> END
        }

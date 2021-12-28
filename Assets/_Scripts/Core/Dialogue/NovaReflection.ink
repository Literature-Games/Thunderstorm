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

VAR ending02 = null //Ending from Scene 2
VAR ending03 = null //Ending from Scene 3
VAR ending04 = null //Ending from Scene 4

//Story states:
VAR Done = false
VAR delima01Done = false
VAR delima02Done = false
VAR total = 0
VAR actorNum = 1 //Player
VAR stroyName = "NovaReflection"

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

=== function setEndingValues(value2, value3, value4) ===
    ~ ending02 = value2
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

=== NovaDenialRain === 
~setEndingValues("heart", "tear", "tear")
    -> start
    
=== NovaAcceptanceRain === 
~setEndingValues("heart", "tear", "heart")
    -> start
    
=== Sun === 
~setEndingValues("heart", "heart", "heart")
    -> start
    
=== start ===
//Greeting
    {ending02 == "heart":
        Nova didn't disserve what I tried to do to her. I attempted to take away something valuable to her. #speaker: you #portrait: speaking #layout: middle
        
        -else:
        Nova had a right to call me out the way she did. I attempted to take away something valuable to her. I did lie. 
        }
        
    {ending03 == "heart":
        I confessed that I messed up.
        -else:
        I denied all of the accusations and shoved them onto someone else. I kept shutting her down whenever she made a point.
        }
        
    {ending04 == "heart":
        But I don't think our relationship will be the same as it once was. Even after I appologised, it didn't feel like enough.
            ->END
        -else:
            Even after all of that, I still refused to appologize to her.
            -> END
        }

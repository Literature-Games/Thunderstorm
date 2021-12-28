//Tags = [ Speaker, Portrait, Layout(background)]
//Layout types = [Left(Jake), Right(), Middle(You Speaking), Center(You Thinking)]
//Scene Decorations = [Phone]
//Emotions = [Annoyed, Neutral, Angry, Happy, Upset]

//Character variables
VAR char1 = "Jake"

//Inventory Items
VAR heart = false
VAR tear = false

/*
* Plays into which options are shown to the player
*/
//Full List of the endings so far
LIST ending = null 

VAR ending01 = null //Ending from Scene 1
VAR ending02 = null //Ending from Scene 2
VAR ending03 = null //Ending from Scene 3

//Story states:
VAR Done = false
VAR delima01Done = false
VAR delima02Done = false
VAR total = 0
VAR actorNum = 2 //Jake and Player

// DEBUG MODE shortcuts
VAR DEBUG = false
{DEBUG:
    IN DEBUG MODE!
    *   [Beginning...] -> Rain
    *   [First Delima...]   -> RuffordSituation
    *   [Second Delima...]   -> NovaSituation
    *   [Third Delima...]   -> Delima_03
    
- else:
    //First diversion: where do we begin?
 -> Rain
}

/*-----------------------------------------------------------------------------
    functions
-----------------------------------------------------------------------------*/

=== function setEndingValues(value1, value2, value3) ===
    ~ ending01 = value1
    ~ ending02 = value2
    ~ ending03 = value3

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
    - Gone... #speaker: Jake #portrait: neutral #layout: left
      Where have they all gone?
    +[Excuse me?]
        ... Hi ...
        
        ** [Sorry to disturb you.]
        <i> I feel that something needs to change. </i> #speaker: you #portrait: thinking #layout: center
            -> END
                
        ** [What's wrong?]
            Everyone is gone.#speaker: Jake #portrait: upset #layout: left
            
            {ending01 == "heart":
                Rufford told me to give him some time. #speaker: Jake #layout: left
                -else:
                Rufford won't answer my texts and refuses to meet up. #speaker: Jake #layout: left
            }
            
            {ending02 == "heart":
                I tried to call Nova, but the call gets ignored. #speaker: Jake #layout: left
                -else:
                I tried to call Nova, but the call goes straight to voicemail. #speaker: Jake #portrait: angry #layout: left
            }
            
                
            ***[ Do you want to talk about it? ]
            
            { ending03 == "heart":
                Yes please. That would help me think this through. #speaker: Jake #portrait: happy #layout: left
                -> chooseSituation
                
                - else:
                    No, just leave me alone. #speaker: Jake #portrait: angry #layout: left
                    <i> Jake shut me out. </i> #speaker: you #portrait: thinking #layout: center
                    ->denialEnding
            }
         
            
=== chooseSituation ===
    <i> He seems deeply upset. </i> #speaker: you #portrait: thinking #layout: center
    
    { delima01Done == false:
    
        +[Talk about Rufford]
            -> RuffordSituation
    }
     
    { delima02Done == false:   
        +[Talk about Nova]
            -> NovaSituation
    }
    
    {delima01Done == true && delima02Done == true:
        ->Delima_03
    }
    
  
/*
*    First Delima
*/

=== RuffordSituation ===
    {ending01 == "heart":
         I agreed to stop trying to make Nova comfortable in our group hangouts. #speaker: Jake #portrait: neutral #layout: left
         I tried to act more like the way I used to so Nova would get to know the <b> real </b> us. #portrait: upset
         It just didn't turn out the way I wanted it to.
         ~calculateTotal(1)

         - else: //tear
            I refused to act the way they wanted me too. #speaker: Jake #portrait: angry #layout: left
            I wanted to keep them happy.  #portrait: upset
            I wanted to keep Nova happy.
            ~calculateTotal(-1)
         }
         
        +[ Was it the right thing to do? ]
            I don't know. #speaker: Jake #portrait: upset #layout: left
            ~delima01Done = true
            -> chooseSituation
         
/*
*    Second Delima
*/

=== NovaSituation ===
    {ending02 == "heart":
        I stole from Nova. #speaker: Jake #portrait: neutral #layout: left
        The person who I wanted to change for.
        She was right. #portrait: upset
        I was a liar and a thief.
        ~calculateTotal(1)
        
        {ending01 == "heart":
            I promised Rufford that I would keep the changes to a minimum. #speaker: Jake #portrait: neutral #layout: left
            But I think the changes that I tried to keep were the ones I should have let go.
            ~calculateTotal(1)
            -else:
            I told Rufford that nothing was wrong with me changing. #speaker: Jake #portrait: neutral #layout: left
            I told him that he would just have to get used to it.
            Get used seeing Nova.
            I ended up losing her and my friends.
            ~calculateTotal(-1)
        }
        
        -else: //tear
        Nova accused me of stealing from her. #speaker: Jake #portrait: annoyed #layout: left
        I felt that everytime I turned around, she was glaring at me.
        I..
        I will admit that there were some secrets that I did keep from her. #portrait: neutral
        ~calculateTotal(-1)
        
        {ending01 == "heart":
            But those secrets showed the real me. #speaker: Jake #portrait: happy #layout: left
            I can't change the way that I am so fast.
            Loosing her meant keeping my friends.
            ~calculateTotal(1)
            
            -else: //tear
            Rufford didn't appreciate the changes I went through. #speaker: Jake #portrait: annoyed #layout: left
            He didn't want to get used to the new me.
            I don't blame him. #portrait: neutral
            It ended in me losing my friends and the person I tried to keep.
            ~calculateTotal(-1)
        }
    }
    
    //can be a heart or player opinion
    +[  Was it the right thing to do? ]
        I don't know. #speaker: Jake #portrait: upset #layout: left
        
    ~calculateTotal(1)
    ~delima02Done = true
    -> chooseSituation

/*
*    Third Delima
*/
=== Delima_03 ===
    <i> I can do no more. </i> #speaker: you #portrait: thinking #layout: center
    + [ I'll leave you be. ]
    {total > 0:
        -> confessionEnding
    }
    {total < 0:
        ->denialEnding
    }
    
    
=== confessionEnding ===
    Thank you. #speaker: Jake #portrait: happy #layout: left
    ... 
    <i> With this, the rain will change. </i> #speaker: you #portrait: speaking #layout: middle
    ~Done = true
    ~heart = true
    -> END

=== denialEnding ===
    Just go away. #speaker: Jake #portrait: angry
    ... 
    <i> With this, the rain will change. </i> #speaker: you #portrait: speaking #layout: middle
    ~Done = true
    ~tear = true
    ->END


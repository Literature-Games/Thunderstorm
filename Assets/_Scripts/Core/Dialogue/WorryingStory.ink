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

//Story states:
VAR Done = false
VAR rainStart = false
VAR ruffordRainStart = false
VAR novaRainStart = false
VAR sunStart = false
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


=== function setEndingValues(value1, value2) ===
    ~ ending01 = value1
    ~ ending02 = value2

=== function resetDelimas() ===
    ~ delima01Done = false
    ~ delima02Done = false
    
=== function calculateTotal(int) ===
    ~ total += int  


/*-----------------------------------------------------------------------------
    Start dialogue
-----------------------------------------------------------------------------*/
=== Rain === 
    ~rainStart = true
    ~setEndingValues("tear", "tear")
    -> start

=== RuffordRain === 
    ~ruffordRainStart = true
    ~setEndingValues("tear", "heart")
    -> start
    
=== NovaRain === 
    ~novaRainStart = true
    ~setEndingValues("heart", "tear")
    -> start
    
=== Sun === 
    ~sunStart = true
    ~setEndingValues("heart", "heart")
    -> start
    
=== start ===
//Greeting
    - I need to fix this. I can't just let all of this go to waste. #speaker: Jake #portrait: upset #layout: left
    *   [Excuse me?]
        Eh, oh hi. #portrait: neutral
        Um, do you need something right now?
        
        ** [Sorry to disturb you.]
        <i> I feel that something needs to change. </i> #speaker: you #portrait: thinking #layout: center
            -> END
                
        ** [What's wrong?]
            I.. I...#speaker: Jake #portrait: neutral #layout: left
            I messed up.
            {rainStart == true:
                I tried to please everyone and ended up hurting them all. #portrait: upset
            }
            {sunStart == true:
                I tried to please everyone at once and became too overbearing. #portrait: upset
            }
            {novaRainStart == true:
                I tried to please Rufford and ended up hurting Nova. #portrait: upset
            }
            {ruffordRainStart:
                I tried to please Nova and ended up hurting Rufford. #portrait: upset
            }
            
            {ending01 == "heart":
                Rufford said he needed some time to think over everything I threw at him. 
                -else:
                Rufford said I'm no longer the same person. I am. #portrait: angry
                We've known each other for years. So what if I want to try something new. #portrait: neutral
            }
            
            {ending02 == "heart":
                Nova said she needs some time to herself.
                She said that I tried to lie and steal from her and damaged any of her trust in me.
                -else:
                Nova says she can't trust me anymore. I don't understand why. #portrait: annoyed
            }
           
            -> chooseSituation
            
            
=== chooseSituation ===
    <i> He doesn't sound like he is thinking clearly. </i> #speaker: you #portrait: thinking #layout: center
    
    { delima01Done == false:
    
        +[Talk about Rufford] #speaker: you #portrait: speaking
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
    I don't understand why this happened. #speaker: Jake #portrait: annoyed #layout: left
    
    +[Why do you think Rufford sees you differently than before?] 
        He stated that I was forcing myself to change for Nova. #portrait: neutral
        That every time they saw me with her, I would just automatically agree to something as if I were a robot.
         
         //can be a heart or player opinion
        { ending01 == "tear":
        ++[I don't think you ever did.]
            See! So I'm not in the wrong.
            <i>Did I give the right answer? </i> #speaker: you #portrait: thinking #layout: center
            ~ calculateTotal(-1)
            ~delima01Done = true
            -> chooseSituation
        }
        
        ++[Did you?]
            No!
            At least, to me no.
             <i>Did I give the right answer? </i> #speaker: you #portrait: thinking #layout: center
             ~ calculateTotal(1)
             ~delima01Done = true
            -> chooseSituation
/*
*    Second Delima
*/

=== NovaSituation ===
    <i> Why do you think Nova doesn't trust you? </i> #speaker: you #portrait: speaking #layout: middle
    
    She said I was a liar and a thief. #speaker: Jake #portrait: annoyed #layout: left
    She said she couldn't stand to look at me anymore because it felt like I kept hiding secrets.
    
    {ending02 == "tear":
    +[ I don't recall you stealing anything. ]
        That's because I didn't. #portrait: angry #layout: left
        I don't know where these accusations are coming from. 
        ~calculateTotal(-1)
        ~delima02Done = true
        -> chooseSituation
    }
    
    //can be a heart or player opinion
    +[ She's right. ]
        So you are agreeing with her? #portrait: angry #layout: left
        <b> You </b> believe I stole from the person I showed trust to? 
        
    ~calculateTotal(1)
    ~delima02Done = true
    -> chooseSituation

/*
*    Third Delima
*/
=== Delima_03 ===
    <i> Hmmm </i> #speaker: you #portrait: speaking #layout: middle
    
    {total > 0:
    + [ No, this isn't right. ]
        -> confessionEnding
        
    -else:
        
    + [ You are right. ]
        -> denialEnding
    }
    
    
=== confessionEnding ===
    I... I.. #speaker: Jake #portrait: upset #layout: left
    I messed up?
    I just wanted to make everyone happy.
    ... #speaker: Jake #portrait: neutral
    <i> With this, the rain will change. </i> #speaker: you #portrait: thinking #layout: center
    ~Done = true
    ~heart = true
    -> END

=== denialEnding ===
    You're right. #speaker: Jake #portrait: neutral #layout: left
    I didn't mess anything up. #portrait: angry
    They are at fault. #portrait: neutral
    ... #speaker: Jake
    <i> With this, the rain will change. </i> #speaker: you #portrait: thinking #layout: center
    ~Done = true
    ~tear = true
    ->END


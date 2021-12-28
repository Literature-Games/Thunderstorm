// Tags = [ Speaker, Portrait, Layout(background)]
//Layout types = [Left(Jake), Right(Nova), Middle(You Speaking), Center(You Thinking)]
//Scene Decorations = [Car, Basket]
//Emotions = [Annoyed, Neutral, Angry, Happy]

//Character variables
VAR char1 = "Jake"
VAR char2 = "Nova"

//Inventory Items
VAR heart = false
VAR tear = false

//Story states:
VAR Done = false
VAR listenToNova = false
VAR listenToJake = false
VAR actorNum = 3 //Jake, Nova, and Player

// DEBUG MODE shortcuts
VAR DEBUG = false
{DEBUG:
    IN DEBUG MODE!
    *   [Beginning...] -> start
    *   [First Delima...]   -> Delima_01
    *   [Second Delima...]   -> Delima_02
    *   [Third Delima...]   -> Delima_03
    
- else:
    //First diversion: where do we begin?
 -> start
}

/*-----------------------------------------------------------------------------
    functions
-----------------------------------------------------------------------------*/

=== function resetListen() ===
    ~ listenToNova = false
    ~ listenToJake = false


/*-----------------------------------------------------------------------------
    Start dialogue
-----------------------------------------------------------------------------*/

=== start ===
//Greeting
    -Then where could it have gone off to? #speaker: Jake #portrait: annoyed #layout: left
        I don't know! I left it in my bag and now it's gone! #speaker: Nova #portrait: angry #layout: right
        Alright, then maybe we can just … replace it? #speaker: Jake #portrait: neutral #layout: left
        Jake! That was an heirloom that has been in my family for generations. It cannot be replaced! #speaker: Nova #layout: right
        I mean...people lose stuff close to them all the time? #speaker: Jake #layout: left
        JAKE! #speaker: Nova #portrait: angry #layout: right
    
        * [Excuse Me?]
        Huh? Oh, I didn't see you walk up. #speaker: Jake #portrait: neutral #layout: left
            Hmm... Hey... #speaker: Nova #portrait: neutral #layout: right
                
            * * [Sorry to disturb you.]
                <i> I feel that something needs to change. </i> #speaker: you #portrait: thinking #layout: center
                    -> END
                        
            * * [What's wrong?]
                Nova accidentally misplac... #speaker: Jake #layout: left
                A necklace that was <b><i> passed down </i></b> to me went missing. #speaker: Nova #portrait: annoyed #layout: right
                I searched the area and still can't find it. I just got the chain replaced. #portrait: neutral
                        
                * * * [Do you have a clue to where it might be or who could have taken it?]
                    Yes. Jake?  #speaker: Nova #portrait: angry #layout: right
                    Wait, you think I took it?  #speaker: Jake #portrait: angry #layout: left
                            -> Delima_01
                
  
/*
*    First Delima
*/
    
=== Delima_01 ===
        {listenToNova == false || listenToJake == false:
        <i> I should listen to both of them. </i> #speaker: you #portrait: thinking  #layout: center
            }
        
        { listenToNova == false:
            + [Listen to Nova]
                -> NovaArgument
            }
        
        { listenToJake == false:
            + [Listen to Jake]
                -> JakeDefence
            }
            
        {listenToNova == true && listenToJake == true:
            <i> The air is stiff. Will this affect the rain? </i> #speaker: you #layout: center
            -> middle
            }
 
 
=== NovaArgument ===
    ~ listenToNova = true
    <i> What makes you believe Jake took it? </i> #speaker: you #portrait: speaking #layout: middle
    After he came back from the car with our picnic supplies, I walked away for a few minutes to visit the restroom. #speaker: nova #portrait: neutral  #layout: right
    When I left, the necklace was in the small compartment <b> inside </b> of my bag.
    No one else here <b> would know </b> that. #portrait: angry
    So you believe that I would want to steal from you? #speaker: Jake #portrait: annoyed #layout: left
    I think you took it out somewhere and left it.
    
    I didn't leave it anywhere else. #speaker: Nova #layout: right
    -> Delima_01   
    
=== JakeDefence ===
    ~ listenToJake = true
    <i> Why would Nova believe you stole from her? </i> #speaker: you #layout: middle
    I understand that you left your bag here when you went to the restroom. #speaker: Jake #portrait: neutral #layout: left
    However! I never went <b> through </b> it. #portrait: annoyed
    And no one came near our blanket while I was setting up. #portrait: neutral
    You saw me just <b> finishing </b> up when you came back.
    When would I have had time to <b> look through </b> your bag and <b> take </b> this? #portrait: annoyed
    Was anything in your bag moved from the last time you dug through it? #portrait: neutral
    The items were going to be moving around as I dug through it anyway. #speaker: Nova #portrait: neutral #layout: right
    But still? #speaker: Jake #portrait: neutral #layout: left
    Hmm... No. #speaker: Nova #portrait: neutral #layout: right
    
    See! So I didn't take it. #speaker: Jake #portrait: happy #layout: left
    -> Delima_01

/*
*    Second Delima
*/
=== middle ===
    ~resetListen()
    Your bag was sitting in the same spot and position as when you left.  #speaker: Jake #portrait: neutral #layout: left
    If I needed something out of your bag, I would have asked first.
    Mmm... normally yes. #speaker: Nova #portrait: annoyed #layout: right
    But... but... you were the only other person here!
    So that automatically makes me a target? #speaker: Jake #portrait: angry  #layout: left
    YES! #speaker: Nova #portrait: angry #layout: right
    
    -> Delima_02
    
=== Delima_02 ===
        {listenToNova == false || listenToJake == false:
            <i> I should listen to both of them. </i> #speaker: you #portrait: thinking #layout: center
            }
            
        { listenToJake == false:
            + [Listen to Jake]
                -> JakeArgument
            }
            
        { listenToNova == false:
            + [Listen to Nova]
                -> NovaDefence
            }
            
        {listenToNova == true && listenToJake == true:
            <i> The air is suffocating. How is the rain going to be able to stop? </i> #speaker: you #portrait: thinking #layout: center
            -> Delima_03
            }

 === JakeArgument ===
    ~ listenToJake = true
    <i> You think she is accusing you too much? </i> #speaker: you #portrait: speaking #layout: middle
    I understand that you're worried, but that doesn't mean you can go straight to accusing me! #speaker: Jake #portrait: neutral #layout: left
    I am willing to help you look for it since I can see that it's very important to you.
    The locket is small, so I feel like you could have left it without noticing.
    Jake, are you listening to what I'm saying? #speaker: Nova #portrait: annoyed #layout: right
    Yes, and I ask why would I want your necklace especially if it brings no value to me. #speaker: Jake #portrait: annoyed #layout: left
    I remind you that I was finishing setting up as you came back. I then took the time to use the restroom myself.
    You saw me walk the same direction you came from.
    But... #speaker: Nova #layout: right
    I didn't take it!!! #speaker: Jake #portrait: angry #layout: left
    -> Delima_02
    
=== NovaDefence ===
    ~ listenToNova = true
    <i> Why does he keep saying that you must have misplaced it? </i> #speaker: you #portrait: speaking #layout: middle
    How would I forget if I just said I took it to the shop for a chain replacement. #speaker: Nova #portrait: neutral #layout: right
    As in <b> today </b>, Jake. In fact, I just came from the shop before I came to meet you here.
    Then what makes you think that I would know it was in your bag?  #speaker: Jake #portrait: neutral #layout: left
    Because I remember talking about this a week ago with you.  #speaker: Nova #portrait: annoyed #layout: right
    A day or two prior to us making plans to meetup at the park.
    Plus you had enough time from when I left. And, you left to the bathroom right as I got back.
    -> Delima_02


/*
*    Third Delima
*/
=== Delima_03 ===
    <i> They're both on edge. Who do I believe more? </i> #speaker: you #portrait: thinking #layout: center
    
    + [ I believe Nova. Sorry Jake. ]
        -> JakeConfess
    + [ I believe Jake. Sorry Nova. ]
        -> NovaAccepts
    
    
=== NovaAccepts ===
    Why would I lie about my necklace disappearing? #speaker: Nova #portrait: neutral #layout: right
    I didn't say it did not disappear, I said I didn't take it. #speaker: Jake #portrait: neutral #layout: left
    You could have left it at home or even...
    I didn't leave it anywhere but in my bag! #speaker: Nova #portrait: angry #layout: right
    No one went in your bag! #speaker: Jake #portrait: angry #layout: left
    Look, like I said earlier I can help you look for it after we eat. #portrait: neutral
    Let's take the time to think and retrace your steps and stop accusing each other.
    Fine... I'm sorry for accusing you Jake. #speaker: Nova #portrait: annoyed #layout: right
    Thank you! #speaker: Jake #portrait: happy #layout: left
    Honestly, why would I want to steal your necklace? #portrait: neutral
    If we can't find it and you want to, we can get you a new one.
    No. I'd rather just forget about it then. #speaker: Nova #portrait: neutral #layout: right
    -> denialEnding
    
=== JakeConfess ===
    Okay! #speaker: Jake #portrait: angry #layout: left
    I took it out of your bag and brought it to the car when I went to the bathroom after I finished setting up. #portrait: neutral
    ... #speaker: Nova #portrait: neutral #layout: right
    I didn't think that it mattered to you anymore. #speaker: Jake #layout: left
    You haven't worn it for over a month and a half. I had thought... <b> or hoped </b>... you did not notice its absence this quickly.
    ... #speaker: Nova #layout: right
    ...Nova? #speaker: Jake #portrait: neutral #layout: left
    ...Not notice that quickly... #speaker: Nova #portrait: annoyed #layout: right
    I wear this necklace <b> all the time </b> Jake.
    I remember being excited to tell you that I finally had the time and money to get it fixed and cleaned.
    What did you even plan to do with it?
    ... #speaker: Jake #layout: left
    ... #speaker: Nova #portrait: angry #layout: right
    Heh, just give it back so I can go home. #portrait: neutral
    .. okay, follow me to the car. I'll clean up the picnic after. #speaker: Jake #layout: left
    -> confessionEnding
    
=== confessionEnding ===
    ... #speaker: Nova #layout: right
    ... #speaker: Jake #layout: left
    <i> With this, the rain will change. </i> #speaker: you #portrait: thinking #layout: center
    ~Done = true
    ~heart = true
    -> END

=== denialEnding ===
    ... #speaker: Nova
    ... #speaker: Jake
    <i> With this, the rain will change. </i> #speaker: you #portrait: thinking #layout: center
    ~Done = true
    ~tear = true
    ->END

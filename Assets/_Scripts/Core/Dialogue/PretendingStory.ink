// Tags = [ Speaker, Portrait, Layout(background)]
//Layout types = [Left(Jake), Right(Rufford), Middle(You Speaking), Center(You Thinking)]
//Scene Decorations = [Person, Calendar, Mirror]
//Emotions = [Annoyed, Neutral, Angry, Happy]

//Character variables
VAR char1 = "Jake"
VAR char2 = "Rufford"

//Inventory Items
VAR heart = false
VAR tear = false

//Story states:
VAR Done = false
VAR listenToRufford = false
VAR listenToJake = false
VAR actorNum = 3 //Jake, Rufford, and Player

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
    ~ listenToRufford = false
    ~ listenToJake = false


/*-----------------------------------------------------------------------------
    Start dialogue
-----------------------------------------------------------------------------*/

=== start ===
//Greeting
    -I'm just worried about you. I understand you’re happy but this is just too many changes at once. #speaker: Rufford #portrait: neutral #layout: right
    How does me picking up new interests and hobbies affect others when we hangout? #speaker: Jake #portrait: neutral #layout: left
    It affects others when you start saying no to the majority of the hobbies we used to do together and start suggesting ones you know neither of us will enjoy. #speaker: Rufford #portrait: annoyed #layout: right
    
    * [Excuse Me?]
    Oh, hi. #speaker:Jake #portrait: neutral #portrait: neutral #layout: left
    Huh... Hello... #speaker: Rufford #portrait: neutral #layout: right
            
    ** [Sorry to disturb you.]
        <i> I feel that something needs to change. </i> #speaker: you #portrait: thinking #layout: center
        -> END
                
    ** [What's wrong?]
        Ever since Jake started dating Nova, he's changed. #speaker: Rufford #portrait: annoyed #layout: right
        A good change! #speaker: Jake #portrait: happy  #layout: left
        No. Jake, no. Not a <b> completely</b> good change. #speaker: Rufford #portrait: angry #layout: right
        -> Delima_01
  
/*
*    First Delima
*/
    
=== Delima_01 ===
        {listenToRufford == false || listenToJake == false:
        <i> I should listen to both of them. </i> #speaker: you #portrait: thinking  #layout: center
            }
        
        { listenToRufford == false:
            + [Listen to Rufford]
                -> RuffordArgument
            }
        
        { listenToJake == false:
            + [Listen to Jake]
                -> JakeDefence
            }
            
        {listenToRufford== true && listenToJake == true:
            <i> The air is stiff. Will this affect the rain? </i> #speaker: you #portrait: thinking #layout: center
            -> middle
            }
 
 
=== RuffordArgument ===
    ~ listenToRufford = true
    <i> Why do you think Jake changing is a bad thing? </i> #speaker: you #portrait: speaking  #layout: middle
    Okay, I don't see it as entirely horrible that you changed. #speaker: Rufford #portrait: neutral #layout: right
    I'll admit that I actually appreciate some of the changes.
    I don't appreciate when you try to shove some of the less favorable onto someone else for Nova's sake. #portrait: annoyed
    I understand that it may have been a bit awkward when you introduced her to the group, but after the first few outings, this is getting ridiculous. #portrait: neutral
    Even some of this stuff you used to despise and now...
    You can become interested in something when the right person shows you it in another light. #speaker: Jake #portrait: annoyed #layout: left
    Yes, but not enough to seem obsessed with it like it was your favorite and only thing in the world. #speaker: Rufford #portrait: annoyed #layout: right
    -> Delima_01   
    
=== JakeDefence ===
    ~ listenToJake = true
    <i> Why does he believe you have changed? </i> #speaker: you #portrait: speaking #layout: middle
    I understand that the hobbies may seem strange to the group since we've known each other for so long. #speaker: Jake #portrait: neutral #layout: left
    But, it's gonna happen. Nova and I spend a lot of time together. #portrait: happy
    I figured if I tried to share the topics with some of you, then we might find things we all might like. That way it feels less forced when we hangout together. #portrait: neutral
    -> Delima_01

/*
*    Second Delima
*/
=== middle ===
    ~resetListen()
    Hm, I still don't know. #speaker: Rufford #portrait: neutral #layout: right
    
    
    -> Delima_02
    
=== Delima_02 ===
        {listenToRufford == false || listenToJake == false:
            <i> I should listen to both of them. </i> #speaker: you #portrait: thinking #layout: center
            }
            
        { listenToJake == false:
            + [Listen to Jake]
                -> JakeArgument
            }
            
        { listenToRufford == false:
            + [Listen to Rufford]
                -> RuffordDefence
            }
            
        {listenToRufford == true && listenToJake == true:
            <i> The air is suffocating. How is the rain going to be able to stop? </i> #speaker: you #portrait: thinking #layout: center
            -> Delima_03
            }

 === JakeArgument ===
    ~ listenToJake = true
    <i> Do you feel that you have taken this too far? </i> #speaker: you #portrait: speaking #layout: middle
    I mean, the rest of the group can start speaking up. #speaker: Jake #portrait: annoyed #layout: left
    No one else tries to recommend new things for us to do when we bring others with us.
    
    
    -> Delima_02
    
=== RuffordDefence ===
    ~ listenToRufford = true
    <i> What if he feels comfortable now?</i> #speaker: you #portrait: speaking #layout: middle
    
    -> Delima_02


/*
*    Third Delima
*/
=== Delima_03 ===
    <i> They're both on edge. Who do I believe more? </i> #speaker: you #portrait: thinking #layout: center
    
    + [ Ruffords right. Sorry Jake. ]
        -> JakeCaves
    + [ Jakes right. Sorry Rufford. ]
        -> RuffordCaves
    
    
=== RuffordCaves === 
    Alright, how about this? #speaker: Rufford #portrait: neutral #layout: right
    I am willing to look past this and try to convince the rest of the group to do the same <b>only</b> if you agree to stop pushing it.
    Agreed, to an extent. #speaker: Jake #portrait: happy #layout: left
    Jake. #speaker: Rufford #portrait: annoyed #layout: right
    Rufford, some of the changes are just me. I'm not gonna change the things I like about myself now. #speaker: Jake #portrait: neutral #layout: left
    I'm not asking you too. Just tone it down. #speaker: Rufford #portrait: neutral #layout: right
    Alright. #speaker: Jake #portrait: neutral #layout: left
    -> denialEnding
    
=== JakeCaves ===
    You're right, Rufford. #speaker: Jake #portrait: neutral #layout: left
    I do like some of the changes I have gone through but others feel like I am forcing myself to enjoy it.
    I'll try to tune it down and talk to Nova about our gatherings more. #portrait: happy
    That's all I'm asking for. #speaker: Rufford #portrait: happy #layout: right
    Like I said, some change is good, but not when it feels like you're someone completely new.
    
    -> confessionEnding
    
=== confessionEnding ===
    ... #speaker: Rufford  #layout: right
    ... #speaker: Jake  #layout: left
    <i> With this, the rain will change. </i> #speaker: you #portrait: thinking #layout: center
    ~Done = true
    ~heart = true
    -> END

=== denialEnding ===
    ... #speaker: Rufford  #layout: right
    ... #speaker: Jake  #layout: left
    <i> With this, the rain will change. </i> #speaker: you #portrait: thinking #layout: center
    ~Done = true
    ~tear = true
    ->END


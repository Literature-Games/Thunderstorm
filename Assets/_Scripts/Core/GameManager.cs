using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
 	public static GameManager gm;

    // levels to move to on victory and lose
	public string levelAfterVictory;
	[HideInInspector] public string currentLevel;
	[HideInInspector] public bool introTextPlaying = false;

    // game performance
    public int rainLevel = 0; //decide which icon the player earned in the end
    public int lives = 1;
	public int totalItems = 0;
	public int itemsEntered = 0;

    // UI elements to control
	[SerializeField] Animator introText;

    // private variables
	GameObject _player;
	Vector3 _spawnLocation;
	Scene _scene;

	// set things up here
	void Awake () {
		// setup reference to game manager
		if (gm == null)
			gm = this.GetComponent<GameManager>();

		// setup all the variables, the UI, and provide errors if things not setup properly.
		setupDefaults();
	}

	void start()
	{

	}

	// game loop
	void Update() {
		// if ESC pressed then pause the game
		if (InputManager.im.GetPausePressed()) {
			if (Time.timeScale > 0f) {
				PauseMenuManager.pm.OpenPause(); // this brings up the pause UI
				Time.timeScale = 0f; // this pauses the game action
			} else {
				Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)
				PauseMenuManager.pm.ClosePause(); // remove the pause UI
			}
		}
	}

	// setup all the variables, the UI, and provide errors if things not setup properly.
	void setupDefaults() {
		// setup reference to player
		if (_player == null)
			_player = GameObject.FindGameObjectWithTag("Player");
		
		if (_player==null)
			Debug.LogError("Player not found in Game Manager");

		// get current scene
		_scene = SceneManager.GetActiveScene();

		// get initial _spawnLocation based on initial position of player
		_spawnLocation = _player.transform.position;

		// if levels not specified, default to current level
		if (levelAfterVictory=="") {
			Debug.LogWarning("levelAfterVictory not specified, defaulted to current level");
			levelAfterVictory = _scene.name;
		}
		
		if(currentLevel=="") {
			currentLevel = _scene.name;
		}

		// get the UI ready for the game
		refreshGUI();
	}

	// refresh all the GUI elements
	void refreshGUI() {
		// turn on the appropriate number of life indicators in the UI based on the number of lives left
	}

	// public function to add points and update the gui and highscore player prefs accordingly
	public void AddPoints(int amount)
	{
		// increase score
		rainLevel+=amount;

		if (rainLevel < 0) rainLevel = 0;
		if(rainLevel > 1) rainLevel = 1;
	}

	// public function for level complete
	public void LevelCompete() {
		// use a coroutine to allow the player to get fanfare before moving to next level
		SceneTransition.st.LoadLevel(levelAfterVictory);
	}

	public void LoadText()
	{
		StartCoroutine(LoadNewText());
	}

	IEnumerator LoadNewText(){
		introTextPlaying = true;
		yield return new WaitForSeconds(1f);
		introText.SetTrigger("On");
		yield return new WaitForSeconds(4f);
		introText.SetTrigger("End");
		yield return new WaitForSeconds(1f);
		introTextPlaying = false;
	}
}

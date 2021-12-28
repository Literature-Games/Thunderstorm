using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{

    public static Bridge instance;
    public ItemID tear;
    public ItemID heart;

    //Allows each level to fluctuate the amount for each
    public float tearCost;
    public float heartCost;

    //Rain System
    [Header("Rain System")]
    public ParticleSystem bkRain;
    public ParticleSystem mainRain;

    [Header("SFX")]
	public AudioClip splashSFX;

    //private variables below
    Inventory _inventory;
    float total;
    AudioSource _audio;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one Ink Inventory in scene");
        }
        if(instance == null)
            instance = this.GetComponent<Bridge>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource> ();
		if (_audio==null) { // if AudioSource is missing
			Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
			// let's just add the AudioSource component dynamically
			_audio = gameObject.AddComponent<AudioSource>();
		}

        _inventory = Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exchange()
    {
        var bkRainEmission = bkRain.emission;
        var mainRainEmission = mainRain.emission;
        int index = _inventory.items.Count;
        ItemID currentItem;

        //Store Value
        if (GameManager.gm)
        {
            if(GameManager.gm.currentLevel != "Scene05")
            {
                currentItem = _inventory.items[0];
                if(InventoryPrefab.ip)
                    InventoryPrefab.ip.StoreValue(GameManager.gm.currentLevel, currentItem.name);
            }
        }

        //Exchange the tear/heart for rain
        for (int i = 0; i < index; i++)
        {
            currentItem = _inventory.items[i];
            if(currentItem.name == "heart")
            {
                total -= heartCost;
            }
            else if(currentItem.name == "tear")
            {
                total += tearCost;
            }
            _inventory.RemoveItems(_inventory.items[i]);
            PlaySound(splashSFX);
            bkRainEmission.rateOverTime = total;
            mainRainEmission.rateOverTime = total;
        }

        if(Controller2D.controller)
            Controller2D.controller.Complete();
    }

	// play sound through the audiosource on the gameobject
	void PlaySound(AudioClip clip)
	{
		_audio.PlayOneShot(clip);
	}
}

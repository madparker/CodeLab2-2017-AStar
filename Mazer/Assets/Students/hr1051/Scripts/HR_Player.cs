using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HR_Player : MonoBehaviour {
	
	private static HR_Player instance = null;

	//========================================================================
	public static HR_Player Instance {
		get { 
			return instance;
		}
	}

	void Awake () {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
	}
	//========================================================================
	[SerializeField] SpriteRenderer myHand;
	public bool haveSeed;
	[SerializeField] Sprite mySeedSprite;
	public bool haveNut;
	[SerializeField] Sprite myNutSprite;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PickUpSeed () {
		haveSeed = true;
		haveNut = false;
		myHand.sprite = mySeedSprite;
	}

	public void PickUpNut () {
		haveSeed = false;
		haveNut = true;
		myHand.sprite = myNutSprite;
	}

	public void Throw () {
		haveSeed = false;
		haveNut = false;
		myHand.sprite = null;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Play Sounds more times than once
public class JumpScareSoundMoreTimes : MonoBehaviour {

	public AudioClip SoundToPlay;
	public new AudioSource audio;
	//public bool alreadyPlayed = false;

	void Start()
	{

		audio = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D()
	{

		//if (!alreadyPlayed) {
			audio.PlayOneShot (SoundToPlay);
			//alreadyPlayed = true;
		//}
	}

	}
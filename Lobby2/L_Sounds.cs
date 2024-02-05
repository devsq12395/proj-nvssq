﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Sounds : MonoBehaviour {
	public static L_Sounds I;
	public void Awake(){ I = this; }

	public AudioSource musicSource, soundSource;

	public AudioClip snd_chatNotif, snd_clickBtn, snd_clickCursor, snd_matchFound;

	public void _start(){
		//if(PlayerPrefs.GetInt("opt_music") == 1)  _playMusic();
	}

	#region "SOUND"
	//MG_ControlSounds.I._playSound(8, 0, 0, false);
	public void _playSound(int audioNumber, int posX, int posY, bool checkPointRevealed = true){
		if(PlayerPrefs.GetInt("opt_sound") != 1)	return;

		if (checkPointRevealed && !MG_ControlFogOfWar.I._pointIsRevealed(posX, posY)) { 
			return;
		}

		Vector3 tempLoc = GameObject.Find("Main Camera").GetComponent<Camera>().transform.position;Debug.Log (audioNumber);
		switch(audioNumber){
			/*Chat Notif*/					case 1: soundSource.PlayOneShot(snd_chatNotif, 1); break;
			/*Click Button*/				case 2: soundSource.PlayOneShot(snd_clickBtn, 1); break;
			/*Click Cursor Move*/			case 3: soundSource.PlayOneShot(snd_clickCursor, 1); break;
			/*Match Found*/					case 4: soundSource.PlayOneShot(snd_matchFound, 1); break;
		}
	}
	#endregion

	#region "MUSIC"
	public AudioClip music1;
	public int curMusic, curMusic_past, randMusic;
	public float musicDur;
	public void _playMusic(){
		musicSource.priority = 0;
		curMusic = 1;
		musicSource.clip = music1; 
		musicDur = 188;
		musicSource.Play();
	}

	public void _stopMusic(){
		musicSource.Stop();
		MG_Globals.I.opt_music = false;
	}

	public void _musicPerFrame(float deltaTime){
		if(MG_Globals.I.opt_music){
			musicDur -= deltaTime;
			if(musicDur <= 0){
				_stopMusic();
				MG_Globals.I.opt_music = true;
				_playMusic();
			}
		}
	}
	#endregion
}
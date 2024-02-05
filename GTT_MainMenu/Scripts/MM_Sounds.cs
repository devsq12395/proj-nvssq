using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_Sounds : MonoBehaviour {
	public static MM_Sounds I;
	public void Awake(){ I = this; }

	public AudioSource musicSource, soundSource;

	public AudioClip snd_chatNotif, snd_clickBtn, snd_clickCursor, snd_buy;

	public void _start(){
		// if(PlayerPrefs.GetInt("opt_music") == 1)  _playMusic();
	}

	#region "SOUND"
	//MG_ControlSounds.I._playSound(8, 0, 0, false);
	public void _playSound(int audioNumber, int posX, int posY, bool checkPointRevealed = true){
		if(PlayerPrefs.GetInt("opt_sound") != 1)	return;

		if (checkPointRevealed && !MG_ControlFogOfWar.I._pointIsRevealed(posX, posY)) { 
			return;
		}

		Vector3 tempLoc = GameObject.Find("Main Camera").GetComponent<Camera>().transform.position;
		switch(audioNumber){
			/*Chat Notif*/					case 1: soundSource.PlayOneShot(snd_chatNotif, 1); break;
			/*Click Button*/				case 2: soundSource.PlayOneShot(snd_clickBtn, 1); break;
			/*Click Cursor Move*/			case 3: soundSource.PlayOneShot(snd_clickCursor, 1); break;
			/*Buy*/							case 4: soundSource.PlayOneShot(snd_buy, 1); break;
		}
	}
	#endregion

	#region "MUSIC"
	public AudioClip music1;
	public int curMusic, curMusic_past, randMusic;
	public float musicDur;
	public bool playMusic;
	public void _playMusic(){
		musicSource.priority = 0;
		curMusic = 1;
		musicSource.clip = music1; 
		musicDur = 188;
		musicSource.Play();
		playMusic = true;
	}

	public void _stopMusic(){
		musicSource.Stop();
		playMusic = false;
	}

	public void _musicPerFrame(float deltaTime){
		if(playMusic){
			musicDur -= deltaTime;
			if(musicDur <= 0){
				_stopMusic();
				_playMusic();
			}
		}
	}
	#endregion
}

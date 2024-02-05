using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlSounds : MonoBehaviour {
	public static MG_ControlSounds I;
	public void Awake(){ I = this; }

	public struct queSound{
		public float playTimer;
		public int soundNum;
		public int posX, posY;
		public bool checkPointRevealed, isPlayed;
	};
	public List<queSound> queSoundList;
	public int lastSoundPlayed = -1;
	public bool soundQued;

	public AudioSource musicSource, soundSource;

	public AudioClip snd_chatNotif, snd_clickBtn, snd_clickCursor, snd_heroDeath, snd_heroKill, snd_step, snd_buy, snd_gameWin, snd_gameLose, snd_pointCap, snd_pointLose, snd_turnEnd,
		snd_atkZap, snd_bowShoot, snd_crossbowHit, snd_crossbowShoot, snd_explode1, snd_explode2, snd_revolverShot, snd_revolverBurst, snd_heavyCharge, snd_hit01, snd_hit02, snd_holy01, snd_holy02, snd_nature01,
		snd_powerup, snd_spectre, snd_spectreAtk, snd_swordDance, snd_throw01, snd_warHorn, snd_windSlash, snd_arrowHit, snd_bulletHit01, snd_bulletHit02, snd_zap, snd_frost, snd_arBurst, snd_rifleSingle, snd_truckMove,
		snd_warStomp, snd_photonBomb, snd_deathArrowHit, snd_drainLife, snd_barrier, snd_unitKill, snd_error, snd_cannon, snd_cannonHit;

	public void _start(){
		if(MG_Globals.I.opt_music)  _playMusic();

		queSoundList = new List<queSound> ();
	}

	#region "SOUND"
	//MG_ControlSounds.I._playSound(8, 0, 0, false);
	public void _playSound(int audioNumber, int posX, int posY, bool checkPointRevealed = true){
		if(!MG_Globals.I.opt_sound)	return;

		#region "Que sound"
//		if (lastSoundPlayed == audioNumber && soundSource.isPlaying) {
//			if (!soundQued) {
//				queNewSound (audioNumber, posX, posY, checkPointRevealed);
//				soundQued = true;
//			} else {
//				return;
//			}
//		} else {
//			soundQued = false;
//		}
//		lastSoundPlayed = audioNumber;
		#endregion

		if (checkPointRevealed && !MG_ControlFogOfWar.I._pointIsRevealed(posX, posY)) { 
			return;
		}

		Vector3 tempLoc = GameObject.Find("Main Camera").GetComponent<Camera>().transform.position;
		switch(audioNumber){
			/*Chat Notif*/					case 1: soundSource.PlayOneShot(snd_chatNotif, 1); break;
			/*Click Button*/				case 2: soundSource.PlayOneShot(snd_clickBtn, 1); break;
			/*Click Cursor Move*/			case 3: soundSource.PlayOneShot(snd_clickCursor, 1); break;
			/*Hero Death*/					case 4: soundSource.PlayOneShot(snd_heroDeath, 1); break;
			/*Hero Kill*/					case 5: soundSource.PlayOneShot(snd_heroKill, 1); break;
			/*Step*/						case 6: soundSource.PlayOneShot(snd_step, 1); break;
			/*Buy*/							case 7: soundSource.PlayOneShot(snd_buy, 1); break;
			/*Game Win*/					case 8: soundSource.PlayOneShot(snd_gameWin, 1); break;
			/*Game Lose*/					case 9: soundSource.PlayOneShot(snd_gameLose, 1); break;
			/*Point Capture*/				case 10: soundSource.PlayOneShot(snd_pointCap, 1); break;
			/*Point Lost*/					case 11: soundSource.PlayOneShot(snd_pointLose, 1); break;
			/*Turn End*/					case 12: soundSource.PlayOneShot(snd_turnEnd, 1); break;

			/*Attack - zap*/				case 13: soundSource.PlayOneShot(snd_atkZap, 1); break;
			/*Attack - bow*/				case 14: soundSource.PlayOneShot(snd_bowShoot, 1); break;
			/*Crossbow hit*/				case 15: soundSource.PlayOneShot(snd_crossbowHit, 1); break;
			/*Crossbow shoot*/				case 16: soundSource.PlayOneShot(snd_crossbowShoot, 1); break;
			/*Explode 1*/					case 17: soundSource.PlayOneShot(snd_explode1, 1); break;
			/*Explode 2*/					case 18: soundSource.PlayOneShot(snd_explode2, 1); break;
			/*Revolver shoot*/				case 19: soundSource.PlayOneShot(snd_revolverShot, 1); break;
			/*Revolver burst*/				case 20: soundSource.PlayOneShot(snd_revolverBurst, 1); break;
			/*Heavy charge*/				case 21: soundSource.PlayOneShot(snd_heavyCharge, 1); break;
			/*Hit 01*/						case 22: soundSource.PlayOneShot(snd_hit01, 1); break;
			/*Hit 02*/						case 23: soundSource.PlayOneShot(snd_hit02, 1); break;
			/*Holy 01*/						case 24: soundSource.PlayOneShot(snd_holy01, 1); break;
			/*Holy 02*/						case 25: soundSource.PlayOneShot(snd_holy02, 1); break;
			/*Nature 01*/					case 26: soundSource.PlayOneShot(snd_nature01, 1); break;
			/*Powerup*/						case 27: soundSource.PlayOneShot(snd_powerup, 1); break;
			/*Spectre*/						case 28: soundSource.PlayOneShot(snd_spectre, 1); break;
			/*Spectre attack*/				case 29: soundSource.PlayOneShot(snd_spectreAtk, 1); break;
			/*Sword Dance*/					case 30: soundSource.PlayOneShot(snd_swordDance, 1); break;
			/*Throw 01*/					case 31: soundSource.PlayOneShot(snd_throw01, 1); break;
			/*War horn*/					case 32: soundSource.PlayOneShot(snd_warHorn, 1); break;
			/*Wind Slash*/					case 33: soundSource.PlayOneShot(snd_windSlash, 1); break;

			/*Arrow Hit*/					case 34: soundSource.PlayOneShot(snd_arrowHit, 1); break;
			/*Bullet Hit 01*/				case 35: soundSource.PlayOneShot(snd_bulletHit01, 1); break;
			/*Bullet Hit 02*/				case 36: soundSource.PlayOneShot(snd_bulletHit02, 1); break;
			/*Zap*/							case 37: soundSource.PlayOneShot(snd_zap, 1); break;

			/*Frost*/						case 38: soundSource.PlayOneShot(snd_frost, 1); break;

			/*GUN - AR Burst*/				case 40: soundSource.PlayOneShot(snd_arBurst, 1); break;
			/*GUN - Rifle Single*/			case 41: soundSource.PlayOneShot(snd_rifleSingle, 1); break;
			/*Truck Move*/					case 42: soundSource.PlayOneShot(snd_truckMove, 1); break;

			/*War Stomp*/					case 43: soundSource.PlayOneShot(snd_warStomp, 1); break;
			/*Photon Bomb*/					case 44: soundSource.PlayOneShot(snd_photonBomb, 1); break;
			/*Death Arrow Hit*/				case 45: soundSource.PlayOneShot(snd_deathArrowHit, 1); break;
			/*Drain Life*/					case 46: soundSource.PlayOneShot(snd_drainLife, 1); break;

			/*Barrier*/						case 47: soundSource.PlayOneShot(snd_barrier, 1); break;
			/*Unit Kill*/					case 48: soundSource.PlayOneShot(snd_unitKill, 1); break;

			/*Error*/						case 49: soundSource.PlayOneShot(snd_error, 1); break;

			/*Cannon*/						case 50: soundSource.PlayOneShot(snd_cannon, 1); break;
			/*Cannon Hit*/					case 51: soundSource.PlayOneShot(snd_cannonHit, 1); break;
		}
	}

	public void queNewSound(int soundNum, int posX, int posY, bool checkPointRevealed){
		queSound newSound = new queSound ();
		newSound.playTimer = 0.5f;
		newSound.soundNum = soundNum;
		newSound.posX = posX;
		newSound.posY = posY;
		newSound.checkPointRevealed = checkPointRevealed;
		newSound.isPlayed = false;

		queSoundList.Add (newSound);
	}

	public void _update(float deltaTime){
		queSound tempS;
		for (var i = 0; i < queSoundList.Count; i++) {
			tempS = queSoundList [i];
			tempS.playTimer -= deltaTime;
			if(tempS.playTimer <= 0 && !tempS.isPlayed){
				tempS.isPlayed = true;
				_playSound (tempS.soundNum, tempS.posX, tempS.posY, tempS.checkPointRevealed);
			}
			queSoundList [i] = tempS;
		}

		for (var i = queSoundList.Count - 1; i >= 0; i--) {
			if (queSoundList [i].isPlayed) {
				queSoundList.RemoveAt (i);
			}
		}
	}
	#endregion

	#region "MUSIC"
	public AudioClip music1, music2, music3, music4;
	public int curMusic, curMusic_past, randMusic;
	public float musicDur;
	public void _playMusic(){
		musicSource.priority = 0;
		while(curMusic == curMusic_past){
			randMusic = Random.Range(1, 20);
			if(randMusic <= 5)			curMusic = 1;
			else if(randMusic <= 10)	curMusic = 2;
			else if(randMusic <= 15)	curMusic = 3;
			else 						curMusic = 4;
		}
		curMusic_past = curMusic;

		switch(curMusic){
			case 1: musicSource.clip = music1; musicDur = 188; break;
			case 2: musicSource.clip = music2; musicDur = 158; break;
			case 3: musicSource.clip = music3; musicDur = 136; break;
			case 4: musicSource.clip = music4; musicDur = 118; break;

		}
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlAI : MonoBehaviour {
	public static MG_ControlAI I;
	public void Awake(){ I = this; }

	public bool DEBUG_MODE;

	struct pointObj{
		public Vector2 pos;
		public int index;

		public pointObj(Vector2 newPoint, int curIndex){
			pos = newPoint;
			index = curIndex;
		}
	}

	public bool isON;
	public float actionCD;
	public MG_ClassUnit current;
	public int ai_player;
	public Vector2 target;

	public void _setup(){
		//Variables
		isON 						= (PlayerPrefs.GetInt("isMultiplayer") == 1) ? false : true;
		DEBUG_MODE 					= false;
		ai_player 					= 2;

		if(isON){
			target = new Vector2 (0, 0);
			
			AI__General.I._start();

			startDeck ();
		}
	}

	#region "Waypoint generator"
	public void createAutoWaypoint(MG_ClassUnit unit, Vector2 targetPoint){
		Debug.Log (unit.posX.ToString() + ", " + unit.posY.ToString());
		Debug.Log (targetPoint);
		bool touchdown = false;
		int curIndex = 0;
		List<pointObj> allObj = new List<pointObj> ();
		List<pointObj> curObj = new List<pointObj> ();
		List<pointObj> newObj = new List<pointObj> ();
		List<pointObj> finalObj = new List<pointObj> ();
		List<Vector2> addedPoints = new List<Vector2> ();
		pointObj endObj = new pointObj(new Vector2(0,0), 0);
		int loopBreaker = 10000;

		void addObj(Vector2 newPoint){//Debug.Log(newPoint);
			if(!MG_GetDoodad.I._pointHasColliderDoodad ((int)newPoint.x, (int)newPoint.y)
				&& (int)newPoint.x < MG_Globals.I.mapLimitX && (int)newPoint.x > -MG_Globals.I.mapLimitX
				&& (int)newPoint.y < MG_Globals.I.mapLimitY && (int)newPoint.y > -MG_Globals.I.mapLimitY
				&& (!addedPoints.Contains(newPoint))
			){
				pointObj obj = new pointObj (newPoint, curIndex);
				allObj.Add (obj);
				newObj.Add (obj);
				addedPoints.Add (newPoint);
				//MG_ControlDoodad.I._createDoodad("treeWinter_001", (int)newPoint.x, (int)newPoint.y, newPoint.x/2, newPoint.y/2);

				if(newPoint == targetPoint){
					touchdown = true;
					endObj = obj;
				}
			}
		}
		void startNewBatch(){
			curObj.Clear();
			curObj.AddRange (newObj);
			newObj.Clear ();
			curIndex++;
		}

		Debug.Log("Starting...");
		addObj (new Vector2(unit.posX, unit.posY));
		startNewBatch ();
		while (!touchdown) {
			foreach(pointObj o in curObj){
				addObj (new Vector2((int)o.pos.x + 1, (int)o.pos.y));
				addObj (new Vector2((int)o.pos.x - 1, (int)o.pos.y));
				addObj (new Vector2((int)o.pos.x, (int)o.pos.y + 1));
				addObj (new Vector2((int)o.pos.x, (int)o.pos.y - 1));

				loopBreaker--;
				if(loopBreaker <= 0){
					break;
				}
			}

			if(touchdown){
				break;
			}
			startNewBatch ();

			loopBreaker--;
			if(loopBreaker <= 0){
				break;
			}
		}

		if(touchdown){
			finalObj.Add(endObj);
			loopBreaker = 10000;
			while(curIndex > 0){
				finalObj.Add(findNextTile(curIndex, finalObj[finalObj.Count-1]));
				curIndex--;

				loopBreaker--;
				if(loopBreaker <= 0){
					break;
				}
			}
		}else{
			Debug.Log("Cannot get to target destination, returning...");
			return;
		}

		pointObj findNextTile(int newIndex, pointObj p){
			pointObj retVal = allObj[0];
			foreach(pointObj o in allObj){
				if(o.index == newIndex 
					&& MG_CALC_Distance.I._distBetweenPoints((int)o.pos.x, (int)o.pos.y, (int)p.pos.x, (int)p.pos.y) <= 1){
					retVal = o;
					break;
				}
			}
			return retVal;
		}

		unit.waypoints.Add(endObj.pos);
		int waypointLoop = 0;
		foreach(pointObj o in finalObj){
			// Add waypoint every 4 tile
			waypointLoop++;
			if(waypointLoop >= 4){
				unit.waypoints.Insert(0, o.pos);
				waypointLoop = 0;
			}
		}
	}
	#endregion
	#region "AI Deck Control"
	public List<string> deck;

	public void startDeck(){
		if (DEBUG_MODE) Debug.Log ("Generating AI deck...");
		/* for (int i = 1; i <= 24; i++) {
			deck.Add (PlayerPrefs.GetString("cardNameAI_" + i.ToString()));

			if (DEBUG_MODE) Debug.Log ("AI Added card to deck: " + deck[i-1]);
		} */
		int presetDeck = -1;
		switch(MG_Globals.I.curMap){
			case "Frozen Sanctuary": presetDeck = 1; break;
			case "Stories02_mis01": presetDeck = 2; break;
			case "Stories01_mis03": presetDeck = 3; break;
		}
		AI__General_BuildOrder.I.generateCardDeck(presetDeck);

		shuffleDeck ();
	}

	public void shuffleDeck() {
		if (DEBUG_MODE) Debug.Log ("Shuffling deck...");

		// Shuffle the deck
		for (int i = 0; i < deck.Count; i++) {
			string temp = deck[i];
			int randomIndex = Random.Range(i, deck.Count);
			deck[i] = deck[randomIndex];
			deck[randomIndex] = temp;
		}
	}

	public void throwCardToBottom(int cardUsed){
		// Bring the used card to bottom of the deck
		string temp = deck [cardUsed], temp2 = "";
		for (int i = deck.Count - 1; i >= cardUsed; i--) {
			temp2 = deck [i];
			deck [i] = temp;
			temp = temp2;
		}
	}

	public void AI_createUnit(string name, int posX, int posY, int playerOwner){
		MG_ControlUnits.I._createUnit (name, posX, posY, playerOwner);
	}
	#endregion

	public void _startTurn(){
		//Disables if AI is not turned on.
		if(!isON)	return;

		//Puts a CD and pause before starting the turn
		actionCD = 2f;
	}

	public void _update(float deltaTime){
		// Only for AI player
		if(MG_Globals.I.curPlayerOnTurn != ai_player || !isON)	return;

		// Pause AI if a UI is moving (mainly the turn changer)
		if(MG_Globals.I.pause_uiMove)				return;

		//AI's cooldown from doing consecutive actions
		if(actionCD > 0){
			actionCD -= deltaTime;
			return;
		}

		//Gets a unit from his units as the AI's current piece to play
		bool hasUnitToMove = false;
		foreach(MG_ClassUnit uLoop in MG_Globals.I.units){
			if(uLoop.playerOwner == 2 && !MG_ControlBuffs.I._unitIsDisabled(uLoop) && uLoop.name != "PointRed"){
				if(MG_ControlBuffs.I._unitHasBuff_returnStack(uLoop, "aiControl") < 5){
					MG_ControlBuffs.I._addBuff (uLoop, "aiControl");
					current = uLoop;
					if(DEBUG_MODE) Debug.Log ("will run AI for unitID = " + uLoop.unitID + ", name = " + uLoop.name);

					hasUnitToMove = true;
					break;
				}
			}
		}

		// If no units available to move, end the turn
		if (!hasUnitToMove) {
			MG_ControlPlayer.I._endTurn ();

			return;
		}

		//Figure out what the AI will do at this turn
		int aiActivity = MG_ControlBuffs.I._unitHasBuff_returnStack(current, "aiControl");
		if(DEBUG_MODE) Debug.Log ("current AI activity is = " + aiActivity);

		// Find a target
		int curHealth = 0, curDist = 0, lDist = 0;
		bool nearbyEnemyHero = false;
		if(current.isAHero && current.HP <= (int)((double)current.HPMax * 0.25)){
			// If injured, retreat
			target = new Vector2(-11, 0);
		}
		else{
			// If no nearby enemy heroes, target a nearby enemy camp
			foreach (MG_ClassUnit uLoop in MG_Globals.I.units) {
				if (MG_ControlPlayer.I._getIsEnemy (current.playerOwner, uLoop.playerOwner) && uLoop.isAHero && (MG_CALC_Distance.I._distBetweenPoints (current.posX, current.posY, uLoop.posX, uLoop.posY) <= 5)) {
					nearbyEnemyHero = true;
					target = new Vector2(uLoop.posX, uLoop.posY);
					break;
				}
			}
			if (!nearbyEnemyHero) {
				// Units with waypoints
				if (current.waypoints.Count > 0) {
					target = new Vector2(current.waypoints[0].x, current.waypoints[0].y);
				}

				// Units without waypoints
				else {
					foreach(MG_ClassUnit uLoop in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(current.playerOwner, uLoop.playerOwner) &&
							(uLoop.isAHero)){
							lDist = MG_CALC_Distance.I._distBetweenPoints (current.posX, current.posY, uLoop.posX, uLoop.posY);
							if(lDist < curDist || curDist == 0){
								target = new Vector2(uLoop.posX, uLoop.posY);
								curDist = lDist;
							}
						}
					}
				}
			}
		}

		if(current.isStationed){
			// If stationed, wait for an enemy to get close
			foreach(MG_ClassUnit uLoop in MG_Globals.I.units){
				if(MG_ControlPlayer.I._getIsEnemy(current.playerOwner, uLoop.playerOwner) && !current.isInvulnerable){
					lDist = MG_CALC_Distance.I._distBetweenPoints (current.posX, current.posY, uLoop.posX, uLoop.posY);
					if(lDist <= 5){
						current.isStationed = false;
						break;
					}
				}
			}
			MG_ControlAI.I.actionCD = 0.05f;
		}

		// Runs a sequence depending on the current piece's unit type
		switch(current.name){
			case "CampRed": AI_CampRed.I._runAI(current, target, aiActivity); break;
			case "BarracksRed": AI_Barracks.I._runAI(current, target, aiActivity); break;
			case "Tower": AI_Tower.I._runAI(current, target, aiActivity); break;
			case "Robin": AI_Robin.I._runAI(current, target, aiActivity); break;
			case "Ajax": AI_Ajax.I._runAI(current, target, aiActivity); break;
			case "Colt": AI_Colt.I._runAI(current, target, aiActivity); break;
			case "Victoria": AI_Victoria.I._runAI(current, target, aiActivity); break;
			case "Amy": AI_Amy.I._runAI(current, target, aiActivity); break;
			case "Alicia": AI_Alicia.I._runAI(current, target, aiActivity); break;
			case "ElderTreant": AI_ET.I._runAI(current, target, aiActivity); break;
			case "Ragnaros": AI_Ragnaros.I._runAI(current, target, aiActivity); break;
			case "Ifreet": AI_Ifreet.I._runAI(current, target, aiActivity); break;
			case "SpectralWitch": AI_SpecWitch.I._runAI(current, target, aiActivity); break;
			case "Drakgul": AI_Drakgul.I._runAI(current, target, aiActivity); break;
			case "Masamune": AI_Masamune.I._runAI(current, target, aiActivity); break;
			case "Siegfried": AI_Siegfried.I._runAI(current, target, aiActivity); break;
			case "Abel": AI_Abel.I._runAI(current, target, aiActivity); break;
			case "Yukino": AI_Yukino.I._runAI(current, target, aiActivity); break;
			case "Cody": AI_Cody.I._runAI(current, target, aiActivity); break;
			case "Hilde": AI_Hilde.I._runAI(current, target, aiActivity); break;
			case "Elise": AI_Elise.I._runAI(current, target, aiActivity); break;
			case "EyeOfTheMagi": AI_EyeOfTheMagi.I._runAI(current, target, aiActivity); break;
			case "May": AI_May.I._runAI(current, target, aiActivity); break;
			case "Walter": AI_Walter.I._runAI(current, target, aiActivity); break;
			case "Aleric": AI_Aleric.I._runAI(current, target, aiActivity); break;
			case "Eve": AI_Eve.I._runAI(current, target, aiActivity); break;
			case "Apprentice": AI_Apprentice.I._runAI(current, target, aiActivity); break;
			case "Grenadier": AI_Grenadier.I._runAI(current, target, aiActivity); break;
			case "Medic": AI_Medic.I._runAI(current, target, aiActivity); break;
			case "HydraSpawn": AI_HydraSpawn.I._runAI(current, target, aiActivity); break;
			case "GiantTurtle":case "WarTurtle": AI_WarTurtle.I._runAI(current, target, aiActivity); break;

			case "Aleric(Boss)": AI__BOSS_Aleric.I._runAI(current, target, aiActivity); break;
			case "Ragnaros(Boss)": AI__BOSS_General.I._runAI(current, target, aiActivity); break;
			case "SpectralWitch(Boss)": AI__BOSS_General.I._runAI(current, target, aiActivity); break;
			case "Ifreet(Boss)": AI__BOSS_Ifreet.I._runAI(current, target, aiActivity); break;
			case "Abel(Boss)": AI__BOSS_General.I._runAI(current, target, aiActivity); break;
			case "Victoria(Boss)": AI__BOSS_General.I._runAI(current, target, aiActivity); break;
			case "Cody(Boss)": AI__BOSS_General.I._runAI(current, target, aiActivity); break;
				
			case "Commander": AI_Commander.I._runAI(current, target, aiActivity); break;
			case "Cannon": AI_Cannon.I._runAI(current, target, aiActivity); break;

			// NO AI
			case "Treasure":
				MG_ControlAI.I.actionCD = 0.05f;
			break;
				
			default:
				AI_Default.I._runAI(current, target, aiActivity);
			break;
		}
	}
}

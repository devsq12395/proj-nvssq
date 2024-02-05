using UnityEngine;
using System.Collections;

/*



	Colors available to be used by this system:

	Red, Green, Blue, Yellow, Purple

	Go to _translateSpecialSymbols inside the controller to add more.

	These database can also store special codes on the event when that dialog is triggered (ie. spawn a unit after a dialog)
*/

public class MG_DB_Dialog : MonoBehaviour {
	public static MG_DB_Dialog I;
	public void Awake(){ I = this; }

	public string speaker, main, portraitName, options1, options2, options3, options4;
	public bool hasContinuation, hasOptions, option1_isRed, option2_isRed, option3_isRed, option4_isRed;
	public int optnEfct1, optnEfct2, optnEfct3, optnEfct4;

	// Custom values are used to store strings to be used in dialog
	// To use, set these values first before calling P_ControlUI_Dialog.I._initDialog()
	public string cusValue1, cusValue2, cusValue3, cusValue4;

	public void _setupDialogText(int dialogNumber, string database = "current map"){
		int prof = PlayerPrefs.GetInt("CurProfile");
		if(database == "current map") database = PlayerPrefs.GetString("P_Map_" + prof.ToString());

		//Defaults for the next line
		hasOptions = false;
		hasContinuation = true;

		//Defaults for options
		options1 = "NONE"; options2 = "NONE"; options3 = "NONE"; options4 = "NONE";
		optnEfct1 = 0; optnEfct2 = 0; optnEfct3 = 0; optnEfct4 = 1;

		switch (database) {
			/*
			 * 	TYPES OF DIALOG:
			 *  continuation, endline, endline-with-options
			 */

			#region "TEST AND ESSENTIAL CODES"
			case "testMap001":
				//Test
				switch (dialogNumber) {
					case 1: // continuation
						speaker = " "; portraitName = "NONE"; 
						main = "Hello World!";
					break;
					case 2: // endline-with-options
						speaker = " ";
						portraitName = "NONE";
						hasContinuation = false;
						main = "Dialog system works perfect!";

						hasOptions = true;
						options1 = "Nice!";
						options2 = "Test effect 2";
						optnEfct1 = 1;
						optnEfct2 = 2;
					break;
				}
			break;
			#endregion
			#region "Stories01_mis01"
			case "Stories01_mis01":
				//Test
				switch (dialogNumber) {
					case 1:
						speaker = "Officer"; portraitName = "Officer"; main = "Princess, we will soon reach the village of Lenley. ";
					break;
					case 2:
						speaker = "Princess May"; portraitName = "May"; main = "Good work, but stay alert. Walter's men may be lurking and just waiting for an ambush.";
					break;
					case 3:
						speaker = "Aleric"; portraitName = "Aleric"; main = "Your hunt for your brother ends here, Princess May.";

						// DIALOG TRIGGERED SPECIAL CODE
						MG_ControlUnits.I._createUnit("Aleric(Boss)", 0, -4, 2);
						MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "11"});
						MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "Aleric Boss");

						MG_ControlUnits.I._createUnit("Swordsman", 1, -3, 2);
						MG_ControlUnits.I._createUnit("Swordsman", 1, -5, 2);
						MG_ControlUnits.I._createUnit("Rifleman", 3, -3, 2);
						MG_ControlUnits.I._createUnit("Rifleman", 3, -5, 2);
					break;
					case 4:
						speaker = "Princess May"; portraitName = "May"; main = "Aleric. So you're one of Walter's men now. I take it you're waiting for us here.";
					break;
					case 5:
						speaker = "Aleric"; portraitName = "Aleric"; main = "Nothing personal, Princess. This rebellion has been long overdue.";
					break;
					case 6:
						speaker = "Aleric"; portraitName = "Aleric"; main = "Prince Walter and the Dark Lord Bael shall give us and our people a new power and a better future.";
					break;
					case 7:
						speaker = "Princess May"; portraitName = "May"; main = "Walter has sold himself and his kingdom to Bael, Aleric!";
					break;
					case 8:
						speaker = "Princess May"; portraitName = "May"; main = "I will not let my kingdom fall to that demon. If you're willing to do so Aleric then so be it.";
					break;
					case 9:
						speaker = "Princess May"; portraitName = "May"; main = "Everyone, prepare for battle!";
					break;
					case 10:
						speaker = "OBJECTIVES:";
						portraitName = "";
						hasContinuation = false;
						main = "- Defeat Aleric\n- Protect your Camp";

						// DIALOG TRIGGERED SPECIAL CODE
						MG_GetUnit.I.us_getUnit ("Aleric Boss")._moveUnit(11, -4);
						MG_ControlSFX.I._createSFX("moveSmoke", 0, -4);
						MG_ControlCursor.I._moveCursor (-4/2, -4/2, true);
						MG_ControlCursor.I._interact (false);
						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"10", "12"});

						MG_ControlUnits.I._createUnit("WeissDummy", 11, -4, 1);
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "VisionBlue");
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife");
						MG_ControlBuffs.I._getBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife").turnDuration = 4;
						MG_ControlCommand.I._specialEffect_outward (11, -4, "cartoonHoly01", 3, 0.2, 0.2);
					break;
					
					case 11:
						speaker = "Aleric"; portraitName = "Aleric"; main = "Uhhhh..... Not..... Bad....... ";
					break;
					case 12:
						speaker = "Aleric"; portraitName = "Aleric"; main = "Let's fall back for now, boys.";
					break;
					case 13:
						speaker = "Aleric"; portraitName = "Aleric"; main = "I'll give you a warning, Princess. It's only luck that you won against him at Ashborne.";
					break;
					case 14:
						speaker = "Aleric"; portraitName = "Aleric"; main = "As we speak, he's just getting more powerful.";
					break;
					case 15:
						speaker = "Aleric";
						portraitName = "Aleric";
						hasContinuation = false;
						main = "By the time you get there, you and your friends will only find death and suffering in Terra.";

						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"15", "13"}); // remember to change target dialog number (1st parameter)
					break;
				}
			break;
			#endregion
			#region "Stories01_mis02"
			case "Stories01_mis02":
				//Test
				switch (dialogNumber) {
					case 1:
						speaker = "Officer"; portraitName = "Officer"; main = "Princess, we're now at the town of Lenley. There seems to be no villagers around.";
					break;
					case 2:
						speaker = "Princess May"; portraitName = "May"; main = "They must have taken the villagers. We must hurry and rescue them.";
					break;
					case 3:
						speaker = "Ifreet"; portraitName = "Ifreet"; main = "This is as far as you go, Princess. You're surrounded.";

						// DIALOG TRIGGERED SPECIAL CODE
						MG_ControlUnits.I._createUnit("Ifreet(Boss)", -1, 12, 2);
						MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "17"});
						MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "Ifreet Boss");
						MG_ControlUnits.I._createUnit("Ragnaros(Boss)", 0, 13, 2);
						MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "Ragnaros Boss");
						MG_ControlUnits.I._createUnit("SpectralWitch(Boss)", 0, 11, 2);
						MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "SpectralWitch Boss");
						MG_ControlSFX.I._createSFX("moveSmoke", -1, 12);
						MG_ControlSFX.I._createSFX("moveSmoke", 0, 13);
						MG_ControlSFX.I._createSFX("moveSmoke", 0, 11);
					break;
					case 4:
						speaker = "Princess May"; portraitName = "May"; main = "Who are you? And what have you done to the villagers?";
					break;
					case 5:
						speaker = "Ifreet"; portraitName = "Ifreet"; main = "I am Ifreet. Let's just say Bael and I have a lot to gain for stopping you here.";
					break;
					case 6:
						speaker = "Ifreet"; portraitName = "Ifreet"; main = "I brought some powerful friends with me. You can still spare the lives of your men if you turn around now.";
					break;
					case 7:
						speaker = "Ragnaros"; portraitName = "Ragnaros"; main = "Give me a good fight, Regnians, or I'll crush you all like twigs.";
					break;
					case 8:
						speaker = "Spectral Witch"; portraitName = "SpectralWitch"; main = "They will make good sacrifices for the Dark Lord.";
					break;
					case 9:
						speaker = "Princess May"; portraitName = "May"; main = "You greatly underestimate us. I shall show you all the strength of the Kingdom of Regnia.";
					break;
					case 10:
						speaker = "OBJECTIVES:";
						portraitName = "";
						hasContinuation = false;
						main = "- Defeat Ifreet\n- Protect your Camp";

						// DIALOG TRIGGERED SPECIAL CODE
						MG_GetUnit.I.us_getUnit ("Ifreet Boss")._moveUnit(10, -3);
						MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.us_getUnit ("Ifreet Boss"), new Vector2(-4, 10));
						MG_GetUnit.I.us_getUnit ("Ragnaros Boss")._moveUnit(-6, -10);
						MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.us_getUnit ("Ragnaros Boss"), new Vector2(-4, 10));
						MG_GetUnit.I.us_getUnit ("SpectralWitch Boss")._moveUnit(-7, -10);
						MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.us_getUnit ("SpectralWitch Boss"), new Vector2(-4, 10));
						MG_ControlSFX.I._createSFX("moveSmoke", -1, 12);
						MG_ControlSFX.I._createSFX("moveSmoke", 0, 13);
						MG_ControlSFX.I._createSFX("moveSmoke", 0, 11);
						MG_ControlCursor.I._moveCursor (-4/2, -4/2, true);
						MG_ControlCursor.I._interact (false);
						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"9", "12"});

						MG_ControlUnits.I._createUnit("WeissDummy", -6, -10, 1);
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "VisionBlue");
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife");
						MG_ControlBuffs.I._getBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife").turnDuration = 4;
						MG_ControlUnits.I._createUnit("WeissDummy", 10, -3, 1);
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "VisionBlue");
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife");
						MG_ControlBuffs.I._getBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife").turnDuration = 4;
					break;
					case 11:
						speaker = "Ifreet"; portraitName = "Ifreet"; main = "Uhhh... you were stronger than I thought.";
					break;
					case 12:
						speaker = "Princess May"; portraitName = "May"; main = "You are beaten, Ifreet. Now tell me, where did you take the villagers?";
					break;
					case 13:
						speaker = "Ifreet"; portraitName = "Ifreet"; main = "You already know the answer, Princess. You just don't want to accept it.";
					break;
					case 14:
						speaker = "Ifreet"; portraitName = "Ifreet"; main = "They joined Prince Walter to Terra. Voluntarily.";
					break;
					case 15:
						speaker = "Ifreet"; portraitName = "Ifreet"; main = "They want to serve their true Dark Lord not your weak and corrupt kingdom.";
					break;
					case 16:
						speaker = "Princess May"; portraitName = "May"; main = "That's not true. He must have taken them to work his defenses for him.";
					break;
					case 17:
						speaker = "Ifreet"; portraitName = "Ifreet"; main = "Believe what you will, Princess. You will all see the truth soon.";
					break;
					case 18:
						speaker = "Ifreet"; portraitName = "Ifreet"; main = "We held them long enough. Let's go back now.";
					break;
					case 19:
						speaker = "Ifreet"; portraitName = "Ifreet"; main = "And let me tell you this, Princess. Regnia is soon to fall into our hands, the hand of darkness.";
					break;
					case 20:
						speaker = "Ifreet"; portraitName = "Ifreet"; main = "Your people will soon worship and serve our Dark Lord. Bwahahahaha...";
					break;
					case 21:
						speaker = "Princess May"; portraitName = "May"; main = "They're pulling back. It is best if we not chase them.";
					break;
					case 22:
						speaker = "Princess May"; portraitName = "May"; main = "We've beaten Walter and his cronies before. We'll finish this in Terra.";
					break;
					case 23:
						speaker = "Princess May";
						portraitName = "May";
						hasContinuation = false;
						main = "For now, we shall rest here. Then we'll head to the Crimson Gate at once tommorow.";

						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"23", "13"}); // remember to change target dialog number (1st parameter)
					break;
				}
			break;
			#endregion
			#region "Stories01_mis03"
			case "Stories01_mis03":
				//Test
				switch (dialogNumber) {
					case 1:
						speaker = "Princess May"; portraitName = "May"; main = "At last we have arrived. Crimson Forest. And just across the path is the Crimson Gate.";
					break;
					case 2:
						speaker = "Scout"; portraitName = "Apprentice"; main = "As expected, Prince Walter's men has powered down the Gate after they left.";
					break;
					case 3:
						speaker = "Scout"; portraitName = "Apprentice"; main = "Walter's forces are also surrounding the forests.";
					break;
					case 4:
						speaker = "Princess May"; portraitName = "May"; main = "Good thing we saw this coming. Our Trucks are filled with Arcanytes that should be enough to power the Gate back on.";
						
						// DIALOG TRIGGERED SPECIAL CODE
						MG_ControlCamera.I._moveCamera(-3.557419f, 5.039912f, true);
					break;
					case 5:
						speaker = "Princess May"; portraitName = "May"; main = "We'll have to move all our Trucks to the gate to power it back.";
					break;
					case 6:
						speaker = "Princess May"; portraitName = "May"; main = "Enemies are expected to come at all sides so protect the Trucks and our Camp at all costs!";
					break;
					case 7:
						speaker = "OBJECTIVES:";
						portraitName = "";
						hasContinuation = false;
						main = "- All your Trucks must reach the Gate\n- Protect all your Trucks\n- Protect your Camp";

						// DIALOG TRIGGERED SPECIAL CODE
						MG_ControlCamera.I._moveCamera(-2.547f, -5.451f, true);
						MG_ControlCursor.I._moveCursor (-4/2, -4/2, true);
						MG_ControlCursor.I._interact (false);
						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"7", "5"});
						MG_ControlEvents.I._addEvent ("UnitEnterRegion", new string[]{"-17", "-8", "13", "6", "20"});
						MG_ControlEvents.I._addEvent ("UnitEnterRegion", new string[]{"14", "17", "-1", "-5", "24"});
					break;
					case 8:
						speaker = ""; portraitName = ""; main = "One of your Trucks has arrived at the Gate!";
						hasContinuation = false;
					break;	
					case 9:
						speaker = "Officer"; portraitName = "Officer"; main = "Princess, all our Trucks have reached the Gate!";
						
						// DIALOG TRIGGERED SPECIAL CODE
						// Pause the game here
					break;
					case 10:
						speaker = "Princess May"; portraitName = "May"; main = "Activate the Gate's power! Then let us cross immediately!";
					break;
					case 11:
						speaker = "Officer";
						portraitName = "Officer";
						hasContinuation = false;
						main = "Yes, Princess!";

						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"11", "13"}); // remember to change target dialog number (1st parameter)
					break;
					case 12:
						speaker = "Scout"; portraitName = "Apprentice"; main = "Princess, one of our Trucks has been destroyed!";
						
						// DIALOG TRIGGERED SPECIAL CODE
						// Pause the game here
					break;
					case 13:
						speaker = "Princess May"; portraitName = "May"; main = "We failed. Fall back, everyone! We will have to do this next time.";
						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"13", "22"}); // remember to change target dialog number (1st parameter)
						hasContinuation = false;
					break;
					case 14:
						speaker = "Princess May"; portraitName = "May"; main = "We are halfway through the gate. Let's keep this up!";
						
						// DIALOG TRIGGERED SPECIAL CODE
						MG_ControlCamera.I._moveCamera(4.91635f, -4.4744f);
					break;
					case 15:
						speaker = "Princess May"; portraitName = "May"; main = "This is a good spot to build a camp. Let us set-up one right here.";
					break;
					case 16:
						speaker = "";
						portraitName = "";
						hasContinuation = false;
						main = "A new base has been built!";

						// DIALOG TRIGGERED SPECIAL CODE
						MG_ControlUnits.I._createUnit("CampBlue", 9, -10, 1);
						MG_ControlSFX.I._createSFX("moveSmoke", 9, -10);
						MG_ControlUnits.I._createUnit("BarracksBlue", 7, -10, 1);
						MG_ControlSFX.I._createSFX("moveSmoke", 7, -10);
						MG_ControlUnits.I._createUnit("SpellTowerBlue", 10, -6, 1);
						MG_ControlSFX.I._createSFX("moveSmoke", 10, -6);
					break;
				}
			break;
			#endregion
			#region "Stories02_mis01"
			case "Stories02_mis01":
				//Test
				switch (dialogNumber) {
					case 1:
						speaker = "Eve"; portraitName = "Eve"; main = "You have come, Dark Prince. Just as our Dark Lord has told us.";
					break;
					case 2:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "I heard you will help us in our coming battles if we'll help you drive the Imperials out of Palude.";
					break;
					case 3:
						speaker = "Eve"; portraitName = "Eve"; main = "We Dark Elves are yours to command.";
					break;
					case 4:
						speaker = "Eve"; portraitName = "Eve"; main = "The Dark Lord has already told us that you will not only crush your pursuers, but you will also build an Empire in his name here at Palude.";
					break;
					case 5:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "Let's not underestimate our enemies yet, Eve.";
					break;
					case 6:
						speaker = "Drakgul"; portraitName = "Drakgul"; main = "Prince Walter? No mistaking it, it's you who I sense this dark power from. What have you done?";

						// DIALOG TRIGGERED SPECIAL CODE
						MG_ControlUnits.I._createUnit("Drakgul(Boss)", -3, -6, 2);
						MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "25"});
						MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "Drakgul Boss");
						MG_ControlSFX.I._createSFX("moveSmoke", -3, -6);
					break;
					case 7:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "Drakgul. So you brought your band of Orcs to stop us.";
					break;
					case 8:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "I was expecting Imperial resistance, not you.";
					break;
					case 9:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "You should be able to tell. You are no match to my new power.";
					break;
					case 10:
						speaker = "Drakgul"; portraitName = "Drakgul"; main = "That kind of dark power cannot be allowed to exist here in Terra.";
					break;
					case 11:
						speaker = "Drakgul"; portraitName = "Drakgul"; main = "I'll fight you here until the Imperials arrive. Then you will have nowhere to run.";
					break;
					case 12:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "Bold words. Then you shall be the first here in Terra to feel the wrath of my new power.";
					break;
					case 13:
						speaker = "OBJECTIVES:";
						portraitName = "";
						hasContinuation = false;
						main = "- Defeat Drakgul\n- Protect your Camp";

						// DIALOG TRIGGERED SPECIAL CODE
						MG_GetUnit.I.us_getUnit ("Drakgul Boss")._moveUnit(8, -6);
						MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.us_getUnit ("Drakgul Boss"), new Vector2(-15, -6));
						MG_GetUnit.I.us_getUnit ("Drakgul Boss").isStationed = true;
						MG_ControlSFX.I._createSFX("moveSmoke", -3, -6);
						MG_ControlCursor.I._moveCursor (-8/2, -6/2, true);
						MG_ControlCursor.I._interact (false);
						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"13", "12"});
						MG_ControlUnits.I._createUnit("WeissDummy", 8, -6, 1);
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "VisionBlue");
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife");
						MG_ControlBuffs.I._getBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife").turnDuration = 4;
					break;
					case 14:
						speaker = "Orc Grunt"; portraitName = "OrcGrunt"; main = "Head Shaman, they're too strong for us. We can't hold much longer.";
					break;
					case 15:
						speaker = "Drakgul"; portraitName = "Drakgul"; main = "No, I think we held them long enough. Let's get back now.";
					break;
					case 16:
						speaker = "Drakgul"; portraitName = "Drakgul"; main = "And Prince Walter, you should know we Terrans have taken down countless demons throughout our history.";
					break;
					case 17:
						speaker = "Drakgul"; portraitName = "Drakgul"; main = "When this is over, you will be nothing more but another number.";
					break;
					case 18:
						speaker = "Drakgul"; portraitName = "Drakgul"; main = "Sound the retreat!";
					break;
					case 19:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "Tsk, they got away.";
					break;
					case 20:
						speaker = "Prince Walter";
						portraitName = "Walter";
						hasContinuation = false;
						main = "No matter, let's rest for now. Then we shall continue our march to Fort Mons.";

						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"20", "13"}); // remember to change target dialog number (1st parameter)
					break;
				}
			break;
			#endregion
			#region "Stories02_mis02"
			case "Stories02_mis02":
				//Test
				switch (dialogNumber) {
					case 1:
						speaker = "Eve"; portraitName = "Eve"; main = "We're now at the base of Mount Novis, Dark Prince.";
					break;
					case 2:
						speaker = "Eve"; portraitName = "Eve"; main = "At the top of the mountain is Fort Mons.";
					break;
					case 3:
						speaker = "Eve"; portraitName = "Eve"; main = "It's a strategical Fort. Capture it, and you control the whole of Palude.";
					break;
					case 4:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "Then let us make haste. Regnian or Imperial reinforcements will get closer to us the more we delay.";
					break;
					case 5:
						speaker = "Abel"; portraitName = "Abel"; main = "You have made a big mistake, Prince Walter.";
						
						// DIALOG TRIGGERED SPECIAL CODE
						MG_ControlUnits.I._createUnit("Abel(Boss)", -1, 9, 2);
						MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "26"});
						MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "Abel Boss");
						MG_ControlUnits.I._createUnit("Victoria(Boss)", 1, 9, 2);
						MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "26"});
						MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "Faylinn Boss");
						MG_ControlSFX.I._createSFX("moveSmoke", -1, 9);
						MG_ControlSFX.I._createSFX("moveSmoke", 1, 9);

						MG_ControlEvents.I.storageCamp1 = 2;
					break;
					case 6:
						speaker = "Faylinn"; portraitName = "Victoria"; main = "Your short-lived reign ends here.";
					break;
					case 7:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "Abel the Paladin, and Faylinn the Cleric.";
					break;
					case 8:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "So you church servants arrived to me before the Imperials do.";
					break;
					case 9:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "You can sense my power, yet you all stand against me with that meager force?";
					break;
					case 10:
						speaker = "Faylinn"; portraitName = "Victoria"; main = "You should know, we Terrans are experienced demon slayers.";
					break;
					case 11:
						speaker = "Abel"; portraitName = "Abel"; main = "To us, you're just another demon to be purged.";
					break;
					case 12:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "So be it then. Brothers, let us show no mercy to our enemies today.";
					break;
					case 13:
						speaker = "OBJECTIVES:";
						portraitName = "";
						hasContinuation = false;
						main = "- Defeat Abel and Faylinn\n- Protect your Camp";

						// DIALOG TRIGGERED SPECIAL CODE
						MG_GetUnit.I.us_getUnit ("Abel Boss")._moveUnit(11, 9);
						MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.us_getUnit ("Abel Boss"), new Vector2(1, 2));
						MG_ControlSFX.I._createSFX("moveSmoke", -1, 9);
						MG_GetUnit.I.us_getUnit ("Faylinn Boss")._moveUnit(-12, -11);
						MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.us_getUnit ("Faylinn Boss"), new Vector2(1, 2));
						MG_ControlSFX.I._createSFX("moveSmoke", 1, 9);
						MG_ControlCursor.I._moveCursor (1/2, 4/2, true);
						MG_ControlCursor.I._interact (false);
						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"13", "12"});

						MG_ControlUnits.I._createUnit("WeissDummy", 11, 9, 1);
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "VisionBlue");
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife");
						MG_ControlBuffs.I._getBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife").turnDuration = 4;
						MG_ControlUnits.I._createUnit("WeissDummy", -12, -11, 1);
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "VisionBlue");
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife");
						MG_ControlBuffs.I._getBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife").turnDuration = 4;
					break;
					case 14:
						speaker = "Faylinn"; portraitName = "Victoria"; main = "I think we held them long enough now, Abel.";
					break;
					case 15:
						speaker = "Abel"; portraitName = "Abel"; main = "Then let's retreat now.";
					break;
					case 16:
						speaker = "Eve"; portraitName = "Eve"; main = "They're retreating. Impressive, Dark Prince. The Dark Lord was not wrong about you.";
					break;
					case 17:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "I'm sensing something. They might be preparing a trap ahead.";
					break;
					case 18:
						speaker = "Prince Walter";
						portraitName = "Walter";
						hasContinuation = false;
						main = "No matter, it will be a perfect time to unleash my full power againt them all.";

						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"18", "13"}); // remember to change target dialog number (1st parameter)
					break;
				}
			break;
			#endregion
			#region "Stories02_mis03"
			case "Stories02_mis03":
				//Test
				switch (dialogNumber) {
					case 1:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "At last here we are. Fort Mons.";
					break;
					case 2:
						speaker = "Eve"; portraitName = "Eve"; main = "We arrived in time, Dark Prince. Only a few garisson is defending the fort.";
					break;
					case 3:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "Let's finish this quick before any Imperial reinforcements arrive.";
					break;
					case 4:
						speaker = "Cody"; portraitName = "Cody"; main = "Prince Walter. I knew you would do this one day.";
						
						// DIALOG TRIGGERED SPECIAL CODE
						MG_ControlUnits.I._createUnit("Cody(Boss)", 0, 0, 2);
						MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "27"});
						MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "Cody Boss");
						MG_ControlSFX.I._createSFX("moveSmoke", 0, 0);

						MG_ControlEvents.I.storageCamp1 = 4;
					break;
					case 5:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "Cody Jackson, the Imperial Wolf. So you're the one defending this Fort.";
					break;
					case 6:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "It's my lucky day. i'll deal with you right here were you're outnumbered and outgunned.";
					break;
					case 7:
						speaker = "Faylinn"; portraitName = "Victoria"; main = "You think you outnumber us, huh?";

						// DIALOG TRIGGERED SPECIAL CODE
						MG_ControlUnits.I._createUnit("Drakgul(Boss)", -6, -4, 2);
						MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "Drakgul Boss");
						MG_GetUnit.I.getLastCreatedUnit()._changeFacing ("right");
						MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "27"});
						MG_ControlUnits.I._createUnit("Victoria(Boss)", 6, -4, 2);
						MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "Victoria Boss");
						MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "27"});

						MG_ControlUnits.I._createUnit("Abel(Boss)", 0, -6, 2);
						MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "Abel Boss");
						MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "27"});

						MG_ControlSFX.I._createSFX("moveSmoke", -6, -4);
						MG_ControlSFX.I._createSFX("moveSmoke", 6, -4);
						MG_ControlSFX.I._createSFX("moveSmoke", 0, -6);
					break;
					case 8:
						speaker = "Abel"; portraitName = "Abel"; main = "We're not done yet, Walter. I'll slay you here myself.";
					break;
					case 9:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "This is your plan all along? Impressive.";
					break;
					case 10:
						speaker = "Prince Walter"; portraitName = "Walter"; main = "I have no time to waste in this so let's make things more interesting.";
					break;
					case 11:
						speaker = "Prince Walter"; portraitName = "DemonPrince"; main = "RRRAAHHHHHHHHH!!!!!!";

						// DIALOG TRIGGERED SPECIAL CODE
						MG_ControlCommand.I._specialEffect_outward(-2, -4, "cartoonExplodeFire01", 3, 0.2, 0.2);
						MG_ControlCommand.I._specialEffect_outward(-2, -4, "cartoonHit04", 3, 0.2, 0.2);
						MG_ControlSFX.I._createSFX ("explodeDust01", -2, -4);
						MG_GetUnit.I.us_getUnit ("Walter Normal")._moveUnit(-60, -60);

						MG_ControlUnits.I._createUnit("DemonPrince", -2, -4, 1);
						MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "Demon Walter");
						MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "27"});
						MG_ControlSounds.I._playSound(17, -2, -4, true);

						MG_Globals.I.players[1].hero_name[0] = "DemonPrince";
						MG_Globals.I.players[1]._HERO_setSummonedState("DemonPrince", true);
					break;
					case 12:
						speaker = "Drakgul"; portraitName = "Drakgul"; main = "So this is the full extent of you power?";
					break;
					case 13:
						speaker = "Cody"; portraitName = "Cody"; main = "We still have him outnumbered. This is a chance that we must take.";
					break;
					case 14:
						speaker = "Abel"; portraitName = "Abel"; main = "You will know soon how we Terrans deal with demons like you, Walter.";
					break;
					case 15:
						speaker = "OBJECTIVES:";
						portraitName = "";
						hasContinuation = false;
						main = "- Slay all enemy heroes\n- Protect your camp\n- Demon Prince Walter must survive";

						// DIALOG TRIGGERED SPECIAL CODE
						MG_ControlSFX.I._createSFX("moveSmoke", -6, -4);
						MG_ControlSFX.I._createSFX("moveSmoke", 6, -4);
						MG_ControlSFX.I._createSFX("moveSmoke", 0, -6);
						MG_GetUnit.I.us_getUnit ("Drakgul Boss")._moveUnit(-16, -6);
						MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.us_getUnit ("Drakgul Boss"), new Vector2(-2, -6));
						MG_GetUnit.I.us_getUnit ("Abel Boss")._moveUnit(15, -7);
						MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.us_getUnit ("Abel Boss"), new Vector2(-2, -6));
						MG_GetUnit.I.us_getUnit ("Victoria Boss")._moveUnit(15, -5);
						MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.us_getUnit ("Victoria Boss"), new Vector2(-2, -6));
						MG_GetUnit.I.us_getUnit ("Cody Boss")._moveUnit(-2, 13);
						MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.us_getUnit ("Cody Boss"), new Vector2(-2, -6));
						MG_ControlCursor.I._moveCursor (0/2, -4/2, true);
						MG_ControlCursor.I._interact (false);
						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"15", "12"});

						MG_ControlUnits.I._createUnit("WeissDummy", -16, -6, 1);
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "VisionBlue");
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife");
						MG_ControlBuffs.I._getBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife").turnDuration = 4;
						MG_ControlUnits.I._createUnit("WeissDummy", 15, -7, 1);
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "VisionBlue");
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife");
						MG_ControlBuffs.I._getBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife").turnDuration = 4;
						MG_ControlUnits.I._createUnit("WeissDummy", -2, 13, 1);
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "VisionBlue");
						MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife");
						MG_ControlBuffs.I._getBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife").turnDuration = 4;
					break;
					case 16:
						speaker = "Cody"; portraitName = "Cody"; main = "Uhhhh..... So...... Strong.......";
					break;
					case 17:
						speaker = "Faylinn"; portraitName = "Victoria"; main = "We can't take him here now. We'll have to fall back.";
					break;
					case 18:
						speaker = "Abel"; portraitName = "Abel"; main = "Tsk. We have no choice but to lose this fort to this demon.";
					break;
					case 19:
						speaker = "Drakgul"; portraitName = "Drakgul"; main = "This is not a good start for us. But our time will come.";
					break;
					case 20:
						speaker = "Eve"; portraitName = "Eve"; main = "They're falling back. Victory is ours, we have captured the fort!";
					break;
					case 21:
						speaker = "Eve"; portraitName = "Eve"; main = "This is a good start for us, Dark Prince. But the war has just begun.";
					break;
					case 22:
						speaker = "Prince Walter";
						portraitName = "Walter";
						hasContinuation = false;
						main = "We shall prepare our defenses for this fort. Soon Regnians and Imperials will arrive with overwhelming force.";

						MG_ControlEvents.I._addEvent ("DialogEnd", new string[]{"22", "13"}); // remember to change target dialog number (1st parameter)
					break;
				}
			break;
			#endregion
		}

		#region "Old references taken from PROJ Ronin"
		/*switch(database){
				 /* case "1": //test 1
					switch(dialogNumber){

						//Test
						case 1: speaker = " "; portraitName = "NONE"; 
							main = "Hello World!"; break;
						case 2: speaker = " "; portraitName = "NONE"; hasContinuation = false;
							main = "Dialog system works perfect!"; break;

							//Test NPC
						case 3: speaker = "TestNPC:"; portraitName = "NONE"; hasContinuation = false;
							main = "Interaction system works perfect!"; break;

							//Test Shop
						case 4: speaker = "TestShop:"; portraitName = "NONE";  hasContinuation = false; hasOptions = true;
							options1 = "Buy"; optnEfct1 = 6; option1_isRed = false;
							options2 = "Sell"; optnEfct2 = 7; option2_isRed = false;
							options3 = "Do Nothing"; optnEfct3 = 5; option3_isRed = true;
							//Go to P_ControlUI_Dialog for more options interaction
							//Always reset the database when using global interactions
							main = "Welcome to TestShop! Cheap and quality goods.";
							P_ControlUI_Dialog.I._resetDatabase();
						break;

					}
				break;

				/// ///// Dialogs that can be used by any map. /////
				/// ///// Dialogs that can be used by any map. /////
				/// ///// Dialogs that can be used by any map. /////
				/// ///// Dialogs that can be used by any map. /////
				case "death": //On death dialog
					switch(dialogNumber){

						case 1: speaker = " "; portraitName = "NONE";
							main = "You have died. Gold lost: " + cusValue1; break;
						case 2: speaker = " "; portraitName = "NONE"; hasContinuation = false; hasOptions = true;
							//Go to P_ControlUI_Dialog for more options interaction
							options1 = "Respawn"; optnEfct1 = 1; option1_isRed = false; 
							main = "Respawn from last checkpoint?";
							P_ControlUI_Dialog.I._resetDatabase();
						break;

					}
				break;
				case "checkpoint": //Checkpoint
					switch(dialogNumber){

						case 1: speaker = "Checkpoint:"; portraitName = "NONE";  hasContinuation = false; hasOptions = true;
							options1 = "Rest"; optnEfct1 = 2; option1_isRed = false;
							options2 = "Craft items"; optnEfct2 = 4; option2_isRed = false;
							options3 = "Access item box"; optnEfct3 = 3; option3_isRed = false;
							options4 = "Nothing"; optnEfct4 = 5; option4_isRed = true;
							//Go to P_ControlUI_Dialog for more options interaction
							//Always reset the database when using global interactions
							main = "What do you want to do?";
							P_ControlUI_Dialog.I._resetDatabase();
						break;

					}
				break;
				case "itemChest": //Item chest
					switch(dialogNumber){

						case 1: speaker = " "; portraitName = "NONE";  hasContinuation = false;
							//Always reset the database when using global interactions
							P_DB_Items.I._setValues(cusValue1);
							if(P_DB_Items.I.itemClass == "Weapon" || P_DB_Items.I.itemClass == "Passive")
								main = "Found <color=yellow>" + cusValue1 + "</color>.";
							else
								main = "Found <color=yellow>" + cusValue1 + " x" + cusValue2 + "</color>.";
							P_ControlUI_Dialog.I._showItemPicture(cusValue1);
							P_ControlUI_Dialog.I._resetDatabase();
						break;

					}
				break;
			}*/
		#endregion
	}
}

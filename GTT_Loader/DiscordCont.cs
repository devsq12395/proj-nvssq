using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscordCont : MonoBehaviour
{
	public static DiscordCont I;
	public void Awake(){ 
		I = this;
		DontDestroyOnLoad(gameObject);
	}

	// Start is called before the first frame update
    void Start() {
		/*discord = new Discord.Discord(650570170972110877, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
		var activityManager = discord.GetActivityManager();
		var activity = new Discord.Activity {
			State = "Still Testing",
			Details = "Bigger Test",
			Instance = true
		};
		activityManager.UpdateActivity(activity, (res) => {
			if (res == Discord.Result.Ok) {
				Debug.LogError("Everything is fine!");
			}
		});*/
    }

    // Update is called once per frame
    void Update() {
		//discord.RunCallbacks();
    }
}

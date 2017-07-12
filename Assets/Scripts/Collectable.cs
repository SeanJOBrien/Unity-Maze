using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collectable : MonoBehaviour {
	private int PhoneCounter = 0;
	public Text score;
	private float time;
	public Text timeDisplay;
	public Text gameOver;
	public Text youWin;
	public Text returnToRestart;
	private bool gameOverBool;
	// Use this for initialization
	void Start () {
		time = 0.0f;
		gameOverBool = false;
		gameOver.gameObject.SetActive(false);
		youWin.gameObject.SetActive(false);
		returnToRestart.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (time > (60*5))
		{
			gameOver.gameObject.SetActive(true);
			returnToRestart.gameObject.SetActive(true);
			gameOverBool = true;
		}
		else
		{
			timmer();
		}
		if(Input.GetKeyDown("return") && gameOverBool)
		{
			Application.LoadLevel("StartUp");
		}
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
   		if (hit.normal.y < 0.9){ // filter out ground collisions
     	// try to call PlayerHit in the other object
     		if(hit.gameObject.name == "WinPhone")
     		{
     			PhoneCounter++;
     			Destroy(hit.gameObject);
				updateScore();
       		    if(PhoneCounter == 5)
            	{
					gameObject.transform.Rotate(new Vector3(180.0f,0.0f,0.0f));
					gameObject.transform.Translate(new Vector3(0.0f,-2.2f,0.0f));
				}
     		}
       		if(hit.gameObject.name == "Bin")
        	{
           		if(PhoneCounter == 5)
           	 	{
                	Destroy(hit.gameObject);
					youWin.gameObject.SetActive(true);
					returnToRestart.gameObject.SetActive(true);
					gameOverBool = true;
            	}   
        	}
  		}
 	}
	void updateScore()
	{
		score.text = "Score: "+(PhoneCounter*10);
	}
	void timmer()
	{
		time += Time.deltaTime;
		int timeMin = (int)(time/60);
		int timeSec = (int)(time%60);
		string timeMessage = "Time: 0"+ timeMin + ((timeSec<10) ? ":0":":") +timeSec;
		timeDisplay.text = timeMessage;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public Text continueText;
	public Text scoreText;

	private float timeElapsed = 0f;
	private float bestTime = 0f;
	private float blinkTime = 0f;
	private bool doBlink;
	private bool gameStarted;
	private GameObject player;
	private GameObject floor;
	private Spawner spawner;
	private TimeManager timeManager;
	private bool gotBestTime;

	private void Awake()
	{
		floor = GameObject.Find("Foreground");
		spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
		timeManager = GetComponent<TimeManager>();
	}

	// Use this for initialization
	void Start () {

		var floorHeight = floor.transform.localScale.y;

		var pos = floor.transform.position;

		pos.x = 0;
		pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnits) / 2);

		floor.transform.position = pos;

		spawner.active = false;

		Time.timeScale = 0;

		//continueText.text = "PRESS ANY BUTTON TO START";

		bestTime = PlayerPrefs.GetFloat("BestTime");
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameStarted && (Time.timeScale == 0)){
			if(Input.anyKeyDown){
				timeManager.ManipulateTime(1, 1f);
				ResetGame();
			}
		}

		if(!gameStarted){
			blinkTime++;

			if((blinkTime % 40) == 0){
				doBlink = !doBlink;
			}

			var bestTextColor = gotBestTime ? "#00FF2D" : "#FFF";

			continueText.canvasRenderer.SetAlpha(doBlink ? 0 : 1);

			scoreText.text = "TIME: " + FormatTime(timeElapsed) + "<color=" + bestTextColor + ">\nBEST: " + FormatTime(bestTime) + "</color>";
		}
		else{
			timeElapsed += Time.deltaTime;
			scoreText.text = "TIME: " + FormatTime(timeElapsed);
		}
	}

	void OnPlayerKilled(){
		spawner.active = false;

		var playerDestroyScript = player.GetComponent<DestroyOffscreenObjects>();
        playerDestroyScript.OnDestroyCallback -= OnPlayerKilled;

		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

		gameStarted = false;
		timeManager.ManipulateTime(0, 5.5f);

		if(timeElapsed > bestTime){
			bestTime = timeElapsed;
			PlayerPrefs.SetFloat("BestTime", bestTime);
			gotBestTime = true;
		}

        continueText.text = "PRESS ANY BUTTON TO RESTART";
	}

	void ResetGame () {
		gameStarted = true;
		spawner.active = true;

		player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(0, (Screen.height / PixelPerfectCamera.pixelsToUnits) / 2 + 100, 0));

		var playerDestroyScript = player.GetComponent<DestroyOffscreenObjects>();      
        playerDestroyScript.OnDestroyCallback += OnPlayerKilled;

		continueText.canvasRenderer.SetAlpha(0);

		timeElapsed = 0f;

		gotBestTime = false;
	}

	string FormatTime(float time){
		TimeSpan t = TimeSpan.FromSeconds(time);

		return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
	}

}

﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerObjectsController : MonoBehaviour {
	public bool StartGame;
	public int LevelLenght;


	public Transform Target;
	public Transform Player;

	public float MoveSpeed = 3f;


	public Vector2 Angles;

	public Text[] CurrentPoints = new Text[2];
	public TextMesh textmidle;

	public int CurrentPlayerShape = 0;
	public Sprite[] PlayerSprites;
	public Sprite[] EnemySprites;

	//**********private field***********//
	private GameController controller;
	private bool CanPress = false;
	private int AngleDir = 0;//0 = <    -- 1 = >
	public bool CanReset = false;
	public bool GoInside = false;
	public bool isInside = false;

	private Transform PlayerChild;
	private Transform TargetChild;
	private float SpeedMultiplier = 1.3f;
	public bool one_click = false;
	public bool timer_running;
	public float timer_for_double_click;
	public float delay = 25;
	private Collider2D thisCol;
	public bool GameOver = false;

	void Start () {
		thisCol = GetComponent <BoxCollider2D>();

		PlayerChild = Player.transform.GetChild (0);
		TargetChild = Target.transform.GetChild (0);

		controller = GameObject.FindObjectOfType<GameController> ();
		RePosition ();

		LevelLenght = 0;
	}
	private Vector3 playerAngle;
	// Update is called once per frame
	void Update () {
		if (StartGame == false) {
			PlayerChild.GetComponent<SpriteRenderer>().sprite = PlayerSprites[CurrentPlayerShape];
			TargetChild.GetComponent<SpriteRenderer>().sprite = EnemySprites[CurrentPlayerShape];
		}

		//player game speed
		//MoveSpeed = 55+SpeedMultiplier * LevelLenght;

		CurrentPoints[0].text = LevelLenght.ToString();
		CurrentPoints[1].text = CurrentPoints[0].text;
		textmidle.text = CurrentPoints[0].text;

		if (GoInside) {
			TargetChild.transform.localScale = new Vector3(0.2645999f, -0.2645999f,0.2645999f);
		} else
			TargetChild.transform.localScale = new Vector3(0.2645999f,0.2645999f,0.2645999f);

		if (isInside) {
			PlayerChild.transform.localScale = new Vector3(0.2645999f, -0.2645999f,0.2645999f);
		} else
			PlayerChild.transform.localScale = new Vector3(0.2645999f,0.2645999f,0.2645999f);


		if (StartGame) {
			if(!isInside)
			Angles.y += MoveSpeed * Time.smoothDeltaTime;
			else
			Angles.y += (MoveSpeed*1.2f) * Time.smoothDeltaTime;
		}

		playerAngle.z = -Angles.y;
		//values with '-' for clocklike rotation
		Target.eulerAngles = new Vector3 (0, 0, -Angles.x);
		Player.eulerAngles = playerAngle;//new Vector3 (0, 0, -Angles.y);



		Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 touchPos = new Vector2(wp.x, wp.y);

		if(one_click)
		{

			if((Time.time - timer_for_double_click) > delay)
			{
			//	if(!CanPress){
			//		GameOver = true;
			//	}
				one_click = false;
				
			}
		}

		if (Input.GetMouseButtonDown (0))//Input.GetMouseButtonDown (0)) 
		{
			if(!one_click)
			{
				if (StartGame == true)
				{
				one_click = true;
				timer_for_double_click = Time.time; // save the current time
				}

				if (StartGame == false && thisCol == Physics2D.OverlapPoint(touchPos) ) {//&& thisCol == Physics2D.OverlapPoint(touchPos)
					StartGame = true;
					controller.SendMessage("startGame");
				}

				if (CanPress ) {
					if(CanReset == true && thisCol == Physics2D.OverlapPoint(touchPos)){//Reset the game!
						CanPress = false;
						StartGame = false;
						Angles.y = 0;
						Angles.x = 0;
						MoveSpeed = 55;
						isInside = false;
						GameOver = false;
						RePosition();
						CurrentPlayerShape=Random.Range(0,PlayerSprites.Length-1);
						LevelLenght = 0;//controller.Level;//reset the counter
						controller.SendMessage("ResetUI",true,SendMessageOptions.RequireReceiver);
						CanReset = false;
					}
					else if(CanReset == false){
						if (GoInside == true)
						{
							if(isInside == true)
							{
							LevelLenght += 1;

								if (LevelLenght % 5 == 0)
									controller.SendMessage("ResetUI",false,SendMessageOptions.RequireReceiver);

							one_click = false;
							RePosition();
							}
						}
						else 
						if (GoInside == false)
						{
							if(isInside == false)
							{
								one_click = false;
								LevelLenght += 1;

								if (LevelLenght % 5 == 0)
									controller.SendMessage("ResetUI",false,SendMessageOptions.RequireReceiver);

								RePosition();
							}
						}
	//					else
	//					{
	//						controller.SendMessage ("YouWin", SendMessageOptions.RequireReceiver);
	//						StartGame = false;
	//						controller.Level += 1;
	//						CanReset = true;
	//					}


						CanPress = false;
					}
				}
			}
			else
			{
				one_click = false; // found a double click, now reset
				isInside = !isInside;
				//do double click things
			}

		}


		if(GameOver){
			controller.CurrentLevel = LevelLenght;
			StartGame = false;
			controller.SendMessage ("YouLose", SendMessageOptions.RequireReceiver);
//			if(controller.CurrentLevel > controller.Level)
//			controller.Level = LevelLenght;
			MoveSpeed = 55;
			CanPress = true;
			CanReset = true;
		}

		if (AngleDir == 0) {
			if (Angles.y < Angles.x - 7) {
				GameOver = true;
			}
			if (Angles.y < Angles.x + 7) 
				CanPress = true;

			}

			if (AngleDir == 1) {
				if (Angles.y > Angles.x + 7) {
					GameOver = true;
				}

			if (Angles.y > Angles.x - 7)
					CanPress = true;
			}
	}

	public void RePosition(){
		if (Angles.x != 0)
			GoInside = randomBoolean ();
		else
			GoInside = false;

		if (Angles.x == 0) {
			Angles.x = Random.Range (-140, 140);

			if(Angles.x < 0)
				AngleDir = 0;
			else
				if(Angles.x > 0)
				AngleDir = 1; 
		}
		else 
		{
			if(AngleDir == 0){
				Angles.x = Angles.x+Random.Range (0, 50);
				AngleDir = 1;
			}
			else
			if(AngleDir == 1){
				Angles.x = Angles.x-Random.Range (0, 50);
				AngleDir = 0;
			}
		}


//			if (Angles.x < 0) {
//			if (Angles.x > -35)
//				Angles.x = -35;
//			}
//			if (Angles.x > 0) {
//			if (Angles.x < 35)
//				Angles.x = 35;
//			}

		if(Mathf.Abs(Angles.x) - Mathf.Abs(Angles.y) < 50){
			if(AngleDir == 0)
				Angles.x = Angles.x-(50-(Mathf.Abs(Angles.x) - Mathf.Abs(Angles.y)));
			if(AngleDir == 1)
				Angles.x = Angles.x+(50-(Mathf.Abs(Angles.x) - Mathf.Abs(Angles.y)));
		}

		if (AngleDir == 0)
			MoveSpeed = 0-Mathf.Abs(MoveSpeed+(SpeedMultiplier * (LevelLenght/4)));
		if (AngleDir == 1)
			MoveSpeed = 0+Mathf.Abs(MoveSpeed-(SpeedMultiplier * (LevelLenght/4)));	
		
	}

	bool randomBoolean ()
	{
		if (Random.value >= 0.5)
		{
			return true;
		}
		return false;
	}

	public void setSkin(int skin){
		CurrentPlayerShape = skin;
	}

}


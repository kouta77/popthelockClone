using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerObjectsController : MonoBehaviour {
	public bool StartGame;
	public int LevelLenght;


	public Transform Target;
	public Transform Player;

	public float MoveSpeed = 3f;


	public Vector2 Angles;

	public Text CurrentPoints;

	//**********private field***********//
	private GameController controller;
	private bool CanPress = false;
	private int AngleDir = 0;//0 = <    -- 1 = >
	private bool CanReset = false;
	public bool GoInside = false;
	public bool isInside = false;

	private Transform PlayerChild;
	private Transform TargetChild;
	private float SpeedMultiplier = 2f;
	public bool one_click = false;
	public bool timer_running;
	public float timer_for_double_click;
	public float delay = 25;


	void Start () {
		PlayerChild = Player.transform.GetChild (0);
		TargetChild = Target.transform.GetChild (0);

		controller = GameObject.FindObjectOfType<GameController> ();
		RePosition ();

		LevelLenght = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//player game speed
		//MoveSpeed = 55+SpeedMultiplier * LevelLenght;

		CurrentPoints.text = LevelLenght.ToString();
		if (GoInside) {
			TargetChild.transform.localScale = new Vector3(-0.2645999f,0.2645999f,0.2645999f);
		} else
			TargetChild.transform.localScale = new Vector3(0.2645999f,0.2645999f,0.2645999f);

		if (isInside) {
			PlayerChild.transform.localScale = new Vector3(-0.2645999f,0.2645999f,0.2645999f);
		} else
			PlayerChild.transform.localScale = new Vector3(0.2645999f,0.2645999f,0.2645999f);


		if (StartGame)
			Angles.y += MoveSpeed * Time.deltaTime;
		//values with '-' for clocklike rotation
		Target.eulerAngles = new Vector3 (0, 0, -Angles.x);
		Player.eulerAngles = new Vector3 (0, 0, -Angles.y);

		if (Input.GetMouseButtonDown (0)) 
		{
			if(!one_click)
			{
				if (StartGame == true)
				{
				one_click = true;
				timer_for_double_click = Time.time; // save the current time
				}

				if (StartGame == false) {
					StartGame = true;
				}

				if (CanPress) {
					if(CanReset == true){//Reset the game!
						CanPress = false;
						StartGame = false;
						Angles.y = 0;
						Angles.x = 0;
						MoveSpeed = 55;
						isInside = false;
						RePosition();

						LevelLenght = 0;//controller.Level;//reset the counter

						controller.SendMessage("ResetUI",SendMessageOptions.RequireReceiver);
						CanReset = false;
					}
				else{
						if (GoInside == true)
						{
							if(isInside == true)
							{
							LevelLenght += 1;
							RePosition();
							}
						}
						else 
						if (GoInside == false)
						{
							if(isInside == false)
							{
								LevelLenght += 1;
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
		if(one_click)
		{
			if((Time.time - timer_for_double_click) > delay)
			{
				one_click = false;
				
			}
		}

		if (AngleDir == 0) {
			if (Angles.y < Angles.x - 15) {
				StartGame = false;
				controller.SendMessage ("YouLose", SendMessageOptions.RequireReceiver);
				controller.Level = LevelLenght;
				MoveSpeed = 55;
				CanReset = true;
			}
			if (Angles.y < Angles.x + 15) 
				CanPress = true;

			}

			if (AngleDir == 1) {
				if (Angles.y > Angles.x + 15) {
					StartGame = false;
					controller.SendMessage ("YouLose", SendMessageOptions.RequireReceiver);
					controller.Level = LevelLenght;
					MoveSpeed = 55;
					CanReset = true;	
				}

				if (Angles.y > Angles.x - 15)
					CanPress = true;
			}
	}

	public void RePosition(){
		GoInside = randomBoolean();

		if (Angles.x == 0) {
			Angles.x = Random.Range (-180, 180);

			if(Angles.x < 0)
				AngleDir = 0;
			else
				if(Angles.x > 0)
				AngleDir = 1; 
		}
		else 
		{
			if(AngleDir == 0){
				Angles.x = Angles.x+20+Random.Range (0, 90);
				AngleDir = 1;
			}
			else
			if(AngleDir == 1){
				Angles.x = Angles.x-20-Random.Range (0, 90);
				AngleDir = 0;
			}
		}

		if (Angles.x == 0) {
			if (Angles.x < 0) {
				if (Angles.x > -25)
					Angles.x = -25;
			}
			if (Angles.x > 0) {
				if (Angles.x < 25)
					Angles.x = 25;
			}
		}

		if (AngleDir == 0)
			MoveSpeed = 0-Mathf.Abs(MoveSpeed+SpeedMultiplier * LevelLenght);
		if (AngleDir == 1)
			MoveSpeed = 0+Mathf.Abs(MoveSpeed-SpeedMultiplier * LevelLenght);	
		
	}

	bool randomBoolean ()
	{
		if (Random.value >= 0.5)
		{
			return true;
		}
		return false;
	}

}


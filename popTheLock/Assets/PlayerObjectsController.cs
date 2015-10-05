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
	public bool CanReset = false;

	void Start () {
		controller = GameObject.FindObjectOfType<GameController> ();
		RePosition ();

		LevelLenght = controller.Level;
	}
	
	// Update is called once per frame
	void Update () {
		CurrentPoints.text = LevelLenght.ToString();

		if (StartGame)
			Angles.y += MoveSpeed * Time.deltaTime;
		//values with '-' for clocklike rotation
		Target.eulerAngles = new Vector3 (0, 0, -Angles.x);
		Player.eulerAngles = new Vector3 (0, 0, -Angles.y);

		if (Input.GetMouseButtonDown (0)) {
			if (StartGame == false) {
				StartGame = true;
			}

			if (CanPress) {
				if(CanReset == true){//Reset the game!
					CanPress = false;
					StartGame = false;
					Angles.y = 0;
					Angles.x = 0;
					RePosition();

					LevelLenght = controller.Level;//reset the counter

					controller.SendMessage("ResetUI",SendMessageOptions.RequireReceiver);
					CanReset = false;
				}
			else{
					if (LevelLenght > 1)
					{
						LevelLenght -= 1;
						RePosition();
					}
					else
					{
						controller.SendMessage ("YouWin", SendMessageOptions.RequireReceiver);
						StartGame = false;
						controller.Level += 1;
						CanReset = true;
					}
					CanPress = false;
				}
			}
		}

		if (AngleDir == 0) {
			if (Angles.y < Angles.x - 15) {
				StartGame = false;
				controller.SendMessage ("YouLose", SendMessageOptions.RequireReceiver);
				CanReset = true;
			}
			if (Angles.y < Angles.x + 15) 
				CanPress = true;

			}

			if (AngleDir == 1) {
				if (Angles.y > Angles.x + 15) {
					StartGame = false;
					controller.SendMessage ("YouLose", SendMessageOptions.RequireReceiver);
					CanReset = true;	
				}

				if (Angles.y > Angles.x - 15)
					CanPress = true;
			}
	}

	public void RePosition(){
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
			MoveSpeed = 0-Mathf.Abs(MoveSpeed);
		if (AngleDir == 1)
			MoveSpeed = 0+Mathf.Abs(MoveSpeed);	
		
	}
}

using UnityEngine;
using System.Collections;

public class PlayerObjectsController : MonoBehaviour {
	public bool StartGame;
	public int LevelLenght;


	public Transform Target;
	public Transform Player;

	public float MoveSpeed = 3f;


	public Vector2 Angles;


	//**********private field***********//
	private GameController controller;
	private bool CanPress = false;
	public int AngleDir = 0;//0 = <    -- 1 = >

	void Start () {
		controller = GameObject.FindObjectOfType<GameController> ();
		RePosition ();

		LevelLenght = controller.Level;
	}
	
	// Update is called once per frame
	void Update () {
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

				if (LevelLenght > 0)
				{
					LevelLenght -= 1;
					RePosition();
				}
				else
					controller.SendMessage ("YouWin", SendMessageOptions.RequireReceiver);

				CanPress = false;
			}
		}

		if (AngleDir == 0) {
			if (Angles.y < Angles.x - 15) {
				StartGame = false;
				controller.SendMessage ("YouLose", SendMessageOptions.RequireReceiver);
			}
			if (Angles.y < Angles.x + 15) 
				CanPress = true;

			}

			if (AngleDir == 1) {
				if (Angles.y > Angles.x + 15) {
					StartGame = false;
					controller.SendMessage ("YouLose", SendMessageOptions.RequireReceiver);
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

//			if(Angles.x < 0){
//				if(AngleDir == 0){
//					AngleDir = 1;
//					Angles.x = Random.Range (-90, Angles.x+90);
//				}
//				//else
//				if(AngleDir == 1){
//					AngleDir = 0;
//					Angles.x = Random.Range (90, Angles.x-90);
//				}
//			}
//			else
//			if(Angles.x > 0){
//				if(AngleDir == 0){
//					AngleDir = 1;
//					Angles.x = Random.Range (90, Angles.x-90);
//				}
//				//else
//				if(AngleDir == 1){
//					AngleDir = 0;
//					Angles.x = Random.Range (-90, Angles.x+90);
//				}
//			}
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

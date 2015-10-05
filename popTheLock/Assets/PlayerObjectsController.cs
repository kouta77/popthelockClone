using UnityEngine;
using System.Collections;

public class PlayerObjectsController : MonoBehaviour {
	public bool StartGame;

	public Transform Target;
	public Transform Player;

	public float MoveSpeed = 3f;


	public Vector2 Angles;


	//**********private field***********//
	private GameController controller;

	void Start () {
		controller = GameObject.FindObjectOfType<GameController> ();
		RePosition ();
	}
	
	// Update is called once per frame
	void Update () {
		if(StartGame)
		Angles.y += MoveSpeed * Time.deltaTime;
		//values with '-' for clocklike rotation
		Target.eulerAngles = new Vector3 (0,0,-Angles.x);
		Player.eulerAngles = new Vector3 (0,0,-Angles.y);

		if (Input.GetMouseButtonDown (0)) {
			StartGame = true;
		}

		if (Angles.x < 0) {
			if(Angles.y < Angles.x-10){
				StartGame = false;
				controller.SendMessage("YouLose",SendMessageOptions.RequireReceiver);
			}
		}
		if (Angles.x > 0) {
			if(Angles.y > Angles.x+10){
				StartGame = false;
				controller.SendMessage("YouLose",SendMessageOptions.RequireReceiver);
			}
		}
		
	}


	public void RePosition(){
		Angles.x = Random.Range (-180,180);
		
		if (Angles.x < 0) {
			if(Angles.x > -30)
				Angles.x = -30;
		}
		if (Angles.x > 0) {
			if(Angles.x < 30)
				Angles.x = 30;
		}
		
		if (Angles.x < 0)
			MoveSpeed = -MoveSpeed;	
		
	}
}

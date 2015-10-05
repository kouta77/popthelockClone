using UnityEngine;
using System.Collections;

public class PlayerObjectsController : MonoBehaviour {
	public Transform Target;
	public Transform Player;

	public float MoveSpeed = 3f;


	public Vector2 Angles;
	//private Vector3 playerAngle;
	// Use this for initialization
	void Start () {
		RePosition ();
	}
	
	// Update is called once per frame
	void Update () {
		Angles.y += MoveSpeed * Time.deltaTime;
		//values with '-' for clocklike rotation
		Target.eulerAngles = new Vector3 (0,0,-Angles.x);
		Player.eulerAngles = new Vector3 (0,0,-Angles.y);


	
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

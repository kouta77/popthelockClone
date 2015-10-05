using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public int Level = 1;


	public GameObject LoseUI;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void YouLose(){
		LoseUI.SetActive (true);
	}

	void YouWin(){
		//LoseUI.SetActive (true);
	}
}

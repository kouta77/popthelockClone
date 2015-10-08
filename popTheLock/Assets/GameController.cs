using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int Level = 1;
	public Text CurrentLevelText;

	public GameObject LoseUI;
	public GameObject WinUI;

	public GameObject StartScreenUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CurrentLevelText.text = "Best: " + Level.ToString ();
	}

	void YouLose(){
		LoseUI.SetActive (true);
	}

	void YouWin(){
		WinUI.SetActive (true);
	}

	void ResetUI(){
		Debug.Log ("reset UI");
		WinUI.SetActive (false);
		LoseUI.SetActive (false);
		StartScreenUI.SetActive (true);
	}

	void startGame(){
		StartScreenUI.SetActive (false);
	}
}

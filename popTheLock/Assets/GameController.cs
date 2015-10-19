using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int CurrentLevel = 0;
	public int Level = 1;
	public Text CurrentLevelText;

	public GameObject LoseUI;
	public GameObject WinUI;

	public GameObject StartScreenUI;

	// Use this for initialization
	void Start () {
		Level = PlayerPrefs.GetInt ("Topscore", 0);
	}
	
	// Update is called once per frame
	void Update () {
		CurrentLevelText.text = "Top " + Level.ToString ();
	}

	void YouLose(){
		LoseUI.SetActive (true);
		if (CurrentLevel > Level) {
			PlayerPrefs.SetInt ("topscore", CurrentLevel);
			Level = CurrentLevel;
			PlayerPrefs.SetInt ("Topscore", Level);
		}
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

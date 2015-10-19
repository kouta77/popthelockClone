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
	public GameObject GameplayUI;

	public Color GameTheme;

	public SpriteRenderer[] spritesColor = new SpriteRenderer[5];

	public GameObject HelpDialog;
	// Use this for initialization
	void Start () {
		Level = PlayerPrefs.GetInt ("Topscore", 0);
	}
	
	// Update is called once per frame
	void Update () {
		CurrentLevelText.text = "Top " + Level.ToString ();
	}

	void YouLose(){
		GameplayUI.SetActive (false);
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
		GameTheme = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		foreach (SpriteRenderer spr in spritesColor) {
			spr.color = GameTheme;
		}

		Debug.Log ("reset UI");
		WinUI.SetActive (false);
		LoseUI.SetActive (false);
		GameplayUI.SetActive (false);
		StartScreenUI.SetActive (true);
	}

	void startGame(){
		StartScreenUI.SetActive (false);
		GameplayUI.SetActive (true);
	}

	public void ShowHelp(){
		HelpDialog.SetActive (!HelpDialog.active);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int CurrentLevel = 0;
	public int Level = 1;
	public Text CurrentLevelText;

	public GameObject LoseUI;
	public GameObject WinUI;

	public GameObject[] StartScreenUI = new GameObject[1];
	public GameObject GameplayUI;

    public Color[] presetColors = new Color[1];
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


	private int themeint = 0;

	IEnumerator ResetUI(){
		var oldTheme = themeint;
		themeint = Random.Range (0, 4);

		while (themeint == oldTheme) {
			themeint = Random.Range (0, 4);
			yield return null;
		}


		GameTheme = presetColors[themeint];//new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));



		foreach (SpriteRenderer spr in spritesColor) {
			spr.color = GameTheme;
		}

//		Debug.Log ("reset UI");
		WinUI.SetActive (false);
		LoseUI.SetActive (false);
		GameplayUI.SetActive (false);
		foreach (GameObject obj in StartScreenUI) {
			obj.SetActive(true);
		}
	}

	void startGame(){
		foreach (GameObject obj in StartScreenUI) {
			obj.SetActive(false);
		}
		GameplayUI.SetActive (true);
	}

	public void ShowHelp(){
		HelpDialog.SetActive (!HelpDialog.active);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int CurrentLevel = 0;
	public int Level = 1;
	public Text CurrentLevelText;

	public GameObject[] LoseUI = new GameObject[1];
	public GameObject WinUI;

	public GameObject[] StartScreenUI = new GameObject[1];
	public GameObject GameplayUI;

    public Color[] presetColors = new Color[1];
	public Color GameTheme;

	public SpriteRenderer[] spritesColor = new SpriteRenderer[5];

	public GameObject HelpDialog;

	public Image[] ToneChangeUI = new Image[1];
	public SpriteRenderer[] ToneChangeUISPR = new SpriteRenderer[1];

	public Sprite[] DarkUI = new Sprite[1];
	public Sprite[] DarkUI2 = new Sprite[1];
	public Sprite[] lightUI = new Sprite[1];
	public Sprite[] lightUI2 = new Sprite[1];
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
		foreach (GameObject obj in LoseUI) {
			obj.SetActive (true);
		}

		if (CurrentLevel > Level) {
			PlayerPrefs.SetInt ("topscore", CurrentLevel);
			Level = CurrentLevel;
			PlayerPrefs.SetInt ("Topscore", Level);
			this.gameObject.SendMessage("PostScore", Level, SendMessageOptions.RequireReceiver);
		}
	}

	void YouWin(){
		WinUI.SetActive (true);
	}


	private int themeint = 0;

	IEnumerator ResetUI(bool reset){
		var oldTheme = themeint;
		themeint = Random.Range (0, 5);


		while (themeint == oldTheme) {
			themeint = Random.Range (0, 5);
			yield return null;
		}


		for (int i=0;i<ToneChangeUI.Length;i++) {
			if(themeint == 1 || themeint == 4)
			{
				ToneChangeUI[i].sprite = lightUI[i];
			}
			else
			{
				ToneChangeUI[i].sprite = DarkUI[i];
			}

		}
		for (int i=0;i<ToneChangeUISPR.Length;i++) {
			if(themeint == 1 || themeint == 4)
			{
				ToneChangeUISPR[i].sprite = lightUI2[i];
			}
			else
			{
				ToneChangeUISPR[i].sprite = DarkUI2[i];
			}
			
		}

		GameTheme = presetColors[themeint];//new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));



		foreach (SpriteRenderer spr in spritesColor) {
			spr.color = GameTheme;
		}

		if (reset == true) {
			WinUI.SetActive (false);
			foreach (GameObject obj in LoseUI) {
				obj.SetActive (false);
			}
			GameplayUI.SetActive (false);
			foreach (GameObject obj in StartScreenUI) {
				obj.SetActive (true);
			}
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

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//can be acessed from everywhere
	public static bool isPlaying;

	//get canvas
	public GameObject canvasPause;
	public GameObject canvasVictory;
	public GameObject canvasPlaying;
	public GameObject canvasDeath;

	//public Text levelText;

	int score = 0;
	[HideInInspector]
	public int coins = 0;

	public Sprite coinFull;
	public Sprite coinEmpty;

	void Awake()
	{
		//checks the scene in order to delete every redundant game managers
		//DontDestroyOnLoad(this.gameObject);
		/*GameObject [] gm = GameObject.FindGameObjectsWithTag("GameManager");
		if (gm.Length > 1)
		{
			foreach (GameObject go in gm)
			{
				if (go.gameObject != this.gameObject)
					Destroy(go.gameObject);
			}
		}*/
	}

	void Start () 
	{
		Time.timeScale = 1;
		//resets score if needed
		if (Application.loadedLevel == 1)
		{
			score = 0;
		}
		coins = 0;
		isPlaying = true;
		canvasPause.SetActive(false);
		canvasVictory.SetActive(false);
		canvasPlaying.SetActive(true);
		canvasDeath.SetActive(false);
		//levelText.text = "Level " + Application.loadedLevel.ToString();
	}
	
	void Update () 
	{
		//sets pause
		if (Input.GetButtonDown("Pause") && !canvasDeath.activeSelf && !canvasVictory.activeSelf)
		{
			canvasPause.SetActive(!canvasPause.activeSelf);
			isPlaying = !isPlaying;
			Time.timeScale = (Time.timeScale == 1) ? 0 : 1;
		}
		if (canvasPlaying.activeSelf)
		{
			if (coins == 1)
				GameObject.Find("Coin1").GetComponent<Image>().sprite = coinFull;
			if (coins == 2)
				GameObject.Find("Coin2").GetComponent<Image>().sprite = coinFull;
			if (coins == 3)
				GameObject.Find("Coin3").GetComponent<Image>().sprite = coinFull;
			if (coins > 3)
				coins = 3;
		}
	}

	//triggers victory event
	public void Victory()
	{
		score += 1000;
		isPlaying = false;
		canvasPlaying.SetActive(false);
		canvasPause.SetActive(false);
		canvasVictory.SetActive(true);
	}

	//triggers death event
	public void Death()
	{
		//destroys player
		GameObject[] playerGo = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject go in playerGo)
			Destroy(go);
		isPlaying = false;
		canvasPlaying.SetActive(false);
		canvasPause.SetActive(false);
		canvasVictory.SetActive(false);
		canvasDeath.SetActive(true);
	}

	#region ButtonHandling

	public void OnButtonReload()
	{
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevel);
	}

	public void OnButtonMenu()
	{
		Time.timeScale = 1;
		Application.LoadLevel(0);
	}

	public void OnButtonWin()
	{
		Time.timeScale = 1;
		if (Application.loadedLevel + 1 <= Application.levelCount)
			Application.LoadLevel(Application.loadedLevel + 1);
		else
			Application.LoadLevel(0);
	}

	#endregion
}

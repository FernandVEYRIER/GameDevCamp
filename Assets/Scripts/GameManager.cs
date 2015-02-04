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

	public Text levelText;
	int score = 0;

	void Start () 
	{
		isPlaying = true;
		canvasPause.SetActive(false);
		canvasVictory.SetActive(false);
		canvasPlaying.SetActive(true);
		canvasDeath.SetActive(false);
		levelText.text = "Level " + Application.loadedLevel.ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//sets pause
		if (Input.GetButtonDown("Pause"))
		{
			canvasPause.SetActive(!canvasPause.activeSelf);
			isPlaying = !isPlaying;
			Time.timeScale = (Time.timeScale == 1) ? 0 : 1;
		}
	}

	public void Victory()
	{
		score += 1000;
		isPlaying = false;
		canvasPlaying.SetActive(false);
		canvasPause.SetActive(false);
		canvasVictory.SetActive(true);
	}

	public void Death()
	{
		isPlaying = false;
		canvasPlaying.SetActive(false);
		canvasPause.SetActive(false);
		canvasVictory.SetActive(false);
		canvasDeath.SetActive(true);
	}

	public void OnButtonReload()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void OnButtonMenu()
	{
		Application.LoadLevel(0);
	}
}

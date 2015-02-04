using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//can be acessed from everywhere
	public static bool isPlaying;

	public GameObject canvasPause;
	public GameObject canvasVictory;

	// Use this for initialization
	void Start () 
	{
		isPlaying = true;
		canvasPause.SetActive(false);
		canvasVictory.SetActive(false);
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
}

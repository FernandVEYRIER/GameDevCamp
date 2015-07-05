using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameManager : MonoBehaviour {

	//can be acessed from everywhere
	public static bool isPlaying;
	public static GameManager GameManagerInstance;
	
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

	LevelData [] data;

	void Awake()
	{
		// Allocates the data necessary for saving all the levels infos
		data = new LevelData[ Application.levelCount - 2 ];
		LoadData();

		// Checks for an existing instance of a game manager
		if  ( GameManagerInstance == null )
		{
			DontDestroyOnLoad( this.gameObject );
			GameManagerInstance = this;
		}
		// if it already exist, destroys it
		else
		{
			Destroy( this.gameObject );
		}
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
		// We don't want to have the overlay active in the menu
		if ( Application.loadedLevel > 0 )
			canvasPlaying.SetActive(true);
		else
			canvasPlaying.SetActive(true);
		canvasDeath.SetActive(false);
		//levelText.text = "Level " + Application.loadedLevel.ToString();
	}
	
	void Update () 
	{
		if ( Input.GetKeyDown( KeyCode.A ) )
		{
			SaveData();
		}
		if ( Input.GetKeyDown( KeyCode.B ) )
		{
			LoadData();
		}

		// if we are in the main menu level, don't execute anything
		if ( Application.loadedLevel < 1 )
		{
			return;
		}
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

		SaveData ();
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

	public void SaveData()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;

		// Ignores save if we are of the outro level or the main menu
		if ( Application.loadedLevel - 1 < 0 || Application.loadedLevel == Application.levelCount - 1 )
		{
			return;
		}

		file = File.Open( Application.persistentDataPath + "/playerData.dat", FileMode.OpenOrCreate );

		data[ Application.loadedLevel - 1 ] = new LevelData();
		if ( data[ Application.loadedLevel - 1 ].levelCoins < coins )
			data[ Application.loadedLevel - 1 ].levelCoins = coins;
		data[ Application.loadedLevel - 1 ].level = Application.loadedLevel;
		bf.Serialize( file, data );
		Debug.Log("level = " + Application.loadedLevel + " data size = " + data.Length );
		file.Close();
	}

	public void LoadData()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;

		if ( File.Exists( Application.persistentDataPath + "/playerData.dat" ) )
		{
			file = File.Open( Application.persistentDataPath + "/playerData.dat", FileMode.Open );
		}
		else
		{
			Debug.LogError( "Could not open file." );
			return;
		}

		data = (LevelData[]) bf.Deserialize( file );
		file.Close();
	}

}

// Contains the necessary data per level
[Serializable]
class LevelData
{
	public int level;
	public int levelCoins;
}

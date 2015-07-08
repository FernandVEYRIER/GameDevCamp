using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class 						Manage_Menu : MonoBehaviour
{
	public GameObject[]				Menu_button;
	public GameObject[]				Settings_button;
	public GameObject[]				Level_Type;
	public AudioSource				Music;
	public Slider					volume_slide;
	public GameObject				Levels;

	// Button handling
	public GameObject 				levelMenu;
	public GameObject 				levelButtonTemplate;
	public Sprite					coinEmpty;
	public Sprite					coinFull;

	private static GameObject[][]	array = new GameObject[3][];
	private Animator				my_anim;
	private List<int[]>				chosen_menu = new List<int[]>();

	LevelData[] data;

	void Awake()
	{
		LoadData();
	}

	void	Start()
	{

		/*
		**	I set my array of GameObject array whith my two arrays in order to use it
		**		by choosing one of the two with a parameter in function Launch_animation()
		*/

		array[0] = Menu_button;
		array[1] = Settings_button;
		array[2] = Level_Type;

		/*
		**	I make button menu come to the scene by the animations
		*/

		StartCoroutine(Launch_anim(0, "come"));

		loadLevelMenu();
		Levels.SetActive(false);
	}

	void loadLevel( int level )
	{
		Application.LoadLevel( level );
	}

	void loadLevelMenu()
	{

		// Adding buttons
		// Should be done when the player opens the level menu
		for( int i = 0; i < Application.levelCount - 2; i++ )
		{
			GameObject button = (GameObject) Instantiate( levelButtonTemplate );
			button.transform.SetParent( levelMenu.transform );
			button.GetComponentInChildren<Text>().text = "LEVEL " + ( i + 1 );

			int tmp = i + 1;
			button.GetComponent<Button>().onClick.AddListener( delegate { loadLevel( tmp ); } );
			if ( i < data.Length && data[i] != null )
			{
				for ( int j = 1; j < 4; j++ )
				{
					if ( j <= data[i].levelCoins )
					{
						button.transform.GetChild( j ).GetComponent<Image>().sprite = coinFull;
					}
					else
					{
						button.transform.GetChild( j ).GetComponent<Image>().sprite = coinEmpty;
					}
				}
			}
			else
			{
				for ( int j = 1; j < 4; j++ )
				{
					button.transform.GetChild( j ).GetComponent<Image>().sprite = coinEmpty;
				}
			}
		}
	}

	public void		InGame_press()
	{
		/*
		**	The choice ingame level make appear the ingame levels + leave last
		*/

		int[]		ingame = new int[] {2, -1};

		chosen_menu.Add(ingame);
		StartCoroutine(Launch_anim(2, "leave"));
		Levels.SetActive(true);
	}

	public void		Settings_press()
	{
		/*
		**	When the setting button is pressed we make the menu button leave
		**		and come the settings button
		**	We push the menus : menu and settings
		*/
		int[] setts = new int[] {0, 1};

		chosen_menu.Add(setts);
		StartCoroutine(Launch_anim(0, "leave"));
		StartCoroutine(Launch_anim(1, "come"));
	}

	public void		Return_press()
	{
		/*
		**	When the setting button is pressed we make the last menus leave and come
		**	We pop the last menus in list
		*/
		int[]	menus;

		menus = chosen_menu[chosen_menu.Count - 1];
		StartCoroutine(Launch_anim(menus[0], "come"));
		StartCoroutine(Launch_anim(menus[1], "leave"));
		chosen_menu.RemoveAt(chosen_menu.Count - 1);
	}

	public void		Quit_press()
	{
		/*
		**	When you click on the quit button... Bitch why do you feel the necessity of reading this comment
		*/

		Application.Quit();
	}

	public void		Play_press()
	{
		/*
		**	We make menu leave and play select come
		**	Don't forget to push the last two menus
		*/

		int[]		plays = new int[] {0, 2};

		chosen_menu.Add(plays);
		StartCoroutine(Launch_anim(0, "leave"));
		StartCoroutine(Launch_anim(2, "come"));
	}

	public void		Change_volume()
	{
		AudioListener.volume = volume_slide.value;
		//Music.GetComponent<AudioManager>().changeVolume(volume_slide.value);
		//appeler l'audio manager de fernand
	}

	IEnumerator 	Launch_anim(int which_one, string var)
	{
		int			i;
		Animator	my_anim;
		int			len;

		if (which_one == -1)
		{
			Levels.SetActive(false);
			yield return(0);
		}
		for (i = 0, len = array[which_one].Length; i < len; i++)
		{
			my_anim = array[which_one][i].GetComponent<Animator>();
			my_anim.SetBool(var, true);
			yield return new WaitForSeconds(0.2f);
			my_anim.SetBool(var, false);
		}
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

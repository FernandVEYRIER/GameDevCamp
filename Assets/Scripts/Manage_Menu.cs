using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class 						Manage_Menu : MonoBehaviour
{
	public GameObject[]				Menu_button;
	public GameObject[]				Settings_button;
	public AudioSource					Music;
	public Slider								volume_slide;

	// Button handling
	public GameObject 					levelMenu;
	public GameObject 					levelButtonTemplate;
	public Sprite							coinEmpty;
	public Sprite							coinFull;

	private static GameObject[][]	array = new GameObject[2][];
	private Animator						my_anim;

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

		/*
		**	I make button menu come to the scene by the animations
		*/

		StartCoroutine(Launch_anim(0, "come"));

		loadLevelMenu();
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

			int tmp = i;
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

	public void		Settings_press()
	{
		/*
		**	When the setting button is pressed I make the menu button leave
		**		and come the settings button
		*/

		StartCoroutine(Launch_anim(0, "leave"));
		StartCoroutine(Launch_anim(1, "come"));
	}

	public void		Return_press()
	{
		/*
		**	When the setting button is pressed I make the settings button leave
		**		and come the menu button
		*/

		StartCoroutine(Launch_anim(0, "come"));
		StartCoroutine(Launch_anim(1, "leave"));
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
		Application.LoadLevel(1);
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

		i = 0;
		while (i < array[which_one].Length)
		{
			my_anim = array[which_one][i].GetComponent<Animator>();
			my_anim.SetBool(var, true);
			yield return new WaitForSeconds(0.2f);
			my_anim.SetBool(var, false);
			i = i + 1;
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

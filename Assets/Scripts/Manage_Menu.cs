using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class 						Manage_Menu : MonoBehaviour
{
	public GameObject[]				Menu_button;
	public GameObject[]				Settings_button;
	public AudioSource				Music;
	private static GameObject[][]	array = new GameObject[2][];
	private Animator				my_anim;

	void	Start()
	{
		array[0] = Menu_button;
		array[1] = Settings_button;
		StartCoroutine(Launch_anim(0, "come"));
	}

	public void		Settings_press()
	{
		StartCoroutine(Launch_anim(0, "leave"));
		StartCoroutine(Launch_anim(1, "come"));
	}

	public void		Return_press()
	{
		StartCoroutine(Launch_anim(0, "come"));
		StartCoroutine(Launch_anim(1, "leave"));
	}

	public void		Quit_press()
	{
		Application.Quit();
	}

	public void		Play_press()
	{
		//Application.LoadLevel('[insert_level_here]');
	}

	public void		Change_volume()
	{
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
}

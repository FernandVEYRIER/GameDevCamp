using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class AudioManager : MonoBehaviour {

	//this contains the menu clip
	public AudioClip menuClip;

	float volume = 1;

	//and this all the other clips
	public AudioClip [] audioClip;

	void Start () 
	{
		audio.loop = true;
		audio.volume = volume;
		if (Application.loadedLevel == 0)
			audio.Play();
		else
			StartCoroutine(playSongs());
	}

	IEnumerator playSongs()
	{
		while (true)
		{
			foreach(AudioClip clip in audioClip)
			{
				audio.clip = clip;
				audio.Play();
				yield return new WaitForSeconds(audio.clip.length);
			}
		}
	}

	//call this funtion to change the audio volume
	public void changeVolume(float value)
	{
		audio.volume = value;
	}
}

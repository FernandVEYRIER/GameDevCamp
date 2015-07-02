using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (AudioSource))]
public class AudioManager : MonoBehaviour {

	//this contains the menu clip
	public AudioClip menuClip;
	public Slider _slider;

	float volume = 1;

	//and this all the other clips
	public AudioClip [] audioClip;

	// Keeps track of the instances of audioManagers already deployed to prevent stacks
	public static bool isAlreadyInstanced;

	void Awake ()
	{
		if ( isAlreadyInstanced )
		{
			Destroy( this.gameObject );
		}
		else
		{
			isAlreadyInstanced = true;
			DontDestroyOnLoad( this.gameObject );
		}
	}

	void Start () 
	{
		audio.loop = true;
		audio.volume = volume;

		if ( !audio.isPlaying )
		{
			StartCoroutine( playSongs() );
		}
		return;
	}

	void Update()
	{
		GameObject newSlider;
		if ( _slider == null )
		{
			newSlider = GameObject.Find("Slider");
			// Need to check before because the object might be deactivated, so unreachable
			if ( newSlider != null )
			{
				_slider = newSlider.GetComponent<Slider>();
			}
		}
		else
		{
            audio.volume = _slider.value;
		}
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

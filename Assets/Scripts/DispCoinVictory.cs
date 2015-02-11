using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DispCoinVictory : MonoBehaviour {

	public Sprite coinFull;
	public Image[] coins;

	int coinAmount;

	void Start () 
	{
		coinAmount = GameObject.Find("_GameManager").GetComponent<GameManager>().coins;
		StartCoroutine(dispCoins());
	}

	IEnumerator dispCoins()
	{
		for (int i = 0; i < 3 ; i++)
		{
			yield return new WaitForSeconds(0.8f);
			if (coinAmount > i)
				coins[i].sprite = coinFull;
		}
	}
}

using System.Collections;
using UnityEngine;


public class LoadingUIActivationProcess : MonoBehaviour
{

	[SerializeField]
	private GameObject slider, border, textLoading, textPercentage;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		StartCoroutine(StartAnimation());
	}

	private IEnumerator StartAnimation()
	{
		border.SetActive(true);
		yield return new WaitForSeconds(1.8f);
		textLoading.SetActive(true);
		yield return new WaitForSeconds(1f);
		slider.SetActive(true);
		textPercentage.SetActive(true);
	}
}

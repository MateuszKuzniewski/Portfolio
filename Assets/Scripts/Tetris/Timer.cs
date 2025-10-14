using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{
	public bool IsFinished => isFinished;

	public event Action OnFinished;


	[SerializeField]
	private TextMeshProUGUI displayText;

	private string originalText;

	private readonly WaitForSeconds delay = new WaitForSeconds(1);

	private int count = 0;

	private bool isFinished = true;

	private void Awake()
	{
		originalText = displayText.text;
	}

	public void StartCounter(int seconds)
	{
		count = seconds;
		StartCoroutine(StartCount());
	}

	public void StopCounter()
	{
		isFinished = true;
		StopAllCoroutines();
	}

	public void Reset()
	{
		displayText.text = originalText;
	}

	private IEnumerator StartCount()
	{
		isFinished = false;
		while (count > 0)
		{
			displayText.text = count.ToString();
			yield return delay;
			count--;
		}

		isFinished = true;
		OnFinished?.Invoke();
		yield return null;
	}
}

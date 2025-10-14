using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using System;


public class TextSlowType : MonoBehaviour
{
	public event Action OnFinished;


	[SerializeField]
	private float delay = 0;

	[SerializeField]
	private float startDelay = 0;

	[SerializeField]
	private List<string> sentanceList;

	[SerializeField]
	private bool startInstantly = false;


	private TextMeshProUGUI text;


	void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
		if (!startInstantly)
		{
			gameObject.SetActive(false);
		}
		text.maxVisibleCharacters = 0;
	}

	void Start()
	{
		StartCoroutine(ShowText());
	}

	private IEnumerator ShowText()
	{
		if (startDelay > 0)
		{
			yield return new WaitForSeconds(startDelay);
		}

		if (sentanceList.Count == 0)
		{
			for (int i = 0; i < text.text.Length + 1; i++)
			{
				text.maxVisibleCharacters = i;
				yield return new WaitForSeconds(delay);
			}
		}
		else
		{
			foreach (var sentance in sentanceList)
			{
				for (int i = 0; i < sentance.Length + 1; i++)
				{
					text.text = sentance;
					text.maxVisibleCharacters = i;
					yield return new WaitForSeconds(delay);
				}
			}
		}

		OnFinished?.Invoke();
	}
}

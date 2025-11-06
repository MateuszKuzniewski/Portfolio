using System.Collections;
using TMPro;
using UnityEngine;

public class SystemMessager : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI displayText;

	[SerializeField]
	private CanvasGroup canvasGroup;

	[SerializeField]
	private float fadeDuration;

	private bool isFinished = true;

	void Awake()
	{
		canvasGroup.alpha = 0;
	}

	public void Display(string text)
	{
		displayText.text = text;
		
		if (isFinished)
		{
			StartCoroutine(TextFade());
		}
	}
	
	private IEnumerator TextFade()
	{
		canvasGroup.alpha = 1f;
		isFinished = false;

		var elapsed = 0.0f;

		while (elapsed < fadeDuration)
		{
			elapsed += Time.deltaTime;
			canvasGroup.alpha = Mathf.Lerp(1.0f, 0, elapsed / fadeDuration);
			yield return null;
		}

		isFinished = true;
		canvasGroup.alpha = 0.0f;
	}
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonOnClickCopy : MonoBehaviour
{
	[SerializeField]
	private SystemMessager systemMessager;

	[SerializeField]
	private string textToCopy;

	[SerializeField]
	private string feedbackText;

	private Button button;

	void Awake()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(AddToClipboard);
	}

	private void AddToClipboard()
	{
		// GUIUtility.systemCopyBuffer = textToCopy;

		WebGLCopyAndPaste.WebGLCopyAndPasteAPI.CopyToClipboard(textToCopy);

		if (!string.IsNullOrEmpty(feedbackText))
		{
			systemMessager.Display(feedbackText);
		}
	} 
}

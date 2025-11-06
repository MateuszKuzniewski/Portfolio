using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClickEmail : MonoBehaviour
{
	[SerializeField]
	private string text;

	private Button button;

	private StringBuilder stringBuilder = new StringBuilder();

	void Awake()
	{
		button = GetComponent<Button>();

		button.onClick.AddListener(OpenEmail);

		stringBuilder.AppendFormat("mailto:{0}", text);
	}

	private void OpenEmail()
	{
		Application.OpenURL(stringBuilder.ToString());
	}
}

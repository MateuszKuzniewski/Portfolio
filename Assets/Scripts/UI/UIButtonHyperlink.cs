using UnityEngine;
using UnityEngine.UI;


public class UIButtonHyperlink : MonoBehaviour
{
	public string Url { get => url; set => url = value; }

	[SerializeField]
	private string url;

	private Button button;

	void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OpenLink);
	}

	private void OpenLink()
	{
		Application.OpenURL(url);
	}
}

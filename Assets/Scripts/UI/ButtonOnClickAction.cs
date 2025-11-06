using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonOnClickAction : MonoBehaviour
{
	private Button button;
	
	private ContentPageManager contentPageManager;

	[SerializeField]
	private BrowserElementView browserElementView;


	void Start()
	{
		contentPageManager = GameObject.FindGameObjectWithTag("ContentPageManager").GetComponent<ContentPageManager>();
		button = GetComponent<Button>();

		button.onClick.AddListener(ButtonOnClick);
	}

	private void ButtonOnClick()
	{
		contentPageManager.ShowPage(browserElementView.ProjectData);
	}
}

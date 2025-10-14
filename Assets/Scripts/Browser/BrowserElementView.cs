using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BrowserElementView : MonoBehaviour
{
	public TextMeshProUGUI Title { get => title; set => title = value; }
	public TextMeshProUGUI Description { get => description; set => description = value; }

	public Image Image { get => image; set => image = value; }

	public ProjectDataContainer ProjectData { get => projectData; set => projectData = value; }


	[SerializeField]
	private TextMeshProUGUI title, description;

	[SerializeField]
	private Image image;

	[SerializeField]
	private Button button;

	private ProjectDataContainer projectData;
}

using TMPro;
using UnityEngine;


public class UIColorTextFilter : MonoBehaviour, IUIColorFilter
{
	private UIColorManager uiColorManager;

	private TextMeshProUGUI text;

	private void Start()
	{
		uiColorManager = GameObject.FindGameObjectWithTag("UIColourManager").GetComponent<UIColorManager>();
		text = GetComponent<TextMeshProUGUI>();
		uiColorManager.Register(this);
	}

	public void ApplyColour(UIColourContainer colourPalette)
	{
		if (text != null)
		{
			text.color = colourPalette.GlobalColour;
		}
	}
}

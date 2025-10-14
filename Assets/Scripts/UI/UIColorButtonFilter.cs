using UnityEngine;
using UnityEngine.UI;


public class UIColorButtonFilter : MonoBehaviour, IUIColorFilter
{
	private UIColorManager uiColorManager;

	private Button button;

	private void Start()
	{
		uiColorManager = GameObject.FindGameObjectWithTag("UIColourManager").GetComponent<UIColorManager>();
		button = GetComponent<Button>();
		uiColorManager.Register(this);
	}

	public void ApplyColour(UIColourContainer colourPalette)
	{
		if (button != null)
		{
			var colours = button.colors;
			colours.normalColor = colourPalette.GlobalColour;
			colours.highlightedColor = colourPalette.HighlightedColour;
			colours.pressedColor = colourPalette.PressedColour;
			colours.selectedColor = colourPalette.SelectedColour;

			button.colors = colours;
		}
	}
}

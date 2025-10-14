using UnityEngine;
using UnityEngine.UI;


public class UIColorScrollbarFilter : MonoBehaviour, IUIColorFilter
{
	private UIColorManager uiColorManager;

	private Scrollbar scrollbar;

	private void Start()
	{
		uiColorManager = GameObject.FindGameObjectWithTag("UIColourManager").GetComponent<UIColorManager>();
		scrollbar = GetComponent<Scrollbar>();
		uiColorManager.Register(this);
	}

	public void ApplyColour(UIColourContainer colourPalette)
	{
		if (scrollbar != null)
		{
			var colours = scrollbar.colors;
			colours.normalColor = colourPalette.GlobalColour;
			colours.highlightedColor = colourPalette.HighlightedColour;
			colours.pressedColor = colourPalette.PressedColour;
			colours.selectedColor = colourPalette.SelectedColour;

			scrollbar.colors = colours;
		}
	}
}

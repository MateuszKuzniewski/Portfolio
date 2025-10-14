using UnityEngine;
using UnityEngine.UI;


public class UIButtonActiveState : MonoBehaviour, IUIButtonActiveState
{
	[SerializeField]
	private UIButtonStateManager uIButtonStateManager;

	private Button button;


	void Start()
	{
		button = GetComponent<Button>();
		uIButtonStateManager.Register(this);
	}


	public void SetActiveState(UIColourContainer colourContainer)
	{
		var colours = button.colors;
		colours.normalColor = colourContainer.SelectedColour;
		colours.highlightedColor = colourContainer.SelectedColour;
		colours.pressedColor = colourContainer.SelectedColour;
		colours.selectedColor = colourContainer.SelectedColour;

		button.colors = colours;
	}

	public void SetInactiveState(UIColourContainer colourContainer)
	{
		var colours = button.colors;
		colours.normalColor = colourContainer.GlobalColour;
		colours.highlightedColor = colourContainer.HighlightedColour;
		colours.pressedColor = colourContainer.PressedColour;
		colours.selectedColor = colourContainer.SelectedColour;

		button.colors = colours;
	}
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIButtonToggle : MonoBehaviour
{
	public bool IsOpen { get => isOpen; set => isOpen = value; }

	[SerializeField]
	private UIColorManager colourManager;

	[SerializeField]
	private GameObject pageMain, pageSecondary, leftArrow, rightArrow;

	private bool isOpen = false;

	private Button button;

	void Awake()
	{
		button = GetComponent<Button>();
	}


	public void TogglePage()
	{
		isOpen = !isOpen;
		pageMain.SetActive(!isOpen);
		pageSecondary.SetActive(isOpen);
		leftArrow.SetActive(isOpen);
		rightArrow.SetActive(isOpen);
		UpdateColourState();
	}

	private void UpdateColourState()
	{
		if (isOpen)
		{
			SetActiveState(colourManager.CurrentGlobalColour);
		}
		else
		{
			SetInactiveState(colourManager.CurrentGlobalColour);
		}

		var selectedObject = EventSystem.current.currentSelectedGameObject;

		if (selectedObject.GetComponent<UIButtonToggle>())
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	private void SetActiveState(UIColourContainer colourContainer)
	{
		var colours = button.colors;
		colours.normalColor = colourContainer.SelectedColour;
		colours.highlightedColor = colourContainer.SelectedColour;
		colours.pressedColor = colourContainer.SelectedColour;
		colours.selectedColor = colourContainer.SelectedColour;

		button.colors = colours;
	}

	private void SetInactiveState(UIColourContainer colourContainer)
	{
		var colours = button.colors;
		colours.normalColor = colourContainer.GlobalColour;
		colours.highlightedColor = colourContainer.HighlightedColour;
		colours.pressedColor = colourContainer.PressedColour;
		colours.selectedColor = colourContainer.SelectedColour;

		button.colors = colours;
	}
}

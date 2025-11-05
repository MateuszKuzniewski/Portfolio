using TMPro;
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

	[SerializeField]
	private UIScrollbarControl scrollbarControl;

	[SerializeField]
	private TextMeshProUGUI slideCounter;

	private bool isOpen = false;

	private Button button;

	void Awake()
	{
		button = GetComponent<Button>();
		scrollbarControl.OnValueChanged.AddListener(_ => UpdateImageControls());
	}

	public void TogglePage()
	{
		isOpen = !isOpen;
		pageMain.SetActive(!isOpen);
		pageSecondary.SetActive(isOpen);
		leftArrow.SetActive(isOpen);
		rightArrow.SetActive(isOpen);
		slideCounter.gameObject.SetActive(isOpen);
		UpdateColourState();
		UpdateImageControls();
	}

	private void UpdateImageControls()
	{
		if (pageSecondary.activeSelf)
		{
			var epsilon = 0.001f;
			leftArrow.SetActive(scrollbarControl.Value > epsilon);
			rightArrow.SetActive(scrollbarControl.Value < 1f - epsilon);
			UpdateSlideCounter();
		}
	}

	private void UpdateSlideCounter()
	{
		if (scrollbarControl.Value == 0)
		{
			slideCounter.text = "1 / 4";
			return;
		}
		slideCounter.text = Mathf.CeilToInt(scrollbarControl.Value * 4) + " / 4";
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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UIButtonStateManager : MonoBehaviour
{
	[SerializeField]
	private UIColorManager colorManager;

	private readonly List<UIButtonActiveState> listeners = new List<UIButtonActiveState>();

	private GameObject lastSelectedGameObject;


	public void Register(UIButtonActiveState buttonActiveState)
	{
		if (!listeners.Contains(buttonActiveState))
		{
			listeners.Add(buttonActiveState);
		}
	}

	public void SetStateActive()
	{
		var currentSelectedObject = EventSystem.current.currentSelectedGameObject;

		foreach (var button in listeners)
		{
			if (button.gameObject == currentSelectedObject)
			{
				lastSelectedGameObject = currentSelectedObject;
				button.SetActiveState(colorManager.CurrentGlobalColour);
			}
			else
			{
				button.SetInactiveState(colorManager.CurrentGlobalColour);
			}
		}
		
		// This prevents menu buttons deselecting when pressing UI colour buttons
		if (!listeners.Contains(EventSystem.current.currentSelectedGameObject.GetComponent<UIButtonActiveState>()))
		{
			var button = lastSelectedGameObject?.GetComponent<UIButtonActiveState>();
			button?.SetActiveState(colorManager.CurrentGlobalColour);
		}
	}

	public void ClearActiveState()
	{
		foreach (var button in listeners)
		{
			button.SetInactiveState(colorManager.CurrentGlobalColour);
		}

		lastSelectedGameObject = null;
	}
}

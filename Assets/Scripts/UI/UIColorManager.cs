using System.Collections.Generic;
using UnityEngine;


public class UIColorManager : MonoBehaviour
{
	public UIColourContainer CurrentGlobalColour => currentGlobalColour;


	[SerializeField]
	private UIColourContainer globalColourGreen;

	[SerializeField]
	private UIColourContainer globalColourRed;

	[SerializeField]
	private UIColourContainer globalColourBlue;

	[SerializeField]
	private UIColourContainer globalColourWhite;

	private UIColourContainer currentGlobalColour;

	private readonly List<IUIColorFilter> listeners = new List<IUIColorFilter>();


	private void Awake()
	{
		currentGlobalColour = globalColourGreen;
	}

	public void Register(IUIColorFilter uiColorFilter)
	{
		if (!listeners.Contains(uiColorFilter))
		{
			listeners.Add(uiColorFilter);
			uiColorFilter.ApplyColour(currentGlobalColour);
		}
	}


	public void ApplyDefaultColour()
	{
		ApplyColour(globalColourGreen);
	}

	public void ApplyRedColour()
	{
		ApplyColour(globalColourRed);
	}

	public void ApplyBlueColor()
	{
		ApplyColour(globalColourBlue);
	}

	public void ApplyWhiteColor()
	{
		ApplyColour(globalColourWhite);
	}

	private void ApplyColour(UIColourContainer colourContainer)
	{
		currentGlobalColour = colourContainer;
		foreach (var listener in listeners)
		{
			listener.ApplyColour(colourContainer);
		}
	}
}

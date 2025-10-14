using UnityEngine;
using UnityEngine.UI;


public class UIColorImageFilter : MonoBehaviour, IUIColorFilter
{
	private UIColorManager uiColorManager;

	private Image image;

	private void Start()
	{
		uiColorManager = GameObject.FindGameObjectWithTag("UIColourManager").GetComponent<UIColorManager>();
		image = GetComponent<Image>();
		uiColorManager.Register(this);
	}

	public void ApplyColour(UIColourContainer colourPalette)
	{
		if (image != null)
		{
			image.color = colourPalette.GlobalColour;
		}
	}
}

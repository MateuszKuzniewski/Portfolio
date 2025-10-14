using UnityEngine;


[CreateAssetMenu(fileName = "Colour Palette", menuName = "UI/Theme/ColourPalette", order = 0)]
public class UIColourContainer : ScriptableObject
{
	public Color GlobalColour;

	[Space]
	public Color HighlightedColour;
	public Color PressedColour;
	public Color SelectedColour;
}

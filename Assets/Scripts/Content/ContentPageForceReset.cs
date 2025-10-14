using UnityEngine;


public class ContentPageForceReset : MonoBehaviour
{
	[SerializeField]
	private GameObject main, gallery;

	[SerializeField]
	private UIButtonToggle buttonToggle;


	void OnEnable()
	{
		buttonToggle.IsOpen = true;
		buttonToggle.TogglePage();
		main.SetActive(true);
		gallery.SetActive(false);
		//TO DO: This is dumb, I need to set it to true despite wanting the page to be off cause
		//TogglePage flips the bool inside the method, needs to be fixed later
	}
}

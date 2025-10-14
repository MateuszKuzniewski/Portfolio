using UnityEngine;


public class BrowserPageManager : MonoBehaviour
{
	[SerializeField]
	private GameObject
	browserPage,
	workPage,
	personalPage,
	animatedPage,
	aboutmePage,
	aboutmePageContent,
	tetrisPage;


	public void ShowAnimatedPage()
	{
		if (!animatedPage.activeSelf)
		{
			animatedPage.SetActive(true);
		}
	}

	public void ShowWorkPage()
	{
		browserPage.SetActive(true);
		workPage.SetActive(true);
		personalPage.SetActive(false);
		aboutmePage.SetActive(false);
		aboutmePageContent.SetActive(false);
		tetrisPage.SetActive(false);
	}

	public void ShowPersonalPage()
	{
		browserPage.SetActive(true);
		personalPage.SetActive(true);
		workPage.SetActive(false);
		aboutmePage.SetActive(false);
		aboutmePageContent.SetActive(false);
		tetrisPage.SetActive(false);
	}

	public void ShowAboutMePage()
	{
		browserPage.SetActive(false);
		personalPage.SetActive(false);
		workPage.SetActive(false);
		aboutmePage.SetActive(true);
		aboutmePageContent.SetActive(true);
		tetrisPage.SetActive(false);
	}

	public void ShowTetrisPage()
	{
		tetrisPage.SetActive(true);
		browserPage.SetActive(false);
		personalPage.SetActive(false);
		workPage.SetActive(false);
		aboutmePage.SetActive(false);
		aboutmePageContent.SetActive(false);

	}

	public void HidePage()
	{
		browserPage.SetActive(false);
		workPage.SetActive(false);
		personalPage.SetActive(false);
		animatedPage.SetActive(false);
		aboutmePage.SetActive(false);
		aboutmePageContent.SetActive(false);
		tetrisPage.SetActive(false);
	}
}

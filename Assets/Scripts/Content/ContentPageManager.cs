using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ContentPageManager : MonoBehaviour
{
	[SerializeField]
	private GameObject contentPage, contentPageContainer, galleryButton, githubButton;

	[SerializeField]
	private TextMeshProUGUI title, description, technologies, languages, teamSize, developmentTime;

	[SerializeField]
	private VideoController videoController;

	[SerializeField]
	private List<Image> galleryImages;

	public void ShowPage(ProjectDataContainer projectDataContainer)
	{
		title.text = projectDataContainer.Title;
		description.text = projectDataContainer.Description;
		technologies.text = "technologies: " + projectDataContainer.Technologies;
		languages.text = "languages: " + projectDataContainer.Languages;
		developmentTime.text = "development time: " + projectDataContainer.DevelopmentTime;
		teamSize.text = "team size: " + projectDataContainer.TeamSize;

		contentPage.SetActive(true);
		contentPageContainer.SetActive(true);
		videoController.VideoURL = projectDataContainer.VideoURL;
		videoController.Prepare();

		var gitComponent = githubButton.GetComponent<UIButtonHyperlink>();
		gitComponent.Url = projectDataContainer.RepositoryUrl;

		if (!string.IsNullOrEmpty(gitComponent.Url))
		{
			githubButton.SetActive(true);
		}
		else
		{
			githubButton.SetActive(false);
		}

		if (projectDataContainer.Images.Count > 0)
		{
			galleryButton.SetActive(true);
			for (int i = 0; i < galleryImages.Count; i++)
			{
				if (projectDataContainer.Images.Count != 0)
				{
					galleryImages[i].sprite = projectDataContainer.Images[i];
				}
			}
		}
		else
		{
			galleryButton.SetActive(false);
		}
	}

	public void HidePage()
	{
		videoController.Stop();
		contentPage.SetActive(false);
		contentPageContainer.SetActive(false);
	}
}

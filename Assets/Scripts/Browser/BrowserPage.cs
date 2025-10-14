using UnityEngine;


public class BrowserPage : MonoBehaviour
{
	[SerializeField]
	private GameObject template, templateEmpty, leftColumnObject, rightColumnObject;

	[SerializeField]
	private ProjectsContainer columnLeft, columnRight;


	void Start()
	{
		InstantiateProjectColumns(columnLeft, leftColumnObject.transform);
		InstantiateProjectColumns(columnRight, rightColumnObject.transform);
	}


	private void InstantiateProjectColumns(ProjectsContainer projects, Transform transform)
	{
		foreach (var project in projects.Projects)
		{
			if (project)
			{
				var obj = Instantiate(template, transform);
				var projectTemplate = obj.GetComponent<BrowserElementView>();

				projectTemplate.ProjectData = project;
				projectTemplate.Title.text = project.Title;
				projectTemplate.Description.text = project.PreviewDescription;
				projectTemplate.Image.sprite = project.Thumbnail;
			}
			else
			{
				Instantiate(templateEmpty, transform);
			}
		}
	}
}

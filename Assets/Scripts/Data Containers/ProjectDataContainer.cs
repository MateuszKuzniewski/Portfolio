using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ProjectData", menuName = "ContentPage/ProjectData")]
public class ProjectDataContainer : ScriptableObject
{
	public Sprite Thumbnail;
	public string Title;
	public string PreviewDescription;

	[TextArea(20, 40)]
	public string Description;
	public string Technologies;
	public string Languages;
	public string RepositoryUrl;
	public string VideoURL;
	public string TeamSize;
	public string DevelopmentTime;

	public List<Sprite> Images;

}

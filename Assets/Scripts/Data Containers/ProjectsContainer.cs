using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ProjectsContainer", menuName = "Projects/ProjectsContainer")]
public class ProjectsContainer : ScriptableObject
{
	public List<ProjectDataContainer> Projects;
}

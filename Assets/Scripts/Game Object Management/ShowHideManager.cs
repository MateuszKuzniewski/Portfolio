using System;
using System.Collections.Generic;
using UnityEngine;


public class ShowHideManager : MonoBehaviour
{

	[SerializeField]
	private List<GameObject> objectList = new List<GameObject>();

	[SerializeField]
	private TextSlowType textSlowType;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		HideObjects();
		textSlowType.OnFinished += ShowObjects;
	}

	public void ShowObjects()
	{
		foreach (var obj in objectList)
		{
			if (!obj.activeSelf)
			{
				obj.SetActive(true);
			}
		}
	}

	public void HideObjects()
	{
		foreach (var obj in objectList)
		{
			if (obj.activeSelf)
			{
				obj.SetActive(false);
			}
		}
	}
}

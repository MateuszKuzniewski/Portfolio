using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SquareLineRenderer : MonoBehaviour
{
	[SerializeField]
	private UIColorManager uiColorManger;

	[SerializeField]
	private RectTransform target;

	[SerializeField]
	private int amountOfLines = 3;

	[SerializeField]
	private float thickness = 5;


	private List<GameObject> lines = new List<GameObject>();


	void Awake()
	{
		for (int i = 0; i < amountOfLines; i++)
			lines.Add(InstantiateLineSegment());
	}

	public void ShowLines()
	{
		var currentSelectedObject = EventSystem.current.currentSelectedGameObject;
		var currentRectTransform = currentSelectedObject.GetComponent<RectTransform>();

		var startOffset = currentRectTransform.rect.width / 2;
		var endOffset = target.rect.width / 2;

		var path = GetRightAnglePath(
			new Vector2(currentRectTransform.anchoredPosition.x + startOffset, currentRectTransform.anchoredPosition.y),
			new Vector2(target.anchoredPosition.x - endOffset, target.anchoredPosition.y));

		for (int i = 0; i < lines.Count; i++)
		{
			if (i % 2 != 0)
			{
				// flip every second line to be vertical
				UpdateLineAnchors(lines[i], path[i], path[i + 1], true);
			}
			else
			{
				UpdateLineAnchors(lines[i], path[i], path[i + 1], false);
			}
		}
	}

	public void HideLines()
	{
		foreach (var line in lines)
		{
			line.SetActive(false);
		}
	}

	private void UpdateLineAnchors(GameObject line, Vector2 startPoint, Vector2 endPoint, bool isVertical)
	{
		line.SetActive(true);
		var rectTransform = line.GetComponent<RectTransform>();
		rectTransform.localScale = Vector3.one;
		var distance = Vector2.Distance(startPoint, endPoint);

		var offset = 0.0f;
		if (isVertical)
		{
			rectTransform.sizeDelta = new Vector2(thickness, distance);
			offset = rectTransform.rect.height / 2;
			rectTransform.anchoredPosition = new Vector2(
			startPoint.x,
			startPoint.y + offset);
		}
		else
		{
			rectTransform.sizeDelta = new Vector2(distance, thickness);
			offset = rectTransform.rect.width / 2;
			rectTransform.anchoredPosition = new Vector2(
			startPoint.x + offset,
			startPoint.y);
		}
	}

	private GameObject InstantiateLineSegment()
	{
		var gameObject = new GameObject("LineSegment");
		gameObject.transform.SetParent(this.transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.AddComponent<Image>();

		gameObject.AddComponent<UIColorImageFilter>();
		gameObject.SetActive(false);

		return gameObject;
	}

	private Vector2[] GetRightAnglePath(Vector2 start, Vector2 end)
	{
		var pathPoints = new List<Vector2>();
		pathPoints.Add(start);

		var midX = (start.x + end.x) / 2f;

		var corner1 = new Vector2(midX, start.y);
		var corner2 = new Vector2(midX, end.y);

		pathPoints.Add(corner1);
		pathPoints.Add(corner2);
		pathPoints.Add(end);

		return pathPoints.ToArray();
	}
}

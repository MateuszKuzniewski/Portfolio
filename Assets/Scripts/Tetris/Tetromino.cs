using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
public class Tetromino : MonoBehaviour, IMoveable, ICollidable
{
	public RawImage Image => image;

	public int ID => id;


	[SerializeField]
	private int id;

	[SerializeField]
	private bool canRotate = true;


	private Transform parentGrid;

	private Vector2 gridBounds;

	private List<RectTransform> children;

	private List<TetrominoRenderer> renderers;

	private RawImage image;

	private Vector3 verticalOffset, horizontalOffset;


	void Awake()
	{
		children = GetComponentsInChildren<RectTransform>().Where(rt => rt != GetComponent<RectTransform>()).ToList();
		renderers = GetComponentsInChildren<TetrominoRenderer>().ToList();
		image = GetComponentInChildren<RawImage>();
	}

	public void Initialise(Transform parentGrid, Vector2 gridBounds, float verticalOffset, float horizontalOffset)
	{
		this.parentGrid = parentGrid;
		this.gridBounds = gridBounds;
		this.verticalOffset = new Vector3(0, verticalOffset, 0);
		this.horizontalOffset = new Vector3(horizontalOffset, 0, 0);

	}

	public void MoveVertical(Vector3 direction)
	{
		var moveOffset = verticalOffset.y * direction;
		transform.localPosition += moveOffset;
	}

	public void MoveHorizontal(Vector3 direction)
	{
		var isWithinBoundries = true;
		var moveOffset = horizontalOffset.x * direction;
		foreach (var block in children)
		{
			var localPos = ConvertToCanvasSpace(block.position) + moveOffset;

			if (!CheckBounds(localPos))
			{
				isWithinBoundries = false;
				break;
			}
		}

		if (isWithinBoundries)
		{
			transform.localPosition += moveOffset;
		}
	}

	public void Rotate(float rotateOffset, float moveOffset)
	{
		if (canRotate)
		{
			transform.RotateAround(transform.position, Vector3.forward, rotateOffset);

			foreach (var block in children)
			{
				var localPos = ConvertToCanvasSpace(block.position);

				if (!CheckBounds(localPos))
				{
					if (transform.localPosition.x < 0)
					{
						transform.localPosition += new Vector3(moveOffset, 0, 0);
					}
					else
					{
						transform.localPosition += new Vector3(-moveOffset, 0, 0);
					}
				}
			}
		}
	}

	public void Blink(bool condition)
	{
		foreach (var renderer in renderers)
		{
			renderer.UpdateMaterial(condition);
		}
	}

	public bool CheckBounds(Vector2 position)
	{
		if (position.x <= -gridBounds.x / 2)
		{
			return false;
		}

		if (position.x >= gridBounds.x / 2)
		{
			return false;
		}

		return true;
	}

	public IEnumerable<Vector3> GetAllPositions()
	{
		foreach (var block in children)
		{
			yield return ConvertToCanvasSpace(block.position);
		}
	}


	private Vector3 ConvertToCanvasSpace(Vector3 pos)
	{
		return parentGrid.InverseTransformPoint(pos);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Grid2D : MonoBehaviour
{
	[SerializeField]
	private GameObject cellPrefab;

	[SerializeField]
	private Color disabledCellColour;


	private Dictionary<Vector2, RawImage> grid = new Dictionary<Vector2, RawImage>();

	private const int CELL_SIZE = 50;


	public void Render(Vector2 gridSize)
	{
		for (int i = 0; i < gridSize.x; i += CELL_SIZE)
		{
			for (int j = 0; j < gridSize.y; j += CELL_SIZE)
			{
				var cell = Instantiate(cellPrefab, new Vector3(i, j, 0), Quaternion.identity, transform);
				var localTransform = cell.GetComponent<RectTransform>();

				var xOffset = (gridSize.x * -0.5f) + i + (CELL_SIZE / 2);
				var yOffset = (gridSize.y * -0.5f) + j + (CELL_SIZE / 2);

				localTransform.localPosition = new Vector3(xOffset, yOffset, 0);

				var localColour = cell.GetComponent<RawImage>();
				localColour.color = disabledCellColour;
				grid.Add(new Vector2(xOffset, yOffset), localColour);
			}
		}
	}

	public void Add(Tetromino tetromino)
	{
		if (tetromino == null)
			return;

		if (tetromino.Image.color == disabledCellColour)
			return;

		var positions = tetromino.GetAllPositions();

		foreach (var position in positions)
		{
			var roundedPos = Round(position);

			if (grid.ContainsKey(roundedPos))
			{
				grid[roundedPos].color = tetromino.Image.color;
			}
		}
	}

	public void Reset()
	{
		foreach (var cell in grid)
		{
			cell.Value.color = disabledCellColour;
		}
	}

	public bool IsSquareEmpty(Vector2 position)
	{
		var roundedPos = Round(position);

		if (grid.ContainsKey(roundedPos))
		{
			if (grid[roundedPos].color == disabledCellColour)
			{
				return true;
			}

			return false;
		}
		return false;
	}

	public float GetHighestPoint()
	{
		var highestValue = 0.0f;

		foreach (var kvp in grid)
		{
			if (kvp.Value.color == disabledCellColour)
				continue;

			if (kvp.Key.y > highestValue)
			{
				highestValue = kvp.Key.y;
			}
		}

		return highestValue;
	}

	public int TryRemoveLines(Tetromino tetromino)
	{
		var positions = tetromino.GetAllPositions();
		var linesRemoved = 0;

		var rowsToCheck = positions
			.Select(p => Round(p).y)
			.Distinct()
			.OrderByDescending(y => y);

		var maxY = float.MinValue;

		foreach (var y in rowsToCheck)
		{
			var rowCells = grid.Where(cell => cell.Key.y == y).ToList();

			if (rowCells.Count == 0)
				continue;

			var isFull = rowCells.All(cell => cell.Value.color != disabledCellColour);

			if (!isFull)
				continue;

			foreach (var cell in rowCells)
			{
				cell.Value.color = disabledCellColour;
			}

			maxY = Math.Max(maxY, y);
			linesRemoved++;
		}

		TryMoveLinesDown(maxY, CELL_SIZE * linesRemoved);

		return linesRemoved;
	}

	private void TryMoveLinesDown(float yPos, int downOffset)
	{
		if (downOffset <= 0)
			return;

		var squaresToMove = grid.Where(p => p.Key.y > yPos);

		foreach (var cell in squaresToMove)
		{
			var newPos = new Vector2(cell.Key.x, cell.Key.y - downOffset);
			grid[newPos].color = cell.Value.color;
			cell.Value.color = disabledCellColour;
		}
	}

	private Vector2 Round(Vector2 position)
	{
		return new Vector2(
			Mathf.RoundToInt(position.x),
			Mathf.RoundToInt(position.y));
	}
}

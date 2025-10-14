using System.Collections.Generic;
using UnityEngine;


public class TetrominoPreview : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> previewTetrominos = new List<GameObject>();


	public void Show(Tetromino tetromino)
	{
		foreach (var obj in previewTetrominos)
		{
			var id = obj.GetComponent<Tetromino>().ID;
			if (id == tetromino.ID)
			{
				obj.SetActive(true);
			}
			else
			{
				obj.SetActive(false);
			}
		}
	}

	public void Reset()
	{
		foreach (var obj in previewTetrominos)
		{
			obj.SetActive(false);
		}
	}
}

using System.Collections.Generic;
using UnityEngine;


public class TetrominoPool : MonoBehaviour
{
	[SerializeField]
	private Transform spawnPoint, parent;


	[SerializeField]
	private List<GameObject> blocks;

	private readonly List<GameObject> pool = new List<GameObject>();

	private GameObject currentItem, nextItem;

	public void Initialise(Vector2 gridSize, float offset)
	{
		foreach (var obj in blocks)
		{
			var block = Instantiate(obj, parent);
			block.transform.localScale = Vector3.one;
			block.GetComponent<Tetromino>().Initialise(parent, gridSize, offset);
			block.SetActive(false);
			Enqueue(block);
		}
	}

	public GameObject Get()
	{
		var obj = Dequeue();
		obj.transform.position = spawnPoint.position;
		obj.transform.localRotation = Quaternion.identity;
		obj.SetActive(true);
		return obj;
	}

	public GameObject GetSpecific(GameObject tetromino)
	{
		var id = tetromino.GetComponent<Tetromino>().ID;
		foreach (var obj in pool)
		{
			if (obj.GetComponent<Tetromino>().ID == id)
			{
				obj.transform.position = spawnPoint.position;
				obj.transform.localRotation = Quaternion.identity;
				obj.SetActive(true);
				return obj;
			}
		}

		throw new System.Exception("Couldn't find object with id: " + id);
	}

	public void Return(GameObject tetromino)
	{
		tetromino.SetActive(false);
		Enqueue(tetromino);
	}

	public GameObject Peek()
	{
		if (pool.Count > 0)
		{
			return nextItem;
		}

		throw new System.Exception("Object Pool is empty");
	}

	private GameObject Dequeue()
	{
		if (currentItem == null)
		{
			int selectedIndex = Random.Range(0, pool.Count);
			currentItem = pool[selectedIndex];

			int nextIndex;
			do
			{
				nextIndex = Random.Range(0, pool.Count);
			}
			while (nextIndex == selectedIndex);

			nextItem = pool[nextIndex];
		}
		else
		{
			currentItem = nextItem;

			int nextIndex;
			do
			{
				nextIndex = Random.Range(0, pool.Count);
			}
			while (pool[nextIndex] == currentItem);

			nextItem = pool[nextIndex];
		}

		pool.RemoveAt(pool.IndexOf(currentItem));
		return currentItem;
	}

	private void Enqueue(GameObject tetromino)
	{
		if (!pool.Contains(tetromino) && tetromino != null)
		{
			pool.Add(tetromino);
		}
	}
}

using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
	[SerializeField]
	private Vector2 gridSize;

	[SerializeField]
	private TetrominoPool pool;

	[SerializeField]
	private TetrominoPreview nextPreview, holdPreview;

	[SerializeField]
	private Grid2D grid;

	[SerializeField]
	private ScoreManager scoreManager;

	[SerializeField]
	private InputManager inputManager;

	[SerializeField]
	private Timer gameStartTimer, blockDeleteTimer;

	[SerializeField]
	private GameObject startgameOverlay, endgameOverlay;

	[SerializeField]
	private RectTransform endgameLine;

	[SerializeField]
	private float baseSpeed, speedIncrease;

	[SerializeField]
	private int gameStartTime, blockDeleteTime;

	private readonly float[] speedValues = new float[MAX_LEVEL + 1];

	private const float MOVE_OFFSET = 50.0f;
	private const float ROTATE_OFFSET = 90.0f;
	private const float HOLD_THRESHOLD = 0.1f;
	private const float REPEAT_INTERVAL = 0.1f;

	private const int MAX_LEVEL = 30;


	private Tetromino currentTetromino, heldTetromino;

	private float time = 0.0f;

	private bool canHold = true;

	private int speedIndex;


	void Start()
	{
		grid.Render(gridSize);
		pool.Initialise(gridSize, MOVE_OFFSET, MOVE_OFFSET);
		inputManager.RegisterActionHold(KeyCode.LeftArrow,  HOLD_THRESHOLD, REPEAT_INTERVAL, MoveLeft, MoveLeft);
		inputManager.RegisterActionHold(KeyCode.RightArrow, HOLD_THRESHOLD, REPEAT_INTERVAL, MoveRight, MoveRight);
		inputManager.RegisterActionHold(KeyCode.DownArrow,  HOLD_THRESHOLD, REPEAT_INTERVAL, MoveDown, MoveDown);
		inputManager.RegisterActionTap(KeyCode.UpArrow, Rotate);
		inputManager.RegisterActionTap(KeyCode.Space, Hold);

		gameStartTimer.OnFinished += StartGame;
		blockDeleteTimer.OnFinished += AddToGrid;

		InitialiseSpeedValues();
		Reset();
	}

	void Update()
	{
		if (startgameOverlay.activeSelf || endgameOverlay.activeSelf)
			return;

		if (gameStartTimer.IsFinished)
		{
			inputManager.HandleInput();

			time += Time.deltaTime;

			if (time >= speedValues[speedIndex])
			{
				MoveDown();
			}
		}
	}

	public void StartCountdown()
	{
		if (gameStartTimer.IsFinished)
		{
			Reset();
			gameStartTimer.StartCounter(gameStartTime);
		}
	}

	public void Reset()
	{
		endgameOverlay.SetActive(false);
		startgameOverlay.SetActive(true);
		scoreManager.Reset();
		holdPreview.Reset();
		nextPreview.Reset();
		grid.Reset();
		gameStartTimer.Reset();

		if (currentTetromino != null)
		{
			pool.Return(currentTetromino.gameObject);
			currentTetromino = null;
		}

		if (heldTetromino != null)
		{
			heldTetromino = null;
		}
	}

	public void MoveDown()
	{
		if (CanMoveVertical(currentTetromino, -MOVE_OFFSET))
		{
			time = 0.0f;
			currentTetromino.MoveVertical(Vector3.down);
			currentTetromino.Blink(false);

			if (!blockDeleteTimer.IsFinished)
				blockDeleteTimer.StopCounter();

		}
		else
		{
			if (blockDeleteTimer.IsFinished)
			{
				currentTetromino.Blink(true);
				blockDeleteTimer.StartCounter(blockDeleteTime);
			}
		}
	}

	public void MoveRight()
	{
		if (CanMoveHorizontal(currentTetromino, MOVE_OFFSET))
		{
			currentTetromino.MoveHorizontal(Vector3.right);
		}
	}

	public void MoveLeft()
	{
		if (CanMoveHorizontal(currentTetromino, -MOVE_OFFSET))
		{
			currentTetromino.MoveHorizontal(Vector3.left);
		}
	}

	public void Rotate()
	{
		var allPositions = currentTetromino.GetAllPositions();

		currentTetromino.Rotate(-ROTATE_OFFSET, MOVE_OFFSET);
		foreach (var pos in allPositions)
		{
			if (!grid.IsSquareEmpty(pos))
			{
				currentTetromino.MoveVertical(Vector3.up);
			}
		}
	}

	public void Hold()
	{
		if (!canHold)
			return;

		// when game starts, there is no block being held
		if (heldTetromino == null)
		{
			heldTetromino = currentTetromino;
			pool.Return(currentTetromino.gameObject);
			SpawnBlock();
			holdPreview.Show(heldTetromino);
		}
		else
		{
			// Swap tetrominos
			(heldTetromino, currentTetromino) = (currentTetromino, heldTetromino);
			pool.Return(heldTetromino.gameObject);
			pool.GetSpecific(currentTetromino.gameObject);
			holdPreview.Show(heldTetromino);
		}

		canHold = false;
	}


	private void StartGame()
	{
		grid.Reset();
		startgameOverlay.SetActive(false);
		SpawnBlock();
	}

	private void EndGame()
	{
		grid.Reset();
		endgameOverlay.SetActive(true);
	}

	private void AddToGrid()
	{
		grid.Add(currentTetromino);
		var linesRemoved = grid.TryRemoveLines(currentTetromino);
		scoreManager.Add(linesRemoved);
		pool.Return(currentTetromino.gameObject);

		if (grid.GetHighestPoint() >= endgameLine.anchoredPosition.y)
		{
			EndGame();
			return;
		}

		SpawnBlock();
	}

	private bool CanMoveHorizontal(Tetromino tetromino, float offset)
	{
		var allPositions = tetromino.GetAllPositions();
		var canMove = true;

		if (CanMoveVertical(currentTetromino, -MOVE_OFFSET))
		{
			blockDeleteTimer.StopCounter();
		}

		foreach (var pos in allPositions)
		{
			var localPos = pos + new Vector3(offset, 0, 0);

			if (!grid.IsSquareEmpty(localPos))
			{
				canMove = false;
				break;
			}
		}

		return canMove;
	}

	private bool CanMoveVertical(Tetromino tetromino, float offset)
	{
		var allPositions = tetromino.GetAllPositions();
		var canMove = true;

		foreach (var pos in allPositions)
		{
			var localPos = pos + new Vector3(0, offset, 0);

			if (localPos.y <= gridSize.y * -0.5f || !grid.IsSquareEmpty(localPos))
			{
				canMove = false;
				break;
			}
		}

		return canMove;
	}

	private void SpawnBlock()
	{
		var block = pool.Get();
		currentTetromino = block.GetComponent<Tetromino>();
		currentTetromino.Blink(false);
		nextPreview.Show(pool.Peek().GetComponent<Tetromino>());
		speedIndex = (scoreManager.Level <= MAX_LEVEL) ? scoreManager.Level : MAX_LEVEL;
		canHold = true;
		time = 0.0f;
	}

	private void InitialiseSpeedValues()
	{
		speedValues[0] = baseSpeed;
		for (int i = 1; i <= MAX_LEVEL; i++)
		{
			var speedVal = speedValues[i - 1] * speedIncrease;

			if (speedVal <= 0.01f)
				speedValues[i] = 0.01f;
			else
				speedValues[i] = speedVal;
		}
	}
}

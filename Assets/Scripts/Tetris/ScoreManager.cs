using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public int Level => level;

	public float Score => score;

	[SerializeField]
	private TextMeshProUGUI scoreText, levelText, linesText;

	[SerializeField]
	private int line1, line2, line3, line4, linesPerLevel, startLevel;

	private float score = 0.0f;

	private int level = 1;
	private int totalLinesCleared = 0;

	void Start()
	{
		UpdateText();
		CalculateLevel(totalLinesCleared);
	}

	public void Add(int linesCleared)
	{
		switch (linesCleared)
		{
			case 0:
				break;
			case 1:
				score += CalculateScore(line1, level);
				break;
			case 2:
				score += CalculateScore(line2, level);
				break;
			case 3:
				score += CalculateScore(line3, level);
				break;
			default:
				score += CalculateScore(line4, level);
				break;
		}

		totalLinesCleared += linesCleared;
		level = CalculateLevel(totalLinesCleared);

		UpdateText();
	}

	public void Reset()
	{
		score = 0.0f;
		level = 1;
		totalLinesCleared = 0;
		UpdateText();
	}

	private int CalculateScore(int baseScore, int currentLevel)
	{
		return baseScore * (currentLevel + 1);
	}

	private int CalculateLevel(int linesCleared)
	{
		return startLevel + (linesCleared / linesPerLevel);
	}

	private void UpdateText()
	{
		scoreText.text = score.ToString();
		levelText.text = level.ToString();
		linesText.text = totalLinesCleared.ToString();
	}
}

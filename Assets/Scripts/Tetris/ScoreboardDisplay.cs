using TMPro;
using UnityEngine;

public class ScoreboardDisplay : MonoBehaviour
{
	[SerializeField]
	private ScoreManager scoreManager;

	[SerializeField]
	private TextMeshProUGUI scoreText;


	private void OnEnable()
	{
		scoreText.text = scoreManager.Score.ToString();
	}
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class SceneManager : MonoBehaviour
{

	[SerializeField]
	private Slider slider;

	// Update is called once per frame
	void Update()
	{
		if (slider.value == slider.maxValue)
		{
			StartCoroutine(SwitchScene());
		}
	}

	private IEnumerator SwitchScene()
	{
		yield return new WaitForSeconds(1);
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
	}
}

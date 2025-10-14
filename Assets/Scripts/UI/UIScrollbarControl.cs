using UnityEngine;
using UnityEngine.UI;


public class UIScrollbarControl : MonoBehaviour
{
	[SerializeField]
	private Scrollbar scrollbar;

	private float stepSize;

	void Start()
	{
		stepSize = 1f / (scrollbar.numberOfSteps - 1);
	}

	void OnEnable()
	{
		scrollbar.value = 0.0f;
	}

	public void Increment()
	{
		scrollbar.value = Mathf.Clamp01(scrollbar.value + stepSize);
	}

	public void Decrement()
	{
		scrollbar.value = Mathf.Clamp01(scrollbar.value - stepSize);
	}
}

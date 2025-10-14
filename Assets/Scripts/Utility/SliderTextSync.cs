using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderTextSync : MonoBehaviour
{

	[SerializeField]
	private TextMeshProUGUI text;

	private Slider slider;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		slider = GetComponent<Slider>();
		slider.onValueChanged.AddListener(OnValueChange);
	}

	// Update is called once per frame
	private void OnValueChange(float value)
	{
		text.text = slider.value.ToString() + "%";
	}
}

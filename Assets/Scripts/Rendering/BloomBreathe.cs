using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class BloomBreathe : MonoBehaviour
{
	[SerializeField]
	private float frequency, amplitude, minLimit, maxLimit;

	private Volume postProcessingVolume;

	private Bloom bloom;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		postProcessingVolume = GetComponent<Volume>();
		postProcessingVolume.profile.TryGet(out bloom);
	}

	// // Update is called once per frame
	// void Update()
	// {
	// 	float pulse = Mathf.Sin(Time.time * frequency) * amplitude;
	// 	float intensity = bloom.intensity.value + pulse;


	// }
}

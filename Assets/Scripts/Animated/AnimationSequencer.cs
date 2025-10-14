using System.Collections.Generic;
using UnityEngine;


public class AnimationSequencer : MonoBehaviour
{
	[SerializeField]
	private GameObject chainObject;

	private new Animation animation;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		animation = gameObject.GetComponent<Animation>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!animation.isPlaying)
		{
			chainObject.gameObject.SetActive(true);

		}
	}
}

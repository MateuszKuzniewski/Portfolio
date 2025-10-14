using System;
using UnityEngine;

public enum InputMode { Tap, TapAndHold }

public class KeyInputAction
{
	public KeyCode key;

	public InputMode mode;

	public float holdThreshold;
	public float repeatInterval;
	public float nextRepeatTime;
	public float pressTime;

	public bool isHolding;
	
	public Action onTap;
	public Action onHold;
}

using System;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
	private readonly List<KeyInputAction> actions = new List<KeyInputAction>();

	public void RegisterActionTap(KeyCode key, Action onTap)
	{
		actions.Add(new KeyInputAction
		{
			key = key,
			mode = InputMode.Tap,
			onTap = onTap
		});
	}

	public void RegisterActionHold(KeyCode key, float holdThreshold, float repeatInterval, Action onTap, Action onHold)
	{
		actions.Add(new KeyInputAction
		{
			key = key,
			mode = InputMode.TapAndHold,
			holdThreshold = holdThreshold,
			repeatInterval = repeatInterval,
			onTap = onTap,
			onHold = onHold
		});
	}

	public void HandleInput()
	{
		foreach (var action in actions)
		{
			if (Input.GetKeyDown(action.key))
			{
				action.pressTime = Time.time;
				action.isHolding = false;
				action.nextRepeatTime = 0f;
				action.onTap?.Invoke();
			}

			if (action.mode == InputMode.TapAndHold && Input.GetKey(action.key))
			{
				if (!action.isHolding && Time.time - action.pressTime >= action.holdThreshold)
				{
					action.isHolding = true;
					action.onHold?.Invoke();
					action.nextRepeatTime = Time.time + action.repeatInterval;
				}
				else if (action.isHolding && Time.time >= action.nextRepeatTime)
				{
					action.onHold?.Invoke();
					action.nextRepeatTime = Time.time + action.repeatInterval;
				}
			}
		}
	}
}

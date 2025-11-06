using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;


public class VideoController : MonoBehaviour
{
	public string VideoURL
	{
		set
		{
			videoPlayer.url = value;
		}
	}


	[SerializeField]
	private VideoPlayer videoPlayer;

	[SerializeField]
	private GameObject videoOverlay;


	void Start()
	{
		videoPlayer.playOnAwake = false;
		videoPlayer.sendFrameReadyEvents = true;
	}

	public void Prepare()
	{
		videoPlayer.Prepare();
		videoPlayer.prepareCompleted += (source) =>
		{
			videoPlayer.Play();
			videoPlayer.frameReady += OnFrameReady;
		};
	}

	private void OnFrameReady(VideoPlayer source, long frameID)
	{
		if (frameID == 10)
		{
			source.Pause();
		}
	}

	public void Play()
	{
		if (videoPlayer.isPlaying)
		{
			videoPlayer.Pause();
			videoOverlay.SetActive(true);
		}
		else
		{
			if (!videoPlayer.isPrepared)
			{
				videoPlayer.Prepare();
			}

			videoPlayer.Play();
			videoOverlay.SetActive(false);
		}
	}

	public void Stop()
	{
		videoPlayer.Stop();
		videoPlayer.targetTexture.Release();
		videoPlayer.targetTexture.DiscardContents();
		videoOverlay.SetActive(true);
	}

	public void Pause()
	{
		if (videoPlayer.gameObject.activeSelf)
		{
			Prepare();
		}

		videoOverlay.SetActive(true);
	}
}

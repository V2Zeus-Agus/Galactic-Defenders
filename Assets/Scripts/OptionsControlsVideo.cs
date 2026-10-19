using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class OptionsControlsVideo : MonoBehaviour
{
    [SerializeField] private GameObject optionsBox;
    [SerializeField] private GameObject controlsBox;

    private VideoPlayer controlsVideo;

    public void OpenControls()
    {
        if (optionsBox) optionsBox.SetActive(false);
        if (controlsBox) controlsBox.SetActive(true);

        controlsVideo = controlsBox ? controlsBox.GetComponentInChildren<VideoPlayer>(true) : null;

        StopAllCoroutines();
        StartCoroutine(PlayNextFrame());
    }

    public void CloseControls()
    {
        StopVideo();
        if (controlsBox) controlsBox.SetActive(false);
        if (optionsBox) optionsBox.SetActive(true);
    }

    private IEnumerator PlayNextFrame()
    {
        yield return null;

        if (!controlsVideo) yield break;

        controlsVideo.enabled = true;
        if (!controlsVideo.gameObject.activeInHierarchy) yield break;

        controlsVideo.audioOutputMode = VideoAudioOutputMode.None;
        controlsVideo.controlledAudioTrackCount = 0;

        controlsVideo.Stop();
        controlsVideo.frame = 0;
        controlsVideo.Play();
    }

    private void StopVideo()
    {
        if (!controlsVideo) return;
        controlsVideo.Stop();
    }
}

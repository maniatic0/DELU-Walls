using UnityEngine;
using System.Collections.Generic;

public class AudioTrackerHandler : MonoBehaviour
{

    /// <summary>
    /// Which Tracker we care for
    /// </summary>
    [Header("Tracking System")]
    [SerializeField]
    [Tooltip("Which Tracker we care for")]
    public Tracker tracker = null;

    /// <summary>
    /// AudioSources we control
    /// </summary>
    [SerializeField]
    [Tooltip("AudioSources we control")]
    private AudioSource[] trackedAudio = null;


    /// <summary>
    /// If we are muted
    /// </summary>
    private bool muted = false;

    /// <summary>
    /// If we are tracked
    /// </summary>
    private bool tracked = false;

    /// <summary>
    /// Analytics Event Name for Muting
    /// </summary>
    private static string kAnalyticsMute = "mute";

    /// <summary>
    /// Analytics Event Name for Unmuting
    /// </summary>
    private static string kAnalyticsUnmute = "unmute";

    /// <summary>
    /// The general muted status
    /// </summary>
    private static bool mutedGeneral = false;

    /// <summary>
    /// The general muted status
    /// </summary>
    public static bool MutedGeneral { get { return mutedGeneral; } }

    /// <summary>
    /// List of all the audio trackers handlers
    /// </summary>
    private static List<AudioTrackerHandler> audios = new List<AudioTrackerHandler>();

    /// <summary>
    /// Set mute status
    /// </summary>
    /// <param name="status">status to set</param>
    public static void SetMute(bool status)
    {
        if (status)
        {
            //AnalyticsManager.ReportGameEvent(kAnalyticsMute);
        }
        else
        {
            //AnalyticsManager.ReportGameEvent(kAnalyticsUnmute);
        }
        mutedGeneral = status;
        foreach (AudioTrackerHandler item in audios)
        {
            item.SetMuteStatus(mutedGeneral);
        }
    }

    private void Awake()
    {
        audios.Add(this);
        DisableSound();
    }

    // Use this for initialization
    void Start()
    {
        tracker.OnTrackingFound += OnTracked;
        tracker.OnTrackingLost += OnLost;
    }

    void OnDestroy()
    {
        tracker.OnTrackingFound -= OnTracked;
        tracker.OnTrackingLost -= OnLost;
        audios.Remove(this);
    }

    /// <summary>
    /// Enable sounds
    /// If muted it will not do anything
    /// </summary>
    public void EnableSound()
    {
        if (muted)
        {
            return;
        }
        for (int i = 0; i < trackedAudio.Length; i++)
        {
            trackedAudio[i].mute = false;
        }
    }

    /// <summary>
    /// Enable sounds
    /// </summary>
    public void DisableSound()
    {
        for (int i = 0; i < trackedAudio.Length; i++)
        {
            trackedAudio[i].mute = true;
        }
    }

    /// <summary>
    /// Set mute status
    /// </summary>
    /// <param name="status">status of mute</param>
    public void SetMuteStatus(bool status)
    {
        muted = status;
        if (!status)
        {
            if (tracked)
            {
                EnableSound();
            }
        }
        else
        {
            DisableSound();
        }
    }


    protected virtual void OnTracked()
    {
        tracked = true;
        EnableSound();
    }

    protected virtual void OnLost()
    {
        tracked = false;
        DisableSound();
    }


}

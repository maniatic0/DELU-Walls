using UnityEngine;
using System.Collections;

public class AnalyticsTrackerHandler : MonoBehaviour
{

    /// <summary>
    /// Which Tracker we care for
    /// </summary>
    [Header("Tracking System")]
    [SerializeField]
    [Tooltip("Which Tracker we care for")]
    private Tracker tracker = null;

    /// <summary>
    /// The name of the model we care for
    /// </summary>
    [SerializeField]
    [Tooltip("The name of the model we care for")]
    private string model_name = null;

    /// <summary>
    /// The type of the model we care for
    /// i. e. Cylinder, Image, ...
    /// </summary>
    [SerializeField]
    [Tooltip("The name of the model we care for.\n" +
        " i. e. Cylinder, Image, ...")]
    private string type;

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
    }

    protected virtual void OnTracked()
    {
        //AnalyticsManager.ReportImageTrackedStatus(model_name, type, true);
    }

    protected virtual void OnLost()
    {
        //AnalyticsManager.ReportImageTrackedStatus(model_name, type, false);
    }
}

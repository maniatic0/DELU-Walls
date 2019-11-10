using UnityEngine;
using System.Collections;

public class TrackerHandleContainer : MonoBehaviour
{
    /// <summary>
    /// Which Tracker we care for
    /// </summary>
    [Header("Tracking System")]
    [SerializeField]
    [Tooltip("Which Tracker we care for")]
    private Tracker tracker = null;

    /// <summary>
    /// container we care for
    /// </summary>
    [SerializeField]
    [Tooltip("Container we care for")]
    private GameObject container = null;

    /// <summary>
    /// Time in seconds to disable container if it is not tracked
    /// </summary>
    [SerializeField]
    [Tooltip("Time in seconds to disable container if it is not tracked")]
    private float timeToTurnOff = 10;

    /// <summary>
    /// Time in seconds to disable container if it is not tracked at the start of the scene
    /// </summary>
    [SerializeField]
    [Tooltip("Time in seconds to disable container if it is not tracked at the start of the scene")]
    /// <summary>
    /// Time to turn off at start
    /// </summary>
    private float kTimeToTurnOffStart = 2;

    /// <summary>
    /// Coroutine to disable container
    /// </summary>
    private IEnumerator coroutine = null;

    /// <summary>
    /// Current Active Status
    /// </summary>
    private bool currentStatus = false;

    private void Awake()
    {
        tracker.OnTrackingFound += OnTracked;
        tracker.OnTrackingLost += OnLost;
    }

    // Use this for initialization
    void Start()
    {
        coroutine = WaitDisableContainer(kTimeToTurnOffStart);
        StartCoroutine(coroutine);
    }

    private void OnDestroy()
    {
        tracker.OnTrackingFound -= OnTracked;
        tracker.OnTrackingLost -= OnLost;
    }

    /// <summary>
    /// Set contatiner status
    /// </summary>
    /// <param name="status">if the container is active or not</param>
    void SetContainerStatus(bool status)
    {
        currentStatus = status;
        container.SetActive(status);
    }

    /// <summary>
    /// Wait time to disable container
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitDisableContainer(float time)
    {
        yield return new WaitForSeconds(time);
        SetContainerStatus(false);
#if UNITY_EDITOR
        Debug.Log("Turning Off: " + gameObject.name, this);
#endif
    }

    protected virtual void OnTracked()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        if (!currentStatus)
        {
            SetContainerStatus(true);
#if UNITY_EDITOR
            Debug.Log("Turning On: " + gameObject.name, this);
#endif
        }
    }

    protected virtual void OnLost()
    {
        if (coroutine != null)
        {
            return;
        }
        coroutine = WaitDisableContainer(timeToTurnOff);
        StartCoroutine(coroutine);
    }
}

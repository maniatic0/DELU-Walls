using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BrickFall : MonoBehaviour
{
    public AudioClip brickFall = null;
    public AudioSource audioSource = null;

    private void OnCollisionEnter(Collision other) {
        audioSource.PlayOneShot(brickFall, 1.0f);
    }
}

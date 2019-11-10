using System.Collections.Generic;
using UnityEngine;
namespace Walls.Assets.Scripts {
    public class PushBrick : MonoBehaviour {

        public AudioClip hammerClip = null;
        public AudioClip clapsClip = null;
        public AudioSource audioSource = null;

        public float impulseForce = 10f;

        public float reloadTime = 0.2f;
        private float startReload = 0f;
        private RaycastHit hit;
        private Ray ray;
        private bool clicked = false;

        private void Update () {
#if UNITY_EDITOR
            clicked = Input.GetMouseButtonDown (0);
#elif UNITY_ANDROID || UNITY_IOS
            clicked = Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began;
#endif

            if (clicked && Time.time - startReload > reloadTime) {
#if UNITY_EDITOR
                ray = Camera.main.ScreenPointToRay (Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IOS
                ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
#endif

                if (Physics.Raycast (ray, out hit)) {
                    if (hit.rigidbody) {
                        hit.rigidbody.AddForceAtPosition (impulseForce * ray.direction, hit.point, ForceMode.Impulse);
                        audioSource.PlayOneShot (hammerClip, 1.0f);

                        if (Random.Range (0, 3) == 1) {
                            AudioSource.PlayClipAtPoint (clapsClip, hit.point, 1.0f);
                        }

                        startReload = Time.time;
                    }
                }
            }
        }
    }
}
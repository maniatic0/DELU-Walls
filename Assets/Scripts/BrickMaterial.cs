using UnityEngine;
namespace Walls.Assets.Scripts
{
    [RequireComponent(typeof(Renderer))]
    public class BrickMaterial : MonoBehaviour
    {
        public float specialMatProb = 0.3f;
        public Material normalMat = null;
        public Material[] specialMat = new Material[0];

        private Renderer rend = null;

        private void Awake() {
            rend = GetComponent<Renderer>();
            SelectMaterial();
        }

        public void SelectMaterial() {
            if (Random.Range(0.0f, 1.0f) < specialMatProb) {
                rend.material = specialMat[Random.Range(0, specialMat.Length)];
            } else {
                rend.material = normalMat;
            }
        }

    }
}
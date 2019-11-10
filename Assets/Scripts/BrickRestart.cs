using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickRestart : MonoBehaviour
{

    private Vector3 originalPos = Vector3.zero;
    private Quaternion orginalRot;
    private Rigidbody body;

    private Walls.Assets.Scripts.BrickMaterial brickMaterial = null;

    private static HashSet<BrickRestart> allBricks = new HashSet<BrickRestart>();

    private void Awake() {
        originalPos = transform.position;
        orginalRot = transform.rotation;
        body = GetComponent<Rigidbody>();
        brickMaterial = GetComponent<Walls.Assets.Scripts.BrickMaterial>();
        allBricks.Add(this);
    }

    public void ResetPos() {
        if (body) {
            body.isKinematic = true;
        }
        transform.position = originalPos;
        transform.rotation = orginalRot;
        brickMaterial.SelectMaterial();
        if (body) {
            body.isKinematic = false;
        }
    }

    public static void ResetAllBricks() {
        foreach (BrickRestart brick in allBricks)
        {
            brick.ResetPos();
        }
    }

    public void ResetAllBrickFromOneBrick() {
        ResetAllBricks();
    }
 
    private void OnDestroy() {
        allBricks.Remove(this);
    }
}

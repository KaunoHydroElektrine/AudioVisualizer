using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotation = new Vector3(0.05f, 2f, 0.6f);
    public float baseSpeed = 80f;
    public float loudnessMultiplier = 200f;

    private void Update()
    {
        float speed = BeatDetector.Loudness * baseSpeed * Time.deltaTime * loudnessMultiplier;

        transform.Rotate(rotation * speed);
    }
}


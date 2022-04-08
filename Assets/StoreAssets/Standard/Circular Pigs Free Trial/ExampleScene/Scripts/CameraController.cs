using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject objectToFollow;
    public float moveSpeed;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position - offset, objectToFollow.transform.position, Time.deltaTime * moveSpeed);
        transform.position += offset;
    }
}

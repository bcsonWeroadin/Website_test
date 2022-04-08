using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChecker : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;

    private bool floorExists = false;
    public bool FloorExists { get { return floorExists; } }

    private void OnTriggerStay(Collider other)
    {
        floorExists = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        floorExists = true;
    }

    private void OnTriggerExit(Collider other)
    {
        floorExists = false;
    }
}
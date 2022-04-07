using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestApple : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            rb.AddForce(Vector3.forward);
            Debug.Log("Å°´­¸²");
        }
    }
}

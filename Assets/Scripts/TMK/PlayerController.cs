using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private Camera m_camera;
    [SerializeField] private CharacterController m_characterController;

    [SerializeField] private float m_speed = 5f;
    [SerializeField] private float m_runSpeed = 8f;
    [SerializeField] private float m_finalSpeed = 10f;
    [SerializeField] private float smoothness = 10;

    private bool toggleCameraRotation;
    private bool run;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCameraRotation = true;
        }
        else
        {
            toggleCameraRotation = false;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        InputMovement();
    }

    void LateUpdate()
    {
        if(toggleCameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(m_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
        
    }

    private void InputMovement()
    {
        m_finalSpeed = (run) ? m_runSpeed : m_speed;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection = forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal");

        m_characterController.Move(moveDirection.normalized * m_finalSpeed * Time.deltaTime);

        float percent = ((run) ? 1 : 0.5f) * moveDirection.magnitude;
        m_animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);
    }
}

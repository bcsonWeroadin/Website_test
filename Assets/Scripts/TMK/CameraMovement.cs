using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform m_Player;
    [SerializeField] private float m_followSpeed = 10f;
    [SerializeField] private float m_sensitivity = 100f;
    [SerializeField] private float m_clampAngle = 70f;

    private float m_rotX;
    private float m_rotY;

    [SerializeField] private Transform m_camera;
    [SerializeField] private Vector3 m_dirNormalized;
    [SerializeField] private Vector3 m_finalDir;
    [SerializeField] private float m_minDistance;
    [SerializeField] private float m_maxDistance;
    [SerializeField] private float m_finalDistance;
    [SerializeField] private float m_smoothness = 10f;

    void Start()
    {
        m_rotX = transform.localRotation.eulerAngles.x;
        m_rotY = transform.localRotation.eulerAngles.y;

        m_dirNormalized = m_camera.localPosition.normalized;
        m_finalDistance = m_camera.localPosition.magnitude;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        m_rotX += -Input.GetAxis("Mouse Y") * m_sensitivity * Time.deltaTime;
        m_rotY += Input.GetAxis("Mouse X") * m_sensitivity * Time.deltaTime;

        m_rotX = Mathf.Clamp(m_rotX, -m_clampAngle, m_clampAngle);

        Quaternion rot = Quaternion.Euler(m_rotX, m_rotY, 0f);
        transform.rotation = rot;
    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_Player.position, m_followSpeed * Time.deltaTime);

        m_finalDir = transform.TransformPoint(m_dirNormalized * m_maxDistance);

        RaycastHit hit;

        if(Physics.Linecast(transform.position, m_finalDir, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                return;
            }
            m_finalDistance = Mathf.Clamp(hit.distance , m_minDistance, m_maxDistance);
        }
        else
        {
            m_finalDistance = m_maxDistance;
        }

        m_camera.localPosition = Vector3.Lerp(m_camera.localPosition, m_dirNormalized * m_finalDistance, Time.deltaTime * m_smoothness);
    }
}

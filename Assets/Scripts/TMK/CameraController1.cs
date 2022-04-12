using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{
    [SerializeField] private GameObject m_player;
    [SerializeField] private Transform m_camera;
    [SerializeField] private float m_offsetX = 0f;
    [SerializeField] private float m_offsetY = 10f;
    [SerializeField] private float m_offsetZ = -10.0f;

    [SerializeField] private float m_cameraSpeed = 10f;

    private Vector3 m_targetPos;

    private void FixedUpdate()
    {
        Vector3 targetPos = m_player.transform.position;

        m_targetPos = new Vector3(targetPos.x + m_offsetX,
                                  targetPos.y + m_offsetY,
                                  targetPos.z + m_offsetZ);

        m_camera.position = Vector3.Lerp(m_camera.position, m_targetPos, Time.deltaTime * m_cameraSpeed);
    }
}

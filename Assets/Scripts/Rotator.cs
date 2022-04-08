using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;

public class Rotator : MonoBehaviour
{
    [SerializeField] private GameObject m_targetPlanet;               //기준행성

    [SerializeField] private bool m_rotateAround = false;
    [SerializeField] private bool m_rotateSelf = false;

    [SerializeField] private float m_aroundSpeed = 1.0f;              //공전 회전 속도
    [SerializeField] private float m_selfSpeed = 1.0f;                //자전 회전 속도


    private void Start()
    {
        if(m_rotateSelf && m_targetPlanet)
        {
            this.UpdateAsObservable()
                .Subscribe(_ => 
                transform.RotateAround(m_targetPlanet.transform.position, 
                            Vector3.down, m_aroundSpeed * Time.deltaTime));
        }

        if (m_rotateAround && m_targetPlanet)
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                    transform.Rotate(Vector3.down * m_selfSpeed * Time.deltaTime));
        }



    }

}


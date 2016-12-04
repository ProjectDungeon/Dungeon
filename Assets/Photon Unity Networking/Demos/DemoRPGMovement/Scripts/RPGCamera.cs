﻿using UnityEngine;
using System.Collections;
using InControl;
public class RPGCamera : MonoBehaviour
{
    public Transform Target;

    public float MaximumDistance;
    public float MinimumDistance;

    public float ScrollModifier;
    public float TurnModifier;

    Transform m_CameraTransform;

    Vector3 m_LookAtPoint;
    Vector3 m_LocalForwardVector;
    float m_Distance;

    void Start()
    {
        m_CameraTransform = transform.GetChild( 0 );
        m_LocalForwardVector = m_CameraTransform.forward;

        m_Distance = -m_CameraTransform.localPosition.z / m_CameraTransform.forward.z;
        m_Distance = Mathf.Clamp( m_Distance, MinimumDistance, MaximumDistance );
        m_LookAtPoint = m_CameraTransform.localPosition + m_LocalForwardVector * m_Distance;
    }

    void LateUpdate()
    {
        UpdateDistance();
        UpdateZoom();
        UpdatePosition();
        UpdateRotation();
    }

    void UpdateDistance()
    {
        m_Distance = Mathf.Clamp( m_Distance - Input.GetAxis( "Mouse ScrollWheel" ) * ScrollModifier, MinimumDistance, MaximumDistance );
    }

    void UpdateZoom()
    {
        m_CameraTransform.localPosition = m_LookAtPoint - m_LocalForwardVector * m_Distance;
    }

    void UpdatePosition()
    {
        if( Target == null )
        {
            return;
        }

        transform.position = Target.transform.position;
    }

    void UpdateRotation()
    {
        var inputDevice = InputManager.ActiveDevice;
        //if( Input.GetMouseButton( 0 ) == true || Input.GetMouseButton( 1 ) == true || Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        
        if (inputDevice.RightStickX)
        {
            transform.Rotate( 0, inputDevice.RightStickX * TurnModifier, 0 );
           // transform.Rotate(Vector3.right, 500.0f * Time.deltaTime * inputDevice.LeftStickY, Space.World);
        }

        if((Input.GetMouseButton( 1 ) || Input.GetButton("Fire2")) && Target != null )
        {
            Target.rotation = Quaternion.Euler( 0, transform.rotation.eulerAngles.y, 0 );
        }
    }
}
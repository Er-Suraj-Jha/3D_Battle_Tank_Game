using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using BattleTank.Tanks;

public class Camera_Controller : MonoSingletonGeneric<Camera_Controller>
{
    public TankService tankService;
    private  Transform target;
    public float m_DampTime = 0.2f;                 
    private Camera m_Camera;                                           
    private Vector3 m_MoveVelocity;                 
    private Vector3 m_DesiredPosition;              


    protected override void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
    }


    private void FixedUpdate()
    {
        Move();
       // Zoom();
    }


    private void Move()
    {
        FindAveragePosition();

        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }


    private void FindAveragePosition()
    {
         Vector3 averagePos = new Vector3();
         target=tankService.tankController.TankView.transform;
         averagePos=target.position;
         averagePos.y = transform.position.y;
         m_DesiredPosition = averagePos;
    }


}

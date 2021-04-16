using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSlide_Controller : MonoBehaviour
{
    public bool Use_Relative_Rotation=true;
    private Quaternion Relative_Rotation;

    void Start()
    {
      Relative_Rotation=transform.parent.localRotation;
    }

    void Update()
    {
        if(Use_Relative_Rotation)
        {
            transform.rotation=Relative_Rotation;
        }
    }
}

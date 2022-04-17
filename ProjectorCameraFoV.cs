using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hiroki;

public class ProjectorCameraFoV : MonoBehaviour
{
    [SerializeField] Transform targetSphere; //接線を求めるスフィア
    [SerializeField] Transform projectorPoint; //始点となる点(カメラ位置)
    SphereLineToPoint sphereLineToPoint = new SphereLineToPoint();
    [SerializeField] Camera cam;
    [SerializeField] Projector projector;
    [SerializeField] GameObject NearMessage;

    void Update()
    {
        cam.fieldOfView = 
            Mathf.Abs(sphereLineToPoint.Contact(projectorPoint.position, targetSphere.position, targetSphere.lossyScale.y/2));
        projector.fieldOfView = cam.fieldOfView;
        if(cam.fieldOfView >= 178 && !NearMessage.activeInHierarchy) NearMessage.SetActive(true);
        else if(cam.fieldOfView < 178 && NearMessage.activeInHierarchy) NearMessage.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

//参考<https://shop-0761.hatenablog.com/entry/2018/01/08/034418>
//トラッカーはひとつあればいいのでDeviceIdをゼロにしている
public class TrackerDeviceSetup : MonoBehaviour
{
    [SerializeField] GameObject[] targetObjs;
    private Transform targetOrigin;
    [SerializeField] ETrackedDeviceClass eTracked = ETrackedDeviceClass.GenericTracker;
    CVRSystem cVRSystem;
    List<int> _validDeviceIds = new List<int>();

    void Start()
    {
        var error = EVRInitError.None;
        cVRSystem = OpenVR.Init(ref error, EVRApplicationType.VRApplication_Other);
        if(error != EVRInitError.None) { Debug.LogWarning("Init error: " + error); }
        else
        {
            Debug.LogWarning("init done");
            foreach (var item in targetObjs) { item.SetActive(false); }
            SetDeviceIds();
        }
        //targetOrigin = targetObjs.transform; //座標空間を合わせるための初期値(動的に座標空間ずらす予定はない)
    }

    void SetDeviceIds()
    {
        _validDeviceIds.Clear();
        for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
        {
            var deviceClass = cVRSystem.GetTrackedDeviceClass(0);
            if(deviceClass != ETrackedDeviceClass.Invalid && deviceClass == eTracked)
            {
                Debug.Log("OpenVR device at : " + deviceClass);
                targetObjs[i].SetActive(true);
            }
        }
    }
    void UpdateTrackedObj()
    {
        TrackedDevicePose_t[] allPoses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];
        cVRSystem.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseStanding, 0, allPoses);
        var absTracking = allPoses[0].mDeviceToAbsoluteTracking;
        var mat = new SteamVR_Utils.RigidTransform(absTracking);
        //var vecMat = mat.TransformPoint(targetOrigin.position);
        targetObjs[0].transform.SetPositionAndRotation(mat.pos, mat.rot);
    }
    void Update()
    {
        UpdateTrackedObj();
        
    }
}

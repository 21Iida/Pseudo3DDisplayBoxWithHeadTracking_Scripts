using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiroki
{
    public class ProjectorLookCenter : MonoBehaviour
    {
        [SerializeField] GameObject WorldCenter;

        void LateUpdate()
        {
            this.transform.LookAt(WorldCenter.transform);
        }
    }
}
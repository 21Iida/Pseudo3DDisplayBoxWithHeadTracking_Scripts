using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiroki
{
    //必ずEvery V Blankにすること
    public class FPSSet : MonoBehaviour
    {
        void Start() {
            Application.targetFrameRate = 60;
        }
    }
}
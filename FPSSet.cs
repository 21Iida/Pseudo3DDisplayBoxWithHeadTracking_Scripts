using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Don't Syncだとfpsが固定できないのに
//Every V Blackにすると60fpsになる
//なぜだ...
public class FPSSet : MonoBehaviour
{
    void Start() {
        Application.targetFrameRate = 60;
    }
}

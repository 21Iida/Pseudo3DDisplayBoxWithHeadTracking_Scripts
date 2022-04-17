using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//未完成
//複数ディスプレイに対応できていない
//レンダーテクスチャ挟めば複数取れるらしい
public class ScreenShot : MonoBehaviour
{
    string filePath;
    string fileName = "ScreenShot.png";
    private void Start() {
        filePath = Application.dataPath;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot(filePath + "/" + fileName);
        }
    }
}

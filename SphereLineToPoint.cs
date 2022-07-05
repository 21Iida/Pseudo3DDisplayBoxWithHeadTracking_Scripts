using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiroki
{
    /// <summary>
    /// カメラのFoV値を設定したい
    /// まず、描画したい空間を覆える球体を設定します
    /// そして球体外にある点(ユーザーの視点)から球体に接線を2本引きます
    /// そのあと接線の角度を返してカメラのFoVに使用します
    /// <summary>
    public class SphereLineToPoint
    {
        //求める接点ふたつ
        //この点とカメラ点とで結んだ線の間の角度をカメラのFOVにあてる
        private Vector3[] ContactPoints = new Vector3[2];
        
        private Vector3 tSpCash = new Vector3(); //targetSphereの位置座標のキャッシュ
        private float period;
        private float maxDegree = 180;

        public float Contact(Vector3 pPp, Vector3 tSp, float r)
        {
            CoordinateTrans(ref pPp, ref tSp);
            
            if((tSp - pPp).sqrMagnitude > Mathf.Pow(r,2))
            {
                //式の参考<https://shogo82148.github.io/homepage/memo/geometry/point-circle.html>
                var xyPow = Mathf.Pow(pPp.x,2) + Mathf.Pow(pPp.y,2);
                var xyrPowSqrt = Mathf.Sqrt(xyPow - Mathf.Pow(r,2));
                ContactPoints[0].x = r * ((pPp.x * r + pPp.y * xyrPowSqrt) / xyPow);
                ContactPoints[0].y = r * ((pPp.y * r - pPp.x * xyrPowSqrt) / xyPow);
                ContactPoints[0].z = 0;
                ContactPoints[1].x = r * ((pPp.x * r - pPp.y * xyrPowSqrt) / xyPow);
                ContactPoints[1].y = r * ((pPp.y * r + pPp.x * xyrPowSqrt) / xyPow);
                ContactPoints[1].z = 0;

                return Vector3.SignedAngle(ContactPoints[0] - pPp, ContactPoints[1] - pPp,Vector3.forward);
            }
            else
            {
                Debug.Log("プレイヤーがスフィアの内側にいます");
                return maxDegree;
            }
        }

        //ワールド座標から扱いやすい座標系に変換させる
        //スフィアの中心を原点として、スフィアと対象の点がxy平面上に並ぶように回転させる
        void CoordinateTrans(ref Vector3 pPp, ref Vector3 tSp)
        {
            tSpCash = tSp; //このあとtSpを弄るため記憶させる
            
            //targetSphreが原点に来るように平行移動
            pPp -= tSp;
            tSp = Vector3.zero;
            
            //xy平面に並ぶように回転
            var normalVec = Vector3.ProjectOnPlane(pPp - tSp, Vector3.up);
            period = Vector3.SignedAngle(Vector3.right, normalVec, Vector3.up);
            var angleAxis = Quaternion.AngleAxis(-period, Vector3.up);
            pPp = angleAxis * pPp;
            
        }
        //CoordinateTransの逆変換。デバッグ確認にどうぞ
        void CoordinateReverse(ref Vector3 pPp, ref Vector3 tSp)
        {
            
            //回転
            var angleAxis = Quaternion.AngleAxis(period, Vector3.up);
            pPp = angleAxis * pPp;
            
            //平行移動
            tSp = tSpCash;
            pPp += tSp;

            //接点の座標系を合わせる
            ContactPoints[0] = angleAxis * ContactPoints[0];
            ContactPoints[1] = angleAxis * ContactPoints[1];
            
            ContactPoints[0] += tSp;
            ContactPoints[1] += tSp;

            Debug.DrawLine(pPp, ContactPoints[0], Color.red);
            Debug.DrawLine(pPp, ContactPoints[1], Color.red);
        }
    }
}

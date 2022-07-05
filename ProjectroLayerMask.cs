using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiroki
{
    //プロジェクターの位置によってディスプレイの表示をオンオフすることで描画負荷を抑えます
    public class ProjectroLayerMask : MonoBehaviour
    {
        Projector projector;
        LayerMask layerMask;
        [SerializeField] Transform worldCenter;

        void Start()
        {
            projector = this.GetComponent<Projector>();
            layerMask = projector.ignoreLayers;
        }

        void Update()
        {
            if(this.transform.position.x > 0)
            {
                layerMask |= 1 << LayerMask.NameToLayer("PlaneRight");
                layerMask &= ~(1 << LayerMask.NameToLayer("PlaneLeft"));
            }
            else
            {
                layerMask |= 1 << LayerMask.NameToLayer("PlaneLeft");
                layerMask &= ~(1 << LayerMask.NameToLayer("PlaneRight"));
            }

            if(this.transform.position.z > 0)
            {
                layerMask |= 1 << LayerMask.NameToLayer("PlaneFront");
                layerMask &= ~(1 << LayerMask.NameToLayer("PlaneBack"));
            }
            else
            {
                layerMask |= 1 << LayerMask.NameToLayer("PlaneBack");
                layerMask &= ~(1 << LayerMask.NameToLayer("PlaneFront"));
            }

            if(this.transform.position.y < worldCenter.position.y)
            {
                layerMask |= 1 << LayerMask.NameToLayer("PlaneTop");
            }
            else
            {
                layerMask &= ~(1 << LayerMask.NameToLayer("PlaneTop"));
            }

            projector.ignoreLayers = layerMask;
        }
    }
}
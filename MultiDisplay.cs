using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDisplay : MonoBehaviour
{
    [SerializeField] int displayCount = 5;
    void Start()
    {
        for(int i = 0; i < displayCount; i++)
        {
            Display.displays[i].Activate();
        }
    }

}

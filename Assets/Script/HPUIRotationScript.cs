using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUIRotationScript : MonoBehaviour
{
    void Update()
    {
        //　カメラと同じ向きに設定
        transform.rotation = Camera.main.transform.rotation;
    }
}
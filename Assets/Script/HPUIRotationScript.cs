using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUIRotationScript : MonoBehaviour
{
    void Update()
    {
        //�@�J�����Ɠ��������ɐݒ�
        transform.rotation = Camera.main.transform.rotation;
    }
}
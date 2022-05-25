using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public BattleLearning battleLearning;
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(new Vector3(transform.position.x, 1f, transform.position.z), transform.forward, Color.blue, 0.1f);
        if (Physics.Raycast(new Vector3(transform.position.x, 1f, transform.position.z), transform.forward, out hit))
        {
            if (hit.collider.tag == "Enemy" && hit.distance < 2f)            
            {
                //battleLearning.SetReward(1f);
                //Debug.Log("hit");
            }
        }
            
    }
}

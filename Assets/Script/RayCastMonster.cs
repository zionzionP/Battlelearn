using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastMonster : MonoBehaviour
{
    public StateController stateController;


    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(new Vector3(transform.position.x, 1f, transform.position.z), transform.forward * 10, Color.blue, 0.1f);
        if (Physics.Raycast(new Vector3(transform.position.x, 1f, transform.position.z), transform.forward, out hit))
        {
            if (hit.collider.tag == "Agent")
            {

                stateController.emeny = hit.transform;
                //battleLearning.SetReward(1f);
                //Debug.Log("hit");
            }
        }


    }
}

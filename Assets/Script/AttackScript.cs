using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public BattleLearning battleLearning;
    public KnightController knightController;
    
    private Animator anim;


    void OnTriggerEnter(Collider other)
    {
        if (knightController.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (other.CompareTag("Enemy"))
            {
                anim = other.GetComponent<Animator>();
                anim.SetTrigger("Die");
                //Invoke("SetUnactive", 2);
                battleLearning.AddReward(2f);
                Debug.Log("AttackReward");                
            }                
        }
        
    }

    /*void SetUnactive()
    {
        GameObject.SetActive(false);
    }*/

        
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public BattleLearning battleLearning;
    public KnightController knightController;
    
    private Animator anim;
    private StateController stateControllerMonster;
    public bool attackable = false;
    private int totalDamage;    
    private CsvWriterScript cw;
    private float time;
    [SerializeField]
    private GameObject csvWriter;


    private void Start()
    {
        csvWriter = GameObject.Find("CsvWriter");
        cw = csvWriter.GetComponent<CsvWriterScript>();
        totalDamage = 0;
    }


    void OnTriggerEnter(Collider other)
    {
        if (attackable)
        {
            if (other.CompareTag("Enemy"))
            {
                stateControllerMonster = other.GetComponent<StateController>();
                if (stateControllerMonster.anim.GetCurrentAnimatorStateInfo(0).IsName("Block"))
                {
                    stateControllerMonster.anim.SetTrigger("Impact");
                }
                else
                {
                    stateControllerMonster.hp -= 40;
                    totalDamage += 40;
                    stateControllerMonster.UpdateHPValue();
                    stateControllerMonster.emeny = knightController.transform;
                    //anim = other.GetComponent<Animator>();
                    //anim.SetTrigger("Die");
                    //Invoke("SetUnactive", 2);
                    battleLearning.AddReward(2f);
                    cw.GetDamageData(totalDamage.ToString(), time.ToString());
                    Debug.Log("Write");
                }

                
            }
        }        
    }

    private void Update()
    {
        time += Time.deltaTime;        
    }




}

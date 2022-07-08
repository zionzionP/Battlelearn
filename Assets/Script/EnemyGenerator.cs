using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float GenerateTime;
    [SerializeField]
    private Transform Env;
    [SerializeField]
    private int num;    
    [SerializeField]
    private StateController knightStateController;
    [SerializeField]
    private BattleLearning battleLearning;
    private GameObject[] prefabArray = new GameObject[10];
    private float time;
    private NavMeshAgent navMeshAgent;
    private StateController stateMonster;
    [SerializeField]
    private State instState;
    private int killCounter;
    private float killTime;
    [SerializeField]
    private GameObject csvWriter;
    private CsvWriterScript cw;




    void Start()
    {
        prefabArray = new GameObject[num];
        //�ŏ���Instantiate�őS�Đ�������prefabArray�Ɋi�[���Ă���
        for (int i = 0; i < prefabArray.Length; i++)
        {
            GameObject prefab = Instantiate(enemyPrefab);
            //���̎���������prefab�͈�U��\����Ԃɂ��Ă���
            prefab.SetActive(false);
            prefabArray[i] = prefab;
        }
        killCounter = -1;
        killTime = 0;
        csvWriter = GameObject.Find("CsvWriter");
        cw = csvWriter.GetComponent<CsvWriterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // �O�t���[������̎��Ԃ����Z���Ă���
        time = time + Time.deltaTime;

        // ��1�b�u���Ƀ����_���ɐ��������悤�ɂ���B
        if (time > GenerateTime)
        {
            float x = Random.Range(-8f, 8f);  
            float z = Random.Range(-8f, 8f);

            ReusePrefab(new Vector3(Env.position.x + x, 1f, Env.position.z + z));


            //Instantiate(enemyPrefab, new Vector3(x, 1f, z), enemyPrefab.transform.rotation);

            time = 0f;
        }
        killTime = killTime + Time.deltaTime;

    }

    private void ReusePrefab(Vector3 position)
    {
        //���ݔ�\����Ԃ�prefab��T��
        for (int i = 0; i < prefabArray.Length; i++)
        {
            if (prefabArray[i].activeSelf == false)
            {
                WriteKillDate();
                //�ʒu���w�肵�ďo��������                
                prefabArray[i].transform.position = position;
                knightStateController.emeny = prefabArray[i].transform;
                battleLearning.enemyTf = prefabArray[i].transform;
                prefabArray[i].SetActive(true);
                navMeshAgent = prefabArray[i].GetComponent<NavMeshAgent>();
                stateMonster = prefabArray[i].GetComponent<StateController>();
                prefabArray[i].GetComponent<CapsuleCollider>().enabled = true;
                battleLearning.enemyState = stateMonster;
                stateMonster.SetMaxHP();
                stateMonster.UpdateHPValue();
                stateMonster.currentState = instState;
                navMeshAgent.speed = 5;
                stateMonster.dead = false;
                //battleLearning.EndEpisode();
                //��ł���������for���𔲂���
                break;
            }
        }
        
    }

    private void WriteKillDate()
    {        
        killCounter += 1;
        cw.GetKillData(killCounter.ToString(), killTime.ToString());
        killTime = 0;
    }
}
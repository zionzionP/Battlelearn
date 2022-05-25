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




    void Start()
    {
        prefabArray = new GameObject[num];
        //最初にInstantiateで全て生成してprefabArrayに格納しておく
        for (int i = 0; i < prefabArray.Length; i++)
        {
            GameObject prefab = Instantiate(enemyPrefab);
            //この時生成したprefabは一旦非表示状態にしておく
            prefab.SetActive(false);
            prefabArray[i] = prefab;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 前フレームからの時間を加算していく
        time = time + Time.deltaTime;

        // 約1秒置きにランダムに生成されるようにする。
        if (time > GenerateTime)
        {
            float x = Random.Range(-8f, 8f);  
            float z = Random.Range(-8f, 8f);

            ReusePrefab(new Vector3(Env.position.x + x, 1f, Env.position.z + z));


            //Instantiate(enemyPrefab, new Vector3(x, 1f, z), enemyPrefab.transform.rotation);

            time = 0f;
        }
    }

    private void ReusePrefab(Vector3 position)
    {
        //現在非表示状態のprefabを探す
        for (int i = 0; i < prefabArray.Length; i++)
        {
            if (prefabArray[i].activeSelf == false)
            {
                //位置を指定して出現させる                
                prefabArray[i].transform.position = position;
                knightStateController.emeny = prefabArray[i].transform;
                battleLearning.enemyTf = prefabArray[i].transform;
                prefabArray[i].SetActive(true);
                navMeshAgent = prefabArray[i].GetComponent<NavMeshAgent>();
                navMeshAgent.speed = 5;
                battleLearning.EndEpisode();
                //一つでも見つけたらfor文を抜ける
                break;
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*------------------------------------------
CSVを元にマップを作るプログラム
プレイヤーやエネミーの初期配置など
------------------------------------------*/

public class MapCreateScript : MonoBehaviour
{
    [SerializeField] private CSVDataReader CSVSC;
    public GameObject[] Mas;

    [SerializeField , Label("敵オブジェクト")] GameObject[] EnemyObject;
    [HideInInspector] EnemyContller[] EnemyScript = new EnemyContller[4];

    [HideInInspector] public float startXpos;
    [HideInInspector] public float startYpos;

    [HideInInspector] public float masXSize = 128;
    [HideInInspector] public float masYSize = 128;

    public GameObject stageParent;

    [SerializeField] int areaTwo;
    [SerializeField] int areaThree;
    [SerializeField] int areaFour;

    bool[] spawnCheck = new bool[4];


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        areaTwo = 0;
        areaThree = 0;
        areaFour = 0;

        for(int i = 0;i< spawnCheck.Length; i++)
        {
            spawnCheck[i] = true;
        }
        RandomSpawn();
        StartCreateMap();
        EnemySpawn();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomSpawn()
    {
        switch (CSVSC.mapNumber)
        {
            case 0:
                switch (Random.Range(0, 4))
                {
                    case 0:
                        startXpos = -25;
                        startYpos = 19;
                        break;

                    case 1:
                        startXpos = -23;
                        startYpos = 21;
                        break;

                    case 2:
                        startXpos = -28;
                        startYpos = 26;
                        break;

                    case 3:
                        startXpos = -26;
                        startYpos = 28;
                        break;
                }
                break;

            case 1:
                switch (Random.Range(0, 4))
                {
                    case 0:
                        startXpos = -3;
                        startYpos = 3;
                        break;

                    case 1:
                        startXpos = -3;
                        startYpos = 6;
                        break;

                    case 2:
                        startXpos = -3;
                        startYpos = 9;
                        break;

                    case 3:
                        startXpos = -3;
                        startYpos = 12;
                        break;
                }
                break;

            case 2:
                switch (Random.Range(0, 4))
                {
                    case 0:
                        startXpos = -28;
                        startYpos = 3;
                        break;

                    case 1:
                        startXpos = -23;
                        startYpos = 3;
                        break;

                    case 2:
                        startXpos = -28;
                        startYpos = 8;
                        break;

                    case 3:
                        startXpos = -23;
                        startYpos = 8;
                        break;
                }
                break;

            case 3:
                switch (Random.Range(0, 4))
                {
                    case 0:
                        startXpos = -3;
                        startYpos = 28;
                        break;

                    case 1:
                        startXpos = -6;
                        startYpos = 28;
                        break;

                    case 2:
                        startXpos = -12;
                        startYpos = 26;
                        break;

                    case 3:
                        startXpos = -12;
                        startYpos = 23;
                        break;
                }
                break;
        }
    }

    public void StartCreateMap()
    {
        for (int x = 0; x < CSVSC.csvDatasInt.Count; x++)
        {
            for (int y = 0; y < CSVSC.csvDatasInt[x].Length; y++)
            {
                Instantiate(
                   Mas[CSVSC.csvDatasInt[y][x]],
                   new Vector3(((float)x * masXSize / 120) + startXpos,
                   ((float)y * masYSize / 120 * -1) + startYpos, 0),
                   Quaternion.identity,
                   stageParent.transform);

                //それぞれのエリアのパネル数の合計を出す
                switch (CSVSC.csvDatasInt[y][x])
                {
                    case 2:
                        areaTwo++;
                        break;

                    case 3:
                        areaThree++;
                        break;

                    case 4:
                        areaFour++;
                        break;
                }
            }
        }
    }

    void EnemySpawn()
    {
        
        for (int x = 0; x < CSVSC.csvDatasInt.Count; x++)
        {
            for (int y = 0; y < CSVSC.csvDatasInt[x].Length; y++)
            {
                //それぞれのエリアにランダムで敵を出現させる
                switch (CSVSC.csvDatasInt[y][x])
                {
                    case 2:
                        if (Probability(100.0f / areaTwo) && spawnCheck[0])
                        {
                            GameObject objOne = Instantiate(EnemyObject[0],
                                new Vector3(((float)x * masXSize / 120) + startXpos,
                                ((float)y * masYSize / 120 * -1) + startYpos, 0),
                                Quaternion.identity) as GameObject;
                            EnemyScript[0] = objOne.GetComponent<EnemyContller>();
                            EnemyScript[0].movement = true;
                            EnemyScript[0].icon.SetActive(true);
                            spawnCheck[0] = false;
                        }
                        break;

                    case 3:
                        if (Probability(100.0f / areaThree))
                        {
                            if (spawnCheck[1])
                            {
                                GameObject objTwo = Instantiate(EnemyObject[1],
                                    new Vector3(((float)x * masXSize / 120) + startXpos,
                                    ((float)y * masYSize / 120 * -1) + startYpos, 0),
                                    Quaternion.identity);
                                EnemyScript[1] = objTwo.GetComponent<EnemyContller>();
                                EnemyScript[1].movement = true;
                                EnemyScript[1].icon.SetActive(true);
                                spawnCheck[1] = false;
                            }
                            else if (spawnCheck[2])
                            {
                                GameObject objThree = Instantiate(EnemyObject[2],
                                    new Vector3(((float)x * masXSize / 120) + startXpos,
                                    ((float)y * masYSize / 120 * -1) + startYpos, 0),
                                    Quaternion.identity);
                                EnemyScript[2] = objThree.GetComponent<EnemyContller>();
                                EnemyScript[2].movement = true;
                                EnemyScript[2].icon.SetActive(true);
                                spawnCheck[2] = false;
                            }
                        }
                        break;

                    case 4:
                        if (Probability(100.0f / areaFour) && spawnCheck[3])
                        {
                            GameObject objFour = Instantiate(EnemyObject[3],
                                new Vector3(((float)x * masXSize / 120) + startXpos,
                                ((float)y * masYSize / 120 * -1) + startYpos, 0),
                                Quaternion.identity);
                            EnemyScript[3] = objFour.GetComponent<EnemyContller>();
                            EnemyScript[3].movement = true;
                            EnemyScript[3].icon.SetActive(true);
                            spawnCheck[3] = false;
                        }
                        break;
                }
            }
        }
        if(spawnCheck[0] || spawnCheck[1] || spawnCheck[2] || spawnCheck[3])
        {
            EnemySpawn();
        }
    }

    public void CreateMap()
    {
        foreach (Transform child in stageParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int x = 0; x < CSVSC.csvDatasInt.Count; x++)
        {
            for (int y = 0; y < CSVSC.csvDatasInt[x].Length; y++)
            {
                Instantiate(
                   Mas[CSVSC.csvDatasInt[y][x]],
                   new Vector3(((float)x * masXSize / 120) + startXpos,
                   ((float)y * masYSize / 120 * -1) + startYpos, 0),
                   Quaternion.identity,
                   stageParent.transform);
            }
        }
    }

    public bool Probability(float fPercent)
    {
        float fProbabilityRate = UnityEngine.Random.value * 100.0f;

        if (fPercent == 100.0f && fProbabilityRate == fPercent)
        {
            
            return true;
        }
        else if (fProbabilityRate < fPercent)
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }
}

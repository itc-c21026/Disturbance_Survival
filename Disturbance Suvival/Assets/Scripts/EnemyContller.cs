using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*------------------------------------
エネミーのプログラム
------------------------------------*/

public class EnemyContller : MonoBehaviour
{

    [SerializeField] private MapCreateScript MapSC; //マップを生成するスクリプト
    [SerializeField] private CSVDataReader CSVSC; //csvを読み込むスクリプト

    [HideInInspector] private PlayerController PlayerSC;//プレイヤーのスクリプト

    public GameObject icon;

    public GameObject clawEffect;

    Vector3 StartPosition;
    [HideInInspector] public float enemySpeed; //移動速度
    [HideInInspector] public Vector3 moveX;//X軸の移動
    [HideInInspector] public Vector3 moveY;//Y軸の移動
    Vector3 movePosition; //移動距離

    Vector3 targetPosition;

    [HideInInspector] public float moveXDistance = 1; // X軸の移動距離
    [HideInInspector] public float moveYDistance = 1; // Y軸の移動距離

    public enum EnemyNumber
    {
        ZERO,
        ONE,
        TWO,
        THREE
    }

    public EnemyNumber enemyNum;

    public float restartMoveTimeLimit;
    float restartMoveTime;

    //自分から見た上下左右の位置にあるマップデータ（数値）を格納する変数
    int upPosition;
    int downPosition;
    int rigthPosition;
    int leftPosition;

    int randomMove;
    public float attackTime;
    float attackLimit;
    int attackPawor;
    bool moveCheck;
    public bool movement;

    //bool attackFlag;

    int overlap;
    int saveRandom;

    bool checkAttack;


    private void Awake()
    {
        switch (enemyNum)
        {
            case EnemyNumber.ZERO:
                attackPawor = 1;
                enemySpeed = 5;
                attackLimit = 2.5f;
                break;

            case EnemyNumber.ONE:
                attackPawor = 1;
                enemySpeed = 5.5f;
                attackLimit = 2.7272f;
                break;

            case EnemyNumber.TWO:
                attackPawor = 2;
                enemySpeed = 4.5f;
                attackLimit = 3;
                break;

            case EnemyNumber.THREE:
                attackPawor = 3;
                enemySpeed = 4f;
                attackLimit = 2.7272f;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerSC = GameObject.Find("Player").GetComponent<PlayerController>();

        moveCheck = true;
        //movement = true;
        //attackFlag = false;
        moveX = new Vector3(moveXDistance, 0, 0);
        moveY = new Vector3(0, moveYDistance, 0);
        //transform.position = StartPosition;
        StartPosition = transform.position;
        movePosition = StartPosition;

        restartMoveTime = 0;
        attackTime = 0;
        overlap = 0;
        checkAttack = true;

    }

    // Update is called once per frame
    void Update()
    {
        //if(movement) EnemyMove();

        if (checkAttack)
        {
            if (movement)
            {
                EnemyMove();

                //移動開始
                transform.position = Vector3.MoveTowards(transform.position, movePosition, enemySpeed * Time.deltaTime);

                //移動先に到着すると他の動作を受け付けるようになる
                if (transform.position == movePosition)
                {
                    moveCheck = true;
                }
            }
        }
    }

    void EnemyMove()
    {

        if (moveCheck)
        {
            restartMoveTime += Time.deltaTime;
            if(restartMoveTime > restartMoveTimeLimit)
            {
                if(overlap == 0)
                {
                    randomMove = Random.Range(0, 8);
                    saveRandom = randomMove;
                    RandomMovement();
                    restartMoveTime = 0;
                    overlap = 1;
                }
                else if(overlap == 1)
                {
                    randomMove = Random.Range(0, 8);
                    if(randomMove != 0 && randomMove != 7)
                    {
                        if(randomMove == saveRandom - 1 || randomMove == saveRandom || randomMove == saveRandom + 1)
                        {
                            RandomMovement();
                            saveRandom = randomMove;
                        }
                        else
                        {
                            overlap = 0;
                            EnemyMove();
                        }
                    }
                    else if(randomMove == 0)
                    {
                        if (randomMove == saveRandom + 1 || randomMove == saveRandom || randomMove == saveRandom + 7)
                        {
                            RandomMovement();
                            saveRandom = randomMove;
                        }
                        else
                        {
                            overlap = 0;
                            EnemyMove();
                        }
                    }
                    else if (randomMove != 7)
                    {
                        if (randomMove == saveRandom - 1 || randomMove == saveRandom || randomMove == saveRandom - 7)
                        {
                            RandomMovement();
                            saveRandom = randomMove;
                        }
                        else
                        {
                            overlap = 0;
                            EnemyMove();
                        }
                    }
                    restartMoveTime = 0;
                }
            }
        }
    }

    bool checkMove(Vector3 enemyPosition)
    {
        if (CSVSC.csvDatasInt[(int)(-enemyPosition.y + MapSC.startYpos)][(int)(enemyPosition.x - MapSC.startXpos)] >= 5)
        {
            overlap = 0;
            EnemyMove();
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "StunShot") StartCoroutine(NotAttack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        if(checkAttack) StartCoroutine(AttackEffeck(collision.transform.position));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            movement = false;
            attackTime += Time.deltaTime;
            if (attackTime > attackLimit)
            {
                attackTime = 0;
                if (checkAttack) StartCoroutine(AttackEffeck(collision.transform.position));
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        movement = true;
        //attackFlag = false;
        if (collision.gameObject.tag == "Player") movement = true;
        attackTime = 0;
    }

    IEnumerator AttackEffeck(Vector3 InstancePosition)
    {
        GameObject obj = Instantiate(clawEffect, InstancePosition, Quaternion.identity);
        PlayerSC.PlayerHPDown(attackPawor);
        yield return new WaitForSeconds(1);
        Destroy(obj);
    }

    void RandomMovement()
    {
        //左上から時計回りで番号をわりふっていく
        switch (randomMove)
        {
            case 0://左上
                if (checkMove(transform.position - moveX + moveY)) movePosition = transform.position + new Vector3(-moveXDistance, moveYDistance, 0);
                moveCheck = false;
                break;

            case 1://上
                if (checkMove(transform.position + moveY)) movePosition = transform.position + moveY;
                moveCheck = false;
                break;

            case 2://右上
                if (checkMove(transform.position + moveX + moveY)) movePosition = transform.position + new Vector3(moveXDistance, moveYDistance, 0);
                moveCheck = false;
                break;

            case 3://右
                if (checkMove(transform.position + moveX)) movePosition = transform.position + moveX;
                moveCheck = false;
                break;

            case 4://右下
                if (checkMove(transform.position + moveX - moveY)) movePosition = transform.position + new Vector3(moveXDistance, -moveYDistance, 0);
                moveCheck = false;
                break;

            case 5://下
                if (checkMove(transform.position - moveY)) movePosition = transform.position + -moveY;
                moveCheck = false;
                break;

            case 6://左下
                if (checkMove(transform.position - moveX - moveY)) movePosition = transform.position + new Vector3(-moveXDistance, -moveYDistance, 0);
                moveCheck = false;
                break;

            case 7://左
                if (checkMove(transform.position - moveX)) movePosition = transform.position + -moveX;
                moveCheck = false;
                break;

        }
    }

    public IEnumerator NotAttack()
    {
        if (checkAttack)
        {
            checkAttack = false;
            yield return new WaitForSeconds(10);
            checkAttack = true;
        }
    }

}

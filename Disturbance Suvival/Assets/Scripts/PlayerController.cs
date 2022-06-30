using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*------------------------------------------
プレイヤーを制御するプログラム
プレイヤー関連のプログラム
------------------------------------------*/

public class PlayerController : MonoBehaviour
{

    [SerializeField , Label("ScreenInputのスクリプト")] private ScreenInput SISC; //タッチ操作を取得するスクリプト
    [SerializeField , Label("StageManagerのスクリプト")] private MapCreateScript MapSC; //マップを生成するスクリプト

    [SerializeField , Label("Investigateのスクリプト")] private Investigate InvestgateSC; //行動｛調べる｝を管理するスクリプト
    [SerializeField , Label("CSVを読み込むスクリプト")] private CSVDataReader CSVSC; //csvを読み込むスクリプト

    [SerializeField , Label("鍵を管理するスクリプト")] private KeyCounter keySC;
    [SerializeField , Label("アイテムを管理するスクリプト")] private GetItem ItemSC;

    [SerializeField] private Life LifeSC;

    [HideInInspector] EnemyContller EnemySC; //一時保存用

    [HideInInspector] public Vector3 StartPosition; //初期位置

    public int PlayerHP; //プレイヤーのHP

    [HideInInspector] public bool InputCheck; //入力判定
    [HideInInspector] public bool ButtonOnCheck; //ボタンの入力判定
    [HideInInspector] public float playerSpeed; //移動速度

    [HideInInspector] public Vector3 moveX;//X軸の移動
    [HideInInspector] public Vector3 moveY;//Y軸の移動
    Vector3 movePosition; //移動距離

    [SerializeField, Label("プレイヤーのX軸の移動距離")] public float moveXDistance; // X軸の移動距離
    [SerializeField, Label("プレイヤーのY軸の移動距離")] public float moveYDistance; // Y軸の移動距離

    //調べるオブジェクトがどこにあるかを識別する定数
    [HideInInspector] public enum VectorType
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        NONE
    }

    //持っている鍵がどの種類かを判別するための定数
    [HideInInspector]public enum KeyType
    {
        FIRSTKEY,
        SECONDKEY,
        THIRDKEY,
        LASTKEY,
        ITEMKEY,
        NONE
    }

    [HideInInspector] public enum KeyRarity
    {
        COMMON,
        UNCOMMON,
        RARE,
        BERRYRARE,
        NONE
    }

    [HideInInspector] public VectorType searchVct = VectorType.NONE;
    [HideInInspector] public KeyType doorKeyNumber = KeyType.NONE;
    [HideInInspector] public KeyRarity boxKeyRarity = KeyRarity.NONE;

    public bool boxKey;

    //自分から見た上下左右の位置にあるマップデータ（数値）を格納する変数
    int upPosition;
    int downPosition;
    int rigthPosition;
    int leftPosition;

    int nullBoxNumer; //調べ終わったマスの書き換え先

    IEnumerator saveSpeed; //コルーチン保存用

    SpriteRenderer CharacterImage;

    [HideInInspector] public bool canStun; //スタンガンを使用できるかどうかのフラグ

    float defaultSpeed;

    [SerializeField] Text[] Ktext;
    void Awake()
    {

        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/Main_Character");

        //選択したキャラによってステータスを変更する
        switch (PlayerPrefs.GetInt("CHARA_NUMBER"))
        {
            case 1:
                if(CharacterImage == null) CharacterImage = GetComponent<SpriteRenderer>();
                CharacterImage.sprite = sprites[0];
                PlayerHP = 5;
                playerSpeed = 5;
#if UNITY_STANDALONE_WIN
                Ktext[0].text = "R";
                Ktext[1].text = "A";
                Ktext[2].text = "S";
                Ktext[3].text = "D";
#endif
                break;

            case 2:
                if (CharacterImage == null) CharacterImage = GetComponent<SpriteRenderer>();
                CharacterImage.sprite = sprites[1];
                PlayerHP = 4;
                playerSpeed = 6;
#if UNITY_STANDALONE_WIN
                Ktext[0].text = "R";
                Ktext[1].text = "A";
                Ktext[2].text = "S";
#endif
                break;

            case 3:
                if (CharacterImage == null) CharacterImage = GetComponent<SpriteRenderer>();
                CharacterImage.sprite = sprites[2];
                PlayerHP = 6;
                playerSpeed = 4.5f;
#if UNITY_STANDALONE_WIN
                Ktext[0].text = "R";
                Ktext[1].text = "A";
                Ktext[2].text = "S";
#endif
                break;

            case 4:
                if (CharacterImage == null) CharacterImage = GetComponent<SpriteRenderer>();
                CharacterImage.sprite = sprites[3];
                PlayerHP = 4;
                playerSpeed = 5.5f;
#if UNITY_STANDALONE_WIN
                Ktext[0].text = "R";
                Ktext[1].text = "A";
                Ktext[2].text = "S";
                Ktext[3].text = "D";
                Ktext[4].text = "F";
#endif
                break;
        }
        LifeSC.AllClear();
        LifeSC.LifeSet();
        LifeSC.AllClearB();
        LifeSC.LifeSetB();
    }

    // Start is called before the first frame update
    void Start()
    {
        InputCheck = false;
        ButtonOnCheck = true;

        boxKey = false;

        moveX = new Vector3(moveXDistance, 0, 0);
        moveY = new Vector3(0, moveYDistance, 0);

        transform.position = StartPosition;
        movePosition = StartPosition;

        saveSpeed = SpeedUp();
        defaultSpeed = playerSpeed;

    }

    // Update is called once per frame
    void Update()
    {

        upPosition = CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos)][(int)(transform.position.x - MapSC.startXpos) + 1];
        downPosition = CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos)][(int)(transform.position.x - MapSC.startXpos) - 1];
        rigthPosition = CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos) + 1][(int)(transform.position.x - MapSC.startXpos)];
        leftPosition = CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos) - 1][(int)(transform.position.x - MapSC.startXpos)];

        if (ButtonOnCheck)
        {
            PlayerMove();
        }

        
        checkVct();
    }

    void PlayerMove()
    {
        if (!InputCheck)
        {
            switch (SISC.GetNowSwipe())
            {
                //上下左右
                case ScreenInput.SwipeDirection.UP:
                    if(checkMove(transform.position + moveY)) movePosition = transform.position + moveY;
                    InputCheck = true;
                    break;

                case ScreenInput.SwipeDirection.DOWN:
                    if (checkMove(transform.position - moveY)) movePosition = transform.position + -moveY;
                    InputCheck = true;
                    break;

                case ScreenInput.SwipeDirection.RIGHT:
                    if (checkMove(transform.position + moveX)) movePosition = transform.position + moveX;
                    InputCheck = true;
                    break;

                case ScreenInput.SwipeDirection.LEFT:
                    if (checkMove(transform.position - moveX)) movePosition = transform.position + -moveX;
                    InputCheck = true;
                    break;


                //斜め方向
                case ScreenInput.SwipeDirection.UP_RIGHT:
                    if (checkMove(transform.position + moveX + moveY)) movePosition = transform.position + new Vector3(moveXDistance, moveYDistance, 0);
                    InputCheck = true;
                    break;

                case ScreenInput.SwipeDirection.UP_LEFT:
                    if (checkMove(transform.position - moveX + moveY)) movePosition = transform.position + new Vector3(-moveXDistance, moveYDistance, 0);
                    InputCheck = true;
                    break;

                case ScreenInput.SwipeDirection.DOWN_RIGHT:
                    if (checkMove(transform.position + moveX - moveY)) movePosition = transform.position + new Vector3(moveXDistance, -moveYDistance, 0);
                    InputCheck = true;
                    break;

                case ScreenInput.SwipeDirection.DOWN_LEFT:
                    if (checkMove(transform.position - moveX - moveY)) movePosition = transform.position + new Vector3(-moveXDistance, -moveYDistance, 0);
                    InputCheck = true;
                    break;

            }
        }

        //移動開始
        transform.position = Vector3.MoveTowards(transform.position, movePosition, playerSpeed * Time.deltaTime);

        //移動先に到着するとタッチ操作を受け付けるようになる
        if (transform.position == movePosition) InputCheck = false;
    }

    bool checkMove(Vector3 playerPosition)
    {
        if (CSVSC.csvDatasInt[(int)(-playerPosition.y + MapSC.startYpos)][(int)(playerPosition.x - MapSC.startXpos)] >= 5)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void checkVct()
    {
        //上のマスに調べたいものがあった時
        if (upPosition == 12
            || (upPosition >= 16
            && upPosition <= 23))
        {
            InvestgateSC.InvestigateButtonOn();
            searchVct = VectorType.UP;
        }
        //下のマスに調べたいものがあった時
        else if (downPosition == 12
            || (downPosition >= 16
            && downPosition <= 23))
        {
            InvestgateSC.InvestigateButtonOn();
            searchVct = VectorType.DOWN;
        }
        //右のマスに調べたいものがあった時
        else if (rigthPosition == 12
            || (rigthPosition >= 16
            && rigthPosition <= 23))
        {
            InvestgateSC.InvestigateButtonOn();
            searchVct = VectorType.RIGHT;
        }
        //左のマスに調べたいものがあった時
        else if (leftPosition == 12
            || (leftPosition >= 16
            && leftPosition <= 23))
        {
            InvestgateSC.InvestigateButtonOn();
            searchVct = VectorType.LEFT;
        }
        //周囲に調べたいものがなかった時
        else
        {
            InvestgateSC.InvestigateButtonOff();
            searchVct = VectorType.NONE;

            //調べたいマスが鍵付きドアだった場合
            if (doorKeyNumber != KeyType.NONE)
            {
                switch (doorKeyNumber)
                {
                    case KeyType.FIRSTKEY:
                        keyconfimation(8);
                        break;

                    case KeyType.SECONDKEY:
                        keyconfimation(9);
                        break;

                    case KeyType.THIRDKEY:
                        keyconfimation(10);
                        break;

                    case KeyType.LASTKEY:
                        keyconfimation(11);
                        break;
                }
            }
        }
    }

    //オブジェクトを調べ終えた時の挙動
    public void investigateCompletion()
    {
        switch (searchVct)
        {
            case VectorType.UP:
                GetKeyNumber(upPosition);
                CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos)][(int)(transform.position.x - MapSC.startXpos) + 1] = nullBoxNumer;
                MapSC.CreateMap();
                break;

            case VectorType.DOWN:
                GetKeyNumber(downPosition);
                CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos)][(int)(transform.position.x - MapSC.startXpos) - 1] = nullBoxNumer;
                MapSC.CreateMap();
                break;

            case VectorType.RIGHT:
                GetKeyNumber(rigthPosition);
                CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos) + 1][(int)(transform.position.x - MapSC.startXpos)] = nullBoxNumer;
                MapSC.CreateMap();
                break;

            case VectorType.LEFT:
                GetKeyNumber(leftPosition);
                CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos) - 1][(int)(transform.position.x - MapSC.startXpos)] = nullBoxNumer;
                MapSC.CreateMap();
                break;

            case VectorType.NONE:
                break;
        }

        InvestgateSC.InvestigateButtonOff();

    }

    public void liftKeyCompletion() // カードキーでゲートを開けた時の挙動
    {
        switch (searchVct)
        {
            case VectorType.UP:
                switch (doorKeyNumber)
                {
                    case KeyType.FIRSTKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos)][(int)(transform.position.x - MapSC.startXpos) + 1] = 1;
                        break;

                    case KeyType.SECONDKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos)][(int)(transform.position.x - MapSC.startXpos) + 1] = 2;
                        break;

                    case KeyType.THIRDKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos)][(int)(transform.position.x - MapSC.startXpos) + 1] = 3;
                        break;

                    case KeyType.LASTKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos)][(int)(transform.position.x - MapSC.startXpos) + 1] = 4;
                        //SceneManagerScript.EndScese();
                        break;
                }
                MapSC.CreateMap();
                keySC.keyCount++;
                break;

            case VectorType.DOWN:
                switch (doorKeyNumber)
                {
                    case KeyType.FIRSTKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos)][(int)(transform.position.x - MapSC.startXpos) - 1] = 1;
                        break;

                    case KeyType.SECONDKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos)][(int)(transform.position.x - MapSC.startXpos) - 1] = 2;
                        break;

                    case KeyType.THIRDKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos)][(int)(transform.position.x - MapSC.startXpos) - 1] = 3;
                        break;

                    case KeyType.LASTKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos)][(int)(transform.position.x - MapSC.startXpos) - 1] = 4;
                        //SceneManagerScript.EndScese();
                        break;
                }
                MapSC.CreateMap();
                keySC.keyCount++;
                break;

            case VectorType.RIGHT:
                switch (doorKeyNumber)
                {
                    case KeyType.FIRSTKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos) + 1][(int)(transform.position.x - MapSC.startXpos)] = 1;
                        break;

                    case KeyType.SECONDKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos) + 1][(int)(transform.position.x - MapSC.startXpos)] = 2;
                        break;

                    case KeyType.THIRDKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos) + 1][(int)(transform.position.x - MapSC.startXpos)] = 3;
                        break;

                    case KeyType.LASTKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos) + 1][(int)(transform.position.x - MapSC.startXpos)] = 4;
                        //SceneManagerScript.EndScese();
                        break;
                }
                MapSC.CreateMap();
                keySC.keyCount++;
                break;

            case VectorType.LEFT:
                switch (doorKeyNumber)
                {
                    case KeyType.FIRSTKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos) - 1][(int)(transform.position.x - MapSC.startXpos)] = 1;
                        break;

                    case KeyType.SECONDKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos) - 1][(int)(transform.position.x - MapSC.startXpos)] = 2;
                        break;

                    case KeyType.THIRDKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos) - 1][(int)(transform.position.x - MapSC.startXpos)] = 3;
                        break;

                    case KeyType.LASTKEY:
                        CSVSC.csvDatasInt[(int)(-transform.position.y + MapSC.startYpos) - 1][(int)(transform.position.x - MapSC.startXpos)] = 4;
                        //SceneManagerScript.EndScese();
                        break;
                }
                MapSC.CreateMap();
                keySC.keyCount++;
                break;

            case VectorType.NONE:
                break;
        }

        InvestgateSC.liftKeyButtonOff();
        doorKeyNumber = KeyType.NONE;
        ItemSC.closecardkey();
    }

    void keyconfimation(int value)
    {
        //上のマスに調べたいものがあった時
        if (upPosition == value)
        {
            InvestgateSC.liftKeyButtonOn();
            searchVct = VectorType.UP;
        }
        //下のマスに調べたいものがあった時
        else if (downPosition == value)
        {
            InvestgateSC.liftKeyButtonOn();
            searchVct = VectorType.DOWN;
        }
        //右のマスに調べたいものがあった時
        else if (rigthPosition == value)
        {
            InvestgateSC.liftKeyButtonOn();
            searchVct = VectorType.RIGHT;
        }
        //左のマスに調べたいものがあった時
        else if (leftPosition == value)
        {
            InvestgateSC.liftKeyButtonOn();
            searchVct = VectorType.LEFT;
        }
        //周囲に調べたいオブジェクトがなかった時
        else
        {
            InvestgateSC.liftKeyButtonOff();
            searchVct = VectorType.NONE;
        }
    }

    void GetKeyNumber(int position)
    {
        switch (position)
        {
            case 12:
                if (boxKey)
                {
                    ItemSC.SelectPanelRarity();
                    nullBoxNumer = 13;
                }
                else
                {
                    nullBoxNumer = 12;
                }
                break;

            case 16:
                ItemBoxSearch();
                nullBoxNumer = 14;
                break;

            case 17:
                nullBoxNumer = 14;
                break;

            case 18:
                ItemBoxSearch();
                nullBoxNumer = 15;
                break;

            case 19:
                //if(!boxKey) boxKey = false;
                nullBoxNumer = 15;
                break;

            case 20:
                // カードキー1ゲット
                doorKeyNumber = KeyType.FIRSTKEY;
                ItemSC.cardkey1();
                nullBoxNumer = 15;
                break;

            case 21:
                // カードキー2ゲット
                doorKeyNumber = KeyType.SECONDKEY;
                ItemSC.cardkey2();
                nullBoxNumer = 15;
                break;

            case 22:
                // カードキー3ゲット
                doorKeyNumber = KeyType.THIRDKEY;
                ItemSC.cardkey3();
                nullBoxNumer = 15;
                break;

            case 23:
                // カードキー4ゲット
                doorKeyNumber = KeyType.LASTKEY;
                ItemSC.cardkey4();
                nullBoxNumer = 15;
                break;
        }
    }

    public void ItemBoxSearch()
    {
        if (!boxKey)
        {
            int keyGetRandom = Random.Range(0, 100);
            if (keyGetRandom < 15)
            {
                int KeyGetRandomRarity = Random.Range(0, 100);
                if (KeyGetRandomRarity < 50)
                {
                    // コモンのボックス鍵をゲット
                    boxKeyRarity = KeyRarity.COMMON;
                    ItemSC.chestkey1();
                }
                else if (KeyGetRandomRarity < 80)
                {
                    // アンコモンのボックス鍵をゲット
                    boxKeyRarity = KeyRarity.UNCOMMON;
                    ItemSC.chestkey2();
                }
                else if (KeyGetRandomRarity < 95)
                {
                    // レアのボックス鍵をゲット
                    boxKeyRarity = KeyRarity.RARE;
                    ItemSC.chestkey3();
                }
                else
                {
                    // ベリーレアのボックス鍵をゲット
                    boxKeyRarity = KeyRarity.BERRYRARE;
                    ItemSC.chestkey4();
                }

                boxKey = true;
            }
            else
            {
                ItemSC.GetRandomItem();
            }
        }
        else
        {
            ItemSC.GetRandomItem();
        }
    }

    public void PlayerHPDown(int value) // HPを減らす関数
    {
        if (ItemSC.PlankNum >= 1) // 盾(板)を所持していた場合
        {
            int num = ItemSC.ItemList.IndexOf(GetItem.Item.PLANK);
            ItemSC.ItemList[num] = GetItem.Item.NONE;
#if UNITY_STANDALONE_WIN
            Destroy(ItemSC.ItemPrefabtmp[num]);
#endif
#if UNITY_ANDROID
            GameObject obj = GameObject.Find("Plank(Clone)");
            Destroy(obj);
#endif
        }
        PlayerHP = Mathf.Clamp((PlayerHP + ItemSC.PlankNum) - value,0,5);
        LifeSC.AllClear();
        LifeSC.LifeSet();
        if (ItemSC.PlankNum == 1) ItemSC.PlankNum--;
    }

    public void PlayerHPRecovery(int RecoveryValue) // HPを増やす関数
    {
        PlayerHP = Mathf.Clamp(PlayerHP + RecoveryValue, 0, 5);
    }

    public void UseSpeedUpItem() // スピードスプレーの効果の関数
    {
        StopCoroutine(saveSpeed);
        saveSpeed = null; //一度nullにすることで処理を初期化する
        saveSpeed = SpeedUp();
        playerSpeed = defaultSpeed;
        StartCoroutine(saveSpeed);
    }

    IEnumerator SpeedUp()
    {
        playerSpeed *= 1.2f;
        yield return new WaitForSeconds(15f);
        playerSpeed = defaultSpeed;
    }
}

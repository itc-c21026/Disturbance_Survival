using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*------------------------------------
アイテムを制御するプログラム
------------------------------------*/

public class GetItem : MonoBehaviour
{
    [SerializeField, Label("プレイヤースクリプト")] private PlayerController PlayerSC;
    [HideInInspector] public int getRandom;

    public enum Item
    {
        SPEEDSPRAY,
        BANDAGE,
        PLANK,
        STUNGUN,
        FIRSTAIDKIT,
        NONE,
    }

    public List<Item> ItemList = new List<Item>();

    [HideInInspector] public int PlankNum;

    [HideInInspector] public int lightCheck;

    [SerializeField, Label("ID0～3まで選択可能なパネル")] private GameObject CommonPanel;
    [SerializeField, Label("ID0～5まで選択可能なパネル")] private GameObject UnCommonPanel;
    [SerializeField, Label("ID0～6まで選択可能なパネル")] private GameObject RarePanel;
    [SerializeField, Label("ID0～7まで選択可能なパネル")] private GameObject BerryRarePanel;

    [SerializeField, Label("SpeedSpray")] private GameObject SpeedSpray1;
    [SerializeField, Label("Bandage")] private GameObject Bandage2;
    [SerializeField, Label("Plank")] private GameObject Plank3;
    [SerializeField, Label("StunGun")] private GameObject StunGun4;
    [SerializeField, Label("FirstAidKit")] private GameObject FirstAidKit5;
    [SerializeField, Label("Light")] private GameObject Light0;
    [SerializeField, Label("Lantern")] private GameObject Lantern6;
    [SerializeField, Label("NightVisionGoggle")] private GameObject NightVisionGoggle7;

    [SerializeField, Label("鍵(コモン)")] private CanvasRenderer CommonKey;
    [SerializeField, Label("鍵(アンコモン)")] private CanvasRenderer UnCommonKey;
    [SerializeField, Label("鍵(レア)")] private CanvasRenderer RareKey;
    [SerializeField, Label("鍵(ベリーレア)")] private CanvasRenderer BerryRareKey;

    [SerializeField, Label("カードキー1")] private CanvasRenderer CardKey1;
    [SerializeField, Label("カードキー2")] private CanvasRenderer CardKey2;
    [SerializeField, Label("カードキー3")] private CanvasRenderer CardKey3;
    [SerializeField, Label("カードキー4")] private CanvasRenderer CardKey4;

    [SerializeField, Label("デフォルトパネル")] public GameObject DefaultPanel;
    [SerializeField, Label("ライターパネル")] public GameObject LightPanel;
    [SerializeField, Label("ランタンパネル")] public GameObject LanternPanel;
    [SerializeField, Label("暗視ゴーグルパネル")] public GameObject NightVisionGogglePanel;

    [SerializeField] CanvasRenderer[] ItemBackCR;

    [SerializeField] GameObject canvas;

    int savePanelNum;

    int count;

    int ItemMax;

    [HideInInspector] public bool lightswitch;
    [HideInInspector] public bool lanternswitch;
    [HideInInspector] public bool nightvisiongoggleswitch;

    [SerializeField] public GameObject[] ItemPrefabtmp;


    // Start is called before the first frame update
    void Start()
    {
        PlankNum = 0;
        lightCheck = 0;
        savePanelNum = 0;
        CommonPanel.SetActive(false);
        UnCommonPanel.SetActive(false);
        RarePanel.SetActive(false);
        BerryRarePanel.SetActive(false);
        CommonKey.SetAlpha(0);
        UnCommonKey.SetAlpha(0);
        RareKey.SetAlpha(0);
        BerryRareKey.SetAlpha(0);
        CardKey1.SetAlpha(0);
        CardKey2.SetAlpha(0);
        CardKey3.SetAlpha(0);
        CardKey4.SetAlpha(0);
        LightPanel.SetActive(false);
        LanternPanel.SetActive(false);
        NightVisionGogglePanel.SetActive(false);
        Light0.SetActive(false);
        Lantern6.SetActive(false);
        NightVisionGoggle7.SetActive(false);

        ItemMax = PlayerPrefs.GetInt("ItemMax_NUMBER");

        for (int i = 0; i < ItemMax; i++)
        {
            ItemList.Add(Item.NONE);
        }

        foreach(CanvasRenderer CRs in ItemBackCR)
        {
            CRs.SetAlpha(0);
        }

        for(int i = 0; i < ItemMax; i++)
        {
            ItemBackCR[i].SetAlpha(1);
        }
    }

    public void GetRandomItem() // 確率でアイテムをゲットする関数
    {
        getRandom = Random.Range(0, 100);
        if (getRandom < 50) {
            foreach (var n in ItemList) // アイテム未所持をカウントする
            {
                if (n == Item.NONE) count++;
            }
            if (count != 0) // 未所持枠がある場合
            {
                switch (Random.Range(0, 4))
                {
                    case 0:
                        // コモンのボックス鍵をゲット
                        if (!PlayerSC.boxKey)
                        {
                            PlayerSC.boxKeyRarity = PlayerController.KeyRarity.COMMON;
                            PlayerSC.boxKey = true;
                            chestkey1();
                        }
                        break;

                    case 1:
                        // アイテムリストの最初に見つけた未所持枠を数値として代入
                        int nul = ItemList.IndexOf(Item.NONE) + 1;
                        if (nul > 0)
                        {
                            // 未所持枠をスピードスプレーに変更し、スピードスプレーを生成
                            ItemList[nul - 1] = Item.SPEEDSPRAY;
#if UNITY_STANDALONE_WIN
                            ItemPrefabtmp[nul - 1] = Instantiate(SpeedSpray1, new Vector3((nul + 1) * 180 + 47, 100, 1), Quaternion.identity);
#endif
#if UNITY_EDITOR
                            ItemPrefabtmp[nul - 1] = Instantiate(SpeedSpray1, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
#if UNITY_ANDROID
                            ItemPrefabtmp[nul - 1] = Instantiate(SpeedSpray1, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
                            ItemPrefabtmp[nul - 1].transform.SetParent(this.canvas.transform, false);
                        }
                        break;

                    case 2:
                        // アイテムリストの最初に見つけた未所持枠を数値として代入
                        int nul2 = ItemList.IndexOf(Item.NONE) + 1;
                        if (nul2 > 0)
                        {
                            // 未所持枠を包帯に変更し、包帯を生成
                            ItemList[nul2 - 1] = Item.BANDAGE;
#if UNITY_STANDALONE_WIN
                            ItemPrefabtmp[nul2 - 1] = Instantiate(Bandage2, new Vector3((nul2 + 1) * 180 + 47, 100, 1), Quaternion.identity);
#endif
#if UNITY_EDITOR
                            ItemPrefabtmp[nul2 - 1] = Instantiate(Bandage2, new Vector3((nul2 + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
#if UNITY_ANDROID
                            ItemPrefabtmp[nul2 - 1] = Instantiate(Bandage2, new Vector3((nul2 + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
                            ItemPrefabtmp[nul2 - 1].transform.SetParent(this.canvas.transform, false);
                        }
                        break;

                    case 3:
                        if (PlankNum == 0)
                        {
                            PlankNum++;
                            // アイテムリストの最初に見つけた未所持枠を数値として代入
                            int nul3 = ItemList.IndexOf(Item.NONE) + 1;
                            if (nul3 > 0)
                            {
                                // 未所持枠を盾(板)に変更し、盾(板)を生成
                                ItemList[nul3 - 1] = Item.PLANK;
#if UNITY_STANDALONE_WIN
                                ItemPrefabtmp[nul3 - 1] = Instantiate(Plank3, new Vector3((nul3 + 1) * 180 + 47, 100, 1), Quaternion.identity);
#endif
#if UNITY_EDITOR
                                ItemPrefabtmp[nul3 - 1] = Instantiate(Plank3, new Vector3((nul3 + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
#if UNITY_ANDROID
                                ItemPrefabtmp[nul3 - 1] = Instantiate(Plank3, new Vector3((nul3 + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
                                ItemPrefabtmp[nul3 - 1].transform.SetParent(this.canvas.transform, false);
                            }
                        }
                        break;
                }
                count = 0;
            }
        }
        else if (getRandom < 80)
        {
            foreach (var n in ItemList) // アイテム未所持をカウントする
            {
                if (n == Item.NONE) count++;
            }
            if (count != 0) // 未所持枠がある場合
            {
                switch (Random.Range(0, 2))
                {
                    case 0:
                        // アイテムリストの最初に見つけた未所持枠を数値として代入
                        int nul = ItemList.IndexOf(Item.NONE) + 1;
                        if (nul > 0)
                        {
                            // 未所持枠をスタンガンに変更し、スタンガンを生成する
                            ItemList[nul - 1] = Item.STUNGUN;
#if UNITY_STANDALONE_WIN
                            ItemPrefabtmp[nul - 1] = Instantiate(StunGun4, new Vector3((nul + 1) * 180 + 47, 100, 1), Quaternion.identity);
#endif
#if UNITY_EDITOR
                            ItemPrefabtmp[nul - 1] = Instantiate(StunGun4, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
#if UNITY_ANDROID
                            ItemPrefabtmp[nul - 1] = Instantiate(StunGun4, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
                            ItemPrefabtmp[nul - 1].transform.SetParent(this.canvas.transform, false);
                        }
                        break;

                    case 1:
                        // アイテムリストの最初に見つけた未所持枠を数値として代入
                        int nul2 = ItemList.IndexOf(Item.NONE) + 1;
                        if (nul2 > 0)
                        { 
                            // 未所持枠を医療キットに変更し、医療キットを生成する
                            ItemList[nul2 - 1] = Item.FIRSTAIDKIT;
#if UNITY_STANDALONE_WIN
                            ItemPrefabtmp[nul2 - 1] = Instantiate(FirstAidKit5, new Vector3((nul2 + 1) * 180 + 47, 100, 1), Quaternion.identity);
#endif
#if UNITY_EDITOR
                            ItemPrefabtmp[nul2 - 1] = Instantiate(FirstAidKit5, new Vector3((nul2 + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
#if UNITY_ANDROID
                            ItemPrefabtmp[nul2 - 1] = Instantiate(FirstAidKit5, new Vector3((nul2 + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
                            ItemPrefabtmp[nul2 - 1].transform.SetParent(this.canvas.transform, false);
                        }
                        break;
                }
                count = 0;
            }
        }
        else if (getRandom < 85)
        {
            // ベリーレアのボックス鍵をゲット
            if (!PlayerSC.boxKey)
            {
                PlayerSC.boxKeyRarity = PlayerController.KeyRarity.BERRYRARE;
                PlayerSC.boxKey = true;
                chestkey1();
            }
        }
        else
        {
            // レアのボックス鍵をゲット
            if (!PlayerSC.boxKey)
            {
                PlayerSC.boxKeyRarity = PlayerController.KeyRarity.RARE;
                PlayerSC.boxKey = true;
                chestkey1();
            }
        }

    }

    public void SelectPanelRarity() // パネル
    {
        switch (PlayerSC.boxKeyRarity) // ボックス鍵のレアリティによってパネルを表示
        {
            case PlayerController.KeyRarity.COMMON:
                CommonPanel.SetActive(true);
                savePanelNum = 1;
                break;

            case PlayerController.KeyRarity.UNCOMMON:
                UnCommonPanel.SetActive(true);
                savePanelNum = 2;
                break;

            case PlayerController.KeyRarity.RARE:
                RarePanel.SetActive(true);
                savePanelNum = 3;
                break;

            case PlayerController.KeyRarity.BERRYRARE:
                BerryRarePanel.SetActive(true);
                savePanelNum = 4;
                break;
        }
    }

    public void AddLight() // ライト
    {
        Light0.SetActive(true);
        Lantern6.SetActive(false);
        NightVisionGoggle7.SetActive(false);
        lanternswitch = false;
        nightvisiongoggleswitch = false;
        PanelOff();
        PlayerSC.boxKey = false;
        lightCheck = 1;
    }

    public void AddSpeedSpray() // スピードスプレー
    {
        // アイテム未所持枠をカウントする
        foreach (var n in ItemList)
        {
            if (n == Item.NONE) count++;
        }
        // アイテム未所持枠がある場合
        if (count != 0)
        {
            // アイテムリストの最初に見つけた未所持枠を数値として代入
            int nul = ItemList.IndexOf(Item.NONE) + 1;
            if (nul > 0)
            {
                // 未所持枠をスピードスプレーに変更し、スピードスプレーを生成する
                ItemList[nul - 1] = Item.SPEEDSPRAY;
#if UNITY_STANDALONE_WIN
                ItemPrefabtmp[nul - 1] = Instantiate(SpeedSpray1, new Vector3((nul + 1) * 180 + 47, 100, 1), Quaternion.identity);
#endif
#if UNITY_EDITOR
                ItemPrefabtmp[nul - 1] = Instantiate(SpeedSpray1, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
#if UNITY_ANDROID
                ItemPrefabtmp[nul - 1] = Instantiate(SpeedSpray1, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
                ItemPrefabtmp[nul - 1].transform.SetParent(this.canvas.transform, false);
            }
            count = 0;
        }
        PanelOff();
        PlayerSC.boxKey = false;
    }

    public void AddBandage() // 包帯
    {
        // アイテム未所持をカウント
        foreach (var n in ItemList)
        {
            if (n == Item.NONE) count++;
        }

        // アイテム未所持枠がある場合
        if (count != 0)
        {
            // アイテムリストの最初に見つけた未所持枠を数値として代入
            int nul = ItemList.IndexOf(Item.NONE) + 1;
            if (nul > 0)
            {
                // 未所持枠を包帯に変更し、包帯を生成する
                ItemList[nul - 1] = Item.BANDAGE;
#if UNITY_STANDALONE_WIN
                ItemPrefabtmp[nul - 1] = Instantiate(Bandage2, new Vector3((nul + 1) * 180 + 47, 100, 1), Quaternion.identity);
#endif
#if UNITY_EDITOR
                ItemPrefabtmp[nul - 1] = Instantiate(Bandage2, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
#if UNITY_ANDROID
                ItemPrefabtmp[nul - 1] = Instantiate(Bandage2, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
                ItemPrefabtmp[nul - 1l].transform.SetParent(this.canvas.transform, false);
            }
            count = 0;
        }
        PanelOff();
        PlayerSC.boxKey = false;
    }

    public void AddPlank() // 盾(板)
    {

        if (PlankNum == 0)
        {
            PlankNum++;
            // アイテム未所持をカウント
            foreach (var n in ItemList)
            {
                if (n == Item.NONE) count++;
            }
            // アイテム未所持枠がある場合
            if (count != 0)
            {
                // アイテムリストの最初に見つけた未所持枠を数値として代入
                int nul = ItemList.IndexOf(Item.NONE) + 1;
                if (nul > 0)
                {
                    // 未所持枠を盾(板)に変更し、盾(板)を生成する
                    ItemList[nul - 1] = Item.PLANK;
#if UNITY_STANDALONE_WIN
                    ItemPrefabtmp[nul - 1] = Instantiate(Plank3, new Vector3((nul + 1) * 180 + 47, 100, 1), Quaternion.identity);
#endif
#if UNITY_EDITOR
                    ItemPrefabtmp[nul - 1] = Instantiate(Plank3, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
#if UNITY_ANDROID
                    ItemPrefabtmp[nul - 1] = Instantiate(Plank3, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
                    ItemPrefabtmp[nul - 1].transform.SetParent(this.canvas.transform, false);
                }
                count = 0;
            }
        }
        PanelOff();
        PlayerSC.boxKey = false;
    }

    public void AddStunGun() // スタンガン
    {
        // アイテム未所持枠をカウントする
        foreach (var n in ItemList)
        {
            if (n == Item.NONE) count++;
        }

        // 未所持枠がある場合
        if (count != 0)
        {
            // アイテムリストの最初に見つけた未所持枠を数値として代入
            int nul = ItemList.IndexOf(Item.NONE) + 1;
            if (nul > 0)
            {
                // 未所持枠をスタンガンに変更し、スタンガンを生成する
                ItemList[nul - 1] = Item.STUNGUN;
#if UNITY_STANDALONE_WIN
                ItemPrefabtmp[nul - 1] = Instantiate(StunGun4, new Vector3((nul + 1) * 180 + 47, 100, 1), Quaternion.identity);
#endif
#if UNITY_EDITOR
                ItemPrefabtmp[nul - 1] = Instantiate(StunGun4, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
#if UNITY_ANDROID
                ItemPrefabtmp[nul - 1] = Instantiate(StunGun4, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
                ItemPrefabtmp[nul - 1].transform.SetParent(this.canvas.transform, false);
            }
            count = 0;
        }
        PanelOff();
        PlayerSC.boxKey = false;
    }

    public void AddFirstAidKit() // 医療キット
    {
        // アイテム未所持をカウントする
        foreach (var n in ItemList)
        {
            if (n == Item.NONE) count++;
        }
        // 未所持枠がある場合
        if (count != 0)
        {
            // アイテムリストの最初に見つけた未所持枠を数値として代入
            int nul = ItemList.IndexOf(Item.NONE) + 1;
            if (nul > 0)
            {
                // 未所持枠を医療キットに変更し、医療キットを生成する
                ItemList[nul - 1] = Item.FIRSTAIDKIT;
#if UNITY_STANDALONE_WIN
                ItemPrefabtmp[nul - 1] = Instantiate(FirstAidKit5, new Vector3((nul + 1) * 180 + 47, 100, 1), Quaternion.identity);
#endif
#if UNITY_EDITOR
                ItemPrefabtmp[nul - 1] = Instantiate(FirstAidKit5, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
#if UNITY_ANDROID
                ItemPrefabtmp[nul - 1] = Instantiate(FirstAidKit5, new Vector3((nul + 1) * 180 - 60, 100, 1), Quaternion.identity);
#endif
                ItemPrefabtmp[nul - 1].transform.SetParent(this.canvas.transform, false);
            }
            count = 0;
        }
        PanelOff();
        PlayerSC.boxKey = false;
    }

    public void AddLantern() // ランタン
    {
        Light0.SetActive(false);
        Lantern6.SetActive(true);
        NightVisionGoggle7.SetActive(false);
        lightswitch = false;
        nightvisiongoggleswitch = false;
        PanelOff();
        PlayerSC.boxKey = false;
        lightCheck = 2;
    }

    public void AddNightVisionGoggle() // 暗視ゴーグル
    {
        Light0.SetActive(false);
        Lantern6.SetActive(false);
        NightVisionGoggle7.SetActive(true);
        lanternswitch = false;
        lightswitch = false;
        PanelOff();
        PlayerSC.boxKey = false;
        lightCheck = 3;
    }

    void PanelOff() // パネルオフ
    {
        switch (savePanelNum)
        {
            case 1:
                CommonPanel.SetActive(false);
                closechestkey();
                break;

            case 2:
                UnCommonPanel.SetActive(false);
                closechestkey();
                break;

            case 3:
                RarePanel.SetActive(false);
                closechestkey();
                break;

            case 4:
                BerryRarePanel.SetActive(false);
                closechestkey();
                break;

        }
    }

    // 鍵表示
    public void chestkey1()
    {
        CommonKey.SetAlpha(1);
    }
    public void chestkey2()
    {
        UnCommonKey.SetAlpha(1);
    }
    public void chestkey3()
    {
        RareKey.SetAlpha(1);
    }
    public void chestkey4()
    {
        BerryRareKey.SetAlpha(1);
    }

    // 鍵非表示
    public void closechestkey()
    {
        CommonKey.SetAlpha(0);
        UnCommonKey.SetAlpha(0);
        RareKey.SetAlpha(0);
        BerryRareKey.SetAlpha(0);
    }

    // カードキー表示
    public void cardkey1()
    {
        CardKey1.SetAlpha(1);
    }
    public void cardkey2()
    {
        CardKey2.SetAlpha(1);
    }
    public void cardkey3()
    {
        CardKey3.SetAlpha(1);
    }
    public void cardkey4()
    {
        CardKey4.SetAlpha(1);
    }

    // カードキー非表示
    public void closecardkey()
    {
        CardKey1.SetAlpha(0);
        CardKey2.SetAlpha(0);
        CardKey3.SetAlpha(0);
        CardKey4.SetAlpha(0);
    }
}

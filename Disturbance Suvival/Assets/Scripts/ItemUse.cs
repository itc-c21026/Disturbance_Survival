using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*----------------------------------
 アイテム使用時のプログラム
----------------------------------*/

public class ItemUse : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Shot ShotCS;
    [SerializeField] Life lifeCS;
    [SerializeField] GetItem ItemCS;

    int LifeMax;

    int keyboard;

    private void Start()
    {
        GameObject obj = GameObject.Find("Player");
        player = obj.GetComponent<PlayerController>();
        ShotCS = obj.GetComponent<Shot>();
        GameObject obj2 = GameObject.Find("GameObject");
        lifeCS = obj2.GetComponent<Life>();
        GameObject obj3 = GameObject.Find("Item");
        ItemCS = obj3.GetComponent<GetItem>();

        LifeMax = PlayerPrefs.GetInt("LifeMax_NUMBER");
    }

    private void Update()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetKeyDown("r"))
        {
            if (ItemCS.lightCheck == 1)
            {
                light0();
            } else if (ItemCS.lightCheck == 2)
            {
                lantern();
            } else if (ItemCS.lightCheck == 3)
            {
                nightvisiongoggle();
            }
        }
        if (Input.GetKeyDown("a"))
        {
            keyboard = 0;
            check1();
        }
        if (Input.GetKeyDown("s"))
        {
            keyboard = 1;
            check2();
        }
        if (Input.GetKeyDown("d"))
        {
            keyboard = 2;
            check3();
        }
        if (Input.GetKeyDown("f"))
        {
            keyboard = 3;
            check4();
        }
#endif
    }

    public void speedspray() // スピードスプレー (移動速度アップ)
    {
        player.UseSpeedUpItem();
#if UNITY_STANDALONE_WIN
        ItemCS.ItemList[keyboard] = GetItem.Item.NONE;
        Destroy(ItemCS.ItemPrefabtmp[keyboard]);
        keyboard = 0;
#endif
#if UNITY_ANDROID
        nl();
        Destroy(this.gameObject);
#endif
    }

    public void bandage() // 包帯 (HP+1)
    {
        if (player.PlayerHP < LifeMax)
        {
            player.PlayerHP++;
            lifeCS.AllClear();
            lifeCS.LifeSet();
#if UNITY_STANDALONE_WIN
            ItemCS.ItemList[keyboard] = GetItem.Item.NONE;
            Destroy(ItemCS.ItemPrefabtmp[keyboard]);
            keyboard = 0;
#endif
#if UNITY_ANDROID
            nl();
            Destroy(this.gameObject);
#endif
        }
    }

    public void stungun() // スタンガン (敵を拘束)
    {
        ShotCS.UseStunGun();
#if UNITY_STANDALONE_WIN
        ItemCS.ItemList[keyboard] = GetItem.Item.NONE;
        Destroy(ItemCS.ItemPrefabtmp[keyboard]);
        keyboard = 0;
#endif
#if UNITY_ANDROID
        nl();
        Destroy(this.gameObject);
#endif
    }

    public void firstaidkit() // 医療キット (HP+2)
    {
        if (player.PlayerHP < LifeMax)
        {
            if (player.PlayerHP < LifeMax - 1) // HPが2以上減っている場合
            {
                player.PlayerHP += 2;
            }
            else if (player.PlayerHP == LifeMax - 1) // HPが1しか減ってない場合
            {
                player.PlayerHP++;
            }
            lifeCS.AllClear();
            lifeCS.LifeSet();
#if UNITY_STANDALONE_WIN
            ItemCS.ItemList[keyboard] = GetItem.Item.NONE;
            Destroy(ItemCS.ItemPrefabtmp[keyboard]);
            keyboard = 0;
#endif
#if UNITY_ANDROID
            nl();
            Destroy(this.gameObject);
#endif
        }
    }

    public void light0() // ライト
    {
        if (ItemCS.lightswitch == false)
        {
            ItemCS.DefaultPanel.SetActive(false);
            ItemCS.LightPanel.SetActive(true);
            ItemCS.LanternPanel.SetActive(false);
            ItemCS.NightVisionGogglePanel.SetActive(false);
            ItemCS.lightswitch = true;
        }
        else if (ItemCS.lightswitch)
        {
            ItemCS.DefaultPanel.SetActive(true);
            ItemCS.LightPanel.SetActive(false);
            ItemCS.LanternPanel.SetActive(false);
            ItemCS.NightVisionGogglePanel.SetActive(false);
            ItemCS.lightswitch = false;
        }
    }

    public void lantern() // ランタン
    {
        if (ItemCS.lanternswitch == false)
        {
            ItemCS.DefaultPanel.SetActive(false);
            ItemCS.LightPanel.SetActive(false);
            ItemCS.LanternPanel.SetActive(true);
            ItemCS.NightVisionGogglePanel.SetActive(false);
            ItemCS.lanternswitch = true;
        }
        else if (ItemCS.lanternswitch)
        {
            ItemCS.DefaultPanel.SetActive(true);
            ItemCS.LightPanel.SetActive(false);
            ItemCS.LanternPanel.SetActive(false);
            ItemCS.NightVisionGogglePanel.SetActive(false);
            ItemCS.lanternswitch = false;
        }
    }

    public void nightvisiongoggle() // 暗視ゴーグル
    {
        if (ItemCS.nightvisiongoggleswitch == false)
        {
            ItemCS.DefaultPanel.SetActive(false);
            ItemCS.LightPanel.SetActive(false);
            ItemCS.LanternPanel.SetActive(false);
            ItemCS.NightVisionGogglePanel.SetActive(true);
            ItemCS.nightvisiongoggleswitch = true;
        }
        else if (ItemCS.nightvisiongoggleswitch)
        {
            ItemCS.DefaultPanel.SetActive(true);
            ItemCS.LightPanel.SetActive(false);
            ItemCS.LanternPanel.SetActive(false);
            ItemCS.NightVisionGogglePanel.SetActive(false);
            ItemCS.nightvisiongoggleswitch = false;
        }
    }

    public void nl() // 消費したアイテムの枠をNONEにする関数
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                Vector3 pos = touch.position;
                if (pos.x >= 220 && pos.x <= 380 && pos.y >= 20 && pos.y <= 180) ItemCS.ItemList[0] = GetItem.Item.NONE;
                if (pos.x >= 400 && pos.x <= 560 && pos.y >= 20 && pos.y <= 180) ItemCS.ItemList[1] = GetItem.Item.NONE;
                if (pos.x >= 580 && pos.x <= 740 && pos.y >= 20 && pos.y <= 180) ItemCS.ItemList[2] = GetItem.Item.NONE;
                if (pos.x >= 760 && pos.x <= 920 && pos.y >= 20 && pos.y <= 180) ItemCS.ItemList[3] = GetItem.Item.NONE;
            }
        }
#endif
    }
    public void check1()
    {
        switch (ItemCS.ItemList[0])
        {
            case GetItem.Item.SPEEDSPRAY:
                speedspray();
                break;
            case GetItem.Item.BANDAGE:
                bandage();
                break;
            case GetItem.Item.STUNGUN:
                stungun();
                break;
            case GetItem.Item.FIRSTAIDKIT:
                firstaidkit();
                break;
        }
    }
    public void check2()
    {
        switch (ItemCS.ItemList[1])
        {
            case GetItem.Item.SPEEDSPRAY:
                speedspray();
                break;
            case GetItem.Item.BANDAGE:
                bandage();
                break;
            case GetItem.Item.STUNGUN:
                stungun();
                break;
            case GetItem.Item.FIRSTAIDKIT:
                firstaidkit();
                break;
        }
    }
    public void check3()
    {
        switch (ItemCS.ItemList[2])
        {
            case GetItem.Item.SPEEDSPRAY:
                speedspray();
                break;
            case GetItem.Item.BANDAGE:
                bandage();
                break;
            case GetItem.Item.STUNGUN:
                stungun();
                break;
            case GetItem.Item.FIRSTAIDKIT:
                firstaidkit();
                break;
        }
    }
    public void check4()
    {
        switch (ItemCS.ItemList[3])
        {
            case GetItem.Item.SPEEDSPRAY:
                speedspray();
                break;
            case GetItem.Item.BANDAGE:
                bandage();
                break;
            case GetItem.Item.STUNGUN:
                stungun();
                break;
            case GetItem.Item.FIRSTAIDKIT:
                firstaidkit();
                break;
        }
    }
}
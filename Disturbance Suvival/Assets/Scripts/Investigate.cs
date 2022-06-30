using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Investigate : MonoBehaviour
{

    public float investigateLimit; //オブジェクトを調べるのにかかる時間
    public float investigateGage; //オブジェクトを調べている時に加算される値

    [SerializeField] private PlayerController PlayerSC;
    public GameObject investigateButton; //[調べる]行為を行うときに押すボタン
    public GameObject investigateSliderObject; //調べている時間を可視化するオブジェクト
    public Slider investigateSlider; //[調べる]行為の間上昇していくスライダー

    public bool investigateCheck; //オブジェクトを調べたかどうか

    bool investigateFlag; //調べている状態にあるかどうか
    bool InvestgatebuttonActiv;　//ボタンを出現させるかどうか
    bool keyButtonActiv; //鍵解除のボタンを出現させているかどうか

    //[特殊]鍵を持っている時のみ呼び出される
    public GameObject liftKeyButton;
    public bool liftKeyCheck;
    bool liftKeyFlag; //鍵を解錠している状態にあるかどうか

    // Start is called before the first frame update
    void Start()
    {
        investigateSlider.value = 0;
        investigateFlag = false;
        InvestgatebuttonActiv = false;
        keyButtonActiv = false;

        investigateButton.SetActive(false);
        investigateSliderObject.SetActive(false);

        investigateCheck = false;

        liftKeyButton.SetActive(false);
        liftKeyFlag = false;
        liftKeyCheck = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(investigateGage);
        if (investigateFlag)
        {
            InvestigatePlus();
        }
        else if (liftKeyFlag)
        {
            liftKeyPlus();
        }
        else
        {
            InvestigateReset();
        }

        if (investigateCheck)
        {
            PlayerSC.investigateCompletion();
            investigateCheck = false;
        }else if (liftKeyCheck)
        {
            PlayerSC.liftKeyCompletion();
            liftKeyCheck = false;

        }


    }

    public void InvestigateButtonDown()
    {
        investigateFlag = true;
        PlayerSC.ButtonOnCheck = false;
    }

    public void InvestigateButtonUp()
    {
        investigateFlag = false;
        PlayerSC.ButtonOnCheck = true;
    }

    void InvestigatePlus()
    {
        if (investigateGage <= investigateLimit)
        {
            investigateGage += Mathf.Clamp(Time.deltaTime, 0, investigateLimit);
        }

        investigateSliderObject.SetActive(true);
        investigateSlider.value = investigateGage / investigateLimit;

        if(investigateGage >= investigateLimit)
        {
            investigateCheck = true;
            investigateGage = 0;
            investigateFlag = false;
            PlayerSC.ButtonOnCheck = true;
        }
    }

    public void InvestigateReset()
    {
        investigateGage = 0;
        investigateSliderObject.SetActive(false);
    }

    public void InvestigateButtonOn()
    {
        if (!InvestgatebuttonActiv)
        {
            investigateButton.SetActive(true);
            InvestgatebuttonActiv = true;
        }
    }

    public void InvestigateButtonOff()
    {
        if (InvestgatebuttonActiv)
        {
            investigateButton.SetActive(false);
            InvestgatebuttonActiv = false;
        }
    }

    /// <summary>
    /// 鍵を持っている時の動作
    /// /// </summary>
    public void liftKeyButtonDown()
    {
        liftKeyFlag = true;
        PlayerSC.ButtonOnCheck = false;
    }

    public void liftKeyButtonUp()
    {
        liftKeyFlag = false;
        PlayerSC.ButtonOnCheck = true;
    }

    void liftKeyPlus()
    {
        if (investigateGage <= investigateLimit)
        {
            investigateGage += Mathf.Clamp(Time.deltaTime, 0, investigateLimit);
        }

        investigateSliderObject.SetActive(true);
        investigateSlider.value = investigateGage / investigateLimit;

        if (investigateGage >= investigateLimit)
        {
            liftKeyCheck = true;
            investigateGage = 0;
            liftKeyFlag = false;
            PlayerSC.ButtonOnCheck = true;
        }
    }

    public void liftKeyButtonOn()
    {
        if (!keyButtonActiv)
        {
            liftKeyButton.SetActive(true);
            keyButtonActiv = true;
        }
    }

    public void liftKeyButtonOff()
    {
        if (keyButtonActiv)
        {
            liftKeyButton.SetActive(false);
            keyButtonActiv = false;
        }
    }
}

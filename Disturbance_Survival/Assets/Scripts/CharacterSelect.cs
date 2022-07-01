using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*-------------------------------------------
 キャラ選択画面のプログラム
-------------------------------------------*/

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    Text Starttext;
    
    // キャラを選ぶImageの配列
    [SerializeField]
    CanvasRenderer[] CRs;

    // 画面中央に表示されるキャラ
    [SerializeField]
    CanvasRenderer[] charaCRs;

    [SerializeField , Label("HPテキスト")] Text HPtext;
    [SerializeField , Label("移動速度テキスト")] Text Speedtext;
    [SerializeField , Label("インベントリテキスト")] Text Inventorytext;

    [HideInInspector] public int num;
    private void Start()
    {
        // 初期化
        CRs[0].SetAlpha(0);
        CRs[1].SetAlpha(0);
        CRs[2].SetAlpha(0);
        CRs[3].SetAlpha(0);
        charaCRs[0].SetAlpha(0);
        charaCRs[1].SetAlpha(0);
        charaCRs[2].SetAlpha(0);
        charaCRs[3].SetAlpha(0);

        Starttext.text = "キャラを選べ";
    }

    private void Update()
    {
        // 選んだキャラに応じて変更
        switch (num)
        {
            case 1:
                CRs[0].SetAlpha(1);
                CRs[1].SetAlpha(0);
                CRs[2].SetAlpha(0);
                CRs[3].SetAlpha(0);
                charaCRs[0].SetAlpha(1);
                charaCRs[1].SetAlpha(0);
                charaCRs[2].SetAlpha(0);
                charaCRs[3].SetAlpha(0);
                HPtext.text = "HP:♡x5";
                Speedtext.text = "移動速度:標準";
                Inventorytext.text = "インベントリ:4";
                break;

            case 2:
                CRs[0].SetAlpha(0);
                CRs[1].SetAlpha(1);
                CRs[2].SetAlpha(0);
                CRs[3].SetAlpha(0);
                charaCRs[0].SetAlpha(0);
                charaCRs[1].SetAlpha(1);
                charaCRs[2].SetAlpha(0);
                charaCRs[3].SetAlpha(0);
                HPtext.text = "HP:♡x4";
                Speedtext.text = "移動速度:速い";
                Inventorytext.text = "インベントリ:3";
                break;

            case 3:
                CRs[0].SetAlpha(0);
                CRs[1].SetAlpha(0);
                CRs[2].SetAlpha(1);
                CRs[3].SetAlpha(0);
                charaCRs[0].SetAlpha(0);
                charaCRs[1].SetAlpha(0);
                charaCRs[2].SetAlpha(1);
                charaCRs[3].SetAlpha(0);
                HPtext.text = "HP:♡x6";
                Speedtext.text = "移動速度:少し遅い";
                Inventorytext.text = "インベントリ:3";
                break;

            case 4:
                CRs[0].SetAlpha(0);
                CRs[1].SetAlpha(0);
                CRs[2].SetAlpha(0);
                CRs[3].SetAlpha(1);
                charaCRs[0].SetAlpha(0);
                charaCRs[1].SetAlpha(0);
                charaCRs[2].SetAlpha(0);
                charaCRs[3].SetAlpha(1);
                HPtext.text = "HP:♡x4";
                Speedtext.text = "移動速度:少し速い";
                Inventorytext.text = "インベントリ:5";
                break;

            default:
                break;
        }

        if (num != 0) Starttext.text = "準備完了";
    }
    public void OnClickStart()
    {
        // キャラを選んだ状態だとボタンを押せる
        if (num != 0)
        {
            PlayerPrefs.SetInt("CHARA_NUMBER", num);
            SceneManager.LoadScene("SampleScene2");
        }
    }
}

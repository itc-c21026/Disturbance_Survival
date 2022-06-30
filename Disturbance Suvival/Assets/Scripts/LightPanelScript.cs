using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-----------------------------------
 明るさ調整のパネル関数
-----------------------------------*/

public class LightPanelScript : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject panel2;
    [SerializeField] GameObject panel3;
    [SerializeField] GameObject panel4;

    public void NoLight()
    {
        panel.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
    }
    public void Light1() // デフォルト
    {
        panel.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
    }
    public void Light2() // ライター
    {
        panel.SetActive(false);
        panel2.SetActive(true);
        panel3.SetActive(false);
        panel4.SetActive(false);
    }
    public void Light3() // ランタン
    {
        panel.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);
        panel4.SetActive(false);
    }
    public void Light4() // 暗視ゴーグル
    {
        panel.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(true);
    }
}

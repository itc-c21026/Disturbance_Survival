using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*-------------------------------------
 * 体力(HP)のプログラム
 ------------------------------------*/

public class Life : MonoBehaviour
{
    [SerializeField]
    CanvasRenderer[] LifeCRs;

    [SerializeField]
    CanvasRenderer[] lifebackCRs;

    [SerializeField]
    PlayerController script;

    private void Update()
    {
        if (script.PlayerHP == 0) SceneManager.LoadScene("LoseScene");
    }

    public void LifeSet() // 現在のHPを表示させる
    {
        for(int i = 0; i < script.PlayerHP; i++)
        {
            LifeCRs[i].SetAlpha(1);
        }
    }

    public void AllClear() // ライフ全て非表示
    {
        foreach (CanvasRenderer nowCR in LifeCRs)
        {
            nowCR.SetAlpha(0);
        }
    }

    public void LifeSetB() // HPのBackImageを表示
    {
        for (int i = 0; i < script.PlayerHP; i++)
        {
            lifebackCRs[i].SetAlpha(1);
        }
    }

    public void AllClearB() // HPのBackImageを全て非表示
    {
        foreach (CanvasRenderer CRs in lifebackCRs)
        {
            CRs.SetAlpha(0);
        }
    }
}

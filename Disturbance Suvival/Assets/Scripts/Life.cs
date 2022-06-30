using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*-------------------------------------
 * �̗�(HP)�̃v���O����
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

    public void LifeSet() // ���݂�HP��\��������
    {
        for(int i = 0; i < script.PlayerHP; i++)
        {
            LifeCRs[i].SetAlpha(1);
        }
    }

    public void AllClear() // ���C�t�S�Ĕ�\��
    {
        foreach (CanvasRenderer nowCR in LifeCRs)
        {
            nowCR.SetAlpha(0);
        }
    }

    public void LifeSetB() // HP��BackImage��\��
    {
        for (int i = 0; i < script.PlayerHP; i++)
        {
            lifebackCRs[i].SetAlpha(1);
        }
    }

    public void AllClearB() // HP��BackImage��S�Ĕ�\��
    {
        foreach (CanvasRenderer CRs in lifebackCRs)
        {
            CRs.SetAlpha(0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-----------------------------------
 ���邳�����̃p�l���֐�
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
    public void Light1() // �f�t�H���g
    {
        panel.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
    }
    public void Light2() // ���C�^�[
    {
        panel.SetActive(false);
        panel2.SetActive(true);
        panel3.SetActive(false);
        panel4.SetActive(false);
    }
    public void Light3() // �����^��
    {
        panel.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);
        panel4.SetActive(false);
    }
    public void Light4() // �Î��S�[�O��
    {
        panel.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(true);
    }
}

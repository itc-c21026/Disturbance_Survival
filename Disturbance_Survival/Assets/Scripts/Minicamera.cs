using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-----------------------------------------
 �v���C���[�ɃJ������Ǐ]������v���O����
 ���[�_�[�̂��߂Ɏg�p���Ă���
-----------------------------------------*/

public class Minicamera : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    // Update is called once per frame
    void Update()
    {
        // y���W
        var pos = player.transform.position;
        pos.y = transform.position.y;
        pos.z = -10;
        transform.position = pos;
        // x���W
        var pos2 = player.transform.position;
        pos2.x = transform.position.x;
        pos2.z = -10;
        transform.position = pos2;
    }
}

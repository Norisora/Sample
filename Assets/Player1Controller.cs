using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player1Controller : PlayerBaseController
{
    //1P�X�N���v�g

    protected override int Move(int key)
    {
        int moveValue = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveValue = 1;
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveValue = -1;
        }
        return moveValue;
        //�M�b�g�e�X�g
    }
}

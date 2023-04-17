using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2Controller : PlayerBaseController
{

    protected override int Move(int key)
    {
        int moveValue = 0;
        if (Input.GetKey(KeyCode.C))
        {
            moveValue = 1;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            moveValue = -1;
        }
        return moveValue;
    }
}

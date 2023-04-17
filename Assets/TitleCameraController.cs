using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraController : MonoBehaviour
{
    //２人のプレイヤーに合わせてカメラSize変更する
    GameObject player;

    void Start()
    {
        this.player = GameObject.Find("cat");
    }

    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(
            transform.position.x, playerPos.y, transform.position.z);
    }

}

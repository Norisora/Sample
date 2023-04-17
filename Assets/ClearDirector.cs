using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearDirector : MonoBehaviour
{
    SceneController sceneController;
    private void Start()
    {
        this.sceneController = GameObject.Find("SceneController").GetComponent<SceneController>(); 
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.sceneController.ChangeScene("TitleScene");
        }
    }
}

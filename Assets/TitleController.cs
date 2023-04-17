using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    Button buttonGameStart;

    SceneController sceneController;
    // Start is called before the first frame update
    void Start()
    {
        this.sceneController = GameObject.Find("SceneController").GetComponent<SceneController>(); ;
        this.buttonGameStart.onClick.AddListener(() => this.sceneController.ChangeScene("GameScene"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

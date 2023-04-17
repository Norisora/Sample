using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance
    {
        
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(SceneController)) as SceneController;
                Debug.Log(instance);
            }
            return instance;
        }
    }
    static SceneController instance = null;

    [SerializeField]
    TitleAnimation titleAnimation;
    
    string targetSceneName;

    private void Awake()
    {
        if (this != Instance) 
        {
            Destroy(this);
            return;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScene(string sceneName)
    {
        targetSceneName = sceneName;
        titleAnimation.GameStartAnimation(AnimationEnd);

        Debug.Log(sceneName);
    }
    //public void ChangeToClearScene(string sceneName)
    //{
    //    targetSceneName = sceneName;
    //    titleAnimation.GameStartAnimation(AnimationEnd);

    //    Debug.Log(sceneName);
    //}

    public void AnimationEnd()
    {
        SceneManager.LoadScene(targetSceneName);
    }
   
}

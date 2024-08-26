using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuStarGame : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject loadingUI;
    static string sceneName = "GameScene";
    void Awake()
    {
        this.button.onClick.AddListener(() => { SceneLoadManager.instance.LoadScene_Async(this.loadingUI, sceneName); });
    }


}

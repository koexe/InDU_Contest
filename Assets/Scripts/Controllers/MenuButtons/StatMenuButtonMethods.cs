using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatMenuButtonMethods : MonoBehaviour
{
    [Header("���۹�ư �޴�")]
    [SerializeField] Button button;
    [SerializeField] GameObject loadingUI;

    [Header("�ɼ� ���� �޴�")]
    [SerializeField] GameObject optionPrefab;


    const string sceneName = "GameScene";
    const string optionUI = "Option";
    void Awake()
    {
        this.button.onClick.AddListener(() => { SceneLoadManager.instance.LoadScene_Async(this.loadingUI, sceneName); });
        return;
    }

    public void ShowOptionUI()
    {
        UIManager.instance.ShowUI(this.optionPrefab,optionUI);
        return;
    }
}

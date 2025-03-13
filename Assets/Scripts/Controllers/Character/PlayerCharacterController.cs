using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [Header("플레이어 스텟")]
    [SerializeField] float moveSpeed;
    [SerializeField] int maxHP;
    [SerializeField] int currentHP;




    void SettingKeyboard()
    {
        IngameInputManager.instance.AddKeyboardAction_Down(KeyCode.I, () => ShowInventroy());
        return;
    }


    [Header("인벤토리 프리팹")]
    [SerializeField] GameObject inventoryPrefab;
    const string inventoryUIName = "Inventory";


    [Header("피격 소리")]
    [SerializeField] AudioClip hitAudio;

    private void Start()
    {
        Initialization();
        return;
    }
    public void Initialization()
    {
        SettingKeyboard();
        this.currentHP = this.maxHP;
        return;
    }



    void ShowInventroy()
    {
        UIManager.instance.ShowUI(this.inventoryPrefab, inventoryUIName);
    }


} 
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButtonController : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public TextMeshProUGUI button1Text;
    public TextMeshProUGUI button2Text;
    public TextMeshProUGUI button3Text;

    [SerializeField]TextUIManager textUIManager;

    void Initialization()
    {
       
    }

    public void ChangeChoiceText(Dialog dialog)
    {
      
        this.button1Text.text = dialog.Choice1[0];
        this.button2Text.text = dialog.Choice2[0];
        this.button3Text.text = dialog.Choice3[0];
        return;
    }
}

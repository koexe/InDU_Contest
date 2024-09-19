using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
 
public class StoryStopWall : MonoBehaviour
{
    [SerializeField] int dialogIndex;
    [SerializeField] int cantMoveDialogIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (SaveGameManager.instance.currentSaveData.chatacterDialogs[this.dialogIndex] == true)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            UIManager.instance.ShowUI("DialogUI", -1, this.cantMoveDialogIndex.ToString());
        }
    }
}

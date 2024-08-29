using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameInventoryManager : MonoBehaviour
{
    public static IngameInventoryManager instance;

    private void Awake()
    {
        instance = this;
        return;
    }

    void Initialization()
    {

    }
}

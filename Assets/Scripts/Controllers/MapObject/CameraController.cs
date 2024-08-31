using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform targetTransfrom;
    [SerializeField] float currentMoveRatio;

    private void FixedUpdate()
    {
        if (this.targetTransfrom.position != this.transform.position)
        {

            Vector3 t_pos = Vector3.zero;
            t_pos = Vector2.Lerp(this.transform.position, this.targetTransfrom.position, this.currentMoveRatio);
            t_pos.z = -10f;
            this.transform.position = t_pos;


        }
    }
}

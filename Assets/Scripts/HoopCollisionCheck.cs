using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopCollisionCheck : MonoBehaviour {

    public int index;

    void OnTriggerEnter(Collider other)
    {
        if(!other.isTrigger)
        {
            gameObject.SendMessageUpwards("SetCheckFlag", index);
        }
    }

}

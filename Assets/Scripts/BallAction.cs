using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BallAction : VRTK.VRTK_InteractableObject {


    FlyToLocation flyToLoc = null;
    public override void StartUsing(GameObject currentUsingObject)
    {
        Debug.Log("Using");
        base.StartUsing(currentUsingObject);

        if(flyToLoc == null)
            flyToLoc = GetComponent<FlyToLocation>();

        flyToLoc.FlyToTransform(currentUsingObject.GetComponent<Transform>());
    }

}

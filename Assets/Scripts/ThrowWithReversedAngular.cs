using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ThrowWithReversedAngular : VRTK.GrabAttachMechanics.VRTK_FixedJointGrabAttach {

    protected override void Initialise()
    {
        
    }

    protected override void ReleaseObject(bool applyGrabbingObjectVelocity)
    {
        Rigidbody releasedObjectRigidBody = ReleaseFromController(applyGrabbingObjectVelocity);
        if (releasedObjectRigidBody && applyGrabbingObjectVelocity)
        {
            ThrowReleasedObject(releasedObjectRigidBody);
        }
    }

    private void ThrowReleasedObject(Rigidbody objectRigidbody)
    {
        
        if (grabbedObjectScript)
        {
            var grabbingObject = grabbedObjectScript.GetGrabbingObject();
            if (grabbingObject)
            {
                var grabbingObjectScript = grabbingObject.GetComponent<VRTK_InteractGrab>();
                var controllerEvents = grabbingObject.GetComponent<VRTK_ControllerEvents>();

                var grabbingObjectThrowMultiplier = grabbingObjectScript.throwMultiplier;

                var origin = VRTK_DeviceFinder.GetControllerOrigin(grabbingObject);

                var velocity = (controllerEvents ? controllerEvents.GetVelocity() : Vector3.zero);
                var angularVelocity = (controllerEvents ? controllerEvents.GetAngularVelocity() : Vector3.zero);


                if (origin != null)
                {
                    objectRigidbody.velocity = origin.TransformVector(velocity) * (grabbingObjectThrowMultiplier * throwMultiplier);
                    objectRigidbody.angularVelocity = origin.TransformDirection(angularVelocity * -1);
                    
                }
                else
                {
                    objectRigidbody.velocity = velocity * (grabbingObjectThrowMultiplier * throwMultiplier);
                    objectRigidbody.angularVelocity = angularVelocity * -1;
                } 

            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ThrowWithReversedAngular : VRTK.GrabAttachMechanics.VRTK_FixedJointGrabAttach {

    public float shotAssistForce = 10.0f;
    public Transform hoop1, hoop2;

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

                Vector3 targetPos;                

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

                if (Vector3.Dot(transform.position - hoop1.position, objectRigidbody.velocity) < 0)
                {
                    targetPos = hoop1.position;
                }
                else
                {
                    targetPos = hoop2.position;
                }

                // Target height for a shot is 15ft(4.572m)
                float deltaY = 4.572f - transform.position.y;
                // solving for y velocity
                // deltaY = v_i*t + 1/2*a*t^2
                // where t = (-v_i/a) and a = -9.8m/s^2 
                float yVelocity = (float)Math.Sqrt(Convert.ToDouble(deltaY / (1.0f / 9.8f + (Math.Pow(1.0/9.8, 2) * -9.8f/2.0f))));
                float t1 = yVelocity / 9.8f;
                // Solve for how long the ball is in flight, 3.048 = 10ft in meters
                float finalVelocity = (float)Math.Sqrt(3.00*2.0*9.8)*-1.0f;
                float totalTime = t1 + finalVelocity / -9.8f;
                // Now calculate the horizontal velocity
                float horizontalDist = (float)Math.Sqrt(Math.Pow(objectRigidbody.transform.position.z - targetPos.z, 2) + Math.Pow(objectRigidbody.transform.position.x - targetPos.x, 2));
                
                float horizontalVelocity = horizontalDist / totalTime;
                float angle = (float)Math.Atan((double)((objectRigidbody.transform.position.z - targetPos.z) / (objectRigidbody.transform.position.x - targetPos.x)));
                float zVelocity = (float)Math.Sin(angle) * horizontalVelocity;
                float xVelocity = (float)Math.Cos(angle) * horizontalVelocity;
                
                objectRigidbody.velocity = new Vector3(xVelocity, yVelocity, zVelocity);

            }
        }
    }
}

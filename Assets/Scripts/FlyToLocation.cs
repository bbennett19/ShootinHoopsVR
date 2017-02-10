using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToLocation : MonoBehaviour {

    private bool active = false;
    private Transform dest;
    private float speed = 10.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            float step = speed * Time.fixedDeltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, dest.transform.position, step);

            if (gameObject.transform.position.Equals(dest.transform.position))
            {
                active = false;
            }
        }
    }

    public void FlyToTransform(Transform transform)
    {
        Debug.Log("Fly");
        if (!active)
        {
            dest = transform;
            active = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}

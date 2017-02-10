using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCheck : MonoBehaviour {

    private bool makeCheck1 = false;
    private float timeout = 4.0f;
    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (makeCheck1)
        {
            timer += Time.fixedDeltaTime;

            if (timer > timeout)
            {
                makeCheck1 = false;
                timer = 0.0f;
            }
        }
	}

    public void SetCheckFlag(int index)
    {
        Debug.Log("SetCheck");
        if (index == 1)
        {
            makeCheck1 = true;
        }
        else if(index == 2 && makeCheck1)
        {
            Debug.Log("Make");
        }
    }

    
}

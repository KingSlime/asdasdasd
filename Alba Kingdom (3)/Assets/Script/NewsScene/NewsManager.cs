using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour {
    public GameObject Star;

    private float Star_Time;
    public float Star_Amount;

    private Vector3 V_Left;
    private Vector3 V_Right;

    private bool direction; //false L2R true R2L
	void Start ()
    {
        direction = false;

        Star_Time = 0.0f;
        Star_Amount = 0.5f;

        V_Left = new Vector3(-180, 0);
        V_Right = new Vector3(180, 0);
	}	

	void Update ()
    {
        if (direction)
        {
            Star_Time = Star_Time - Time.deltaTime * Star_Amount;
            if (Star_Time < 0.0f)
                direction = false;            
        }
        else
        {
            Star_Time = Star_Time + Time.deltaTime * Star_Amount;
            if (Star_Time > 1.0f)
                direction = true;            
        }


        Vector3 Vctr = ((1 - Star_Time) * V_Left + Star_Time * V_Right);
        Star.transform.localPosition = Vctr;
    }

    public void Touch()
    {
        if(Star.transform.localPosition.x >= 5)
        {

        }
    }
}

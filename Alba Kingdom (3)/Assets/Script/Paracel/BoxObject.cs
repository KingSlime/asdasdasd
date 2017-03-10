using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : MonoBehaviour {
    private Vector3 Center;
    private Vector3[] BoxVector = new Vector3[3];

    public int BoxType;
    public int BoxIndex;

    public float BoxTime;

    public bool isBoxMove;

    void Start ()
    {
        isBoxMove = false;
        Center = new Vector3(0, 78);
        BoxVector[0] = new Vector3(-410, 78);
        BoxVector[1] = new Vector3(0, 625);
        BoxVector[2] = new Vector3(410, 78);
    }

	void Update ()
    {
		if(isBoxMove)
        {
            if (BoxTime < 1.0f)
            {
                BoxTime = BoxTime + Time.deltaTime * 1.5f;
            }

            Vector3 Vctr = ((1 - BoxTime) * Center + BoxTime * BoxVector[BoxType]);
            transform.localPosition = Vctr;    
            
            if(BoxTime > 1.0f)
            {
                isBoxMove = false;
                BoxTime = 0.0f;
            }        
        }
	}
}

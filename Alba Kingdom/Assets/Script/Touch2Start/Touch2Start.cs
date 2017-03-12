using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch2Start : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(540, 960, false);

        DataManager.instance.Ham_HighScore = 0;
        DataManager.instance.Parcel_HighScore = 0;
        DataManager.instance.Quick_HighScore = 0;
        DataManager.instance.Gas_HighScore = 0;
        DataManager.instance.Cvs_HighScore = 0;


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using UnityEngine;
using System.Collections.Generic;
using AcquisLib;

public class DataManager
    : AcSingleton<DataManager>
{
    public int Money;

    public int Ham_HighScore;
    public int Parcel_HighScore;
    public int Quick_HighScore;
    public int Gas_HighScore;
    public int Cvs_HighScore;

    public int TimeBuff = 60;
    public int DailyWageBuff =0;

    public bool Chair_Status = false;
    public bool Table_Status = false;
    public bool Sofa_Status = false;
    public bool Bad_Status = false;
    public bool Wardrobe_Status = false;
    public bool Tv_Status = false;


    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

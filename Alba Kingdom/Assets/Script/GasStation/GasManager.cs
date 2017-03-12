using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GasManager : MonoBehaviour {
    public int Score;
    public UILabel Score_Label;

    public GameObject GameOverPopup;
    public UILabel GameOverLabel;
    public bool GameBool;

    private float f_GameTime;
    public UILabel LBL_Time;
    public UISlider OBJ_TimeBar;

    void Start ()
    {
        GameBool = true;
        f_GameTime = DataManager.instance.TimeBuff;		
	}
	
	void Update ()
    {
        if (GameBool)
        {
            Score_Label.text = "일당 " + Score.ToString() + "원";

            f_GameTime -= Time.deltaTime;
            OBJ_TimeBar.value = 1.0f - f_GameTime / DataManager.instance.TimeBuff;
            int intTime = (int)f_GameTime;
            LBL_Time.text = intTime.ToString();

            if (f_GameTime <= 0.0f)
            {
                GameOver();
                GameBool = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameOver();
                GameBool = false;
            }

        }
    }

    public void GameOver()
    {

        Score += (Score / 100) * DataManager.instance.DailyWageBuff;

        GameOverLabel.text = "일당 " + Score.ToString() + "원";
        if (DataManager.instance.Gas_HighScore < Score)
        {
            DataManager.instance.Gas_HighScore = Score;
        }
        DataManager.instance.Money += Score;
        GameOverPopup.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DarkFlameMaster : MonoBehaviour
{

    public GameObject OXPopUp;
    public GameObject GameOverPopup;
    public UILabel TotalLabel;
    public UILabel PaymentMoneyLabel;
    public UILabel InPutNumberLabel;
    public UILabel ScoreLabel;
    public UILabel TimeLabel;
    public UILabel GameOverLabel;
    public UISlider TimeBar;
    public AudioClip S;
    public AudioClip F;
    public Texture[] OX = new Texture[2];

    private float DeltaTime = DataManager.instance.TimeBuff;
    private int Limit = 100;
    private int Score = 0;
    private bool Gamebool = true;

    void Start()
    {
        ScoreLabel.text = "일당 000원";
        Setting();
    }

    void Update()
    {
        if (Gamebool)
        {
            DeltaTime -= Time.deltaTime;
            TimeBar.value = 1.0f - DeltaTime / DataManager.instance.TimeBuff;
            int intTime = (int)DeltaTime;
            TimeLabel.text = intTime.ToString();

            if (DeltaTime <= 0.0f)
            {
                Gamebool = false;
                Score += (Score / 100) * DataManager.instance.DailyWageBuff;
                GameOverLabel.text = "일당 " + Score.ToString() + "원";
                if (DataManager.instance.Cvs_HighScore < Score)
                {
                    DataManager.instance.Cvs_HighScore = Score;
                }
                DataManager.instance.Money += Score;
                GameOverPopup.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Gamebool = false;
                Score += (Score / 100) * DataManager.instance.DailyWageBuff;
                GameOverLabel.text = "일당 " + Score.ToString() + "원";
                if (DataManager.instance.Cvs_HighScore < Score)
                {
                    DataManager.instance.Cvs_HighScore = Score;
                }
                DataManager.instance.Money += Score;
                GameOverPopup.SetActive(true);
            }
        }
    }

    private void Setting()
    {
        TotalLabel.text = System.Convert.ToString(Random.Range(1, Limit)) + "00";
        PaymentMoneyLabel.text = System.Convert.ToString(Random.Range(System.Convert.ToInt32(TotalLabel.text.Substring(0, TotalLabel.text.Length - 2)), Limit)) + "00";
        InPutNumberLabel.text = "";
    }

    private void Check(bool SoF)
    {
        Debug.Log(SoF);
        if (SoF)
        {
            AudioSource.PlayClipAtPoint(S, transform.position);
            OXPopUp.GetComponent<UITexture>().mainTexture = OX[0];
            OXPopUp.SetActive(true);
            Invoke("PopUpDelet", 0.2f);
            Score += 300;
            ScoreLabel.text = "일당" + Score + "원";
            if (Score % 500 == 0 && Limit != 10000)
            {
                Limit *= 10;
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(F, transform.position);
            OXPopUp.GetComponent<UITexture>().mainTexture = OX[1];
            OXPopUp.SetActive(true);
            Invoke("PopUpDelet", 0.2f);
        }

        Setting();
    }

    private void PopUpDelet()
    {
        OXPopUp.SetActive(false);
    }

    public void Click0() { InPutNumberLabel.text = InPutNumberLabel.text + "0"; }
    public void Click1() { InPutNumberLabel.text = InPutNumberLabel.text + "1"; }
    public void Click2() { InPutNumberLabel.text = InPutNumberLabel.text + "2"; }
    public void Click3() { InPutNumberLabel.text = InPutNumberLabel.text + "3"; }
    public void Click4() { InPutNumberLabel.text = InPutNumberLabel.text + "4"; }
    public void Click5() { InPutNumberLabel.text = InPutNumberLabel.text + "5"; }
    public void Click6() { InPutNumberLabel.text = InPutNumberLabel.text + "6"; }
    public void Click7() { InPutNumberLabel.text = InPutNumberLabel.text + "7"; }
    public void Click8() { InPutNumberLabel.text = InPutNumberLabel.text + "8"; }
    public void Click9() { InPutNumberLabel.text = InPutNumberLabel.text + "9"; }
    public void ClickEnter() { InPutNumberLabel.text = InPutNumberLabel.text + "00"; }
    public void ClickBackSpace()
    {
        if (InPutNumberLabel.text.Length > 0)
            InPutNumberLabel.text = InPutNumberLabel.text.Substring(0, InPutNumberLabel.text.Length - 1);
    }
    public void ClickSelect()
    {
        if (InPutNumberLabel.text.Length > 0)
            Check(System.Convert.ToInt32(InPutNumberLabel.text) == System.Convert.ToInt32(PaymentMoneyLabel.text) - System.Convert.ToInt32(TotalLabel.text));
    }

}

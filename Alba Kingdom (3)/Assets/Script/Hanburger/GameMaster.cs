using UnityEngine;
using System.Collections;
using System;

public class GameMaster : MonoBehaviour
{
    public GameObject[] ExampleHamberg = new GameObject[5];
    public GameObject[] MakeHamHamberg = new GameObject[5];
    public GameObject[] TouchObject = new GameObject[6];
    public GameObject GameOverPopup;
    public GameObject SuccessOrFailurePopup;
    public GameObject Qkd;
    public UILabel ScoreLabel;
    public UILabel Score_L;
    public UILabel TimeLabel;
    public UISlider TimeGage;
    public UITexture[] ExampleHamberg_t = new UITexture[5];
    public UITexture[] MakeHamHamberg_t = new UITexture[5];
    public UITexture SuccessOrFailurePopup_t;
    public Texture[] Ingredient_t = new Texture[6];
    public Texture[] SuccessOrFailureTexture = new Texture[2];
    public AudioClip S;
    public AudioClip F;
    private bool GameBool;

    private int Level = 3;
    private int floor = 0;
    private int Score = 0;
    private float Time_f = DataManager.instance.TimeBuff;
    private bool IsLevelUp = false;

    void Start()
    {
        GameBool = true;
        SettingTouchUI();
        SettingExample();
    }
    void Update()
    {
        if (GameBool)
        {
            Time_f -= Time.deltaTime;
            TimeGage.value = 1.0f - Time_f / DataManager.instance.TimeBuff;
            int intTime = (int)Time_f;
            TimeLabel.text = intTime.ToString();

            if (Time_f <= 0.0f)
            {
                Gameover();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Gameover();
            }
        }
    }

    public void SettingTouchUI()
    {
        for (int i = 0; i < Level; i++)
            TouchObject[i].SetActive(true);
        if (Level == 3)
        {
            for (int i = 0; i < Level; i++)
            {
                TouchObject[i].transform.localPosition = new Vector3(-250 + i * 250, 180, 0);
            }
        }
        else if (Level == 4)
        {
            TouchObject[0].transform.localPosition = new Vector3(-170, 100, 0);
            TouchObject[1].transform.localPosition = new Vector3(170, 100, 0);
            TouchObject[2].transform.localPosition = new Vector3(-170, 250, 0);
            TouchObject[3].transform.localPosition = new Vector3(170, 250, 0);
        }
        else if (Level == 5)
        {
            for (int i = 0; i < Level; i++)
            {
                TouchObject[i].transform.localPosition = new Vector3(-250 + i * 250, 100, 0);
            }
            TouchObject[3].transform.localPosition = new Vector3(-170, 250, 0);
            TouchObject[4].transform.localPosition = new Vector3(170, 250, 0);
        }
        else if (Level == 6)
        {
            for (int i = 0; i < 3; i++)
            {
                TouchObject[i].transform.localPosition = new Vector3(-250 + i * 250, 100, 0);
                TouchObject[i + 3].transform.localPosition = new Vector3(-250 + i * 250, 250, 0);
            }
        }
    }
    public void SettingExample()
    {
        for (int i = 0; i < 5; i++)
        {
            ExampleHamberg_t[i].mainTexture = Ingredient_t[UnityEngine.Random.Range(0, Level)];
            ExampleHamberg[i].SetActive(true);
        }
    }
    public void GetTouchObject(int i)
    {
        MakeHamHamberg_t[floor].mainTexture = Ingredient_t[i];
        MakeHamHamberg[floor].SetActive(true);
        SuccessOrFailure(ExampleHamberg_t[floor].mainTexture.name == MakeHamHamberg_t[floor].mainTexture.name);
    }
    public void SuccessOrFailure(bool IsSuccess)
    {
        if (IsSuccess)
        {
            floor++;
            if (floor == 5)
            {
                AudioSource.PlayClipAtPoint(S, transform.position);
                Score += 200;
                Score_L.text = "일당" + Score + "원";
                SuccessOrFailurePopup_t.mainTexture = SuccessOrFailureTexture[0];
                SuccessOrFailurePopup.SetActive(true);
                Qkd.SetActive(true);
                Invoke("PopupDelete", 0.2f);
                if (Score % 500 == 0 && Level < 6)
                {
                    IsLevelUp = true;
                }
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(F, transform.position);
            SuccessOrFailurePopup_t.mainTexture = SuccessOrFailureTexture[1];
            SuccessOrFailurePopup.SetActive(true);
            Invoke("PopupDelete", 0.2f);
        }
    }
    public void PopupDelete()
    {
        SuccessOrFailurePopup.SetActive(false);
        ReSetting();
    }
    public void ReSetting()
    {
        if (IsLevelUp)
        {
            Level++;
            SettingTouchUI();
            IsLevelUp = false;
        }
        floor = 0;
        SettingExample();
        for (int i = 0; i < 5; i++)
        {
            MakeHamHamberg[i].SetActive(false);
        }
        Qkd.SetActive(false);
    }
    public void Gameover()
    {
        GameBool = false;
        Score += (Score / 100) * DataManager.instance.DailyWageBuff;
        ScoreLabel.text = "일당 " + Score.ToString() + "원";
        if (DataManager.instance.Cvs_HighScore < Score)
        {
            DataManager.instance.Cvs_HighScore = Score;
        }
        DataManager.instance.Money += Score;
        GameOverPopup.SetActive(true);
    }
}
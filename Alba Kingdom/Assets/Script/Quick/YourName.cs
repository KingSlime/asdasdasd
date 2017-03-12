using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourName : MonoBehaviour
{
    public GameObject BackGround;
    public GameObject ObstacleParent;
    public GameObject GetObstacle;
    public GameObject Player;
    public GameObject SpeedEffect;
    public GameObject GameOverPopup;
    public UILabel GameOverLabel;
    public UILabel ScoreLabel;
    public UILabel LBL_Time;
    public UISlider OBJ_TimeBar;
    public Texture[] GetCarTexture = new Texture[3];
    public ParticleSystem Particle1;
    public ParticleSystem Particle2;
    public ParticleSystem Particle3;
    public AudioClip S;
    public float Speed = 0;
    private float Minute_Time;
    private bool GameBool;


    GameObject[,] Obstacle = new GameObject[5, 3];
    bool[] IsObstacleLife = new bool[5];
    float MakeChk = 0;
    float yPosMoveChk = 0;
    float ScoreChk = 0;
    int Score = 3000;

    void Start()
    {
        ScoreLabel.text = "일당" + Score + "원";
        Minute_Time = DataManager.instance.TimeBuff;
        GameBool = true;
        for (int i = 0; i < 5; i++)
        {
            IsObstacleLife[i] = false;
            for (int j = 0; j < 3; j++)
            {
                Obstacle[i, j] = Instantiate(GetObstacle);
                Obstacle[i, j].transform.parent = ObstacleParent.transform;
                Obstacle[i, j].transform.localScale = new Vector3(1, 1, 1);
                Obstacle[i, j].SetActive(false);
            }
        }
    }

    public void Update()
    {
        if (GameBool)
        {
            Minute_Time -= Time.deltaTime;
            OBJ_TimeBar.value = 1.0f - Minute_Time / DataManager.instance.TimeBuff;
            int intTime = (int)Minute_Time;
            LBL_Time.text = intTime.ToString();

            if (Minute_Time <= 0.0f)
            {
                GameOver();
                GameBool = false;
            }

            if (Speed > 0)
                Speed -= 20 * Time.deltaTime;

            ScoreChk += (Speed + 100) * Time.deltaTime;
            if (ScoreChk > 300)
            {
                Score += 100;
                ScoreChk = 0;
                ScoreLabel.text = "일당" + Score + "원";
            }

            #region  MOVE BACKGROUND

            yPosMoveChk += (Speed + 2000) * Time.deltaTime;



            BackGround.transform.localPosition -= new Vector3(0, (Speed + 2000) * Time.deltaTime, 0);

            if (yPosMoveChk > 1280f)
            {
                yPosMoveChk = 0;
                BackGround.transform.localPosition = new Vector3(0, 0, 0);
            }

            #endregion

            #region  Make Object
            MakeChk += (Speed + 1000) * Time.deltaTime;

            if (MakeChk > 1000f)
            {
                MakeObstacle();
                MakeChk = 0;
            }

            for (int i = 0; i < 5; i++)
            {
                if (IsObstacleLife[i] == true)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Obstacle[i, j].transform.localPosition.y < -1280)
                        {
                            IsObstacleLife[i] = false;
                            break;
                        }
                        Obstacle[i, j].transform.localPosition -= new Vector3(0, (Speed + 1500) * Time.deltaTime, 0);
                    }

                }
            }

            #endregion

            #region  Touch

            if (Input.GetKeyDown(KeyCode.A) && Player.transform.localPosition.x != -250)
            {
                Player.transform.localPosition = new Vector3(Player.transform.localPosition.x - 250, Player.transform.localPosition.y, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D) && Player.transform.localPosition.x != 250)
            {
                Player.transform.localPosition = new Vector3(Player.transform.localPosition.x + 250, Player.transform.localPosition.y, 0);
            }



            #endregion
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver();
            GameBool = false;
        }
    }

    public void MakeObstacle()
    {
        int R = Random.Range(0, 3);
        for (int i = 0; i < 5; i++)
        {
            if (IsObstacleLife[i] == false)
            {
                IsObstacleLife[i] = true;
                for (int j = 0; j < 3; j++)
                {
                    Obstacle[i, j].transform.localPosition = new Vector3(-250 + j * 250, 1000, 0);
                    if (j != R)
                    {
                        Obstacle[i, j].SetActive(true);
                        Obstacle[i, j].GetComponent<UITexture>().mainTexture = GetCarTexture[Random.Range(0, 3)];
                    }
                    else
                    {
                        Obstacle[i, j].SetActive(false);
                    }
                }
                break;
            }

        }
    }

    public void Acceleration()
    {
        if (Speed > 1400)
            Speed = 1500;
        else
            Speed += 100;
        if (Speed > 1000)
        {
            SpeedEffect.SetActive(true);
        }
        else
        {
            SpeedEffect.SetActive(false);
        }
    }

    public void FuckingYourLife()
    {
        AudioSource.PlayClipAtPoint(S, transform.position);
        SpeedEffect.SetActive(false);
        Speed = 0;
        Score -= 500;
        ScoreLabel.text = "일당" + Score + "원";
    }

    public void GameOver()
    {
        Particle1.Stop();
        Particle2.Stop();
        Particle3.Stop();
        Particle3.Clear();

        Score += (Score / 100) * DataManager.instance.DailyWageBuff;

        GameOverLabel.text = "일당 " + Score.ToString() + "원";
        if (DataManager.instance.Quick_HighScore < Score)
        {
            DataManager.instance.Quick_HighScore = Score;
        }
        DataManager.instance.Money += Score;
        GameOverPopup.SetActive(true);
    }
}

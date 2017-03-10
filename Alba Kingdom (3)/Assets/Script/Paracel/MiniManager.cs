using UnityEngine;
using System.Collections;

public class MiniManager : MonoBehaviour {
    public GameObject[] Box = new GameObject[9];
    public GameObject[] Arrow = new GameObject[3];
    public GameObject BoxParent;
    public GameObject BeltParent;
    public GameObject GameOverPopup;
    
    private Vector3 Center;
    private Vector3[] BoxVector = new Vector3[3];

    public UILabel LBL_Score;
    public UILabel LBL_Time;
    public UILabel GameOverLabel;
    public UISlider OBJ_TimeBar;
    public UIAtlas SpriteAtlas;

    public AudioClip S;

    private int ArrowInput;
    private int RandomArrow;
    private int Score;
    private int BoxTurn;
    private int[] BoxPosition = new int[9];

    private bool GameStart;
    private bool SetArrow;
    private bool ReadyArrow;
    private bool CheckStart;
    private bool BoxMove;

    private float start_Time;
    private float Minute_Time;
    private float box_Amount;

    public enum ArrowIndex { LEFT = 0, UP, RIGHT };

	void Start ()
    {
        Score = 0;
        BoxTurn = 0;
        start_Time = 0;
        Minute_Time = DataManager.instance.TimeBuff;
        BoxTurn = 0;

        SetArrow = false;
        GameStart = true;
        ReadyArrow = true;
        CheckStart = false;

        box_Amount = 0.2f;

        Center = new Vector3(0, 78);
        BoxVector[(int)ArrowIndex.LEFT] = new Vector3(-410, 78);
        BoxVector[(int)ArrowIndex.UP] = new Vector3(0, 625);
        BoxVector[(int)ArrowIndex.RIGHT] = new Vector3(410, 78);

        for(int i = 0; i < 9 ;i++)
        {
            BoxSetting(i);
        }
    }
	
	void Update ()
    {
        if(GameStart)
        {
            #region INPLAYUPDATE         

            if (CheckStart)
            {
                CheckStart = false;
                if (Box[BoxTurn].GetComponent<BoxObject>().BoxType == ArrowInput)
                {
                    AudioSource.PlayClipAtPoint(S, transform.position);
                    Score += 50;
                    box_Amount += 0.03f;
                    Box[BoxTurn].transform.parent = BeltParent.transform;
                    Box[BoxTurn].GetComponent<BoxObject>().isBoxMove = true;
                    StartCoroutine(SwitchBox(Box[BoxTurn],BoxTurn));
                    BoxTurn++;
                    if(BoxTurn > 8)                    
                        BoxTurn = 0;
                    ReadyArrow = true;
                }
                else
                {
                    ReadyArrow = true;
                }
            }
            #endregion

            LBL_Score.text = "일당 " + Score.ToString() + "원";

            Minute_Time -= Time.deltaTime;
            OBJ_TimeBar.value = 1.0f - Minute_Time/ DataManager.instance.TimeBuff;
            int intTime = (int)Minute_Time;
            LBL_Time.text = intTime.ToString();

            if(Minute_Time <= 0.0f)
            {
                GameStart = false;
                GameOver();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameOver();
                GameStart = false;
            }
        }          
	}

    IEnumerator SwitchBox(GameObject obj, int index)
    {
        int temp_index;
        if (index == 0)
            temp_index = 8;
        else
            temp_index = index - 1;
        Vector3 BoxVector;
        BoxVector = new Vector3(BoxParent.transform.localPosition.x + 120, BoxParent.transform.localPosition.y);
        BoxParent.transform.localPosition = BoxVector;
        yield return new WaitForSeconds(1.0f);
        obj.transform.parent = BoxParent.transform;
        BoxSetting(index);
        Vector3 temp_Vector = new Vector3(Box[temp_index].transform.localPosition.x - 120, Box[temp_index].transform.localPosition.y);
        obj.transform.localPosition = temp_Vector;

        yield return null;
    }
    
    public void BoxSetting(int index)
    {
        int Random_Box = Random.Range(1, 4);
        Box[index].GetComponent<UISprite>().spriteName = "Box_" + Random_Box;
        Box[index].GetComponent<BoxObject>().BoxType = Random_Box - 1;
    }

    #region Arrow Script

    public void LeftArrow()
    {
        if (ReadyArrow)
        {
            CheckStart = true;
            ReadyArrow = false;
            ArrowInput = (int)ArrowIndex.LEFT;
        }
    }

    public void UpArrow()
    {
        if (ReadyArrow)
        {
            CheckStart = true;
            ReadyArrow = false;
            ArrowInput = (int)ArrowIndex.UP;
        }
    }

    public void RightArrow()
    {
        if(ReadyArrow)
        {
            CheckStart = true;
            ReadyArrow = false;
            ArrowInput = (int)ArrowIndex.RIGHT;
        }
    }
    #endregion

    public void GameOver()
    {
        Score += (Score / 100) * DataManager.instance.DailyWageBuff;

        GameOverLabel.text = "일당 " + Score.ToString() + "원";
        if (DataManager.instance.Parcel_HighScore < Score)
        {
            DataManager.instance.Parcel_HighScore = Score;
        }
        DataManager.instance.Money += Score;
        GameOverPopup.SetActive(true);
    }
}


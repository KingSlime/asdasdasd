using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject GameObj;
    public GameObject ShopObj;
    public GameObject HomeObj;
    public GameObject furniture_Chair;
    public GameObject furniture_Table;
    public GameObject furniture_Sofa;
    public GameObject furniture_Bed;
    public GameObject furniture_Wardrobe;
    public GameObject furniture_Tv;
    public GameObject Popup;

    public UILabel Ham;
    public UILabel Parcel;
    public UILabel Quick;
    public UILabel Gas;
    public UILabel Cvs;
    public UILabel MoneyAmount;
    public UILabel PopupLabel;

    public AudioClip S;

    private string furniture_Name;
    private int furniture_Priec;

    void Start()
    {
        Ham.text = DataManager.instance.Ham_HighScore.ToString() + "원";
        Parcel.text = DataManager.instance.Parcel_HighScore.ToString() + "원";
        Quick.text = DataManager.instance.Quick_HighScore.ToString() + "원";
        Gas.text = DataManager.instance.Gas_HighScore.ToString() + "원";
        Cvs.text = DataManager.instance.Cvs_HighScore.ToString() + "원";

    }
    void Update()
    {

    }

    public void GameButton()
    {
        ResetTab();
        GameObj.SetActive(true);
    }
    public void ShopButton()
    {
        ResetTab();
        ShopObj.SetActive(true);
    }
    public void HomeButton()
    {
        ResetTab();
        HomeObj.SetActive(true);

        
        MoneyAmount.text = string.Format("{0:#,###}", DataManager.instance.Money).ToString() + "원";
        if (DataManager.instance.Money == 0)
            MoneyAmount.text = "0원"
;
        if (DataManager.instance.Chair_Status)
            furniture_Chair.SetActive(true);
        if (DataManager.instance.Table_Status)
            furniture_Table.SetActive(true);
        if (DataManager.instance.Sofa_Status)
            furniture_Sofa.SetActive(true);
        if (DataManager.instance.Bad_Status)
            furniture_Bed.SetActive(true);
        if (DataManager.instance.Wardrobe_Status)
            furniture_Wardrobe.SetActive(true);
        if (DataManager.instance.Tv_Status)
            furniture_Tv.SetActive(true);


    }

    private void ResetTab()
    {
        GameObj.SetActive(false);
        ShopObj.SetActive(false);
        HomeObj.SetActive(false);
    }

    public void ClickChair()
    {
        furniture_Name = "편안한 의자";
        furniture_Priec = 5000000;
    }
    public void ClickTable()
    {
        furniture_Name = "튼튼한 탁자";
        furniture_Priec = 10000000;
    }
    public void ClickSofa()
    {
        furniture_Name = "푹신한 소파";
        furniture_Priec = 30000000;
    }
    public void ClickBad()
    {
        furniture_Name = "포근한 침대";
        furniture_Priec = 50000000;
    }
    public void ClickWardrobe()
    {
        furniture_Name = "편리한 옷장";
        furniture_Priec = 80000000;
    }
    public void ClickTv()
    {
        furniture_Name = "넓은 TV";
        furniture_Priec = 100000000;
    }
    public void OpenPopup()
    {
        Popup.SetActive(true);
        Debug.Log(furniture_Priec);
        PopupLabel.text = furniture_Name + "를 정말로 구매하시겠습니까?";
    }
    public void ClickYes()
    {
        Popup.SetActive(false);
        AudioSource.PlayClipAtPoint(S, transform.position);
        if (furniture_Priec == 5000000)
        {
            DataManager.instance.Chair_Status = true;
            DataManager.instance.Money -= furniture_Priec;
            DataManager.instance.TimeBuff += 5;
        }
        else if (furniture_Priec == 10000000)
        {
            DataManager.instance.Table_Status = true;
            DataManager.instance.Money -= furniture_Priec;
            DataManager.instance.DailyWageBuff += 5;
        }
        else if (furniture_Priec == 30000000)
        {
            DataManager.instance.Sofa_Status = true;
            DataManager.instance.Money -= furniture_Priec;
            DataManager.instance.TimeBuff += 7;
        }
        else if (furniture_Priec == 50000000)
        {
            DataManager.instance.Bad_Status = true;
            DataManager.instance.Money -= furniture_Priec;
            DataManager.instance.DailyWageBuff += 7;
        }
        else if (furniture_Priec == 80000000)
        {
            DataManager.instance.Wardrobe_Status = true;
            DataManager.instance.Money -= furniture_Priec;
            DataManager.instance.TimeBuff += 10;
        }
        else if (furniture_Priec == 100000000)
        {
            DataManager.instance.Tv_Status = true;
            DataManager.instance.Money -= furniture_Priec;
            DataManager.instance.DailyWageBuff += 10;
        }
    }
    public void ClickNo()
    {
        Popup.SetActive(false);
    }

}

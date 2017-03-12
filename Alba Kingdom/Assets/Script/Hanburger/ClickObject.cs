using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickObject : MonoBehaviour
{
    public GameObject GameMaster;
    public void Click1() { GameMaster.GetComponent<GameMaster>().GetTouchObject(0); }
    public void Click2() { GameMaster.GetComponent<GameMaster>().GetTouchObject(1); }
    public void Click3() { GameMaster.GetComponent<GameMaster>().GetTouchObject(2); }
    public void Click4() { GameMaster.GetComponent<GameMaster>().GetTouchObject(3); }
    public void Click5() { GameMaster.GetComponent<GameMaster>().GetTouchObject(4); }
    public void Click6() { GameMaster.GetComponent<GameMaster>().GetTouchObject(5); }
    public void ClickReStart() { SceneManager.LoadScene("Hanburger"); }
    public void ClickParcel() { SceneManager.LoadScene("Parcel"); }
    public void ClickReturn() { SceneManager.LoadScene("MainScene"); }
    public void ClickGasStation() { SceneManager.LoadScene("GasStation"); }
    public void ClickQuick() { SceneManager.LoadScene("Quick"); }
    public void ClickCvs() { SceneManager.LoadScene("convenience store"); }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public GameObject GameManager;

    bool check = false;

    public void Click1()
    {
        GameManager.GetComponent<YourName>().Acceleration();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Car" && check == false)      // 테그 이름
        {
            check = true;
            GameManager.GetComponent<YourName>().FuckingYourLife();
            Invoke("fuck", 1f);
        }
    }

    void fuck()
    {
        check = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//http://www.youtube.com

public class Lubricator : MonoBehaviour
{
    public GameObject OorX;
    public GasManager GameManager;
    public GameObject Car;
    public UISlider Gage;
    public AudioClip S;
    public AudioClip F;

    private Vector3 StartPos;
    private Vector3 Center;
    private Vector3 EndPos;

    private bool isCar;
    private bool waitingCar;
    private bool carComing;
    private bool carLeaving;
    private bool refueling;
    private bool isClear;

    private float movingTime;
    private float blankTime;
    private float waitingTime;
    private float impatient;
    private float GasGage;
    private float waitsetting;
    
    void Start()
    {
        StartPos = new Vector3(-690, -79);
        Center = new Vector3(55, -79);
        EndPos = new Vector3(780, -79);

        isCar = false;
        waitingCar = false;
        refueling = false;

        GasGage = 0.0f;
        Setting();
    }

    void Update()
    {
        if (GameManager.GameBool)
        {
            if (waitingCar)
            {
                blankTime += Time.deltaTime;
                if (blankTime >= waitsetting)
                {
                    waitingCar = false;
                    carComing = true;
                    int randomCar = Random.Range(0, 5);
                    Car.GetComponent<UISprite>().spriteName = "Car_" + randomCar;
                }
            }

            if (carComing)
            {
                if (movingTime < 1.0f)
                    movingTime += Time.deltaTime * 0.75f;
                else
                {
                    carComing = false;
                    isCar = true;
                    GasGage = Random.Range(1.0f, 0.7f);
                    Gage.value = GasGage;
                }
                Vector3 Vctr = ((1 - movingTime) * StartPos + movingTime * Center);
                Car.transform.localPosition = Vctr;
            }

            if (isCar)
            {
                waitingTime += Time.deltaTime;
                if (waitingTime > impatient)
                {
                    isCar = false;
                    carLeaving = true;
                    movingTime = 0.0f;
                }
            }

            if (refueling)
            {
                GasGage -= Time.deltaTime * 0.3f;
                Gage.value = GasGage;

                if (GasGage <= 0.0f)
                {
                    refueling = false;
                    isClear = false;
                    EndProcess();
                }
            }



            if (carLeaving)
            {
                Vector3 Vctr = ((1 - movingTime) * Center + movingTime * EndPos);
                Car.transform.localPosition = Vctr;

                if (movingTime < 1.0f)
                    movingTime += Time.deltaTime * 0.75f;
                else
                {
                    carLeaving = false;
                    Setting();
                }
            }
        }
    }

    public void Setting()
    {
        waitsetting = Random.Range(3.0f, 8.0f);
        impatient = Random.Range(8.0f, 15.0f);
        waitingCar = true;
        blankTime = 0.0f;
        movingTime = 0.0f;
        waitingTime = 0.0f;
        Car.transform.localPosition = new Vector3(-1000, -1000);

    }

    public void EndProcess()
    {
        carLeaving = true;
        movingTime = 0.0f;

        if(isClear)
        {
            AudioSource.PlayClipAtPoint(S, transform.position);
            GameManager.Score += 300;
            StartCoroutine(Ocheck());
        }
        else
        {
            AudioSource.PlayClipAtPoint(F, transform.position);
            StartCoroutine(Xcheck());
        }
        
        Setting();
    }

    void OnClick()
    {
        if(refueling)
        {
            refueling = false;

            if (GasGage <= 0.08f)
            {
                isClear = true;
            }
            else
            {
                isClear = false;
            }

            EndProcess();
        }

        if(isCar)
        {
            refueling = true;
            isCar = false;
        }
    }
    
    IEnumerator Ocheck()
    {
        OorX.SetActive(true);
        OorX.GetComponent<UISprite>().spriteName = "O_Sprite";
        Debug.Log("O");     
        yield return new WaitForSeconds(0.5f);
        OorX.SetActive(false);
    }

    IEnumerator Xcheck()
    {
        OorX.SetActive(true);
        OorX.GetComponent<UISprite>().spriteName = "X_Sprite";
        Debug.Log("X");
        yield return new WaitForSeconds(0.5f);
        OorX.SetActive(false);
    }
}

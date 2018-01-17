using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UniRx;
using System.Collections;

public class ItemGeneratorWithX : MonoBehaviour
{
    public GameObject gameObject2Generate;
    public float genRatePercentPer1m;
    public float minInterval;
    public float offsetX;
    private float originX;
    private float targetX;
    private float lastGeneratedX;
    private Camera cameraBefore;
    private Camera cameraAfter;
    public float generateY;

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }

    private void Start()
    {
        Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Initialize()
    {

        originX = transform.position.x;
        lastGeneratedX = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0f, Camera.main.transform.position.z)).x + offsetX;
        cameraBefore = Camera.main;
    }

    private void Update()
    {
        cameraAfter = Camera.main;
        if (cameraBefore == cameraAfter)
        {
            cameraAfter = null;
        };



        targetX = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0f, Camera.main.transform.position.z)).x + offsetX;
        if ((Mathf.Abs(targetX - lastGeneratedX) > minInterval)& !(GamePresentator.isPlayerFixed))
        {
            Debug.Log("GenerateTrial");
            lastGeneratedX = targetX;
            if (ReturnBoolByPercent(genRatePercentPer1m))
            {
                Debug.Log("CoinGenerated");
                GenerateWithSettings(targetX);
            }
        }
    }

    private void GenerateWithSettings(float X2Generate)
    {

        Instantiate(gameObject2Generate, new Vector3(X2Generate, generateY, 0.0f), gameObject2Generate.transform.rotation);


    }

    private bool ReturnBoolByPercent(float rate)
    {
        bool bool2Return;
        if (rate>100f)
        {
            rate = 100f;
        }
        if (rate < 0f)
        {
            rate = 0f;
        }

        float random = (float) new System.Random().NextDouble()*100;
        if (random <= rate)
        {
            bool2Return = true;
        }
        else
        {
            bool2Return = false;
        }
        return bool2Return;
    }
}

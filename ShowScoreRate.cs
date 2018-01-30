using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using System.Globalization;

public class ShowScoreRate : MonoBehaviour {

	private Text textAttached;
    public GameObject gameObject2Commit;
    public CalculateMoney calculateMoney;


    private void Start()
    {
        Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }

    void Initialize()
    {
        textAttached = gameObject.GetComponent<Text>();


        calculateMoney.scoreRateSum.Subscribe(
             rateSum => textAttached.text = "×" + rateSum.ToString("F1", CultureInfo.InvariantCulture)

            );

    }

}


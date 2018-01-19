using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using System.Globalization;

public class ShowScoreRate : MonoBehaviour {

	private Text textAttached;
    public GameObject gameObject2Commit;
    public CountBonusByItem countScoreByItem;

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


        countScoreByItem.bonusPointByTheItem.Subscribe(
             score => textAttached.text = "×" + (1.0 + (score/100)).ToString("F1", CultureInfo.InvariantCulture)

            );

    }

}


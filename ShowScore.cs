using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

public class ShowScore : MonoBehaviour {

	private Text textAttached;
    public GameObject gameObject2Commit;
    public CountScoreByItem countScoreByItem;

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


        countScoreByItem.scoreByTheItem.Subscribe(
             score => textAttached.text = score.ToString() + "円"

            );

    }

}


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

public class ShowNumber : MonoBehaviour {

	private Text textAttached;
    public GameObject gameObject2Commit;
    public DistanceMeasure distanceMeasure;


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


        distanceMeasure.distance.Subscribe(
             distance => textAttached.text = ((Mathf.Floor(distance*10f))/10f).ToString() + "m"

            );

    }

}


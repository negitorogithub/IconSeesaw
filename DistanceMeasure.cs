using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

public class DistanceMeasure: MonoBehaviour{

    private float originPosX;
    public ReactiveProperty<float> distance { get; private set; }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }

    private void Start()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        distance = new ReactiveProperty<float>();
        originPosX = transform.position.x;
    }

    private void Update()
    {
        distance.Value = transform.position.x - originPosX;
    }
}

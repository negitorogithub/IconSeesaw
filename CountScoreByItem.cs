using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Assets.Scripts;
using System.Collections;

public class CountScoreByItem : MonoBehaviour
{
    public Collider2D Prefab2Count;
    public float scorePerItem;
    public ReactiveProperty<float> scoreByTheItem;

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Initialize();
    }

    private void Awake()
    {
        Initialize();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Initialize()
    {
        scoreByTheItem = new ReactiveProperty<float>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == MyUtil.plusClone(Prefab2Count.name))
        {
            scoreByTheItem.Value += scorePerItem;
            Destroy(collision.gameObject);
        }
    }



}

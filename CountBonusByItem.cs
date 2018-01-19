using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Assets.Scripts;
using System.Collections;

public class CountBonusByItem : MonoBehaviour
{
    public Collider2D Prefab2Count;
    public float bonusPointPerItem;
    public ReactiveProperty<float> bonusPointByTheItem;

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
        bonusPointByTheItem = new ReactiveProperty<float>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == MyUtil.plusClone(Prefab2Count.name))
        {
            bonusPointByTheItem.Value += bonusPointPerItem;
            Destroy(collision.gameObject);
        }
    }



}

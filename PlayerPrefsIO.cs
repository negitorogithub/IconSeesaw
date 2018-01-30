using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

public class PlayerPrefsIO
{


    public static void SaveInt( PLAYER_PREFS_KEY_ENUM key, int value)
    {
        PlayerPrefs.SetInt(key.ToString(), value);
    }
    public static void SaveFloat( PLAYER_PREFS_KEY_ENUM key, float value)
    {
        PlayerPrefs.SetFloat(key.ToString(), value);
    }
    public static int GetInt(PLAYER_PREFS_KEY_ENUM key)
    {
        return PlayerPrefs.GetInt(key.ToString());
    }
    public static float GetFloat(PLAYER_PREFS_KEY_ENUM key)
    {
        return PlayerPrefs.GetFloat(key.ToString());
    }
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}

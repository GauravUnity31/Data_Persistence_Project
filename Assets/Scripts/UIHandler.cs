using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIHandler : MonoBehaviour
{
    public TMP_InputField plyerNameInput;
    public TextMeshProUGUI nameText;

    [HideInInspector] public string playerName;
    [HideInInspector] public int bestScore;

    public static UIHandler instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadData();
        UpdateData();

        Debug.Log("Start func");
    }

    public void SaveData()
    {
        PlayerData playerData = new PlayerData();
        playerData.playerName = plyerNameInput.text;
        playerData.highScore = bestScore;

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log(Application.persistentDataPath);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

            playerName = playerData.playerName;
            bestScore = playerData.highScore;

            Debug.Log(path);
        }
    }

    public void UpdateData()
    {
        nameText.text = "Best Score : " + playerName + " : " + bestScore;
        plyerNameInput.text = playerName;
    }

    public void ClickStart()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Click Start");
    }

    public void ClickQuit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}

[System.Serializable]
public class PlayerData
{
    public int highScore;
    public string playerName;
}

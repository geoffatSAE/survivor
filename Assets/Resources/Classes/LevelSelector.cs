using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject selection;
    public GameObject menu;

    public void setLevel()
    {
        menu.SetActive(true);
        GameObject.Find("StartButton").GetComponent<LevelSelect>().SelectLevel(selection);
    }
}

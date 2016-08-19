using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject levelSelect;
    public GameObject selection;
    public GameObject player;

    void Start()
    {
        levelSelect.SetActive(false);
    }
    
    public void SelectLevel(GameObject sel)
    {
        selection = sel;
        OpenCloseLevelSelect();
        GameObject.Find("LevelIndicator").GetComponent<Text>().text = selection.name;
    }

    public void OpenCloseLevelSelect()
    {
        if (levelSelect.activeSelf == false)
        {
            levelSelect.SetActive(true);
            GameObject.Find("MainMenu").SetActive(false);
        }else{
            levelSelect.SetActive(false);
            GameObject.Find("MainMenu").SetActive(true);
        }
    }

    public void StartGame()
    {
        if(selection != null)
        {
            selection.SetActive(true);
            //GameObject.Find("MainMenu").SetActive(false);
            //player.SetActive(true);
            GameObject.Find("Spawner").GetComponent<Spawner>().SetWaveFlow(true);
            Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 0, 0), transform.rotation);
        }
        else{
            GameObject.Find("LevelIndicator").GetComponent<Text>().text = "Level Not Selected";
        }
    }
}

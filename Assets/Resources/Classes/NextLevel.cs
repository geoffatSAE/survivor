using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject selection;
    public GameObject selection2;
    public GameObject selection3;
    public GameObject mainMenu;
    public GameObject adfsbijou;

    public void loadLevel()
    {
        if(selection.activeSelf)
        {
            selection.SetActive(false);
            selection2.SetActive(true);
        }else if (selection2.activeSelf)
        {
            selection2.SetActive(false);
            selection3.SetActive(true);
        }else if(selection3.activeSelf){
            selection3.SetActive(false);
            mainMenu.SetActive(true);
        }

        adfsbijou.gameObject.SetActive(false);

        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().transform.position = new Vector3(0, 0, 0);
        GameObject.Find("Spawner").GetComponent<Spawner>().ResetWaves();
    }
}

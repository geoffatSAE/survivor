using UnityEngine;
using System.Collections;

public class InstructionsScript : MonoBehaviour
{
    public GameObject instructions;
    public GameObject menu;

    public void OpenCloseInstructions()
    {
        if (instructions.activeSelf == false)
        {
            instructions.SetActive(true);
            menu.SetActive(false);
        }
        else {
            instructions.SetActive(false);
            menu.SetActive(true);
        }
    }
}

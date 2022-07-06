using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    private GameObject window;
    private int index = 0;

    private void Start()
    {
        window = transform.GetChild(index).gameObject;
        window.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (window.transform.name)
        {
            case "WASD":
                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    ChangeWindow();
                }
                break;
            case "Jump":
                if (Input.GetButtonDown("Jump"))
                {
                    ChangeWindow();
                }
                break;
            case "Tutorial End":
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    window.SetActive(false);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                break;
            default:
                break;
        }
    }

    private void ChangeWindow()
    {
        window.SetActive(false);
        window = transform.GetChild(++index).gameObject;
        window.SetActive(true);
    }
}

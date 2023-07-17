using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botaocarregarfase : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Retry()
    {
        SceneManager.LoadScene("fase 1 MAIN");
    }

    // Update is called once per frame
    public void Quit()
    {
        Application.Quit();
        Debug.Log("player quit");
    }
    public void inicio()
    {
        SceneManager.LoadScene("inicio");
    }
}

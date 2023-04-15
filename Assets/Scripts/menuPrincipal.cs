using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Exit()
    {
        Debug.Log("Se ha salido del juego");
        Application.Quit();
        //mordida.Play();
    }
    public void Menu()
    {
        //SceneManager.LoadScene("MainMenu");
        SceneManager.LoadScene("StartMenu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}

using UnityEngine;

public class CloseGame : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Menu entrando");
    }
}

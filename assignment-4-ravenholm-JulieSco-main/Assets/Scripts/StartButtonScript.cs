using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void doExitGame()
    {

        SceneManager.LoadScene(0);

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public void GoToScene()
    {
        //scene = sceneName;
        Debug.Log("about to press button");
        SceneManager.LoadScene("CtScene");
        Debug.Log("button press complete");
    }
}

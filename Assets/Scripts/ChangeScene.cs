using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public GameObject button;
    void GoToScene()
    {
        //scene = sceneName;
        GameObject newObj;
        newObj = (GameObject)Instantiate(button, transform);
        newObj.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("CtScene"));
    }
}

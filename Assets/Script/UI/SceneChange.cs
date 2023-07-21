using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey) 
        {
            if (SceneManager.GetActiveScene().name == "TitleScene")
            {
                SceneManager.LoadScene("MainScene");
            }

        }
    }
   


}

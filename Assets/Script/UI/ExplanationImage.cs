using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplanationImage : MonoBehaviour
{
    [SerializeField] private GameObject explanationImage1;
    [SerializeField] private GameObject explanationImage2;
    [SerializeField] private GameObject explanationImage3;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (explanationImage1.activeSelf)
            {
                explanationImage1.SetActive(false);
                explanationImage2.SetActive(true);
            }
            else if (explanationImage2.activeSelf)
            {
                explanationImage2.SetActive(false);
                explanationImage3.SetActive(true);
            }
            else if (explanationImage3.activeSelf) 
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }


}

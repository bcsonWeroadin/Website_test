using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MovePlanet : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision Enter" + other.gameObject.name);
        //SceneManager.LoadScene("");
        //SceneManager.LoadSceneAsync("Beach Demo Scene 1");
        StartCoroutine(LoadScean("Beach Demo Scene 1"));
    }
    

    IEnumerator LoadScean(string sceneName)
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(sceneName);
        
        while(!oper.isDone)
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log(oper.progress);
        }
    }
}

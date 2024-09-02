using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class piojoDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            //Destroy(gameObject);
        }
            
        
    }

}

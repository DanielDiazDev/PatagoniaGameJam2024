using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;



public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string _destinoZonaId;
    [SerializeField] private Transform _teleport;
    [SerializeField] private LevelLoaderScript levelLoader;

    [SerializeField] private int sceneIndexToGo = 0;
   // [SerializeField] private Transform _piojoTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelLoader = GameObject.FindObjectOfType<LevelLoaderScript>();
        if (collision.CompareTag("Piojo") && (levelLoader != null))
        {
            levelLoader.LoadIndexScene(sceneIndexToGo);
            //GameManager.Instance().CambiarZona(_destinoZonaId);
            //collision.transform.position = _teleport.position;
        }

    }
}

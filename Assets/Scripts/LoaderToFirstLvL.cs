using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderToFirstLvL : MonoBehaviour
{
    [SerializeField] private LevelLoaderScript levelLoader;
    void Start()
    {
        levelLoader.LoadIndexScene(4);
    }
}

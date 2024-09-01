using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Zona
{
    public string ZonaID; 
    public Camera camara; 
}
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private List<Zona> _zonas;
    public enum StatesFoca
    {
        FueraDelMar,
        MarNoProfundo,
        MarProfundo
    }
    [SerializeField] private Piojo _piojo;
    [SerializeField] private float _timeForLevelUp;
    [SerializeField] private float _timeForChangeOfState;
    private StatesFoca _currentState;
    private Camera _currentCamera;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    public static GameManager Instance()
    {
        return _instance;
    }

    private void Start()
    {
        _currentState = StatesFoca.FueraDelMar;
        StartCoroutine(LevelUpIncrement());
        StartCoroutine(RoutineOfFoca());

        if(_zonas.Count > 0)
        {
            _currentCamera = _zonas[0].camara;
            ActivarCamara(_currentCamera);
        }
    }
    public void CambiarZona(string zonaId)
    {
        Zona nuevaZona = _zonas.Find(z=>z.ZonaID == zonaId);
        if (nuevaZona != null && nuevaZona.camara != _currentCamera)
        {
            _currentCamera = nuevaZona.camara;
            ActivarCamara(_currentCamera);
        }
    }
    private void ActivarCamara(Camera camera)
    {
        foreach (var zona in _zonas)
        {
            zona.camara.gameObject.SetActive(zona.camara == camera);
        }
    }

    private IEnumerator LevelUpIncrement()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(_timeForLevelUp);
            _piojo.LevelUp();
        }
    }
    private IEnumerator RoutineOfFoca()
    {
        while (true)
        {
            switch (_currentState)
            {
                case StatesFoca.FueraDelMar:
                    Debug.Log("La foca esta fuera del mar");
                    yield return new WaitForSeconds(_timeForChangeOfState);
                    _currentState = StatesFoca.MarNoProfundo;
                    break;
                //Haz algo
                case StatesFoca.MarNoProfundo:
                    Debug.Log("La foca esta en el mar no profundo");
                    yield return new WaitForSeconds(_timeForChangeOfState);
                    _currentState = StatesFoca.MarProfundo;
                    break;
                //Haz algo
                case StatesFoca.MarProfundo:
                    Debug.Log("La foca esta en el mar profundo");
                    yield return new WaitForSeconds(_timeForChangeOfState);
                    _currentState = StatesFoca.FueraDelMar;
                    break;
                //Haz algo
                default:
                    Debug.LogError("Estado desconocido");
                    break;

            }
        }
    }
    public bool EsEsteState(StatesFoca statesFoca)
    {
        return _currentState == statesFoca;
    }
}

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
    [SerializeField] private AudioSource _audioSource;
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
        _piojo.ActualizarRestriccionesCamara(GetCurrentCameraRestrictions());
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
                    Debug.Log("La foca está fuera del mar.");
                    // Espera 15 segundos antes de la advertencia
                    yield return new WaitForSeconds(_timeForChangeOfState - 15f);
                    AdvertirCambioDeEstado(StatesFoca.MarNoProfundo);
                    // Esperar los últimos 15 segundos
                    yield return new WaitForSeconds(15f);
                    _currentState = StatesFoca.MarNoProfundo;
                    break;

                case StatesFoca.MarNoProfundo:
                    Debug.Log("La foca está en el mar no profundo.");
                    yield return new WaitForSeconds(_timeForChangeOfState - 15f);
                    AdvertirCambioDeEstado(StatesFoca.MarProfundo);
                    yield return new WaitForSeconds(15f);
                    _currentState = StatesFoca.MarProfundo;
                    break;

                case StatesFoca.MarProfundo:
                    Debug.Log("La foca está en el mar profundo.");
                    yield return new WaitForSeconds(_timeForChangeOfState - 15f);
                    AdvertirCambioDeEstado(StatesFoca.FueraDelMar);
                    yield return new WaitForSeconds(15f);
                    _currentState = StatesFoca.FueraDelMar;
                    break;

                default:
                    Debug.LogError("Estado desconocido");
                    break;
            }
        }
    }
    public RestriccionesCamara GetCurrentCameraRestrictions()
    {
        return _currentCamera.GetComponent<RestriccionesCamara>();
    }
    private void AdvertirCambioDeEstado(StatesFoca nuevoEstado)
    {
        Debug.Log($"Advertencia: La foca cambiará a {nuevoEstado} en 15 segundos.");
    }

    public bool EsEsteState(StatesFoca statesFoca)
    {
        return _currentState == statesFoca;
    }
}

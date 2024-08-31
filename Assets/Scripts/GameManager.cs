using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
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

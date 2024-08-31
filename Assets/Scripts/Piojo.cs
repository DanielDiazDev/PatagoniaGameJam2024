using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piojo : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float _speed;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    [Header("Salud")] //O Hambre
    [SerializeField] private float _currentHealth;
    private float _maxHealth = 10;
    [SerializeField] private float _decreaseHealthPorSegundo = 0.5f;
    [Header("Humedad")]
    [SerializeField] private float _currentHumedad;
    private float _maxHumedad = 10;
    [Header("Temperatura")]
    [SerializeField] private float _currentTemperatura;
    private float _maxTemeperatura = 10;
    [SerializeField] private float _decreaseTemperaturaPorSegundo = 1;
    [SerializeField] private bool _estaEnZonaCaliente;
    [Header("Agarrarse")]
    [SerializeField]private bool _estaAgarrado;
    [Header("Etapa de Ninfa")]
    [SerializeField] private int _ninfaLevel;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //if(!_estaAgarrado && (GameManager.Instance().EsEsteState(GameManager.StatesFoca.MarNoProfundo) Descomentar
        //    || GameManager.Instance().EsEsteState(GameManager.StatesFoca.MarProfundo)))
        //{
        //        Die();
        //}
        if (!_estaEnZonaCaliente)
        {
            QuitarTemperatura(_decreaseTemperaturaPorSegundo * Time.deltaTime);
        }
            QuitarSalud(_decreaseHealthPorSegundo * Time.deltaTime);


        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        _direction = new Vector2(horizontal, vertical);
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _direction * (_speed * Time.fixedDeltaTime));
    }

    //Salud/Hambre
    public void QuitarSalud(float health)
    {
        _currentHealth -= health;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        if(_currentHealth <= 0)
        {
            Die();
        }
    }
    public void AñadirSalud(float health)
    {
        _currentHealth += health;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }

    //Temperatura
    public void QuitarTemperatura(float temperatura)
    {
        _currentTemperatura -= temperatura;
        _currentTemperatura = Mathf.Clamp(_currentTemperatura, 0, _maxTemeperatura);
        if (_currentTemperatura <= 0)
        {
            Die();
        }
    }
    public void AñadirTemperatura(float temperatura)
    {
        _currentTemperatura += temperatura;
        _currentTemperatura = Mathf.Clamp(_currentTemperatura, 0, _maxTemeperatura);
    }
    public void EstaAgarrandose(bool agarrado)
    {
        _estaAgarrado = agarrado;
    }
    public void EstaEnZonaCaliente(bool estaEnZona)
    {
        _estaEnZonaCaliente = estaEnZona;
    }
    //Humedad
    public void QuitarHUmedad(float humedad)
    {
        _currentHumedad -= humedad;
        _currentHumedad = Mathf.Clamp(_currentHumedad, 0, _maxHumedad);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    public void AñadirHumedad(float humedad)
    {
        _currentHumedad += humedad;
        _currentHumedad = Mathf.Clamp(_currentHumedad, 0, _maxHumedad);
    }
    public void LevelUp()
    {
        if(_ninfaLevel < 3)
        {
            _ninfaLevel++;
        }
    }
    private void Die()
    {
        Debug.Log("Esta muerto");
    }
}

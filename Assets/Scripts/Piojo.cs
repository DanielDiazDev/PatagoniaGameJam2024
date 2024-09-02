using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piojo : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float _speed;
    private Rigidbody2D _rb;
    private bool _estaCaminando;
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
    [Header("Audios")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _caminarClip;
    private RestriccionesCamara _restriccionesCamara;
    private bool isDead = false;

    [SerializeField] private LevelLoaderScript levelLoaderScript;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_restriccionesCamara = GameManager.Instance().GetCurrentCameraRestrictions();
        Debug.Log(_restriccionesCamara);
    }

   

    private void Update()
    {
        //if (!_estaAgarrado && (GameManager.Instance().EsEsteState(GameManager.StatesFoca.MarNoProfundo)
        //    || GameManager.Instance().EsEsteState(GameManager.StatesFoca.MarProfundo)))
        // {
        //     Die();
        // }
        if (!_estaEnZonaCaliente)
        {
            QuitarTemperatura(_decreaseTemperaturaPorSegundo * Time.deltaTime);
        }
        QuitarSalud(_decreaseHealthPorSegundo * Time.deltaTime);

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        _direction = new Vector2(horizontal, vertical);

        bool wasWalking = _estaCaminando;
        _estaCaminando = _direction.magnitude > 0.1f;
        GenerarSonidoDeCaminar(wasWalking);
    }
    public void ActualizarRestriccionesCamara(RestriccionesCamara restricciones)
    {
        _restriccionesCamara = restricciones;
    }

    private void FixedUpdate()
    {
        // _rb.MovePosition(_rb.position + _direction * (_speed * Time.fixedDeltaTime));
        Vector2 nuevaPos = _rb.position + _direction * (_speed * Time.fixedDeltaTime);

        //if (_restriccionesCamara.EstaDentroDeCamara(nuevaPos))
        //{
            _rb.MovePosition(nuevaPos);
        //}
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
        }else
        {
            Win();
        }
    }
    private void GenerarSonidoDeCaminar(bool wasWalking)
    {
        if (_estaCaminando && !wasWalking)
        {
            _audioSource.clip = _caminarClip;
            _audioSource.Play();
            _audioSource.loop = true;
        }
        else if (!_estaCaminando && wasWalking)
        {
            _audioSource.clip = null;
            _audioSource.Stop();
            _audioSource.loop = false;
        }
    }
    private void Die()
    {
        if (!isDead)
        {
            isDead = true;
        
            Debug.Log("Esta muerto");
            levelLoaderScript = GameObject.FindObjectOfType<LevelLoaderScript>();
            levelLoaderScript.LoadIndexScene(2);
            Destroy(gameObject);
        }
    }

    private void Win()
    {
        Debug.Log("Ganastes!");
        levelLoaderScript = GameObject.FindObjectOfType<LevelLoaderScript>();
        levelLoaderScript.LoadIndexScene(3);
        Destroy(gameObject);
    }
}

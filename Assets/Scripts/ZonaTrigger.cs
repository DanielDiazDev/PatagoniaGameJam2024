using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaTrigger : MonoBehaviour
{
    [SerializeField] private string _destinoZonaId;
    [SerializeField] private Transform _teleport;
   // [SerializeField] private Transform _piojoTransform;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Piojo"))
        {
            GameManager.Instance().CambiarZona(_destinoZonaId);
            collision.transform.position = _teleport.position;
        }

    }

}

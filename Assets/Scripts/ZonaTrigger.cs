using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaTrigger : MonoBehaviour
{
    [SerializeField] private string _destinoZonaId;
    [SerializeField] private ZonaTrigger _triggerOpuesto;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Piojo"))
        {
            GameManager.Instance().CambiarZona(_destinoZonaId);
        }
        if (_triggerOpuesto != null)
        {
            StartCoroutine(DesactivarTriggerTemporalmente());
        }
    }

    private IEnumerator DesactivarTriggerTemporalmente()
    {
        _triggerOpuesto.gameObject.GetComponent<Collider2D>().enabled = false; 
        yield return new WaitForSeconds(1f); 
        _triggerOpuesto.gameObject.GetComponent<Collider2D>().enabled = true; 
    }

}

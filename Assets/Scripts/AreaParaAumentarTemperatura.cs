using UnityEngine;

public class AreaParaAumentarTemperatura : MonoBehaviour
{
    [SerializeField] private float _addTemperatura;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Piojo"))
        {
            if (collision.TryGetComponent<Piojo>(out var piojo))
            {
                piojo.AñadirTemperatura(_addTemperatura * Time.deltaTime);
                piojo.EstaEnZonaCaliente(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Piojo"))
        {
            if (collision.TryGetComponent<Piojo>(out var piojo))
            {
                piojo.EstaEnZonaCaliente(false);
            }
        }
    }

}

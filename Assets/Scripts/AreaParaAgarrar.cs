using UnityEngine;

public class AreaParaAgarrar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Piojo"))
        {
            if(collision.TryGetComponent<Piojo>(out var piojo))
            {
                piojo.EstaAgarrandose(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Piojo"))
        {
            if (collision.TryGetComponent<Piojo>(out var piojo))
            {
                piojo.EstaAgarrandose(false);
            }
        }
    }
}

using UnityEngine;

public class AreaParaChupar : MonoBehaviour
{
    [SerializeField] private float _addHealth;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Piojo"))
        {
            if (collision.TryGetComponent<Piojo>(out var piojo))
            {
                piojo.AñadirSalud(_addHealth);
            }
        }
        
    }
}
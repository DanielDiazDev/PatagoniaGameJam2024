using System.Collections;
using UnityEngine;

public class AreaParaChupar : MonoBehaviour
{
    [SerializeField] private float _addHealth;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clipChupar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Piojo"))
        {
            if (collision.TryGetComponent<Piojo>(out var piojo))
            {
                piojo.AñadirSalud(_addHealth);
                _audioSource.clip = _clipChupar;
                _audioSource.Play(); StartCoroutine(DestroyAfterSound());
            }
        }
    }

    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(_audioSource.clip.length);

        Destroy(gameObject);
    }
}
using UnityEngine;

public class RestriccionesCamara : MonoBehaviour
{
    private Camera _camera;
    private BoxCollider2D _collider;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _collider = GetComponent<BoxCollider2D>();

        if (_camera != null && _collider == null)
        {
            _collider = gameObject.AddComponent<BoxCollider2D>();
            _collider.isTrigger = true;
        }
        if (_camera != null)
        {
            // Actualizar el tamaño del collider según el tamaño de la cámara
            Vector2 cameraSize = new Vector2(_camera.orthographicSize * _camera.aspect * 2, _camera.orthographicSize * 2);
            _collider.size = cameraSize;
        }
    }

    private void Update()
    {
        
    }

    public bool EstaDentroDeCamara(Vector2 position)
    {
        // Convertir la posición del piojo a coordenadas de la cámara
        Vector3 viewportPos = _camera.WorldToViewportPoint(position);
        return viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1;
    }
}

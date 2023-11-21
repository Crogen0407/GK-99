using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private void Update()
    {
        transform.forward = _camera.forward;
    }
}

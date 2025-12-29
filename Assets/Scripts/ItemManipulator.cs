using UnityEngine;

public class ItemManipulator
{
    private Camera _camera;
    private Rigidbody _heldItemRigidbody;
    private float _fixedY;

    public ItemManipulator(Camera camera)
    {
        _camera = camera;
    }

    public void Execute(Ray ray, LayerMask mask)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask.value))
        {
            _heldItemRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();

            if (_heldItemRigidbody != null)
            {
                _heldItemRigidbody.isKinematic = true;
                _fixedY = _heldItemRigidbody.position.y;

                Plane plane = new Plane(Vector3.up, new Vector3(0, _fixedY, 0));

                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 targetPosition = ray.GetPoint(distance);
                    _heldItemRigidbody.MovePosition(targetPosition);
                }
            }
        }
    }

    public void Release()
    {
        if (_heldItemRigidbody != null)
        {
            _heldItemRigidbody.isKinematic = false;
            _heldItemRigidbody = null;
        }
    }
}

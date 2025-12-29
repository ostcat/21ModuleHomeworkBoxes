using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMaskItems;
    [SerializeField] private LayerMask _layerMaskGround;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _upwardModifier;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private ExplosionPusher _explosionPusher;
    
    private const int LeftMouseClick = 0;
    private const int RightMouseClick = 1;
    private Camera _camera;
    private ItemManipulator _itemManipulator;
    private Ray _mouseRay;

    private void Awake()
    {
        _camera = Camera.main;
        _itemManipulator = new ItemManipulator(_camera);
    }

    private void Update()
    {
        if (_camera != null)
            _mouseRay = _camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(LeftMouseClick))
        {
            _itemManipulator.Execute(_mouseRay, _layerMaskItems);
        }

        if (Input.GetMouseButtonUp(LeftMouseClick))
        {
            _itemManipulator.Release();
        }

        if (Input.GetMouseButtonDown(RightMouseClick))
        {
            _explosionPusher.ThrowBomb(_mouseRay.origin, _mouseRay.direction, _layerMaskGround);
        }
    }

    private void FixedUpdate()
    {
        if (_explosionPusher.CanPush == false)
            return;

        _explosionPusher.PushItems(_explosionRadius, _explosionForce, _layerMaskItems, _upwardModifier);
    }   
}

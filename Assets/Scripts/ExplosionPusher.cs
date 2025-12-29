using UnityEngine;

public class ExplosionPusher : MonoBehaviour 
{
    [SerializeField] private ParticleSystem _explosionEffectPrefab;
    private Vector3 _hitPoint;

    public bool CanPush { get; private set; }

    public void ThrowBomb(Vector3 origin, Vector3 direction, LayerMask layerMask)
    {
        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask.value))
        {
            _hitPoint = hit.point;
            ParticleSystem explosionEffect = Instantiate(_explosionEffectPrefab, _hitPoint, Quaternion.identity);
            explosionEffect.Play();
            CanPush = true;

            float duration = explosionEffect.main.duration + explosionEffect.main.startLifetime.constantMax;
            Destroy(explosionEffect.gameObject, duration);
        }
    }

    public void PushItems(float radius, float force, LayerMask layerMask, float upwardModifier)
    {
        Collider[] targets = Physics.OverlapSphere(_hitPoint, radius, layerMask.value);

        foreach (Collider target in targets)
        {
            Rigidbody rigidbody = target.GetComponent<Rigidbody>();
            Vector3 pushDirection = (rigidbody.position - _hitPoint).normalized;
            pushDirection.y = pushDirection.y + upwardModifier;


            if (rigidbody != null)
                rigidbody.AddForce(pushDirection * force, ForceMode.Impulse);
        }

        CanPush = false;
    }
}

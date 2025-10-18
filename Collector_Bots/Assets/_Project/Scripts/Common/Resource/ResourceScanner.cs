using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private Collider _scanCollider;

    private ResourceLifecycle _resourceLifecycle;

    public void Init(ResourceLifecycle resourceLifecycle)
    {
        _resourceLifecycle = resourceLifecycle;
    }

    public IReadOnlyList<Resource> FindAllAvailableResources()
    {
        return _resourceLifecycle.GetActiveResources();
    }
    
    public Resource FindNearest(Vector3 position)
    {
        var resources = _resourceLifecycle.GetActiveResources()
            .Where(r => !r.IsReserved)
            .ToList();

        if (resources.Count == 0) return null;

        Resource nearest = null;
        float minDistance = float.MaxValue;

        foreach (Resource resource in resources)
        {
            float distance = Vector3.Distance(position, resource.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = resource;
            }
        }

        return nearest;
    }
}

using UnityEngine;

public class ResourceDeliveredHandler
{
    private ResourceLifecycle _resourceLifecycle;

    public void Init(ResourceLifecycle resourceLifecycle)
    {
        _resourceLifecycle = resourceLifecycle;
    }
    
    public void OnResourceDeliveryHandler(Resource resource)
    {
        resource.IsReserved = false; // ← освобождаем
        _resourceLifecycle.ReturnResourceToPool(resource);
    }
}

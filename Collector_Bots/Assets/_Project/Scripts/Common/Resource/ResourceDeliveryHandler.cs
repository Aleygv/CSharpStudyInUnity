using UnityEngine;

public class ResourceDeliveryHandler
{
    private Base _base;
    private ResourceLifecycle _resourceLifecycle;

    public void Init(Base baseInstance, ResourceLifecycle resourceLifecycle)
    {
        _base = baseInstance;
        _resourceLifecycle = resourceLifecycle;
        _base.OnResourceDelivered += OnResourceDeliveryHandler;
    }
    
    private void OnResourceDeliveryHandler(Resource resource)
    {
        resource.IsReserved = false; // ← освобождаем
        _resourceLifecycle.ReturnResourceToPool(resource);
    }
}

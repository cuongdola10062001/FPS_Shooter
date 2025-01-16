using UnityEngine;

public abstract class ResetMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValue();
    }

    protected virtual void OnEnable()
    {
        this.ResetValueWhenOnEnable();
    }

    protected virtual void OnDisable()
    {
        
    }

    protected virtual void LoadComponents()
    {

    }

    protected virtual void ResetValue()
    {

    }

    protected virtual void ResetValueWhenOnEnable()
    {

    }
}

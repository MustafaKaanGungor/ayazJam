using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHp;
    [SerializeField] private float maximumHp;
    [SerializeField] private float invulnerabilityPeriod;

    public UnityEvent<float> damaged;
    public UnityEvent<float, float> healthUpdated;
    public UnityEvent death;

    private float lastDamaged;
    private float current;
    
    public float Current
    {
        get => current;
        private set
        {
            current = Mathf.Clamp(value, 0, Max);
            healthUpdated?.Invoke(current, Max);
        }
    }
    
    public float Max 
    { 
        get => maximumHp; 
        private set => maximumHp = Mathf.Clamp(value, 0, float.MaxValue); 
    }

    public bool IsDead => Current <= 0 ;

    private bool IsVulnerable => Time.time - lastDamaged >= invulnerabilityPeriod;
    
    private void Awake()
    {
        Current = startingHp;
        lastDamaged = -invulnerabilityPeriod;
    }

    public void Damage(float damage)
    {
        if(damage <= 0)
            return;
        
        if(!IsVulnerable)
            return;
        
        if (IsDead)
            return;
        
        Current -= damage;
        lastDamaged = Time.time;
        damaged?.Invoke(damage);
        
        if(IsDead)
            death?.Invoke();
    }
    
    public void Heal(float healing)
    {
        if(healing <= 0)
            return;
        
        if(IsDead)
            return;

        Current += healing;
    }
    
    public void SetMaxHealth(float newMax)
    {
        if (newMax <= 0)
            return;
        
        Max = newMax;
        
        if(Current > Max)
            Current = Max;
    }

    private void OnValidate()
    {
        if(maximumHp <= 0)
        {
            Debug.LogWarning($"{gameObject.name}'s Maximum HP needs to be greater than 0");
            return;
        }
        
        if(startingHp > maximumHp)
        {
            Debug.LogWarning($"{gameObject.name}'s Starting HP is more than Maximum HP. Updating MaxHP");
            
            //Not sure I like this functionality. Can be removed if its more annoying than helpful
            maximumHp = startingHp;
        }
    }

    [ContextMenu("TakeRandomDamage")]
    private void DebugTakeRandomDamageAmount()
    {
        var dmg = Random.Range(1f, 10f);
        Damage(dmg);
    } 
}
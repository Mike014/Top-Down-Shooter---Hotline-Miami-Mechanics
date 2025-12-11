using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int hp = 100;
    private PlayerAnimationHandler _animController;
    private EnemiesAnimationHandler _enemyController;

    void Start()
    {
        _animController = GetComponentInChildren<PlayerAnimationHandler>();
        _enemyController = GetComponent<EnemiesAnimationHandler>();
        
    }

    public int GetHp() => hp;

    public void SetHp(int value)
    {
        hp = Mathf.Clamp(value, 0, maxHp); // Mantiene la vita da 0 a maxHp cosi' da non andare sotto lo 0 o sopra maxhp
    }

    private void Awake() // Uso l'awake cosi' il maxhp iniziale rimane 999 anche se da inspector metto di piu'
    {
        maxHp = Mathf.Min(maxHp, 999); // Max 999
        hp = Mathf.Clamp(hp, 0, maxHp); // Inizializzo gli hp tra 0 e maxHp
    }

    // private int GetMaxHp() => maxHp; // Servono solo se dobbiamo richiamarli in altri script (magari se dobbiamo aggiungere power-up o effetti speciali)

    // private void SetMaxHp(int value) // e quindi limitare gli hp massimi a 999
    // {
    //     maxHp = Mathf.Min(value, 999); // inutile al momento
    //     hp = Mathf.Clamp(hp, 0, maxHp); // inutile al momento
    // }

    public void AddHp(int heal)
    {
        SetHp(hp + heal);
        Debug.Log($"HP aumentati di {heal}! Vita attuale: {hp}");
    }

    public void TakeDamage(int damage)
    {
        SetHp(hp - damage);
        Debug.Log($"Danno subito: {damage}. Vita attuale: {hp}");

        if (IsAlive())
        {
            _animController.PlayDamageAnimation();
            _enemyController.PlayDamageAnimation();
        }
        else
        {
            _animController.DeathAnimation();
            _enemyController.DeathAnimation();
        }
    }

    public bool IsAlive()
    {
        if (hp <= 0)
        {
            Debug.Log($"{gameObject} e' stato sconfitto!");
            return false;
        }
        return true;
    }
}
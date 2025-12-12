using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;   //array di armi possibili
    [SerializeField] private int dropChance = 10;    //probabilità di droppare

    private void Awake()
    {
        if (weapons == null || weapons.Length == 0)
            Debug.LogWarning($"Nessuna arma assegnata al drop di {gameObject.name}");
    }

    private bool HasDropped()
    {
        return Random.Range(0, 100) < dropChance;
    }

    public void TryDrop()
    {
        if (HasDropped() && weapons.Length > 0)
        {
            int index = Random.Range(0, weapons.Length);       // sceglie un'arma casuale
            GameObject weaponToDrop = weapons[index];

            Instantiate(weaponToDrop, transform.position, Quaternion.identity);
        }
    }
}

using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private string weaponName;
    [SerializeField] private int damage;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject bulletPrefab; // prefab del proiettile

    private float lastFireTime; // tempo dell’ultimo colpo sparato, serve per gestire il fireRate
    
    private void Awake()
    {
        lastFireTime = -1f / fireRate; // Forza il primo colpo immediato
    }

    public string GetWeaponName() => weaponName;

    // Metodo per sparare in piu' direzioni
    public void ShootAllDirections(Vector2[] directions, Vector3 spawnPosition, bool ignoreFireRate = false)
    {
        // Se non ignoriamo il fire rate e non è ancora passato il tempo sufficiente, esci
        if (!ignoreFireRate && Time.time < lastFireTime + 1f / fireRate) return; 

        lastFireTime = Time.time; // Aggiorna l'orario dell'ultimo sparo

        foreach (Vector2 dir in directions)
        {
            if (bulletPrefab != null)
            {
                GameObject bulletObj = Instantiate(bulletPrefab, spawnPosition, transform.rotation); // Istanzia il proiettile nella posizione dello spawn

                Bullet bullet = bulletObj.GetComponent<Bullet>(); // Imposta direzione e danno sul proiettile

                if (bullet != null)
                    bullet.SetUp(dir, damage);
            }
        }
    }

    public void LevelUp() // Funzione per potenziare l'arma
    {
        damage += 1;          // esempio di aumenta danno
        fireRate += 0.1f;     // esempio di aumenta firerate
        Debug.Log($"{weaponName} potenziata: danno={damage}, fireRate={fireRate}");
    }
}

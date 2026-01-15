using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager; // Riferimento all'inventario del pg
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Camera mainCam;

    private Vector2 mouseDirection = Vector2.right;
    // private float mouseAngle = 0f;

    private void Start()
    {
        mainCam = Camera.main;
        if (weaponManager == null) return;
        // Primo colpo immediato di tutte le armi
        foreach (Weapon weapon in weaponManager.GetAllWeapons())
        {
            if (weapon == null) continue;

            Vector2[] directions = GetWeaponDirections(weapon);

            weapon.ShootAllDirections(directions, transform.position, true); // ignora fireRate per primo colpo

        }
    }

    private void Update()
    {
        if (weaponManager == null) return;

        UpdateMouseDirection();

        weaponManager.RemoveDestroyedWeapons();

        if (Input.GetMouseButtonDown(0))
        {

            // Chiamata a Shoot per tutte le armi ogni frame, lasciando che la singola arma gestisca il fireRate
            foreach (Weapon weapon in weaponManager.GetAllWeapons())
            {
                if (weapon == null) continue;

                Vector2[] directions = GetWeaponDirections(weapon);

                weapon.ShootAllDirections(directions, bulletSpawnPoint.position); // fireRate rispettato internamente
            }
        }
    }

    private void UpdateMouseDirection()
    {
        if (mainCam == null) return;

        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorld - transform.position).normalized;

        if (direction.sqrMagnitude > 0.001f)
        {
            mouseDirection = direction;
            
            // Calcola l'angolo verso il mouse per ruotare le direzioni di sparo
            // mouseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            // Rotazione a 360 gradi su asse Y verso il mouse
            // transform.rotation = Quaternion.Euler(0, mouseAngle, 0);
        }
        
    }

    private Vector2[] GetWeaponDirections(Weapon weapon) //Gestisce gli spari in base alle armi (le ho messe per nome ma volendo possiamo modificare)
    {
        if (weapon == null) return new Vector2[] { Vector2.right };

        string name = weapon.GetWeaponName().ToLower();

        // Spaccatesta: spara avanti E dietro rispetto al mouse
        if (name.Contains("spaccatesta"))
        {
            return new Vector2[] { mouseDirection, -mouseDirection };
        }

        // Veronica: spara perpendicolare alla direzione del mouse
        if (name.Contains("veronica"))
        {
            Vector2 perpendicular = new Vector2(-mouseDirection.y, mouseDirection.x);
            return new Vector2[] { perpendicular, -perpendicular };
        }

        // LaserSonico: segue il mouse
        if (name.Contains("lasersonico"))
        {
            return new Vector2[] { mouseDirection };
        }

        // Base weapon e tutte le altre: sparano verso il mouse
        if (name.Contains("base") || name.Contains("default") || name.Contains("player"))
        {
            return new Vector2[] { mouseDirection };
        }

        // Default fallback
        return new Vector2[] { mouseDirection };
    }

    // private Vector2 RotateDirection(Vector2 direction, float angleInDegrees)
    // {
    //     float rad = angleInDegrees * Mathf.Deg2Rad;
    //     float cos = Mathf.Cos(rad);
    //     float sin = Mathf.Sin(rad);
    //     return new Vector2(direction.x * cos - direction.y * sin, direction.x * sin + direction.y * cos);
    // }
}
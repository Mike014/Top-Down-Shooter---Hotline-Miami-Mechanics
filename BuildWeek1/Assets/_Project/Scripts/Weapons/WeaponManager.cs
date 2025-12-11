using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons = new List<Weapon>(); // Lista delle armi

    public List<Weapon> GetAllWeapons() => weapons; // Da tutte le armi al player

    public void AddWeapon(Weapon newWeapon) // Aggiunge una nuova arma al player
    {
        if (newWeapon == null) return;

        if (!weapons.Contains(newWeapon)) // Evita i duplucati ( da sistemare con il levelup)
        {
            weapons.Add(newWeapon);
            Debug.Log("Arma aggiunta: " + newWeapon.GetWeaponName());
        }
    }
    public void RemoveDestroyedWeapons()
    {
        weapons.RemoveAll(w => w == null); // rimuove armi distrutte dalla lista
    }
}
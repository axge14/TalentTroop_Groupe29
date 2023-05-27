using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "My Game/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string name = "AK-47";
    public float damage = 10f;
    public float range = 100f;

    public float fireRate = 0f;

    public int magazineSize = 20;
    public float reloadTime = 1.5f;

    public GameObject graphics;

    public AudioClip shootSound;
    public AudioClip reloadSound;
}


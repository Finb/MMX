using UnityEngine;

public class VehicleInfo : MonoBehaviour
{
    public int id = 0;
    // 载客量
    public int busload = 0;

    public string vehicleName;

    public UnityEngine.Sprite avatar {
        get {
            return gameObject.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
using UnityEngine;
using System.Collections.Generic;
public class VehicleInfo : MonoBehaviour
{
    public int id = 0;
    // 载客量
    public int busload = 0;

    public string vehicleName;

    //自身重量
    public float weight;
    //载重量
    public float capacity;
    public void refresh()
    {
        weight = 0;
        foreach (var item in vehicleEquipment)
        {
            weight += item.weight;
        }

        capacity = 0;
        foreach (var item in vehicleEquipment)
        {
            if (item is MMX.VehicleEngineEquipment) {
                capacity += (item as MMX.VehicleEngineEquipment).capacity;
            }
        }

        
        
    }

    public MMX.VehicleEquipment[] vehicleEquipment = new MMX.VehicleEquipment[8];
    public UnityEngine.Sprite avatar
    {
        get
        {
            return gameObject.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
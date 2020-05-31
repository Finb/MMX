﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PackageMenuController : BaseUIInputController
{
    public override void Awake()
    {
        base.Awake();
        inputs.UI.A.performed += ctx =>
        {
            ItemsListViewController.Create().show();
        };
        inputs.UI.B.performed += ctx => hide();

        Dictionary<int, VehicleInfo> vehicles = new Dictionary<int, VehicleInfo>();
        for (var index = 0; index < TeamQueue.shared.humans.Count; index++)
        {
            var human = TeamQueue.shared.humans[index].GetComponent<HumanInfo>();
            var vehicle = human.currentTakedVehicle;
            if (vehicle != null && !vehicles.ContainsValue(vehicle))
            {
                vehicles.Add(index, vehicle);
            }
        }

        var vehiclePanel = gameObject.FindObject("Vehicle").GetComponent<RectTransform>();
        for (int i = 0; i < 4; i++)
        {
            var vehicleButton = vehiclePanel.GetChild(i);
            
            if (vehicles.ContainsKey(i))
            {
                var vehicle = vehicles[i];
                vehicleButton.gameObject.FindObject("Image").GetComponent<UnityEngine.UI.Image>().sprite = vehicle.avatar;
                vehicleButton.gameObject.FindObject("Text").GetComponent<UnityEngine.UI.Text>().text = vehicle.name;
            }
            else
            {
                vehicleButton.gameObject.GetComponent<CanvasGroup>().alpha = 0;
                vehicleButton.gameObject.GetComponent<CanvasGroup>().interactable = false;
                vehicleButton.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
        if (vehicles.Count <= 0)
        {
            Destroy(vehiclePanel.gameObject);
        }
    }

    public static PackageMenuController Create()
    {
        var obj = Instantiate(Resources.Load<GameObject>("UI/PackageMenu"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<PackageMenuController>();
    }
    public void show()
    {
        MMX.GameManager.Input.pushTarget(gameObject);
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("Human").GetComponent<RectTransform>());
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("Vehicle").GetComponent<RectTransform>());
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("BorderImage").GetComponent<RectTransform>());
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("Panel").GetComponent<RectTransform>());
        EventSystem.current.SetSelectedGameObject(gameObject.GetComponentInChildren<UnityEngine.UI.Button>().gameObject);
    }
    public void hide()
    {
        MMX.GameManager.Input.popTarget();
        Destroy(gameObject);
    }
}
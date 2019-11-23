using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectButtonBoxController : MonoBehaviour, InputEventInterface
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void willOnFocus()
    {
        foreach (var item in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            item.interactable = true;
        }
    }
    public void willLostFocus()
    {
        foreach (var item in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            item.interactable = false;
        }
    }
    public void inputAction()
    {
        if (MMX.Input.GameButtonPressRecognition.getKeyDown(MMX.Input.GameButton.A))
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        }
        else if (MMX.Input.GameButtonPressRecognition.getKeyDown(MMX.Input.GameButton.B))
        {
            hide();
        }
    }
    public void addButton(string text)
    {
        var button = Resources.Load<GameObject>("UI/按钮");
        button.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        Instantiate(button, Vector3.zero, Quaternion.identity, gameObject.FindObject("ImageBox").transform);
    }

    public static SelectButtonBoxController Create(){
        var obj = Instantiate(Resources.Load<GameObject>("UI/SelectButtonBox"), new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("UI").GetComponent<Transform>());
        return obj.GetComponent<SelectButtonBoxController>();
    }

    public void show()
    {
        MMX.GameManager.Input.pushTarget(gameObject);
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.FindObject("ImageBox").GetComponent<RectTransform>());
        EventSystem.current.SetSelectedGameObject(gameObject.GetComponentInChildren<UnityEngine.UI.Button>().gameObject);
    }
    public void hide()
    {
        MMX.GameManager.Input.popTarget();
        Destroy(gameObject);
    }
}

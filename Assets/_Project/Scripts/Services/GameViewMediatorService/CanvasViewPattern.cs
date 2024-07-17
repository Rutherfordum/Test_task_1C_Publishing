using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasViewPattern: MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    public TextMeshProUGUI TextMesh;
    public Button Button;

    public void OnValidate()
    {
        _canvas = GetComponent<Canvas>();
        TextMesh = transform.GetComponentInChildren<TextMeshProUGUI>();
        Button = transform.GetComponentInChildren<Button>();
    }

    public void Open()
    {
        _canvas.enabled = true;
    }

    public void Close()
    {
        _canvas.enabled = false;
    }
}
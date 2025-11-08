using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadConnectionNotifierSimple : MonoBehaviour
{
    private string message = "";
    private Color messageColor = Color.white;
    private float messageTimer = 0f;
    private float displayTime = 3f;

    // Asigna aquí una fuente desde el inspector si quieres usar una fuente personalizada
    public Font customFont;

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad)
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    ShowMessage("Se ha conectado un Joystick", Color.blue);
                    break;
                case InputDeviceChange.Removed:
                    ShowMessage("Se ha Desconectado el Joystick", Color.red);
                    break;
            }
        }
    }

    private void ShowMessage(string msg, Color color)
    {
        message = msg;
        messageColor = color;
        messageTimer = displayTime;
    }

    private void Update()
    {
        if (messageTimer > 0)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0)
            {
                message = "";
            }
        }
    }

    private void OnGUI()
    {
        if (!string.IsNullOrEmpty(message))
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 36;                  // Tamaño de fuente más grande
            style.fontStyle = FontStyle.Bold;    // Negrita
            style.normal.textColor = messageColor;
            style.alignment = TextAnchor.UpperCenter;

            if (customFont != null)
            {
                style.font = customFont;          // Fuente personalizada si está asignada
            }

            float width = 600;
            float height = 60;

            // Posición centrada horizontalmente y más abajo en la pantalla (1/6 de altura)
            Rect rect = new Rect((Screen.width - width) / 2, Screen.height / 6, width, height);

            GUI.Label(rect, message, style);
        }
    }
}

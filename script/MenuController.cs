using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private bool mostrarMensaje = false;

    private string mensaje = "Aventura de exploración entre distintos mundos, descubriendo caminos, secretos y momentos de diversión.";

    public void Jugar()
    {
        SceneManager.LoadScene("Juego");
    }

    public void SobreLaAplicacion()
    {
        mostrarMensaje = !mostrarMensaje;
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }

    private void OnGUI()
    {
        if (mostrarMensaje)
        {
            // Fondo negro opaco para bloquear menú
            Color fondoNegroOscuro = new Color(0f, 0f, 0f, 0.9f);
            GUI.backgroundColor = fondoNegroOscuro;

            Rect rect = new Rect(0, 0, Screen.width, Screen.height);
            GUI.Box(rect, "");

            // Estilo texto blanco, alineado a la izquierda, con padding
            GUIStyle styleTexto = new GUIStyle(GUI.skin.label);
            styleTexto.alignment = TextAnchor.UpperLeft;  // Texto alineado a la izquierda
            styleTexto.fontSize = 48;
            styleTexto.wordWrap = true;
            styleTexto.padding = new RectOffset(20, 20, 20, 20);
            styleTexto.normal.textColor = Color.white;

            // Ancho reducido a 30% del ancho pantalla para texto
            float mensajeAncho = Screen.width * 0.3f;
            float textoAlto = Screen.height - 500;
            float textoX = 50;  // Margen fijo a la izquierda
            Rect textoRect = new Rect(textoX, 400, mensajeAncho, textoAlto);
            GUI.Label(textoRect, mensaje, styleTexto);

            // Botón rojo con texto blanco alineado a la izquierda justo debajo del texto con margen 10
            GUIStyle styleBoton = new GUIStyle(GUI.skin.button);
            styleBoton.fontSize = 48;
            styleBoton.normal.textColor = Color.white;
            styleBoton.hover.textColor = new Color(1f, 0.7f, 0.7f);
            styleBoton.active.textColor = Color.white;
            styleBoton.alignment = TextAnchor.MiddleCenter;

            float btnAncho = 320;
            float btnAlto = 90;
            float btnX = 50;  // Misma posición que el texto, margen a la izquierda
            float btnY = 400 + textoAlto - btnAlto - 10;
            Rect btnRect = new Rect(btnX, btnY, btnAncho, btnAlto);

            Color colorAnterior = GUI.backgroundColor;
            GUI.backgroundColor = new Color(1f, 0f, 0f, 1f); // rojo sólido botón

            if (GUI.Button(btnRect, "Cerrar", styleBoton))
            {
                mostrarMensaje = false;
            }

            GUI.backgroundColor = colorAnterior;
        }
    }
}

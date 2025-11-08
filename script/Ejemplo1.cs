//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class PlayerController : MonoBehaviour
//{
//    MeshRenderer mesh;
//    [SerializeField] private float moveSpeed = 5f;
//    [SerializeField] private float runSpeed = 10f;
//    [SerializeField] float jumpForce = 3.0f;
//    Rigidbody rigi;
//    PlayerInput inputMaps;
//    float hMouse, vMouse;
//    [SerializeField] float mouse_horizontal = 0.1f;
//    [SerializeField] float mouse_vertical = 0.1f;
//    [SerializeField] float maxRotationLookUp = -25.0f;
//    [SerializeField] float maxRotationLookDown = 25.0f;
//    [SerializeField] private float interactDistance = 2f;
//    private GameObject objetoAgarrado = null;
//    private bool isRunning = false;
//    public Text legacyText;

//    private Collider playerCollider;

//    // Referencia a la imagen en el Canvas que se mostrará/ocultará al comer
//    public Image imagenComer;

//    void Start()
//    {
//        mesh = GetComponent<MeshRenderer>();
//        rigi = GetComponent<Rigidbody>();
//        inputMaps = GetComponent<PlayerInput>();
//        playerCollider = GetComponent<Collider>(); // Collider del jugador
//        if (legacyText != null)
//            legacyText.text = "";

//        // Asegurar que la imagen está inicialmente desactivada
//        if (imagenComer != null)
//            imagenComer.gameObject.SetActive(false);
//    }

//    void Update()
//    {
//        MovePlayerInput();
//        MoveCamera();

//        if (Input.GetKeyDown("q"))
//            mesh.enabled = false;
//        if (Input.GetKeyDown("e"))
//            mesh.enabled = true;

//        UpdateInteractionText();
//        HandleBackInput();
//    }

//    void HandleBackInput()
//    {
//        string currentScene = SceneManager.GetActiveScene().name;

//        if (Input.GetKeyDown(KeyCode.JoystickButton6))
//        {
//            if (currentScene == "Juego")
//                SceneManager.LoadScene("Menu");
//            else if (currentScene == "Menu")
//                Application.Quit();
//#if UNITY_EDITOR
//            UnityEditor.EditorApplication.isPlaying = false;
//#endif
//        }
//        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
//        {
//            if (currentScene == "Juego")
//                SceneManager.LoadScene("Menu");
//            else if (currentScene == "Menu")
//                Application.Quit();
//#if UNITY_EDITOR
//            UnityEditor.EditorApplication.isPlaying = false;
//#endif
//        }
//        else if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
//        {
//            if (currentScene == "Juego")
//                SceneManager.LoadScene("Menu");
//            else if (currentScene == "Menu")
//                Application.Quit();
//#if UNITY_EDITOR
//            UnityEditor.EditorApplication.isPlaying = false;
//#endif
//        }
//    }

//    public void Jump(InputAction.CallbackContext callbackContext)
//    {
//        if (callbackContext.performed)
//            rigi.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//    }

//    public void OnCorrer(InputAction.CallbackContext context)
//    {
//        if (context.performed)
//            isRunning = true;
//        else if (context.canceled)
//            isRunning = false;
//    }

//    void MovePlayerInput()
//    {
//        Vector2 inputs = inputMaps.actions["Move"].ReadValue<Vector2>();
//        Vector3 move = (transform.forward * inputs.y + transform.right * inputs.x);
//        move.y = 0;
//        float currentSpeed = isRunning ? runSpeed : moveSpeed;
//        transform.position += move.normalized * currentSpeed * Time.deltaTime;
//    }

//    void MoveCamera()
//    {
//        Vector2 inputs = inputMaps.actions["MoveCamera"].ReadValue<Vector2>();
//        hMouse = mouse_horizontal * inputs.x;
//        vMouse += mouse_vertical * inputs.y;
//        vMouse = Mathf.Clamp(vMouse, maxRotationLookUp, maxRotationLookDown);
//        Camera.main.transform.localEulerAngles = new Vector3(-vMouse, 0, 0);
//        transform.Rotate(0, hMouse, 0);
//    }

//    public void OnAbrir(InputAction.CallbackContext context)
//    {
//        if (context.performed)
//        {
//            RaycastHit hit;
//            if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
//            {
//                GameObject hitObject = hit.collider.gameObject;
//                if (hitObject.name == "mesh-1.009")
//                {
//                    MeshRenderer doorMesh = hitObject.GetComponent<MeshRenderer>();
//                    if (doorMesh != null)
//                        doorMesh.enabled = !doorMesh.enabled;
//                }
//                else if (hitObject.name == "Bathroom Vanity")
//                {
//                    AudioSource audioSource = hitObject.GetComponent<AudioSource>();
//                    if (audioSource != null)
//                        audioSource.Play();
//                    else
//                        Debug.LogWarning("El objeto 'Bathroom Vanity' no tiene AudioSource asignado.");
//                }
//            }
//        }
//    }

//    public void OnAgarrar(InputAction.CallbackContext context)
//    {
//        if (context.performed)
//        {
//            if (objetoAgarrado == null)
//            {
//                RaycastHit hit;
//                if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
//                {
//                    if (hit.collider.gameObject.name.StartsWith("Sphere"))
//                    {
//                        objetoAgarrado = hit.collider.gameObject;
//                        Rigidbody rb = objetoAgarrado.GetComponent<Rigidbody>();
//                        Collider esferaCollider = objetoAgarrado.GetComponent<Collider>();
//                        if (rb != null)
//                        {
//                            rb.isKinematic = true;
//                        }
//                        if (playerCollider != null && esferaCollider != null)
//                        {
//                            Physics.IgnoreCollision(playerCollider, esferaCollider, true);
//                        }
//                        objetoAgarrado.transform.SetParent(transform);
//                        objetoAgarrado.transform.localPosition = new Vector3(0, 0, 1);
//                    }
//                }
//            }
//            else
//            {
//                Rigidbody rb = objetoAgarrado.GetComponent<Rigidbody>();
//                Collider esferaCollider = objetoAgarrado.GetComponent<Collider>();
//                if (rb != null)
//                    rb.isKinematic = false;
//                if (playerCollider != null && esferaCollider != null)
//                    Physics.IgnoreCollision(playerCollider, esferaCollider, false);
//                objetoAgarrado.transform.SetParent(null);
//                objetoAgarrado = null;
//            }
//        }
//    }

//    // Función modificada para mostrar/ocultar la imagen en el Canvas al presionar C/R1 (toggle)
//    public void OnComer(InputAction.CallbackContext context)
//    {
//        if (context.performed)
//        {
//            if (imagenComer != null)
//            {
//                bool isActive = imagenComer.gameObject.activeSelf;
//                imagenComer.gameObject.SetActive(!isActive);
//            }
//        }
//    }

//    void UpdateInteractionText()
//    {
//        if (legacyText == null) return;
//        RaycastHit hit;
//        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
//        {
//            string name = hit.collider.gameObject.name;
//            if (name == "Hamburger")
//            {
//                legacyText.text = "Presiona 'C'/'R1' para comer";
//                return;
//            }
//            else if (name.StartsWith("Sphere"))
//            {
//                if (objetoAgarrado == null)  // Solo mostrar texto si no hay objeto agarrado
//                {
//                    legacyText.text = "Presiona 'R'/'▲' para Coger Objeto";
//                    return;
//                }
//            }
//            else if (name == "mesh-1.009")
//            {
//                legacyText.text = "Presiona 'E'/'○' para abrir";
//                return;
//            }
//            else if (name == "Bathroom Vanity")
//            {
//                legacyText.text = "Presiona 'E'/'○' para lavarse";
//                return;
//            }
//        }
//        legacyText.text = "";
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    MeshRenderer mesh;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] float jumpForce = 3.0f;
    Rigidbody rigi;
    PlayerInput inputMaps;
    float hMouse, vMouse;
    [SerializeField] float mouse_horizontal = 0.1f;
    [SerializeField] float mouse_vertical = 0.1f;
    [SerializeField] float maxRotationLookUp = -25.0f;
    [SerializeField] float maxRotationLookDown = 25.0f;
    [SerializeField] private float interactDistance = 2f;
    private GameObject objetoAgarrado = null;
    private bool isRunning = false;
    public Text legacyText;

    private Collider playerCollider;

    // Referencia a la imagen en el Canvas que se mostrará/ocultará al comer
    public Image imagenComer;

    // === ADDITION for jump cooldown ===
    private bool canJump = true;
    private float jumpCooldown = 0.5f;
    private float jumpTimer = 0f;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        rigi = GetComponent<Rigidbody>();
        inputMaps = GetComponent<PlayerInput>();
        playerCollider = GetComponent<Collider>(); // Collider del jugador
        if (legacyText != null)
            legacyText.text = "";

        // Asegurar que la imagen está inicialmente desactivada
        if (imagenComer != null)
            imagenComer.gameObject.SetActive(false);
    }

    void Update()
    {
        MovePlayerInput();
        MoveCamera();

        //if (Input.GetKeyDown("q"))
         //   mesh.enabled = false;
        //if (Input.GetKeyDown("e"))
          //  mesh.enabled = true;

        UpdateInteractionText();
        HandleBackInput();

        // === ADDITION for jump cooldown timer ===
        if (!canJump)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0f)
            {
                canJump = true;
            }
        }
    }

    void HandleBackInput()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            if (currentScene == "Juego")
                SceneManager.LoadScene("Menu");
            else if (currentScene == "Menu")
                Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if (currentScene == "Juego")
                SceneManager.LoadScene("Menu");
            else if (currentScene == "Menu")
                Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentScene == "Juego")
                SceneManager.LoadScene("Menu");
            else if (currentScene == "Menu")
                Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        // === MODIFIED to check jump cooldown ===
        if (callbackContext.performed && canJump)
        {
            rigi.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            jumpTimer = jumpCooldown;
        }
    }

    public void OnCorrer(InputAction.CallbackContext context)
    {
        if (context.performed)
            isRunning = true;
        else if (context.canceled)
            isRunning = false;
    }

    void MovePlayerInput()
    {
        Vector2 inputs = inputMaps.actions["Move"].ReadValue<Vector2>();
        Vector3 move = (transform.forward * inputs.y + transform.right * inputs.x);
        move.y = 0;
        float currentSpeed = isRunning ? runSpeed : moveSpeed;
        transform.position += move.normalized * currentSpeed * Time.deltaTime;
    }

    void MoveCamera()
    {
        Vector2 inputs = inputMaps.actions["MoveCamera"].ReadValue<Vector2>();
        hMouse = mouse_horizontal * inputs.x;
        vMouse += mouse_vertical * inputs.y;
        vMouse = Mathf.Clamp(vMouse, maxRotationLookUp, maxRotationLookDown);
        Camera.main.transform.localEulerAngles = new Vector3(-vMouse, 0, 0);
        transform.Rotate(0, hMouse, 0);
    }

    public void OnAbrir(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.name == "mesh-1.009")
                {
                    MeshRenderer doorMesh = hitObject.GetComponent<MeshRenderer>();
                    if (doorMesh != null)
                        doorMesh.enabled = !doorMesh.enabled;
                }
                else if (hitObject.name == "Bathroom Vanity")
                {
                    AudioSource audioSource = hitObject.GetComponent<AudioSource>();
                    if (audioSource != null)
                        audioSource.Play();
                    else
                        Debug.LogWarning("El objeto 'Bathroom Vanity' no tiene AudioSource asignado.");
                }
            }
        }
    }

    public void OnAgarrar(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (objetoAgarrado == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
                {
                    if (hit.collider.gameObject.name.StartsWith("Sphere"))
                    {
                        objetoAgarrado = hit.collider.gameObject;
                        Rigidbody rb = objetoAgarrado.GetComponent<Rigidbody>();
                        Collider esferaCollider = objetoAgarrado.GetComponent<Collider>();
                        if (rb != null)
                        {
                            rb.isKinematic = true;
                        }
                        if (playerCollider != null && esferaCollider != null)
                        {
                            Physics.IgnoreCollision(playerCollider, esferaCollider, true);
                        }
                        objetoAgarrado.transform.SetParent(transform);
                        objetoAgarrado.transform.localPosition = new Vector3(0, 0, 1);
                    }
                }
            }
            else
            {
                Rigidbody rb = objetoAgarrado.GetComponent<Rigidbody>();
                Collider esferaCollider = objetoAgarrado.GetComponent<Collider>();
                if (rb != null)
                    rb.isKinematic = false;
                if (playerCollider != null && esferaCollider != null)
                    Physics.IgnoreCollision(playerCollider, esferaCollider, false);
                objetoAgarrado.transform.SetParent(null);
                objetoAgarrado = null;
            }
        }
    }

    // Función modificada para mostrar/ocultar la imagen en el Canvas al presionar C/R1 (toggle)
    public void OnComer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (imagenComer != null)
            {
                bool isActive = imagenComer.gameObject.activeSelf;
                imagenComer.gameObject.SetActive(!isActive);
            }
        }
    }

    void UpdateInteractionText()
    {
        if (legacyText == null) return;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
        {
            string name = hit.collider.gameObject.name;
            if (name == "Hamburger")
            {
                legacyText.text = "Presiona 'C'/'R1' para comer";
                return;
            }
            else if (name.StartsWith("Sphere"))
            {
                if (objetoAgarrado == null)  // Solo mostrar texto si no hay objeto agarrado
                {
                    legacyText.text = "Presiona 'R'/'▲' para Coger Objeto";
                    return;
                }
            }
            else if (name == "mesh-1.009")
            {
                legacyText.text = "Presiona 'E'/'○' para abrir";
                return;
            }
            else if (name == "Bathroom Vanity")
            {
                legacyText.text = "Presiona 'E'/'○' para lavarse";
                return;
            }
        }
        legacyText.text = "";
    }
}




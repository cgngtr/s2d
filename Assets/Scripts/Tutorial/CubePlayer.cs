using UnityEngine;
using UnityEngine.InputSystem;
public class CubePlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    private Vector2 moveInput = Vector2.zero;
    public void Update()
    {
        Vector3 movement = new Vector3(moveInput.x , moveInput.y, 0) * moveSpeed * Time.deltaTime; // Multiplying outside so we don't have to do it twice.
        transform.Translate(movement);
    }
    public void ChangeColour()
    {
        var color = Color.HSVToRGB(Random.value, 0.8f, 1f);
        GetComponent<Renderer>().material.color = color;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // Fetching & storing.
        Debug.Log(moveInput);
        Debug.Log(context);
    }

    public void OnChangeColour(InputAction.CallbackContext context)
    {
        // Importance of checking if "performed" here is because we don't want it to execute two times, pressing and releasing.
        if (context.performed) 
        {
            ChangeColour();
        }
    }
}
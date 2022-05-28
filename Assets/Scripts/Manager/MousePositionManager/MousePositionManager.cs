using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePositionManager : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private LayerMask aimColliderLayerMask;
    
    private RaycastHit raycastHit;
    private GameObject player;
    private ThirdPersonController playerController;
    private Vector3 MousePosition;

    public float AngleBetweenMouseAndPlayer { get; set; }

    private void Start()
    {
        player = input.gameObject;
        playerController = player.GetComponent<ThirdPersonController>();
    }

    public float GetAngleBetweenMouseAndPlayer()
    {
        Vector2 vector = player.GetComponent<StarterAssets.StarterAssetsInputs>().move;
        var second = new Vector3(player.transform.position.x, 0, player.transform.position.z) + new Vector3(vector.x, player.transform.position.y, vector.y);
        Vector3 targetDir = second - player.transform.position;
        float angle = Vector3.Angle(targetDir, player.transform.forward);
        AngleBetweenMouseAndPlayer = angle;
        return angle;
    }

    // MOUSEUPDATE
    public Vector3 GetMousePosition()
    {
        OnRaycastSystem();
        return MousePosition;
    }

    private void OnRaycastSystem()
    {
        Ray ray = Camera.main.ScreenPointToRay(CalculateMousePosition());
        if (Physics.Raycast(ray, out raycastHit, 999f, aimColliderLayerMask))
        {
            MousePosition = raycastHit.point;
        }
    }

    private Vector3 CalculateMousePosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = player.transform.position.z;
        return mousePos;
    }

    public void LookAtMouseDirection() 
    {
        GetMousePosition();
        playerController.isAtacking = true;
        Vector3 direction = new Vector3(MousePosition.x, player.transform.position.y, MousePosition.z);
        player.transform.LookAt(Vector3.Lerp(player.transform.position, direction, 1f));
        GetAngleBetweenMouseAndPlayer();
    }

    public void StopLookingAtMouseDirection()
    {
        playerController.isAtacking = false;
    }
}

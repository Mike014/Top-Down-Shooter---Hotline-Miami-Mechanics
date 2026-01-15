using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private string _verticalSpeedName = "vSpeed";
    [SerializeField] private string _horizontalSpeedName = "hSpeed";

    [Header("Mouse Rotation")]
    [SerializeField] private bool rotateTowardMouse = true;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Animator _animator;
    private Camera mainCam;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();

        if(spriteRenderer == null)
           spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        mainCam = Camera.main;
    }

    void Update()
    {
        if (rotateTowardMouse)
        {
            rotateTowardsMouse();
        }
    }

    private void rotateTowardsMouse()
    {
        if (mainCam == null) return;

        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorld - transform.position).normalized;

        if (direction.sqrMagnitude > 0.001f)
        {
            // Rotazione a 360 gradi su asse Y verso il mouse
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void SetVerticalSpeed(float speed)
    {
        _animator.SetFloat(_verticalSpeedName, speed);
    }

    private void SetHorizontalSpeed(float speed)
    {
        _animator.SetFloat(_horizontalSpeedName, speed);
    }

    public void MovementAnimation(Vector2 speed)
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            SetVerticalSpeed(speed.y);
            SetHorizontalSpeed(speed.x);
        }
    }

    public void PlayDamageAnimation()
    {
        _animator.SetBool("isDamaged", true);
    }

    public void StopDamageAnimation()
    {
        _animator.SetBool("isDamaged", false);
    }

    public void DeathAnimation()
    {
        _animator.SetBool("isDead", true);
    }

    public void DeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}

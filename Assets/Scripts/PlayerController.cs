using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float scrollSpeed;
    public float maxSpeed;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = Vector2.right * new Vector2(scrollSpeed, 0);
    }

    private void Update()
    {
        _rigidbody2D.velocity = new Vector2(
            x: _rigidbody2D.velocity.x,
            y: Input.GetAxis("Vertical") * maxSpeed);
    }

    private void OnCollisionEnter2D()
    {
        transform.position = new Vector3(-7.66f, -0.03f, 0);
        _rigidbody2D.velocity = Vector2.right * new Vector2(scrollSpeed, 0);
    }
}

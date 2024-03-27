using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float scrollSpeed;
    public float maxSpeed;
    public float duration;
    public GameObject pipelinePrefab;


    private Rigidbody2D _rigidbody2D;
    private readonly Queue<KeyValuePair<GameObject, GameObject>> _pipelines = new();
    private float _time = -8;

    private void Start()
    {
        var position = transform.position;
        for (var i = 1; i < 6; i++)
        {
            var y = Random.Range(0, 10);
            var top = Instantiate(
                pipelinePrefab,
                new Vector2(position.x + i * 10, y),
                new Quaternion(0, 0, 0, 0));
            var bottom = Instantiate(
                pipelinePrefab,
                new Vector2(position.x + i * 10, y - 10),
                new Quaternion(0, 0, 0, 0));

            _pipelines.Enqueue(new KeyValuePair<GameObject, GameObject>(top, bottom));
        }
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = Vector2.right * new Vector2(scrollSpeed, 0);
    }

    private void Update()
    {
        _rigidbody2D.velocity = new Vector2(
            x: _rigidbody2D.velocity.x,
            y: Input.GetAxis("Vertical") * maxSpeed);
        _time += Time.deltaTime;
        if (!(_time >= duration)) return;
        var pipeline = _pipelines.Dequeue();
        var x = _pipelines.Last().Key.transform.position.x + 10;
        var y = Random.Range(0, 10);
        pipeline.Key.transform.position = new Vector3(x, y);
        pipeline.Value.transform.position = new Vector3(x, y - 10);
        _pipelines.Enqueue(pipeline);
        _time = 0;
    }

    private void OnCollisionEnter2D()
    {
        transform.position = new Vector3(-7.66f, -0.03f, 0);
        _rigidbody2D.velocity = Vector2.right * new Vector2(scrollSpeed, 0);
    }
}

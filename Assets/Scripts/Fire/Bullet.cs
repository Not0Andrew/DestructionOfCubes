using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 _start;
    private Vector3 _end;
    
    public AnimationCurve curve;
    private float _time;
    
    public void SetTarget(Vector3 start, Vector3 end)
    {
        transform.position = start;
        
        _start = start;
        _end = new Vector3(end.x, 0, end.z);
        
        gameObject.SetActive(true);
    }

    private void Update()
    {
        _time += Time.deltaTime * speed;
        
        Vector3 pos = Vector3.Lerp(_start, _end, _time);

        pos.y *= curve.Evaluate(_time);
        
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag($"Cube"))
        {
            other.GetComponent<Cube>().Death();
        }

        _time = 0;
        gameObject.SetActive(false);
    }
}

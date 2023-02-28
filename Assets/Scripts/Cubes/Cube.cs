using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    private Vector3 _target;

    [SerializeField] private float speed;
    [SerializeField] private float minTimeToSprint, maxTimeToSprint;

    private IEnumerator _sprint = null;
    private bool _isSprint = false;

    private void Start()
    {
        _target = CubesController.Instance.GetRandomPosition();
    }

    private void Update()
    {
        if (!_isSprint)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * speed);

            if (transform.position == _target)
            {
                _target = CubesController.Instance.GetRandomPosition();

                RotateToTarget();
            }

            if (_sprint == null)
            {
                _sprint = Sprint();
                StartCoroutine(_sprint);
            }
        }
    }

    private IEnumerator Sprint()
    {
        yield return new WaitForSeconds(Random.Range(minTimeToSprint, maxTimeToSprint));

        _isSprint = true;
        
        for (float i = 0; i < 1; i += Time.deltaTime * 3)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            
            transform.position = Vector3.Lerp(transform.position, _target, i);
        }
        
        _isSprint = false;
        _sprint = null;
    }
    
    private void RotateToTarget()
    {
        Vector3 direction = (_target - transform.position).normalized;

        Quaternion  lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;
 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1);
    }
    
    public void Death()
    {
        UIController.Instance.AddKill();
        CubesController.Instance.CallRespawn(gameObject);
        
        _target = CubesController.Instance.GetRandomPosition();

        _isSprint = false;
        _sprint = null;
        
        gameObject.SetActive(false);
    }
}

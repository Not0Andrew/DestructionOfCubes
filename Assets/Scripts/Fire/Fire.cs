using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private Pool bullets;
    
    [SerializeField] private float reloadTime;
    private bool _isFire = true;
    
    private Camera _camera;

    private void Awake()
    {
        bullets.Initialize();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isFire)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast (ray, out var hit))
            {
                var bullet = bullets.GetObject();

                if (bullet != null)
                {
                    bullet.SetTarget(_camera.transform.position, hit.point);

                    StartCoroutine(Reload());
                }
            }
        }
    }

    private IEnumerator Reload()
    {
        _isFire = false;
        
        yield return new WaitForSeconds(reloadTime);
        
        _isFire = true;
    }
}

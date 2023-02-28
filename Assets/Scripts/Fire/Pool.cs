using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    
    [SerializeField] private int maxCountBullet;
    [SerializeField] private float reloadTime;
    
    private readonly List<Bullet> _items = new List<Bullet>();
    
    public void Initialize()
    {
        for (int i = 0; i < maxCountBullet; i++)
        {
            AddObject();
        }
    }

    private void AddObject()
    {
        var temp = Instantiate(bulletPrefab);
        
        _items.Add(temp);
        temp.gameObject.SetActive(false);
    }

    public Bullet GetObject()
    {
        foreach (var item in _items)
        {
            if (item.gameObject.activeInHierarchy == false)
            {
                StartCoroutine(TimeRemoveFromPool(item));
                
                return item;
            }
        }

        return null;
    }

    private IEnumerator TimeRemoveFromPool(Bullet bullet)
    {
        _items.Remove(bullet);
        UIController.Instance.RemoveBullet();

        yield return new WaitForSeconds(reloadTime);

        _items.Add(bullet);
        UIController.Instance.AddBullet();
    }
}

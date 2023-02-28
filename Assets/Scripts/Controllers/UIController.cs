using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    private int _killings;
    private int _bulletCount = 4;

    [SerializeField] private TextMeshProUGUI killingsText;
    [SerializeField] private List<GameObject> bulletCells;

    private void Awake()
    {
        Instance = this;
    }

    public void AddKill()
    {
        _killings++;
        
        killingsText.SetText("Killings : " + _killings);
    }
    
    public void RemoveBullet()
    {
        _bulletCount--;

        UpdateBulletUI();
    }
    
    public void AddBullet()
    {
        _bulletCount++;

        UpdateBulletUI();
    }

    private void UpdateBulletUI()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i <= _bulletCount)
            {
                bulletCells[i].SetActive(true);
            }
            else
            {
                bulletCells[i].SetActive(false);
            }
        }
    }
}

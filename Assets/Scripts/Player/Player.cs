using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject bulletPrefab;
    
    public float shootRate;
    
    private List<int> _poss;
    
    public List<Sprite> sprites;
    private SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Shoot), 0, shootRate);
    }
    
    private void Shoot()
    {
        if (_poss == null)
        {
            ChangePos(0);
        }
        foreach (var pos in _poss!)
        {
            Instantiate(bulletPrefab)
                .transform.position = transform.GetChild(pos).position;
        }
    }

    public void ChangePos(int index)
    {
        _spriteRenderer.sprite = sprites[index];
        _poss = new List<int>();
        switch (index)
        {
            case 0:
                _poss.Add(0);
                break;
            case 1:
                _poss.Add(1);
                _poss.Add(2);
                break;
            case 2:
                _poss.Add(1);
                _poss.Add(2);
                _poss.Add(3);
                _poss.Add(4);
                break;
            default:
                _poss.Add(0);
                break;
        }

    }
    
}
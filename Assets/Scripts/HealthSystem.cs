using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int MaxHealth = 3;
    public int Health;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public  void HealthChange(int val)
    {
        Health += val;
    }
}

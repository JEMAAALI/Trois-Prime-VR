﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject, 5f);
    }
}
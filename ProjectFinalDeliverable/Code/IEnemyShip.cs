﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//Akmal and Norleitey Marsya

public interface IEnemyShip
{
   public EnemyShipController Template {  get; }

    public Vector2 SpawnPoint { get; }
}
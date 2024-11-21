using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//Akmal and Norleitey Marsya

public class EnemyShip : IEnemyShip
{
   public EnemyShipController Template {  get; }
    
    public List<Transform> WayPoints { get; }

    public Vector2 SpawnPoint { get; }

    public EnemyShip(EnemyShipController template, List<Transform> waypoints, Vector2 spawn)
    {
        Template = template;
        WayPoints = waypoints;
        SpawnPoint = spawn;
    }
}

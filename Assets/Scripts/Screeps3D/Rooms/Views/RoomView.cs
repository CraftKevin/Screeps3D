﻿using Common;
using UnityEngine;

namespace Screeps3D.Rooms.Views
{
    public class RoomView : MonoBehaviour
    {
        [SerializeField] private TerrainView _terrain;
        [SerializeField] private RoadNetworkView _roadNetwork;
        [SerializeField] private ScaleVis _vis;
        
        public Room Room { get; private set; }
        private IRoomViewComponent[] _viewComponents;

        public void Init(Room room)
        {
            _vis.Show();
            Room = room;
            
            _viewComponents = GetComponentsInChildren<IRoomViewComponent>();
            foreach (var component in _viewComponents)
            {
                component.Init(room);
            }
        }
    }
}
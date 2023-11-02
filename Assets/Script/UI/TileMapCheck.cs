using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace UI
{
    public class TileMapCheck : MonoBehaviour
    {
        private int contactRoad = 0;
        [SerializeField] private TileBase fieldTile;
        public int GetStrongTile(int strongNum,Vector3Int grid,Tilemap tilemap) 
        {
            contactRoad = 0;
            for (int i = -1; i <= 1; i++) 
            {
                for (int k = 1; k >= -1; k--) 
                {
                    if (i == 0 && k == 0) continue;

                    var newGrid = new Vector3Int(grid.x + i, grid.y + k, grid.z);
                    //Debug.Log("調べるタイル"+newGrid);
                    //if (!tilemap.HasTile(newGrid)) contactRoad++;
                    if (tilemap.GetTile(newGrid)!=fieldTile) contactRoad++;
                } 
            
            }


            //Debug.Log("密接タイル" + contactRoad);
            if (contactRoad >= 5) return strongNum + 1;

            return strongNum;
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using UnityEngine.Tilemaps;

public class Pointer : MonoBehaviour
{
    [SerializeField] private TileBase tile;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject chara;

    
    private TileBase CurrentPutTile;
    private void Start()
    {
        PointerObservable();
    }

    private void PointerObservable() 
    {

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ =>
            {
                Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //タイルマップじゃなくてもキャラでも出来そう。
                Vector3Int grid = tilemap.WorldToCell(mouse_position);
                //キャラの場合
                // Vector3 gridChara = tilemap.WorldToCell(mouse_position);
                Debug.Log(grid);
                //キャラの場合
               // Instantiate(chara, grid, Quaternion.identity);
                if (tilemap.HasTile(grid)) 
                {
                    //キャラの場合
                    //Instantiate(chara, gridChara, Quaternion.identity);

                    
                    //現在選ばれているtileを入れる
                    CurrentPutTile = tile;
                    tilemap.SetTile(grid, CurrentPutTile);

                }
            })
            .AddTo(this);
    }
}

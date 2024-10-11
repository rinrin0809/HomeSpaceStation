using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AStar : MonoBehaviour
{
    public struct GridNode
    {
        public Vector2 position { get; set; }//このグリッドのポジション
        public Vector3 parent_index { get; set; }//このグリッドのリンク先　Vector3(x , y , layer)
        public float score { get; set; }// 移動に必要なコスト　＋　現在の位置とゴール地点の距離　Heu + Cost
        public float Heu { get; set; } // 現在の位置とゴール地点の距離　Vector2.Distance(grid_position, goal)
        public float cost { get; set; } // 移動に必要なコスト　cell_size * n 
        public int layer { get; set; }//レイヤー番号

        public override string ToString()
        {
            return "now:" + position + "  parent:" + parent_index + "  Heu:" + Heu + "  score:" + score + "  cost:" + cost + "  layer:" + layer;
        }
    }
    int cell_size = 1;

    //探索する
    public List<Vector3> Serch(Vector2 start_position, Vector2 goal_position, int start_layer, int goal_layer)
    {
        //ポジションをグリッド単位の中心に設定する。
        start_position = NormarizePosition(start_position);
        goal_position = NormarizePosition(goal_position);
        Vector3 goal_index = new Vector3(goal_position.x, goal_position.y, goal_layer);
        //経路完成後のList作成に使う
        Vector3 final_flag;
        //ノード管理用変数
        Vector3 node_index;
        //KeyのVector3は　x,yはポジションでzはレイヤー値
        Dictionary<Vector3, GridNode> open_list;
        open_list = new Dictionary<Vector3, GridNode>();
        Dictionary<Vector3, GridNode> close_list;
        close_list = new Dictionary<Vector3, GridNode>();
        //壁検知に必要なのでcontactFilterを初期化しておく。
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();//デフォルトだとトリガーがヒットしないので全てHitする様にする

        //スタートノードを生成＆諸々初期化
        GridNode node = new GridNode();
        node.position = start_position;
        node.parent_index = (Vector3)start_position + new Vector3(0, 0, start_layer);
        node.Heu = Vector2.Distance(new Vector2(start_position.x, start_position.y), new Vector2(goal_position.x, goal_position.y));
        node.score = node.Heu;
        node.cost = 0;
        node.layer = start_layer;

        final_flag = node.parent_index;
        node_index = new Vector3(0, 0, node.layer) + (Vector3)node.position;
        float parent_layer = start_layer;
        open_list.Add(node_index, node);

        int i = 1;//何回回ったか見る為のデバック変数

        while (open_list?.Count > 0) //オープンリストが空でない
        {
            Debug.Log("スタート:" + i + "回目;" + node.ToString());
            i++;

            //Dictionaryをソートしてノードを更新する
            var sort_list = open_list.OrderBy((score) => score.Value.score);
            node = sort_list.ElementAt(0).Value;
            node_index = sort_list.ElementAt(0).Key;
            parent_layer = node.layer;


            //経路が完成
            if (node_index == goal_index)
            {
                //success_list xyはポジションでzはレイヤー値
                List<Vector3> success_list;
                success_list = new List<Vector3>();
                //ゴールからスタートまでの最短経路を抽出する
                while (node_index != final_flag)
                {
                    success_list.Add(node_index);
                    node = close_list[node.parent_index];
                    Debug.DrawLine(node_index, node.parent_index, Color.red, 3.5f);
                    node_index = node.parent_index;
                }
                success_list.Add(node_index);

                Debug.Log(success_list.Count + "マスでゴール");
                success_list.Reverse();//反転して結果を返す
                return success_list;
            }
            else
            {
                //現在のノードをクローズに移す
                open_list.Remove(node_index);
                close_list.Add(node_index, node);
                //現在のノードに隣接する8方向ノードを調べる
                for (int w = -1; w < 2; w++)
                {
                    for (int h = -1; h < 2; h++)
                    {
                        if (!(w == 0 && h == 0))//自分自身でない
                        {
                            //クローズリストに含まれてなければ
                            if (!close_list.ContainsKey(new Vector3(node_index.x + w, node_index.y + h, node.layer)))
                            {
                                //オープンリストに含まれてなければ
                                if (!open_list.ContainsKey(new Vector3(node_index.x + w, node_index.y + h, node.layer)))
                                {
                                    //コライダー情報をとる
                                    List<Collider2D> col_results = new List<Collider2D>();
                                    var col_pos = new Vector3(node_index.x + w, node_index.y + h, 0);
                                    Physics2D.OverlapBox(col_pos, new Vector2(cell_size * 0.5f, cell_size * 0.5f), 0, contactFilter, col_results);
                                    Debug.Log("  OverlapBox   " + col_results.Count);
                                    bool isContinue = false;
                                    //ここで壁かどうか処理　ここらの処理をごにょごにょしたら動くと思います。
                                    foreach (Collider2D result in col_results)
                                    {
                                        //マップスイッチの場合
                                        if (result.name.Contains("Switch"))
                                        {

                                            //layer id を切り替える
                                            if (result.name.Contains("1F"))
                                            {
                                                node.layer = LayerMask.NameToLayer("Map_1F");
                                            }
                                            if (result.name.Contains("2F"))
                                            {
                                                node.layer = LayerMask.NameToLayer("Map_2F");
                                            }
                                            if (result.name.Contains("3F"))
                                            {
                                                node.layer = LayerMask.NameToLayer("Map_3F");
                                            }
                                            if (open_list.ContainsKey((Vector3)node.position + new Vector3(w, h, node.layer)) || close_list.ContainsKey((Vector3)node.position + new Vector3(w, h, node.layer)))
                                            {
                                                isContinue = true;
                                                break;
                                            }
                                        }
                                        //壁の場合　
                                        //layer id を比較して同じならクローズ
                                        if (result.name.Contains("F_Collider") || result.name.Contains("Dialog"))
                                        {
                                            if (node.layer == result.gameObject.layer)
                                            {
                                                close_list.Add((Vector3)node.position + new Vector3(w, h, node.layer), new GridNode());
                                                isContinue = true;
                                                break;
                                            }
                                        }
                                        Debug.Log("  OverlapBox   " + result.name + ":" + col_pos);
                                    }


                                    if (isContinue)
                                    {
                                        //何もしない。
                                    }
                                    else
                                    {
                                        GridNode next_node = new GridNode();
                                        next_node.position = node.position + new Vector2(w, h);
                                        next_node.parent_index = (Vector3)node.position + new Vector3(0, 0, parent_layer);
                                        next_node.Heu = Vector2.Distance(next_node.position, goal_position);
                                        next_node.cost = node.cost + cell_size;
                                        next_node.score = next_node.Heu + next_node.cost;
                                        next_node.layer = node.layer;

                                        open_list.Add((Vector3)next_node.position + new Vector3(0, 0, node.layer), next_node);
                                        Debug.Log(next_node.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        //ゴールに辿りつけなかった場合
        return new List<Vector3>();
    }


    private Vector3 NormarizePosition(Vector3 target)
    {
        target = new Vector3(Mathf.FloorToInt(target.x) + (cell_size * 0.5f), Mathf.FloorToInt(target.y) + (cell_size * 0.5f), 0);
        return target;
    }



}
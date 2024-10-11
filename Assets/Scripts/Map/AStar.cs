using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AStar : MonoBehaviour
{
    public struct GridNode
    {
        public Vector2 position { get; set; }//���̃O���b�h�̃|�W�V����
        public Vector3 parent_index { get; set; }//���̃O���b�h�̃����N��@Vector3(x , y , layer)
        public float score { get; set; }// �ړ��ɕK�v�ȃR�X�g�@�{�@���݂̈ʒu�ƃS�[���n�_�̋����@Heu + Cost
        public float Heu { get; set; } // ���݂̈ʒu�ƃS�[���n�_�̋����@Vector2.Distance(grid_position, goal)
        public float cost { get; set; } // �ړ��ɕK�v�ȃR�X�g�@cell_size * n 
        public int layer { get; set; }//���C���[�ԍ�

        public override string ToString()
        {
            return "now:" + position + "  parent:" + parent_index + "  Heu:" + Heu + "  score:" + score + "  cost:" + cost + "  layer:" + layer;
        }
    }
    int cell_size = 1;

    //�T������
    public List<Vector3> Serch(Vector2 start_position, Vector2 goal_position, int start_layer, int goal_layer)
    {
        //�|�W�V�������O���b�h�P�ʂ̒��S�ɐݒ肷��B
        start_position = NormarizePosition(start_position);
        goal_position = NormarizePosition(goal_position);
        Vector3 goal_index = new Vector3(goal_position.x, goal_position.y, goal_layer);
        //�o�H�������List�쐬�Ɏg��
        Vector3 final_flag;
        //�m�[�h�Ǘ��p�ϐ�
        Vector3 node_index;
        //Key��Vector3�́@x,y�̓|�W�V������z�̓��C���[�l
        Dictionary<Vector3, GridNode> open_list;
        open_list = new Dictionary<Vector3, GridNode>();
        Dictionary<Vector3, GridNode> close_list;
        close_list = new Dictionary<Vector3, GridNode>();
        //�ǌ��m�ɕK�v�Ȃ̂�contactFilter�����������Ă����B
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();//�f�t�H���g���ƃg���K�[���q�b�g���Ȃ��̂őS��Hit����l�ɂ���

        //�X�^�[�g�m�[�h�𐶐������X������
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

        int i = 1;//��������������ׂ̃f�o�b�N�ϐ�

        while (open_list?.Count > 0) //�I�[�v�����X�g����łȂ�
        {
            Debug.Log("�X�^�[�g:" + i + "���;" + node.ToString());
            i++;

            //Dictionary���\�[�g���ăm�[�h���X�V����
            var sort_list = open_list.OrderBy((score) => score.Value.score);
            node = sort_list.ElementAt(0).Value;
            node_index = sort_list.ElementAt(0).Key;
            parent_layer = node.layer;


            //�o�H������
            if (node_index == goal_index)
            {
                //success_list xy�̓|�W�V������z�̓��C���[�l
                List<Vector3> success_list;
                success_list = new List<Vector3>();
                //�S�[������X�^�[�g�܂ł̍ŒZ�o�H�𒊏o����
                while (node_index != final_flag)
                {
                    success_list.Add(node_index);
                    node = close_list[node.parent_index];
                    Debug.DrawLine(node_index, node.parent_index, Color.red, 3.5f);
                    node_index = node.parent_index;
                }
                success_list.Add(node_index);

                Debug.Log(success_list.Count + "�}�X�ŃS�[��");
                success_list.Reverse();//���]���Č��ʂ�Ԃ�
                return success_list;
            }
            else
            {
                //���݂̃m�[�h���N���[�Y�Ɉڂ�
                open_list.Remove(node_index);
                close_list.Add(node_index, node);
                //���݂̃m�[�h�ɗאڂ���8�����m�[�h�𒲂ׂ�
                for (int w = -1; w < 2; w++)
                {
                    for (int h = -1; h < 2; h++)
                    {
                        if (!(w == 0 && h == 0))//�������g�łȂ�
                        {
                            //�N���[�Y���X�g�Ɋ܂܂�ĂȂ����
                            if (!close_list.ContainsKey(new Vector3(node_index.x + w, node_index.y + h, node.layer)))
                            {
                                //�I�[�v�����X�g�Ɋ܂܂�ĂȂ����
                                if (!open_list.ContainsKey(new Vector3(node_index.x + w, node_index.y + h, node.layer)))
                                {
                                    //�R���C�_�[�����Ƃ�
                                    List<Collider2D> col_results = new List<Collider2D>();
                                    var col_pos = new Vector3(node_index.x + w, node_index.y + h, 0);
                                    Physics2D.OverlapBox(col_pos, new Vector2(cell_size * 0.5f, cell_size * 0.5f), 0, contactFilter, col_results);
                                    Debug.Log("  OverlapBox   " + col_results.Count);
                                    bool isContinue = false;
                                    //�����ŕǂ��ǂ��������@������̏��������ɂ傲�ɂ債���瓮���Ǝv���܂��B
                                    foreach (Collider2D result in col_results)
                                    {
                                        //�}�b�v�X�C�b�`�̏ꍇ
                                        if (result.name.Contains("Switch"))
                                        {

                                            //layer id ��؂�ւ���
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
                                        //�ǂ̏ꍇ�@
                                        //layer id ���r���ē����Ȃ�N���[�Y
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
                                        //�������Ȃ��B
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
        //�S�[���ɒH����Ȃ������ꍇ
        return new List<Vector3>();
    }


    private Vector3 NormarizePosition(Vector3 target)
    {
        target = new Vector3(Mathf.FloorToInt(target.x) + (cell_size * 0.5f), Mathf.FloorToInt(target.y) + (cell_size * 0.5f), 0);
        return target;
    }



}
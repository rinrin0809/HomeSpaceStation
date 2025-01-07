using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SelectGimmick : MonoBehaviour
{
    //public GameObject Book;

    [SerializeField]
    private List<GameObject> BookList = new List<GameObject>();

    [SerializeField]
    private List<GameObject> Bookshelf = new List<GameObject>();

    string inputValue = "";

    [SerializeField]
    int answer = 1234;

    private int Num = 0;

    //移動時のインターバルの時間上限
    const float MAX_TIME = 50.0f;
    //移動時のインターバルの時間
    float time = MAX_TIME;

    int NumCount = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1.0f;
        MoveNum();
    }

    private void MoveNum()
    {
        Time.timeScale = 0.0f;
        time--;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                // 次の行に行く
                if (Num >= BookList.Count)
                {
                    Num %= BookList.Count;
                }
            }
        }

        // 上に移動
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                if (Num < 0)
                {
                    Num += BookList.Count;
                }
            }
        }

        // 左に移動
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                Num -= 1;
                if (Num < 0)
                {
                    // 最初の要素を超えたら最後の要素に戻る
                    Num = BookList.Count - 1;
                }
            }
        }

        // 右キー（D）で次の列に移動
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (time < 0.0f)
            {
                time = MAX_TIME;

                Num += 1;
                if (Num >= BookList.Count)
                {
                    // 最後の要素を超えたら最初の要素に戻る
                    Num = 0;
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            ExportNumber exportNum = BookList[Num].GetComponent<ExportNumber>();
            if (exportNum != null)
            {

                if (inputValue.Length > -1)
                {


                    foreach (GameObject shelf in Bookshelf)
                    {
                        ExportNumber targetshlf = Bookshelf[Num].GetComponent<ExportNumber>();
                        if (targetshlf != null)
                        {
                            Image shelfImage = shelf.GetComponent<Image>();

                            if (shelfImage != null && shelfImage.sprite == null) // 空のImageを探す
                            {
                                Debug.Log("3 - 空のImage発見");

                                // spriteを設定
                                targetshlf.SetSprite(shelf, exportNum.sprite);
                                Debug.Log("spriteを設定しました: " + shelf.name);

                                // BookListの現在の選択アイテムのImageを空にする
                                Image selectedImage = BookList[Num].GetComponent<Image>();
                                if (selectedImage != null)
                                {
                                    float alpha = 0;
                                    Color color = selectedImage.color;
                                    color.a = Mathf.Clamp01(alpha);
                                    selectedImage.color = color;

                                    Debug.Log("BookListの選択オブジェクトのImageを空にしました: " + BookList[Num].name);
                                }


                                break; // 1つ設定したらループを抜ける
                            }

                        }
                    }
                }
            }


        }

        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Backspaceが押されました");
            // Bookshelfの一番大きいインデックスから空ではないスロットを探す
            for (int i = Bookshelf.Count - 1; i >= 0; i--) // 後ろからチェック
            {
                Image shelfImage = Bookshelf[i].GetComponent<Image>();

                if (shelfImage != null && shelfImage.sprite != null) // 空でないスロットを発見
                {
                    Debug.Log($"Bookshelfのスロット {i} に画像があり、削除します");

                    // 画像を削除する
                    shelfImage.sprite = null;
                    Debug.Log($"Bookshelfのスロット {i} を空にしました");

                    // BookListの対応するスロットに戻す
                    if (i < BookList.Count)
                    {
                        Image selectedImage = BookList[i].GetComponent<Image>();
                        if (selectedImage != null && selectedImage.sprite == null) // 元が空の場合のみ戻す
                        {
                            Debug.Log($"BookListのスロット {i} に画像を戻します");

                            selectedImage.sprite = null; // 必要ならspriteを設定
                            Color color = selectedImage.color;
                            color.a = 1.0f; // 不透明に戻す
                            selectedImage.color = color;
                        }
                    }

                    break; // 最初に見つけたスロットで処理終了
                }
                // Bookshelfの対象スロットを取得
                //ExportNumber targetShelf = Bookshelf[Num].GetComponent<ExportNumber>();
                //if (targetShelf != null)
                //{
                //    Image shelfImage = Bookshelf[Num].GetComponent<Image>();

                //    if (shelfImage != null && shelfImage.sprite != null) // Bookshelfが空でない場合
                //    {
                //        Debug.Log("Bookshelfのスロットに画像があり、元に戻します");

                //        // BookListの選択中のスロットに戻す
                //        Image selectedImage = BookList[Num].GetComponent<Image>();
                //        if (selectedImage != null && selectedImage.sprite == null) // 元が空の場合のみ戻す
                //        {
                //            float alpha = 100;
                //            Color color = selectedImage.color;
                //            color.a = Mathf.Clamp01(alpha);
                //            selectedImage.color = color;
                //        }

                //        // Bookshelfのスロットを空にする
                //        shelfImage.sprite = null;
                //        Debug.Log("Bookshelfのスロットを空にしました: " + Bookshelf[Num].name);
                //    }
                //    else
                //    {
                //        Debug.Log("Bookshelfのスロットが既に空です。何もしません。");
                //    }
                //}
            }

            UpdateItemColors();
        }
    }
    private void UpdateItemColors()
    {
        for (int i = 0; i < BookList.Count; i++)
        {
            Image itemImage = BookList[i].GetComponent<Image>();

            if (itemImage != null)
            {
                // Num番目のアイテムは緑、それ以外は白に設定
                itemImage.color = (i == Num) ? Color.red : Color.white;
            }
        }
    }


}

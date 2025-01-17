using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SelectGimmick : MonoBehaviour
{
    public GameObject Book;

    [SerializeField]
    private List<GameObject> BookList = new List<GameObject>();

    [SerializeField]
    private List<GameObject> Bookshelf = new List<GameObject>();
    [SerializeField]
    private List<GameObject> testList = new List<GameObject>();

    private List<GameObject> testBookList = new List<GameObject>();

    private List<bool> selectedFlag = new List<bool>();
    [SerializeField]
    string inputValue = "";

    [SerializeField]
    int answer = 1234;
    // 入力文字数
    [SerializeField]
    private int ResultNum = 4;

    private int Num = 0;

    //移動時のインターバルの時間上限
    const float MAX_TIME = 50.0f;
    //移動時のインターバルの時間
    float time = MAX_TIME;

    int NumCount = 0;

    public bool Ans = false;

    float alpha = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        // BookListと同じサイズの選択状態リストを初期化
        for (int i = 0; i < BookList.Count; i++)
        {
            selectedFlag.Add(false); // 全て未選択状態
        }

        
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
            Image selectedImage = BookList[Num].GetComponent<Image>();
       

            if (exportNum != null)
            {
                int number = exportNum.ExpNum; // オブジェクトの値を取得
               
               
                if (inputValue.Length > -1)
                {
                    foreach (GameObject shelf in Bookshelf)
                    {
                        ExportNumber targetshlf = Bookshelf[Num].GetComponent<ExportNumber>();

                        if (targetshlf != null)
                        {
                            // 選択済みかどうかをチェック
                            if (selectedFlag[Num])
                            {
                                break; // 処理を中断
                            }

                            Image shelfImage = shelf.GetComponent<Image>();
                            //if(shelfImage.color.a==0.3f && shelfImage ==null )
                            if (shelfImage != null && shelfImage.sprite == null) // 空のImageを探す
                            {
                                shelfImage.color = Color.white;
                                // spriteを設定
                                targetshlf.SetSprite(shelf, exportNum.sprite);
                                inputValue += number.ToString();


                                if (selectedImage != null)
                                {
                                    selectedImage.enabled = false;
                                }
                                // 選択状態を更新
                                selectedFlag[Num] = true;

                                testList.Add(BookList[Num]);
                                testBookList.Add(shelf);


                                break; // 1つ設定したらループを抜ける
                            }

                        }
                    }
                }
            }


        }

     

        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (testList.Count > 0)
            {
                int lastIndex = testList.Count - 1; // testListの最後のインデックス
                GameObject book = testList[lastIndex]; // 最後のオブジェクトを取得
                Image bookImage = Bookshelf[lastIndex].GetComponent<Image>();
                
                // BookListの中で同じ名前のオブジェクトを探す
                for (int j = 0; j < BookList.Count; j++)
                {
                    if (book.name == BookList[j].name) // 名前が一致するか確認
                    {
                        Image selectedImage = BookList[j].GetComponent<Image>();
                        if (selectedImage != null)
                        {
                            selectedImage.enabled = true; // Imageを有効にする
                            bookImage.sprite = null;
                            selectedFlag[j] = false;
                            bookImage.color = Color.gray;
                            Debug.Log("trueになった: " + selectedImage.name);
                        }
                        break; // 一致するオブジェクトが見つかったらループを抜ける
                    }
                }

                testList.RemoveAt(lastIndex); // testListから最後のオブジェクトを削除
            }
        }


        //if (inputValue.Length < ResultNum && Input.GetKeyDown(KeyCode.Space))
        //{
        //    //inputValue = "";
           
        //    Debug.Log("不正解");
        //}
        if (inputValue.Length == ResultNum && Input.GetKeyDown(KeyCode.Space)) // Spaceで判定
        {

            if (inputValue == answer.ToString())
            {
                Ans = true;
                Debug.Log("正解！");
                inputValue = "";
                foreach (GameObject book in BookList)
                {
                    Image bookImage = book.GetComponent<Image>();
                    if (bookImage != null && !bookImage.enabled) // 無効化されている場合のみ処理
                    {
                        bookImage.enabled = true; // Imageを有効化
                    }
                }
                foreach (GameObject shelf in Bookshelf)
                {
                    Image shelfImage = shelf.GetComponent<Image>();
                    if (shelfImage != null)
                    {
                        shelfImage.sprite = null; // spriteを削除
                    }
                }
                // 選択フラグをリセット
                for (int i = 0; i < selectedFlag.Count; i++)
                {
                    selectedFlag[i] = false; // すべて未選択状態にリセット
                  
                }
                Book.gameObject.SetActive(false);
                testList.Clear();
            }
            else
            {
                Debug.Log("不正解");
                Ans = false;
                inputValue = "";
                foreach (GameObject book in BookList)
                {
                    Image bookImage = book.GetComponent<Image>();
                    if (bookImage != null && !bookImage.enabled) // 無効化されている場合のみ処理
                    {
                        bookImage.enabled = true; // Imageを有効化
                    }
                }
                foreach (GameObject shelf in Bookshelf)
                {
                    Image shelfImage = shelf.GetComponent<Image>();
                    if (shelfImage != null)
                    {
                        shelfImage.sprite = null; // spriteを削除
                    }
                }
                // 選択フラグをリセット
                for (int i = 0; i < 4; i++)
                {
                    selectedFlag[i] = false; // すべて未選択状態にリセット
                    
                }
                
                testList.Clear();
            }
        }
        UpdateItemColors();
    }

    private void UpdateItemColors()
    {
        for (int i = 0; i < BookList.Count; i++)
        {
            Image itemImage = BookList[i].GetComponent<Image>();

            if (itemImage != null)
            {
                float alpha1 = 0.3f;
                Color colorwhite = itemImage.color;
                colorwhite = Color.white;
                colorwhite.a = Mathf.Clamp01(alpha1);
                float alpha2 = 1f;
                Color colorblack = itemImage.color;
                colorblack = Color.white;
                colorblack.a = Mathf.Clamp01(alpha2);
                itemImage.color = (i == Num) ? colorwhite : colorblack;
              

            }
        }
    }


}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class Character : MonoBehaviour
//{
//    /// <summary>
//    /// �X�s�[�h
//    /// </summary>
//    [SerializeField]
//    protected float speed;
//    /// <summary>
//    /// ����
//    /// </summary>
//    private Vector2 direction;

//    [SerializeField]
//    protected Transform hitBox;

//    //private Animator animator;

//    //private void Start()
//    //{
//    //    animator = GetComponent<Animator>();
//    //}

//    protected virtual void Update()
//    {

//    }

//    public Vector2 Direction
//    {
//        get { return direction; }
//        set { direction = value; }
//    }

//    public float Speed
//    {
//        get { return speed; }
//        set { speed = value; }
//    }

//    protected void FixedUpdate()
//    {
//        Move();
//    }

//    /// <summary>
//    /// �G�̈ړ�
//    /// </summary>
//    public void Move()
//    {
//        transform.Translate(direction * speed * Time.deltaTime);

//        //AnimationMovement(direction);
//    }

//    //public void AnimationMovement(Vector2 direction)
//    //{
//    //    animator.SetFloat("x", direction.x);
//    //    animator.SetFloat("y", direction.y);
//    //}
//}

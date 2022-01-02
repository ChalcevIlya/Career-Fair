using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public float speed;  // скорость движения игрока
    public Camera playerCamera;  // камера игрока
    private float xRot;  // коэффицент вращения по x
    private float yRot;  // коэффицент вращения по y
    public float sensitive;  // чувствительность мыши
    public float up;  // коэффицент прыжка
    private Rigidbody rb;  // компонент твердого тела облака
    private bool isGround;  // флаг нахождения на земле
    private static int number = 0;  // счетчик экземпляров класса
    private int id;  // id объекта

    
    private void Start() {
        this.id = Player.number++;  // получение id/увеличение кол-ва экземпляров класса
        this.rb = this.GetComponent<Rigidbody>();  // получение компонента твердого тела
    }

    // private void OnStartLocalPlayer() {
    //     this.playerCamera = this.transform.GetChild(0).gameObject.GetComponent<Camera>();
    // }

    private void Update() {
        if (!isLocalPlayer) {
            playerCamera.enabled = false;
            return;
        }

        playerCamera.enabled = true;

        this.Movement();
        this.SelfRotation();
        
        // прыжок, по нажатию на пробел
        if (Input.GetKeyDown(KeyCode.Space)) {
            this.Jump();
        }
        
        if (!Application.isFocused) {
            print(isLocalPlayer.Equals(false));
        }

        // if (Input.GetKey(KeyCode.P)) {
        //     this.PrintID();
        // }
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "ground") {
            this.isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "ground") {
            this.isGround = false;
        }
    }

    private void Movement() {
        /* движение игрока */

        // движение вправо/влево кнопками A/D
        this.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0));
        
        // движение вперед/назад кнопками W/S
        this.transform.Translate(new Vector3(0, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime));
    }

    private void SelfRotation() {
        /* поворот камеры и игрока в направление камеры */
        
        if (isLocalPlayer) {
            // поворот камеры вправо/влево
            this.xRot += Input.GetAxis("Mouse Y") * this.sensitive * Time.deltaTime;
            
            // поворот камеры вверх/вниз
            this.yRot += Input.GetAxis("Mouse X") * this.sensitive * Time.deltaTime;
            
            // поворот камеры
            this.playerCamera.transform.rotation = Quaternion.Euler(-this.xRot, this.yRot, 0f);
            
            // поворот игрока
            this.transform.rotation = Quaternion.Euler(0f, this.yRot, 0f);
        }
    }

    private void Jump() {
        if (this.isGround) {  // если игрок на земле
            this.rb.AddForce(Vector3.up * this.up, ForceMode.Impulse);  // прыжок
        }
    }

    private void PrintID() {
        print($"{this.id}");
    }
}

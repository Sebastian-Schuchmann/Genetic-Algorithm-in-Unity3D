using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour {

    public Transform Goal;
   
    public DNA<Vector2> dna;
    Rigidbody2D rigidbodyFish;
    Color baseColor;
    public Vector2 basePosition;
    int moveCount;
    int reachedGoalCounter;

    public float Speed = 10.0f;
    bool DoMove;
    int currentMove = 0;

    //Events
    public event Action MovesCompleted;

    void Start()
    {
        rigidbodyFish = GetComponent<Rigidbody2D>();
        baseColor = GetComponent<Renderer>().material.color;
        DoMove = true;
        Goal = GameObject.Find("Goal").transform;
        Goal.GetComponent<GoalController>().Collided += GoalUpdated;
    }

    public void StartMove(){
        transform.position = basePosition;
        currentMove = 0;
        moveCount = 0;
        reachedGoalCounter = 0;
        DoMove = true;
    }

    void FixedUpdate () {
        if(DoMove)
        {
            Move();
            UpdateFitness();
        } 
	}

    void Move(){
        
        if(dna != null){
            if (currentMove >= dna.GetMoveCount())
            {
                DoMove = false;

                //Reset all Interia and Force
                rigidbodyFish.velocity = Vector2.zero;
                MovesCompleted();
                return;
            }

            Vector2 nextMove = dna.GetMoveWithIndex(currentMove++);
            rigidbodyFish.velocity = Vector2.zero;
            rigidbodyFish.AddForce(nextMove*Speed);
            moveCount++;
        }else{
            Debug.LogWarning("DNA NULL");
        }     
    }

    void UpdateFitness(){
        float Distance = Vector2.Distance(transform.position, Goal.position);

        if (Distance < 1f)
        {
            Debug.Log("Reached Goal");
            reachedGoalCounter++;
        }
        Distance = Mathf.Pow(Distance, 1);
        Distance -= reachedGoalCounter;
        dna.UpdateFitness(Distance);
    }

    void GoalUpdated(){
        dna.RandomizeMoveset();
    }

    //Just for Visual Adjustments
    public void RankUpdated(bool isBest){
        if(isBest){
            if(Camera.main.GetComponent<CameraFollow>() != null)
           Camera.main.GetComponent<CameraFollow>().target = transform;
            
            GetComponent<Renderer>().material.color = Color.green;

        } else{
            GetComponent<Renderer>().material.color = baseColor;
        }
    }
}

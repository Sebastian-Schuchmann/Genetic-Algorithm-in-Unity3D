using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA<T>{
    
    private List<T> MoveSet;
    public float FitnessScore;
    public bool Best;
    public event Action<DNA<T>> FitnessUpdated;
    public event Action<bool> RankUpdated;
    public event Action MovesetUpdated;

    readonly Func<int, T> GenerateRandomMove;

    public DNA(int AmountMoves, Func<int, T> GenerateRNDMove){
        MoveSet = new List<T>();
        GenerateRandomMove = GenerateRNDMove;
        GenerateMoveset(AmountMoves);
    }

    public DNA(List<T> Moves)
    {
        MoveSet = Moves;
    }

    public void UpdateMoveset(List<T> Moves){
        MoveSet = Moves;
        MovesetUpdated();
    }

    public List<T> GetMoveSet(){
        return MoveSet;
    }

    public void GenerateMoveset(int Size){
        for (int i = 0; i < Size; i++){
            T generatedMove = GenerateRandomMove(i);
            MoveSet.Add(generatedMove);
        }
    }

    public void UpdateFitness(float Score){
        FitnessScore = Score;
        FitnessUpdated(this);
    }

    public void SetBest(bool best){
        Best = best;
        RankUpdated(best);
    }

    public T GetMoveWithIndex(int Index){
        return MoveSet[Index];
    }

    public int GetMoveCount(){
        return MoveSet.Count;
    }

    public void PrintMoveSet()
    {
        foreach (T Move in MoveSet)
            Debug.Log("Move in DNA: " + Move.ToString());
    }

    public void RandomizeMoveset()
    {
        for (int i = 0; i < MoveSet.Count; i++)
        {
            MoveSet[i] = GenerateRandomMove(i);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution : MonoBehaviour {

    Population<Vector2> population;

    public int PopulationSize;
    public float MutationRate;
    public SelectionType selectionType;
    public int PropertiesLength;

    public FishController Prefab;
    List<FishController> Fishes;
    int MovesCompleted;

    //Later in extra Class!
    public TMPro.TextMeshProUGUI GenerationCountDisplay;
    int generationCounter;

    public float TimeScale;


	void Start () {
        
        population = new Population<Vector2>(PopulationSize,
                                             MutationRate, //around 0.01 - 0.1
                                             selectionType, //Eletist, WheelOfFortune
                                             PropertiesLength, //e.g. amount of Moves, length of String
                                             EvolutionHelper.RandomVector2, //Function for generating a random Set of T
                                             EvolutionHelper.CrossoverVec2, //Crossover function for List<Vector2>
                                             EvolutionHelper.MutateMoveset //Mutation function for List<Vector2>
                                            );
        
        Fishes = new List<FishController>();
        CreateFishesFromPrefab();
	}

    void CreateFishesFromPrefab(){
        foreach(DNA<Vector2> Gene in population.Genes){
            FishController fish = Instantiate(Prefab, transform.position, Quaternion.identity, transform);
            fish.dna = Gene;
            fish.basePosition = transform.position;
            fish.StartMove();

            //Attach Events
            fish.MovesCompleted += MoveCompletedCheck;
            Gene.MovesetUpdated += fish.StartMove;
            Gene.RankUpdated += fish.RankUpdated;
            Fishes.Add(fish);
        }
    }

    void MoveCompletedCheck(){
        MovesCompleted++;
        if(MovesCompleted >= PopulationSize){
            population.EvaluateFitness();
            MovesCompleted = 0;
            //Update GUI
            generationCounter++;
            GenerationCountDisplay.text = generationCounter.ToString();
        } 
    }

    public void PrintMovesFromFishes(){
        foreach(FishController Fish in Fishes){
            Fish.dna.PrintMoveSet();
        }
    }

	void Update () {
        Time.timeScale = TimeScale;
	}
}

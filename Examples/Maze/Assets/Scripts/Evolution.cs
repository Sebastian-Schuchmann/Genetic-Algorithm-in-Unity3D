using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution : MonoBehaviour {

    Population<Vector2> population;

    public int PopulationSize;
    public float MutationRate;
    public SelectionType selectionType;
    public int AmountMoves;

    public FishController Prefab;
    List<FishController> Fishes;
    int MovesCompleted;

    public float TimeScale;


	void Start () {
        
        population = new Population<Vector2>(PopulationSize, MutationRate, selectionType,
                                             AmountMoves,EvolutionHelper.RandomVector2,
                                             EvolutionHelper.CombineVectorListsRandomly,
                                             EvolutionHelper.MutateMoveset);
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

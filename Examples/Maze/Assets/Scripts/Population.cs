using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public enum SelectionType {
    Elitist,
    WheelOfFortune
}

public class Population<T> {
    
    public List<DNA<T>> Genes;
    DNA<T> BestGene;
    DNA<T> SecondBestGene;
    SelectionType selectionType;

    int PopulationSize;
    float MutationRate;
    int AmountMoves;
    readonly Func<int, T> GenerateRandomMove;
    readonly Func<List<T>, List<T>, List<T>> Crossover;
    readonly Func<List<T>, float, List<T>> Mutate;

    public Population(int popSize, float mutRate, SelectionType slctType, int amtMoves, Func<int, T> GenerateRNDMove, Func<List<T>, List<T>, List<T>> CrssOver, Func<List<T>, float, List<T>> Mtate){
        Genes = new List<DNA<T>>();

        PopulationSize = popSize;
        MutationRate = mutRate;
        selectionType = slctType;
        AmountMoves = amtMoves;
        GenerateRandomMove = GenerateRNDMove;
        Crossover = CrssOver;
        Mutate = Mtate;
        CreatePopulation();
    }

    void CreatePopulation(){
        for (int i = 0; i < PopulationSize; i++){
            DNA<T> gene = new DNA<T>(AmountMoves, GenerateRandomMove);
            gene.FitnessUpdated += UpdateFitnessHierachy;
            Genes.Add(gene);
        }
    }

    void UpdateFitnessHierachy(DNA<T> gene){
        if(BestGene == null){
            BestGene = gene;
            BestGene.SetBest(true);
            return;
        }
        //Higher Score is better
        if(BestGene.FitnessScore < gene.FitnessScore){
            BestGene.SetBest(false);
            SecondBestGene = BestGene;
            BestGene = gene;
            BestGene.SetBest(true);
            return;
        }

        if(SecondBestGene == null){
            SecondBestGene = gene;
            return;
        }

        if(SecondBestGene.FitnessScore < gene.FitnessScore){
            SecondBestGene = gene;
        }
    }

    void MatePopulation(){
        foreach (DNA<T> Gene in Genes)
        {
            List<T> Moveset = Crossover(
                BestGene.GetMoveSet(),
                SecondBestGene.GetMoveSet());

            Moveset = Mutate(Moveset, MutationRate);

            Gene.UpdateMoveset(Moveset);
        }
    }


    public void EvaluateFitness()
    {
        if(selectionType == SelectionType.Elitist){
            MatePopulation();
        }
        if(selectionType == SelectionType.WheelOfFortune)
        {
            NormalizeFitness();
            foreach (DNA<T> Gene in Genes){
                //Select two Parents with a Chance according to Fitness
                DNA<T> ParentA = MonteCarloSelector();
                DNA<T> ParentB = MonteCarloSelector();

                List<T> Moveset = Crossover(
                    ParentA.GetMoveSet(),
                    ParentB.GetMoveSet());

                Moveset = Mutate(Moveset, MutationRate);
                Gene.UpdateMoveset(Moveset);
            }
        }
    }

    //Chooses a Random Canidate and then he has a Chance
    //which is as high as his fitness score - to be selected 
    private DNA<T> MonteCarloSelector()
    {
        do{
            DNA<T> Canidate = Genes[UnityEngine.Random.Range(0, Genes.Count)];
            if (UnityEngine.Random.Range(0f, 1f) < Canidate.FitnessScore){
                return Canidate;
            }
        }while (true);
    }

    private void NormalizeFitness()
    {
        //Calculate Sum of all Fitness Scores
        float FitnessSum = 0.0f;
        foreach(DNA<T> Gene in Genes){
            FitnessSum += Gene.FitnessScore; 
        }

        //Normalize all Scores between 0 and 1
        foreach (DNA<T> Gene in Genes){
            Gene.FitnessScore /= FitnessSum;
        }
    }
}

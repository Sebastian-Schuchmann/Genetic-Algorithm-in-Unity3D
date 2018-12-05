using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EvolutionHelper {

    public static List<Vector3> CombineVectorListsRandomly(List<Vector3> VecA,  List<Vector3> VecB)
    {
        int maxRange = Mathf.Min(VecA.Count, VecB.Count);
        List<Vector3> combinedVector = new List<Vector3>();

        for (int i = 0; i < maxRange; i++){
            float random = Random.Range(0f, 1f);
            //Coinflip
            if(random < 0.5f){
                combinedVector.Add(VecA[i]);
            } else{
                combinedVector.Add(VecB[i]);
            }
        }

        return combinedVector;  
    }

    public static List<Vector2> CombineVectorListsRandomly(List<Vector2> VecA, List<Vector2> VecB)
    {
        int maxRange = Mathf.Min(VecA.Count, VecB.Count);
        List<Vector2> combinedVector = new List<Vector2>();

        for (int i = 0; i < maxRange; i++)
        {
            float random = Random.Range(0f, 1f);
            //Coinflip
            if (random < 0.5f)
            {
                combinedVector.Add(VecA[i]);
            }
            else
            {
                combinedVector.Add(VecB[i]);
            }
        }

        return combinedVector;
    }

    public static List<Vector3> MutateMoveset(List<Vector3> moveset, float MutationRate)
    {
        for (int i = 0; i < moveset.Count; i++)
        {
            float rnd = Random.Range(0f, 1f);
            if (rnd < MutationRate)
            {
                moveset[i] = RandomVector3(moveset[i].GetHashCode());
            }
        }

        return moveset;
    }

    public static List<Vector2> MutateMoveset(List<Vector2> moveset, float MutationRate)
    {
        for (int i = 0; i < moveset.Count; i++)
        {
            float rnd = Random.Range(0f, 1f);
            if (rnd < MutationRate)
            {
                moveset[i] = RandomVector3(moveset[i].GetHashCode());
            }
        }

        return moveset;
    }

    public static Vector3 RandomVector3(int extraSeed = 0)
    {
        Vector3[] possibleVectors = {
            Vector3.back,
            Vector3.forward,
            Vector3.left,
            Vector3.right,
            Vector3.up,
            Vector3.down
        };

        return possibleVectors[Random.Range(0, possibleVectors.Length)];
    }


    public static Vector2 RandomVector2(int extraSeed = 0)
    {
        Vector2[] possibleVectors = {
            Vector2.down,
            Vector2.up,
            Vector2.left,
            Vector2.right
        };

        return possibleVectors[Random.Range(0, possibleVectors.Length)];
    }
}

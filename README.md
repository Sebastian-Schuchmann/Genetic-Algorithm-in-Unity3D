# Genetic Algorithm in Unity3D
### Maze 2D Example
![Gif](https://media.giphy.com/media/28kLOviybkC5v7Xsts/giphy.gif)


*The goal in this game is for the blue cubes to reach the green goal line. They have a preset amount of moves which gets optimized over time by the genetic algorithm.*

## Prerequisites
The Project was made using Unity 2018.2.3f1 but prior versions should work fine, too. 

## Goal
This Project is supposed to lay the groundwork for anyone who wants to experiment with Genetic Algorithms in Unity. The main classes DNA and Population are generic so they can work with any type. The Selection, Crossover and Mutate functions are easy to swap out.

Unity is a great platform for rapid prototyping on any platform. 

## Setup
First, you need to create a population with the type you want to evolve. You may need to write your own functions for Selection, Crossover and Mutation, because they are different for each datatype (functions for Vector2 and Vector3 and supplied). 

![](https://github.com/Sebastian-Schuchmann/Genetic-Algorithm-in-Unity3D/blob/master/README%20Images/Setup.png?raw=true)

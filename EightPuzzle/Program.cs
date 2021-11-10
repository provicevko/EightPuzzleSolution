using System.Diagnostics;
using EightPuzzle.Services;
using EightPuzzle.Helpers;
using Algorithm = EightPuzzle.Enums.Algorithm;

var startState = new byte[] { 1, 8, 2, 0, 4, 3, 7, 6, 5 };
var endState = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 0 };

IPuzzleSolver puzzleSolver = new PuzzleSolverService(new PuzzleMovementService((int)Math.Sqrt(startState.Length)));
var t = new Stopwatch();
t.Start();
var result1 = puzzleSolver.FindSolution(startState, endState, Algorithm.Bfs);
t.Stop();
var time1 = t.ElapsedMilliseconds;

t.Restart();
var result2 = puzzleSolver.FindSolution(startState, endState, Algorithm.AStar);
t.Stop();
var time2 = t.ElapsedMilliseconds;

Console.WriteLine(GC.GetTotalMemory(false) / 1024);

Console.WriteLine("BFS:");
Console.WriteLine((double)time1 / 1000);
PrintHelper.PrintResult(result1);
Console.WriteLine((double)time2 / 1000);
PrintHelper.PrintResult(result2);

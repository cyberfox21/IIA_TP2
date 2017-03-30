﻿  using UnityEngine;
  using System.Collections;
  using System.Collections.Generic;
  
  public class Astar : SearchAlgorithm
  {
  
	  private List<SearchNode> queue = new List<SearchNode>(); 	   
  	  //private Queue<SearchNode> openQueue = new Queue<SearchNode> (); 		
      //private HashSet<object> openSet = new HashSet<object> ();     // open set & close set 
      private HashSet<object> closedSet = new HashSet<object> ();
	  private int heuristica;
  	   //Limite da pesquisa em profundidade
  
  
  
  
  	protected override void Begin ()
  	{ 
  		SearchNode start = new SearchNode (problem.GetStartState (), 0);
		problem = GameObject.Find("Map").GetComponent<Map>().GetProblem();
		queue.Add (start);
  	   // openQueue.Enqueue (start);
  	   // openSet.Add (start.state);   // Define o Set
  	}
  
  	protected override void Step ()
  	{
		if (queue.Count > 0) {
			SearchNode cur_node = queue [0];
			queue.RemoveAt (0);
			closedSet.Add (cur_node.state);
			if (problem.IsGoal (cur_node.state)) {
				solution = cur_node;
				finished = true;
				running = false;
			} else {
				Successor[] sucessors = problem.GetSuccessors (cur_node.state);
				foreach (Successor suc in sucessors) {
					if (!closedSet.Contains (suc.state)) {
						SearchNode new_node;
						if (heuristica == 1) {
							new_node = new SearchNode (suc.state, cur_node.g + suc.cost,problem.HeuristicObjectivos(suc.state), suc.action, cur_node);
						} else if (heuristica == 2) {
							new_node = new SearchNode (suc.state, cur_node.g + suc.cost, problem.HeuristicBoxDistObjective (suc.state), suc.action, cur_node);
						} else if (heuristica == 3) {
							new_node = new SearchNode (suc.state, cur_node.g + suc.cost, problem.HeuristicBoxGoalManhattan (suc.state), suc.action, cur_node);
						} else if (heuristica == 4) {
							new_node = new SearchNode (suc.state, cur_node.g + suc.cost, problem.HeuristicCharToCrate (suc.state), suc.action, cur_node);
						} else if (heuristica == 5) {
							new_node = new SearchNode (suc.state, cur_node.g + suc.cost, problem.Heurística5 (suc.state), suc.action, cur_node);
						} else {
							new_node = new SearchNode (suc.state, cur_node.g + suc.cost, problem.GetGoals(suc.state), suc.action, cur_node);
						}
						queue.Add (new_node);
						int i = queue.Count - 1;
						while (i > 0) {
							SearchNode actual = queue [i];
							SearchNode previous = queue [i - 1];
							if (actual.f < previous.f) {
								SearchNode temp = actual;
								queue [i] = previous;
								queue [i - 1] = temp;
								i--;
							} else {
								break;
							}
						}
					}
				}
			}
		} 
		else {
			finished = true;
			running = false;
		}
  	}
  }
  
  
  
  

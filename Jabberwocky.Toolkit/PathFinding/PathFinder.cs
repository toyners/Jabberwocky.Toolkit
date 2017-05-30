
namespace Jabberwocky.Toolkit.PathFinding
{
  using System;
  using System.Collections.Generic;

  public class PathFinderBase<T> where T : class
  {
    private HashSet<UInt32> closedSet;
    private HashSet<UInt32> openSet;

    // For each point, which point it can most efficiently be reached from.
    // If a point can be reached from many points, mostEfficientNeighbour will eventually contain the
    // most efficient previous step.
    private Dictionary<UInt32, UInt32> mostEfficientNeighbour;

    // For each point, the cost of getting from the start point to that point.
    private Dictionary<UInt32, Single> distancesFromStartToPoint;

    // For each point, the total cost of getting from the start point to the goal
    // by passing by that point. That value is partly known, partly heuristic.
    private Dictionary<UInt32, Single> totalCostOfStartToGoalViaThisPoint;

    private Single[,] distances;

    public PathFinderBase(Single[,] distances)
    {
      this.closedSet = new HashSet<UInt32>();
      this.openSet = new HashSet<UInt32>();
      this.mostEfficientNeighbour = new Dictionary<UInt32, UInt32>();
      this.distancesFromStartToPoint = new Dictionary<UInt32, Single>();
      this.totalCostOfStartToGoalViaThisPoint = new Dictionary<UInt32, Single>();
      this.distances = distances;
    }

    public List<UInt32> GetPathBetweenPoints(UInt32 startIndex, UInt32 endIndex)
    {
      this.distancesFromStartToPoint.Add(startIndex, 0f);

      Single distanceCoveringAllPermanentPoints = this.CalculateDistanceCoveringAllVerticies();

      this.openSet.Add(startIndex); // Index of first node
      this.distancesFromStartToPoint.Add(startIndex, 0f);
      this.totalCostOfStartToGoalViaThisPoint.Add(startIndex, distanceCoveringAllPermanentPoints);

      List<UInt32> list = null;
      while (openSet.Count > 0)
      {
        var currentIndex = this.GetIndexFromOpenSetWithLowestTotalCost();
        if (currentIndex == endIndex)
        {
          list = this.ConstructPath(currentIndex, startIndex);
          break;
        }

        openSet.Remove(currentIndex);
        closedSet.Add(currentIndex);

        for (var index = 0u; index < this.distances.Length; index++)
        {
          if (currentIndex == index)
          {
            continue;
          }

          if (this.closedSet.Contains(index))
          {
            continue;
          }

          var distance = this.distances[currentIndex, index];
        }
      }

      this.closedSet.Clear();
      this.openSet.Clear();
      this.mostEfficientNeighbour.Clear();
      this.distancesFromStartToPoint.Clear();
      this.totalCostOfStartToGoalViaThisPoint.Clear();

      return list;

      throw new NotImplementedException();
    }

    protected virtual Single CalculateDistanceCoveringAllVerticies()
    {
      throw new NotImplementedException();
    }

    protected virtual List<UInt32> ConstructPath(UInt32 currentIndex, UInt32 startIndex)
    {
      throw new NotImplementedException();
    }

    protected virtual UInt32 GetIndexFromOpenSetWithLowestTotalCost()
    {
      var workingDistance = Single.MaxValue;
      UInt32 lowestIndex = 0;
      foreach (var index in openSet)
      {
        if (this.totalCostOfStartToGoalViaThisPoint[index] < workingDistance)
        {
          lowestIndex = index;
          workingDistance = this.totalCostOfStartToGoalViaThisPoint[index];
        }
      }

      if (lowestIndex == 0)
      {
        throw new NotImplementedException();
      }

      return lowestIndex;
    }


  }
}

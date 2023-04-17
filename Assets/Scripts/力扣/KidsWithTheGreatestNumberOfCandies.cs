using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KidsWithTheGreatestNumberOfCandies : MonoBehaviour
{
    public int[] candies;
    public int extracandies;
    void Start()
    {
        KidsWithCandies(candies, extracandies);
        //Main();
    }

    public List<bool> KidsWithCandies(int[] candies, int extraCandies)
    {
        bool[] GreatestCandies = new bool[candies.Length];
        int greatest = candies.Max();

        print(string.Join(",", candies.Where(b => b + extracandies > greatest)));
        print(string.Join(",", candies.Any(b => b + extracandies > greatest)));
        print(string.Join(",", candies.All(b => b + extracandies > greatest)));
        print(string.Join(",", GreatestCandies.Select(value => value)));

        IEnumerable<bool> enumerable = GreatestCandies.Select((item, index) => candies[index] + extraCandies >= greatest);
        print(string.Join(",", enumerable));

        return enumerable.ToList();
    }

    void Main()
    {
        var listOne = new List<bool> { true, false, true, false, true, false, true, true };
        var listTwo = new List<bool> { false, true, true };
        var result = Merge(listOne, listTwo);

        print(string.Join(",", result));
    }

    List<bool> Merge(List<bool> one, List<bool> two)
    {
        var longer = one.Count > two.Count ? one : two;
        var shorter = one.Count > two.Count ? two : one;
        return longer
            .Select((item, ix) => ix >= shorter.Count ? item : item && shorter[ix])
            .ToList();
    }
}

using System;
using System.Collections.Generic;

namespace PermutationsOfShorterInLongerString
{
    /**
     *
     * Given a smaller string s and a bigger string b, design an algorithm to find all permutations of the shorter string within the longer one. Print the location of each permutations
     *
     *
     * s="abbc"
     * b="cbabadcbbabbcbabaabccbabc"
     *
     * create a hashmap for the smaller string to act as a reference
     *
     * create a hashmap to keep the track of the current characters
     *
     * create a list of int arrays to keep the locations
     *
     * create two variables i & j and initialize to 0
     * 
     */
    class Solution
    {
        public List<(int,int)> Solve(string s, string b)
        {
            Dictionary<char, int> refHash = new();
            Dictionary<char, int> dynHash = new();
            List<(int,int)> results = new();

            int i = 0, j;

            //fill refHash
            foreach (var c in s)
            {
                if (!refHash.ContainsKey(c))
                {
                    refHash.Add(c, 1);
                }
                else
                {
                    refHash[c]++;
                }
            }

            //Give j a head start to index s.Length
            for (j = 0; j < s.Length; j++)
            {
                if (!dynHash.ContainsKey(b[j]))
                {
                    dynHash.Add(b[j], 1);
                }
                else
                {
                    dynHash[b[j]]++;
                }
            }

            j--;

            while (j < b.Length)
            {
                if (AreSimilar(refHash, dynHash))
                {
                    results.Add((i,j));
                }

                dynHash[b[i]]--;
                j++;
                i++;
                
                if (!dynHash.ContainsKey(b[j]))
                {
                    dynHash.Add(b[j], 1);
                }
                else
                {
                    dynHash[b[j]]++;
                }
                
            }

            return results;

        }

        public bool AreSimilar(Dictionary<char,int> refHash, Dictionary<char,int> dynHash)
        {
            foreach (var (key, _) in dynHash)
            {
                if (!refHash.ContainsKey(key) || dynHash[key] != refHash[key]) return false;
            }
            
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var s = "abbc";
            var b = "cbabadcbbabbcbabaabccbabc";
            Solution solution = new();

            var results= solution.Solve(s, b);

            foreach (var result in results)
            {
                Console.WriteLine($"From index {result.Item1} to index {result.Item2}");
            }
        }
    }
}
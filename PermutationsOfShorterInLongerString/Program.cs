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
        /**
         * 
         */
         public List<(int, int)> Solve(string s, string b)
        {
            Dictionary<char, int> refHash = new();
            Dictionary<char, int> dynHash = new();
            List<(int, int)> results = new();

            int i = 0, j;

            //fill refHash
            foreach (var c in s)//O(s)
            {
                if (!refHash.ContainsKey(c))
                {
                    refHash.Add(c, 1);
                    dynHash.Add(c, 0);
                }
                else
                {
                    refHash[c]++;
                }
            }

            //Give j a head start to index s.Length
            for (j = 0; j < s.Length; j++)
            {
                if (dynHash.ContainsKey(b[j])) // O(s)
                {
                    dynHash[b[j]]++;
                }
            }

            j--;

            while (j < b.Length) // O(b-s)
            {
                if (AreSimilar(refHash, dynHash)) // O(s)
                {
                    results.Add((i, j));
                }

                if (dynHash.ContainsKey(b[i])) dynHash[b[i]]--;
                
                j++;
                i++;

                if (j >= b.Length) break;

                if (dynHash.ContainsKey(b[j])) dynHash[b[j]]++;
            }

            return results;
            
            
            /*
             *  Assuming checking if hashTables contains elements is constant time O(1)
             *  Our time complexity will be O(s + (b-s) * s)
             *  = O(s+(b-s^2))
             * 
             */
        }

        
        /**
         * O(s)
         */
        public bool AreSimilar(Dictionary<char, int> refHash, Dictionary<char, int> dynHash)
        {
            foreach (var (key, _) in dynHash)
            {
                if (dynHash[key] != refHash[key]) return false;
            }

            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string s = "abbc";
            const string b = "cbabadcbbabbcbabaabccbabc";
            Solution solution = new();

            var results = solution.Solve(s, b);

            foreach (var result in results)
            {
                Console.WriteLine($"From index {result.Item1} to index {result.Item2}");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Denomination
    {
        public String getDenominations(int amount)
        {
            StringBuilder denominations = new StringBuilder();
            List<Combinations> finalCOmbinations = new List<Combinations>();

            if (amount < 10 || amount % 10 != 0)
            {
                denominations.AppendLine("Please enter an amount in multiples of ten euros");
            }
            else
            {
                int remainderOf50 = amount % 50;

                if (amount - remainderOf50 == 0)
                {
                    finalCOmbinations.Add(new Combinations(remainderOf50 / 10, 0, 0));
                }
                else
                {
                    int balance = amount - remainderOf50;
                    int remainderOf100 = balance % 100;

                    if (balance < 100)
                    {
                        finalCOmbinations.Add(new Combinations(remainderOf50 / 10, remainderOf100 / 50, 0));
                        finalCOmbinations.Add(new Combinations(remainderOf50 / 10 + remainderOf100 / 50 * 5, 0, 0));
                    }
                    else
                    {
                        List<Combinations> combinationAbove100s = new List<Combinations>();

                        combinationAbove100s = this.getPlus100Combinations(balance - remainderOf100);

                        for (int i = 0; i < combinationAbove100s.Count; i++)
                        {
                            if (remainderOf100 / 50 != 0)
                            {
                                finalCOmbinations.Add(new Combinations(combinationAbove100s[i].tens + remainderOf50 / 10, combinationAbove100s[i].fifties + remainderOf100 / 50, combinationAbove100s[i].hundrads));
                                finalCOmbinations.Add(new Combinations(combinationAbove100s[i].tens + remainderOf50 / 10 + remainderOf100 / 50 * 5, combinationAbove100s[i].fifties, combinationAbove100s[i].hundrads));
                            }
                            else if (remainderOf100 / 50 == 0)
                            {
                                finalCOmbinations.Add(new Combinations(combinationAbove100s[i].tens + remainderOf50 / 10, combinationAbove100s[i].fifties, combinationAbove100s[i].hundrads));
                            }

                        }

                        finalCOmbinations = this.refactorList(finalCOmbinations);
                    }

                }
            }

            for (int j = 0; j < finalCOmbinations.Count; j++)
            {
                denominations.Append(finalCOmbinations[j].tens > 0 ? finalCOmbinations[j].tens + "*10 EUR + " : "");
                denominations.Append(finalCOmbinations[j].fifties > 0 ? finalCOmbinations[j].fifties + "*50 EUR + " : "");
                denominations.Append(finalCOmbinations[j].hundrads > 0 ? finalCOmbinations[j].hundrads + "*100 EUR + " : "");
                denominations.Remove(denominations.Length - 3, 3);
                denominations.AppendLine();
            }

            return denominations.ToString();
        }

        private List<Combinations> getPlus100Combinations(int amount)
        {
            int loops = amount / 100;
            List<Combinations> refactoredList = new List<Combinations>();
            refactoredList.Add(new Combinations(0, 0, 1));
            refactoredList.Add(new Combinations(0, 2, 0));
            refactoredList.Add(new Combinations(5, 1, 0));
            refactoredList.Add(new Combinations(10, 0, 0));

            List<Combinations> newList = null;

            for (int i = 1; i < loops; i++)
            {
                newList = new List<Combinations>();

                for (int j = 0; j < refactoredList.Count; j++)
                {
                    Combinations comb = refactoredList[j];
                    newList.Add(new Combinations(comb.tens, comb.fifties, comb.hundrads + 1));
                    newList.Add(new Combinations(comb.tens, comb.fifties + 2, comb.hundrads));
                    newList.Add(new Combinations(comb.tens + 5, comb.fifties + 1, comb.hundrads));
                    newList.Add(new Combinations(comb.tens + 10, comb.fifties, comb.hundrads));
                }

                refactoredList = this.refactorList(newList);
            }

            return refactoredList;
        }

        private List<Combinations> refactorList(List<Combinations> combinations)
        {
            List<String> list = new List<String>();
            List<Combinations> refactoredList = new List<Combinations>(combinations);

            for (int k = 0; k < combinations.Count; k++)
            {
                String key = combinations[k].tens + "~" + combinations[k].fifties + "~" + combinations[k].hundrads;

                if (!list.Contains(key))
                {
                    list.Add(key);
                }
                else
                {
                    refactoredList.Remove(combinations[k]);
                }
            }

            return refactoredList;
        }
    }

    class Combinations
    {
        public int tens;
        public int fifties;
        public int hundrads;

        public Combinations(int tens, int fifties, int hundrads)
        {
            this.tens = tens;
            this.fifties = fifties;
            this.hundrads = hundrads;
        }
    }
}

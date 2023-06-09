﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entropy
{
    public class EntropyCalculation
    {
        #region Коллекции 
        public Dictionary<string, double> BigramProbabilities { get; private set; }
        public Dictionary<char, double> SymbolProbabilities { get; private set; }

        public Dictionary<string, double> MatrixUncondEntropy { get; private set; }
        #endregion

        #region Выходные значения
        public int Ansambl { get; private set; }
        public double Entropy { get; private set; }
        public double EntropyFirstStage { get; private set; }
        public double MaxEntropy { get; private set; }
        public double UnderLoadAlphabet { get; private set; }

        public double XUnconditionalEntropy { get; private set; }
        public double YUnconditionalEntropy { get; private set; }
        public double MutuaLEntropy { get; private set; }
        public double CountMutualInfo { get; private set; }
        #endregion
        public string Message { get; set; }

        public EntropyCalculation() 
        {
            SymbolProbabilities = new Dictionary<char, double>();
            BigramProbabilities = new Dictionary<string, double>(); 
        }

        public async void CalcAllFields()
        {
            await Task.Run(() =>
            {
                if (Message != null)
                {
                    CalcBigramProbabilities();
                    CalcSymbolProbabilities();

                    CalcAnsambl();
                    CalcEntropy();
                    CalcMaxEntropy();
                    CalcUnderLoadAlphabet();
                    CalcEntropyFirstStage();
                }
            });

        }
        private void CalcBigramProbabilities()
        {
            BigramProbabilities = new Dictionary<string, double>();
            Dictionary<string, int> twoSymbCountContains = TwoSymbCountContains();

            int totalBigrams = Message.Length - 1;
            foreach (KeyValuePair<string, int> kvp in twoSymbCountContains)
            {
                double probability = (double)kvp.Value / totalBigrams;
                BigramProbabilities.Add(kvp.Key, probability);
            }
        }

        private void CalcSymbolProbabilities()
        {
            SymbolProbabilities = new Dictionary<char, double>();
            Dictionary<char, int> symbolCounts = OneSymbCountContains();

            foreach (KeyValuePair<char, int> kvp in symbolCounts)
            {
                double probability = (double)kvp.Value / Message.Length;
                SymbolProbabilities.Add(kvp.Key, probability);
            }
        }

        private void CalcAnsambl()
        {
            Ansambl = SymbolProbabilities.Count;  
        }

        private async void CalcEntropy()
        {
            await Task.Run(() =>
            {
                Entropy = 0;
                foreach (double p in SymbolProbabilities.Values)
                {
                    Entropy -= p * Math.Log(p, 2);
                }
            });
        }

        private void CalcMaxEntropy()
        {
            MaxEntropy = Math.Log(Ansambl, 2);
        }

        private void CalcUnderLoadAlphabet()
        {
            UnderLoadAlphabet = MaxEntropy - Entropy;
        }

        private async void CalcEntropyFirstStage()
        {
            await Task.Run(() =>
            {
                EntropyFirstStage = 0.0;
                double firstSum;
                foreach (KeyValuePair<string, double> bigram in BigramProbabilities)
                {
                    foreach (KeyValuePair<char, double> onegram in SymbolProbabilities)
                    {
                        if (IsFirstSymbEqually(bigram.Key, onegram.Key))
                        {
                            firstSum = bigram.Value * Math.Log2(bigram.Value);
                            EntropyFirstStage -= onegram.Value * firstSum;
                        }
                    }
                }
            });
        }

        private Dictionary<char, int> OneSymbCountContains()
        {
            Dictionary<char, int> symbolCounts = new Dictionary<char, int>();
            foreach (char symbol in Message)
            {
                if (symbolCounts.ContainsKey(symbol))
                {
                    symbolCounts[symbol]++;
                }
                else
                {
                    symbolCounts[symbol] = 1;
                }
            }
            return symbolCounts;
        }

        private Dictionary<string, int> TwoSymbCountContains()
        {
            Dictionary<string, int> twoSymbCountContains = new Dictionary<string, int>();;
            for (int i = 0; i < Message.Length - 1; i++)
            {
                string bigram = Message.Substring(i, 2);
                if (twoSymbCountContains.ContainsKey(bigram))
                {
                    twoSymbCountContains[bigram]++;
                }
                else
                {
                    twoSymbCountContains.Add(bigram, 1);
                }
            }
            return twoSymbCountContains;
        }

        private bool IsFirstSymbEqually(string firstMessage, char symbol)
        {
            if (firstMessage[0] == symbol)
            {
                return true;
            }
            else return false;
        }

        private void CalcCountMutualInfo()
        {
            CountMutualInfo = XUnconditionalEntropy + YUnconditionalEntropy - MutuaLEntropy;
        }

        private void CalcMatrixUncondEntropy()
        {
            MatrixUncondEntropy = new Dictionary<string, double>();

            double element;
            foreach (KeyValuePair<string, double> bigram in BigramProbabilities)
            {
                foreach (KeyValuePair<char, double> onegram in SymbolProbabilities)
                {
                    if (IsFirstSymbEqually(bigram.Key, onegram.Key))
                    {
                        element = onegram.Value * bigram.Value;
                        MatrixUncondEntropy.Add(bigram.Key, element);
                    }
                }
            }
        }


        private void CalcXUncondEntropy()
        {
           /* XUnconditionalEntropy = 0.0;

            for (int i = 0; i < ; i++)
            {

            }
            XUnconditionalEntropy -= */
        }
    }
}

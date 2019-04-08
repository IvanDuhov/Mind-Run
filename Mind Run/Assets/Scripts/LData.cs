using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LData
{
    // -------------------------------------//
    // Language stats and games
    // -------------------------------------//
    // Linguistic stats
    public uint LanguageScore; // All time score
    public int LanguageCount; // All time count
    public int LanguageAverage; // Average
    public int[] LanguageHistory; // Saving the last 5 results

    // Languistic games
    // 1. WordFormation
    public uint WordFormationScore;
    public int WordFormationCount;
    public int WordFormationAverage;
    public float WordFormationProgress;
    public int WordFormationHighest;
    public int[] WordFormationHistory;
    // -------------------------------------//


    // -------------------------------------//
    // Memory stats and games
    // -------------------------------------//
    // Memory stats
    public uint MemoryScore;
    public int MemoryCount;
    public int MemoryAverage;
    public int[] MemoryHistory;

    // Memory games
    // 1. Memory Grid
    public uint MemoryGridScore;
    public int MemoryGridCount;
    public int MemoryGridAverage;
    public float MemoryGridProgress;
    public int MemoryGridHighest;
    public int[] MemoryGridHistory;
    // -------------------------------------//


    // -------------------------------------//
    // Problem solving stats and games
    // -------------------------------------//
    // Memory stats
    public uint ProblemSolvingScore;
    public int ProblemSolvingCount;
    public int ProblemAverage;
    public int[] ProblemHistory;

    // 1. Train
    public uint TrainScore;
    public int TrainCount;
    public int TrainAverage;
    public float TrainProgress;
    public int TrainHighest;
    public int[] TrainHistory;
    // -------------------------------------//


    // -------------------------------------//
    // Focus stats and games
    // -------------------------------------//
    // Focus stats
    public uint FocusScore;
    public int FocusCount;
    public int FocusAverage;
    public int[] FocusHistory;

    // Focus games
    // 1. CardSort
    public uint CardSortScore;
    public int CardSortCount;
    public int CardSortAverage;
    public float CardSortSuccessRate;
    public float CardSortProgress;
    public int CardSortHighest;
    public int[] CardSortHistory;
    // -------------------------------------//


    // -------------------------------------//
    // Mental agility stats and games
    // -------------------------------------//
    // Mental agility stats
    public uint MentalAScore;
    public int MentalACount;
    public int MentalAverage;
    public int[] MentalHistory;

    // Focus games
    // 1. TrueColour
    public uint TrueColourScore;
    public int TrueColourCount;
    public int TrueColourAverage;
    public float TrueColourProgress;
    public int TrueColourHighest;
    public int[] TrueColourHistory;
    // -------------------------------------//

    public int overall;
    public bool tutorialMenu;
}

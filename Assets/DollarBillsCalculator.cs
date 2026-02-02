using UnityEngine;

/// <summary>
/// Calculates the optimal bill breakdown for a given dollar amount.
/// Uses the largest denominations first to minimize total bill count.
/// </summary>
public class DollarBillsCalculator : MonoBehaviour
{
    [SerializeField] private int dollarAmount = 247;

    private void Start()
    {
        CalculateAndDisplayBills(dollarAmount);
    }

    /// <summary>
    /// Main calculation flow: validates input, calculates bills, and displays results.
    /// </summary>
    private void CalculateAndDisplayBills(int amount)
    {
        if (!ValidateAmount(amount))
        {
            return;
        }

        // Calculate bills from largest to smallest denomination
        int hundreds = CalculateBillCount(ref amount, 100);
        int fifties = CalculateBillCount(ref amount, 50);
        int twenties = CalculateBillCount(ref amount, 20);
        int tens = CalculateBillCount(ref amount, 10);
        int fives = CalculateBillCount(ref amount, 5);
        int ones = amount; // Remaining amount becomes ones

        DisplayResults(dollarAmount, hundreds, fifties, twenties, tens, fives, ones);
    }

    /// <summary>
    /// Validates that the amount is positive and non-zero.
    /// </summary>
    /// <returns>True if valid, false otherwise</returns>
    private bool ValidateAmount(int amount)
    {
        if (amount < 0)
        {
            Debug.LogError("Dollar amount cannot be negative.");
            return false;
        }

        if (amount == 0)
        {
            Debug.Log("Amount is $0. No bills needed.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Calculates how many bills of a given denomination fit into the remaining amount.
    /// Uses ref to modify remainingAmount in-place for memory efficiency.
    /// </summary>
    /// <param name="remainingAmount">Amount left to break down, modified by reference</param>
    /// <param name="billDenomination">Bill value (100, 50, 20, etc.)</param>
    /// <returns>Number of bills of this denomination</returns>
    private int CalculateBillCount(ref int remainingAmount, int billDenomination)
    {
        int billCount = remainingAmount / billDenomination;
        remainingAmount %= billDenomination; // Update remaining amount
        return billCount;
    }

    /// <summary>
    /// Outputs bill breakdown to console. Only displays denominations with count > 0.
    /// </summary>
    private void DisplayResults(int total, int hundreds, int fifties, int twenties, int tens, int fives, int ones)
    {
        Debug.Log($"=== Dollar Bills Breakdown for ${total} ===");

        if (hundreds > 0)
        {
            Debug.Log($"{hundreds} x $100 bill(s)");
        }

        if (fifties > 0)
        {
            Debug.Log($"{fifties} x $50 bill(s)");
        }

        if (twenties > 0)
        {
            Debug.Log($"{twenties} x $20 bill(s)");
        }

        if (tens > 0)
        {
            Debug.Log($"{tens} x $10 bill(s)");
        }

        if (fives > 0)
        {
            Debug.Log($"{fives} x $5 bill(s)");
        }

        if (ones > 0)
        {
            Debug.Log($"{ones} x $1 bill(s)");
        }

        Debug.Log("=====================================");
    }
}
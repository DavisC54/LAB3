using UnityEngine;

/// <summary>
/// Calculates bookstore wholesale costs and profit margins.
/// Accounts for 40% discount and tiered shipping costs.
/// Shipping is a cost to the bookstore, not passed to customers.
/// </summary>
public class BookstoreProfitCalculator : MonoBehaviour
{
    [SerializeField] private float coverPrice = 24.95f;
    [SerializeField] private int numberOfCopies = 60;

    // Business constants
    private const float BOOKSTORE_DISCOUNT = 0.40f;
    private const float FIRST_COPY_SHIPPING = 3.00f;
    private const float ADDITIONAL_COPY_SHIPPING = 0.75f;

    private void Start()
    {
        CalculateAndDisplayBookstoreCosts(coverPrice, numberOfCopies);
    }

    /// <summary>
    /// Main calculation flow: validates inputs, calculates costs and profit, displays results.
    /// </summary>
    private void CalculateAndDisplayBookstoreCosts(float price, int copies)
    {
        if (!ValidateInputs(price, copies))
        {
            return;
        }

        // Calculate cost components
        float discountedPrice = CalculateDiscountedPrice(price);
        float booksCost = CalculateBooksCost(discountedPrice, copies);
        float shippingCost = CalculateShippingCost(copies);
        float totalWholesaleCost = booksCost + shippingCost;

        // Calculate revenue and profit
        float totalRevenue = CalculateTotalRevenue(price, copies);
        float profit = CalculateProfit(totalRevenue, totalWholesaleCost);

        DisplayResults(price, copies, discountedPrice, booksCost, shippingCost, totalWholesaleCost, totalRevenue, profit);
    }

    /// <summary>
    /// Validates that price and copy count are positive values.
    /// </summary>
    /// <returns>True if inputs are valid, false otherwise</returns>
    private bool ValidateInputs(float price, int copies)
    {
        if (price <= 0)
        {
            Debug.LogError("Cover price must be greater than zero.");
            return false;
        }

        if (copies <= 0)
        {
            Debug.LogError("Number of copies must be greater than zero.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Applies 40% bookstore discount to cover price.
    /// </summary>
    private float CalculateDiscountedPrice(float price)
    {
        return price * (1.0f - BOOKSTORE_DISCOUNT);
    }

    /// <summary>
    /// Calculates total cost of books at discounted price.
    /// </summary>
    private float CalculateBooksCost(float discountedPrice, int copies)
    {
        return discountedPrice * copies;
    }

    /// <summary>
    /// Calculates tiered shipping cost: $3 for first copy, $0.75 per additional copy.
    /// This is a cost to the bookstore, not the customer.
    /// </summary>
    private float CalculateShippingCost(int copies)
    {
        if (copies == 1)
        {
            return FIRST_COPY_SHIPPING;
        }

        // First copy + additional copies
        return FIRST_COPY_SHIPPING + (ADDITIONAL_COPY_SHIPPING * (copies - 1));
    }

    /// <summary>
    /// Calculates total revenue if all books sell at cover price.
    /// </summary>
    private float CalculateTotalRevenue(float price, int copies)
    {
        return price * copies;
    }

    /// <summary>
    /// Calculates profit before operational costs (rent, salaries, etc.).
    /// </summary>
    private float CalculateProfit(float revenue, float cost)
    {
        return revenue - cost;
    }

    /// <summary>
    /// Outputs complete financial breakdown to console.
    /// </summary>
    private void DisplayResults(float price, int copies, float discountedPrice, float booksCost, float shippingCost, float totalCost, float revenue, float profit)
    {
        Debug.Log($"=== Bookstore Calculation for {copies} Copies ===");
        Debug.Log($"Cover Price per Book: ${price:F2}");
        Debug.Log($"Discounted Price per Book (40% off): ${discountedPrice:F2}");
        Debug.Log($"Cost of Books: ${booksCost:F2}");
        Debug.Log($"Shipping Cost: ${shippingCost:F2}");
        Debug.Log($"Total Wholesale Cost: ${totalCost:F2}");
        Debug.Log($"Total Revenue (when sold): ${revenue:F2}");
        Debug.Log($"Profit (before operational costs): ${profit:F2}");
        Debug.Log("===========================================");
    }
}
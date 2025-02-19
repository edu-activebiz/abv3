using System;

namespace FMCGWebApplication.Models
{
    public static class UtilityHelper
    {
        public static string CodCnvZ(string inputValue, int length)
        {
            if (string.IsNullOrWhiteSpace(inputValue))
                inputValue = string.Empty;

            // Check if the input is numeric
            if (long.TryParse(inputValue, out _))
            {
                // Pad with leading zeros if numeric
                return inputValue.PadLeft(length, '0');
            }
            else
            {
                // If it's text, trim or pad with spaces
                if (inputValue.Length > length)
                    return inputValue.Substring(0, length); // Trim if too long
                else
                    return inputValue.PadRight(length, '0'); // Pad with spaces
            }
        }
    }

    public static class GlobalVariables
    {
        public static short SelectedCompany { get; set; } = 0;
        public static string SelectedCompanyName { get; set; } = string.Empty;
    }
}

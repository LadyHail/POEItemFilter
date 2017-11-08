namespace POEItemFilter.Library
{
    public static class InequalitySelector
    {
        public static string Select(int number)
        {
            switch (number)
            {
                case 300:
                    return null;

                case 1:
                    return "=";

                case 2:
                    return ">";

                case 3:
                    return "<";

                case 4:
                    return ">=";

                case 5:
                    return "<=";

                default:
                    return null;
            }
        }
    }
}
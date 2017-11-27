namespace POEItemFilter.Library
{
    public static class InequalitySign
    {
        public static string Select(int number)
        {
            switch (number)
            {
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
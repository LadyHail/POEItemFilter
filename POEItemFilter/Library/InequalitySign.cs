namespace POEItemFilter.Library
{
    public static class InequalitySign
    {
        public static string SelectSign(int number)
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

        public static int? SelectInt(string sign)
        {
            switch (sign)
            {
                case "=":
                    return 1;

                case ">":
                    return 2;

                case "<":
                    return 3;

                case ">=":
                    return 4;

                case "<=":
                    return 5;

                default:
                    return null;
            }
        }
    }
}
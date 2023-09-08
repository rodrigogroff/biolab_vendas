namespace Master.Service.Helper
{
    public class HelperCheck
    {
        public bool CheckEmail(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            if (!str.Contains("@") || !str.Contains("."))
            {
                return false;
            }

            var e = str.Split('@');

            if (e.Length != 2)
            {
                return false;
            }

            if (e[0].Length < 1)
            {
                return false;
            }

            if (e[1].Length < 2)
            {
                return false;
            }

            if (!e[1].Contains("."))
            {
                return false;
            }

            return true;
        }
    }
}

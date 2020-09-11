using System;
using System.Text;
using CGCCrawler.Libs.Extensions;

namespace CGCCrawler.Libs.NumString
{
    [Serializable]
    public struct NString
    {
        /// <summary>
        /// maximum number string available length
        /// </summary>
        private static int MAX_LENGTH = 80;

        /// <summary>
        /// Stores number string value
        /// </summary>
        private string Value { get; set; }

        /// <summary>
        /// Creates Nstring struct
        /// </summary>
        /// <param name="numString"></param>
        public NString(string numString = "0")
        {
            Value = numString;
            Length = 0;

            FilterInput(numString);
        }

        public NString(int num = 0) : this(num.ToString()) { }

        /// <summary>
        /// input string length
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Returs data type to the string format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// implicit operator for initialization nstring struct by unsigned int
        /// </summary>
        /// <param name="number"></param>
        public static implicit operator NString(uint number)
        {
            return new NString(number.ToString());
        }

        /// <summary>
        /// implicit operator for initialization nstring struct by string
        /// </summary>
        /// <param name="numString"></param>
        public static implicit operator NString(string numString)
        {
            return new NString(numString);
        }

        /// <summary>
        /// definition + operator for Nstring data type
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static NString operator +(NString Num1, NString Num2)
        {
            int maxLen;
           
            string maxnum;
            string minnum;

            StringBuilder build;

            if (Num1.Length > Num2.Length)
            {
                maxnum = Num1.ToString();
                minnum = InitMinNum(Num2.ToString(), Num1.Length,Num2.Length);

                maxLen = maxnum.Length;

                build = new StringBuilder(maxLen + 1);
            }
            else if (Num2.Length > Num1.Length)
            {
                maxnum = Num2.ToString();
                minnum = InitMinNum(Num2.ToString(), Num2.Length, Num1.Length);

                maxLen = maxnum.Length;

                build = new StringBuilder(maxLen + 1);
            }
            else
            {
                maxnum = "0" + Num1.ToString();
                minnum = "0" + Num2.ToString();

                maxLen = maxnum.Length;

                build = new StringBuilder(maxLen + 1);
            }

            int n1;
            int n2;
            int save = 0;
            int result;
            int sum;

            int i = maxLen - 1;


            for (; i >= 0; i--)
            {
                n1 = int.Parse(maxnum[i].ToString());
                n2 = int.Parse(minnum[i].ToString());

                sum = n1 + n2 + save;

                save = sum / 10;
                result = sum % 10;

                build.Append(result);
            }

            if (save != 0)
                build.Append(save);

            string res = Reverse(build.ToString());


            return new NString(res);
        }

        public static NString operator ++(NString num)
        {
            num += 1;

            return new NString(num.ToString());
        }

        /// <summary>
        /// filters string for correct input
        /// </summary>
        /// <param name="input"></param>
        private void FilterInput(string input)
        {
            string tempstr = input;

            if (input.Length > 0)
            {

                if (tempstr.IsMatch(GVars.REGEX_WHITE_SPACE_PATTERN))
                    throw new Exception("Input data contains white space!");
                if (!tempstr.IsMatch(GVars.REGEX_DIGITS_ONLY_PATTERN))
                    throw new Exception("Input data must be only digits!");

                tempstr = RemoveZeroFromStart(tempstr);

                Value = tempstr;

                Length = tempstr.Length;

                if (Length > MAX_LENGTH)
                    throw new Exception("More than max value!");
            }
            else
            {
                Value = "0";
                Length = Value.Length;
            }
        }

        /// <summary>
        /// Removes zeros from begiinning of the inputed data
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string RemoveZeroFromStart(string input)
        {
            if (input.Length == 1)
                return input;

            if (!input.StartsWith("0"))
                return input;

            input = input.Remove(0, 1);
            input = RemoveZeroFromStart(input);
            return input;
        }

        /// <summary>
        /// add zeros biggining of teh string
        /// </summary>
        /// <param name="minnum"></param>
        /// <param name="MaxLen"></param>
        /// <param name="MinLen"></param>
        /// <returns></returns>
        private static string InitMinNum(string minnum,int MaxLen, int MinLen)
        {
            for (int i = 0; i < MaxLen-MinLen; i++)
                minnum = "0" + minnum;

            return minnum;
        }

        /// <summary>
        /// revers string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}

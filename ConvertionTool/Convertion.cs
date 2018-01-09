using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertionTool
{
    public class Convertion
    {
        static Stack<byte> BinaryNum;
        static StringBuilder Temp;
        static Stack<char> CharNum;
        static string[] PrimaryNum;
        static StringBuilder ResultNum;

        static string OctCharToTriada(char ch)
        {
            switch (ch)
            {
                case '0':
                    return "000";
                case '1':
                    return "001";
                case '2':
                    return "010";
                case '3':
                    return "011";
                case '4':
                    return "100";
                case '5':
                    return "101";
                case '6':
                    return "110";
                case '7':
                    return "111";
                case '.':
                    return ",";
                case ',':
                    return ",";
                default:
                    return "";

            }
        }

        static string HexCharToTetra(char ch)
        {
            switch (ch)
            {
                case '0':
                    return "0000";
                case '1':
                    return "0001";
                case '2':
                    return "0010";
                case '3':
                    return "0011";
                case '4':
                    return "0100";
                case '5':
                    return "0101";
                case '6':
                    return "0110";
                case '7':
                    return "0111";
                case '8':
                    return "1000";
                case '9':
                    return "1001";
                case 'A':
                    return "1010";
                case 'B':
                    return "1011";
                case 'C':
                    return "1100";
                case 'D':
                    return "1101";
                case 'E':
                    return "1110";
                case 'F':
                    return "1111";
                case '.':
                    return ",";
                case ',':
                    return ",";
                default:
                    return "";
                    
            }
        }

        static char TriadaToChar(string tri)
        {
            switch (tri)
            {
                case "000":
                    return '0';
                case "001":
                    return '1';
                case "010":
                    return '2';
                case "011":
                    return '3';
                case "100":
                    return '4';
                case "101":
                    return '5';
                case "110":
                    return '6';
                case "111":
                    return '7';
                case ".":
                    return ',';
                case ",":
                    return ',';
                default:
                    return ' ';

            }
        }

        static char TetraToChar(string tetr)
        {
            switch (tetr)
            {
                case "0000":
                    return '0';
                case "0001":
                    return '1';
                case "0010":
                    return '2';
                case "0011":
                    return '3';
                case "0100":
                    return '4';
                case "0101":
                    return '5';
                case "0110":
                    return '6';
                case "0111":
                    return '7';
                case "1000":
                    return '8';
                case "1001":
                    return '9';
                case "1010":
                    return 'A';
                case "1011":
                    return 'B';
                case "1100":
                    return 'C';
                case "1101":
                    return 'D';
                case "1110":
                    return 'E';
                case "1111":
                    return 'F';
                case ".":
                    return ',';
                case ",":
                    return ',';
                default:
                    return ' ';
            }
        }

        public static string HexToBinary(string hex)
        {
            ResultNum = new StringBuilder();
            hex = hex.ToUpper();
            for (int i = 0; i < hex.Length; i++)
            {
                ResultNum.Append(HexCharToTetra(hex[i]));
            }
            return ResultNum.ToString();
        }

        public static string OctToBinary(string oct)
        {
            ResultNum = new StringBuilder();
            for (int i = 0; i < oct.Length; i++)
            {
                ResultNum.Append(OctCharToTriada(oct[i]));
            }
            return ResultNum.ToString();
        }

        
        public static string DecimalToBinary(string dec, uint signs)
        {
            decimal fract = 0;
            BinaryNum = new Stack<byte>();
            ResultNum = new StringBuilder();
            PrimaryNum = dec.Split(new[] { ',','.'});
            
            if (PrimaryNum.Length == 2)
            {
                ResultNum.Append("0.");
                ResultNum.Append(PrimaryNum[1]);
                fract = Convert.ToDecimal(ResultNum.ToString());
            }
            
            var num = Convert.ToUInt64(PrimaryNum[0]);            
            while (num > 0)
            {
                BinaryNum.Push((byte)(num % 2));
                num /= 2;
            }
            ResultNum.Clear();

            while (BinaryNum.Count>0)
            {
                ResultNum.Append(BinaryNum.Pop());
            }
            if (PrimaryNum.Length == 2 && PrimaryNum[1].Length>0)
            {
                ResultNum.Append(",");
                int temp;
                while (signs > 0 && fract > 0)
                {
                    temp = (int)Math.Truncate(fract * 2);
                    ResultNum.Append(temp);
                    fract = (fract * 2) - temp;
                    signs--;
                }
            }
            return ResultNum.ToString();
        }
        
        public static string BinaryToOctal(string bin)
        {
            ResultNum = new StringBuilder();

            PrimaryNum = bin.Split(',', '.');
            if (PrimaryNum[0].Length % 3 != 0)
            {
                while (PrimaryNum[0].Length % 3 != 0)
                {
                    PrimaryNum[0] = "0" + PrimaryNum[0];

                }
            }
            
            Console.WriteLine($"Primary num 0: {PrimaryNum[0]}");
            if (PrimaryNum.Length == 2)
            {
                while (PrimaryNum[1].Length % 3 != 0)
                {
                    PrimaryNum[1] = PrimaryNum[1] + "0";
                }
                Console.WriteLine($"Primary num 1: {PrimaryNum[1]}");
            }

            for (int i = 0; i < PrimaryNum[0].Length; i += 3)
            {
                ResultNum.Append(TriadaToChar(PrimaryNum[0].Substring(i, 3)));
            }
            if (PrimaryNum.Length == 2)
            {
                ResultNum.Append(",");
                for (int i = 0; i < PrimaryNum[1].Length; i += 3)
                {
                    ResultNum.Append(TriadaToChar(PrimaryNum[1].Substring(i, 3)));
                }
            }
            return ResultNum.ToString();
        }

        public static string BinaryToHex(string bin)
        {
            ResultNum = new StringBuilder();

            PrimaryNum = bin.Split(',', '.');
            while (PrimaryNum[0].Length % 4 != 0)
            {
                PrimaryNum[0] = "0" + PrimaryNum[0];
            }
            if (PrimaryNum.Length == 2)
            {
                while (PrimaryNum[1].Length % 4 != 0)
                {
                    PrimaryNum[1] = PrimaryNum[1] + "0";
                }
            }
            
            for (int i = 0; i < PrimaryNum[0].Length; i += 4)
            {
                ResultNum.Append(TetraToChar(PrimaryNum[0].Substring(i, 4)));
            }
            if (PrimaryNum.Length == 2)
            {
                ResultNum.Append(",");
                for (int i = 0; i < PrimaryNum[1].Length; i += 4)
                {
                    ResultNum.Append(TetraToChar(PrimaryNum[1].Substring(i, 4)));
                }
            }
            
            return ResultNum.ToString();
        }

        public static string BinaryToDecimal(string bin)
        {
            int sum = 0;
            double prec = 0;
            
            
            ResultNum = new StringBuilder();
            PrimaryNum = bin.Split('.', ',');
            
            for (int i = 0; i < PrimaryNum[0].Length; i++)
            {
                sum = sum + ((int)(Char.GetNumericValue(PrimaryNum[0][i]) * Math.Pow(2.0, (double)PrimaryNum[0].Length - 1 - i)));
            }
            ResultNum.Append(sum.ToString());
            if (PrimaryNum.Length == 2)
            {
                
                ResultNum.Append(",");
                var start = Char.GetNumericValue(PrimaryNum[1][PrimaryNum[1].Length - 1]);
                for (int i = 0; i < PrimaryNum[1].Length - 1; i++)
                {

                    prec = start / 2.0;
                    if(PrimaryNum[1].Length - 1 - i - 1 >= 0)
                        prec += Char.GetNumericValue(PrimaryNum[1][PrimaryNum[1].Length - 1 - i - 1]);
                    start = prec;
                }
                ResultNum.Append((prec / 2.0).ToString().Split(',','.')[1]);
            }
            return ResultNum.ToString();
        }
    }
}

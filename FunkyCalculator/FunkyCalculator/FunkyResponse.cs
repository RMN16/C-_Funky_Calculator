using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunkyCalculator
{
    public class FunkyResponse
    {
        private Color backgroundColor;
        private Color textColor;
        private string message;

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }
        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public FunkyResponse(Color bgColor, Color txtColor, string msg)
        {
            BackgroundColor = bgColor;
            TextColor = txtColor;
            Message = msg;
        }

        public override string ToString()
        {
            return $"FunkyResponce: {message} (BG: {backgroundColor.Name}, Text: {textColor.Name})";
        }
    }

    public class Calculator
    {
        private double currentValue;
        private double previousValue;
        private string currentOperation;
        public double CurrentValue
        {
            get { return currentValue; }
            set { currentValue = value; }
        }
        public double PreviousValue
        {
            get { return previousValue; }
            set { previousValue = value; }
        }
        public string CurrentOperation
        {
            get { return currentOperation; }
            set { currentOperation = value; }
        }
        public Calculator()
        {
            CurrentValue = 0;
            PreviousValue = 0;
            CurrentOperation = string.Empty;
        }

        public double Calculate(string operation, double firstValue, double secondValue)
        {
            double result = 0;

            switch (operation)
            {
                case "+":
                    result = firstValue + secondValue;
                    break;
                case "-":
                    result = firstValue - secondValue;
                    break;
                case "*":
                    result = firstValue * secondValue;
                    break;
                case "/":
                    if (secondValue != 0)
                    {
                        result = firstValue / secondValue;
                    }
                    else
                    {
                        throw new DivideByZeroException("Cannot divide by zero.");
                    }
                    break;
                case "%":
                    if (secondValue != 0)
                    {
                        result = firstValue % secondValue;
                    }
                    else
                    {
                        throw new DivideByZeroException("Cannot modulo by zero.");
                    }
                    break;
                case "^":
                    result = Math.Pow(firstValue, secondValue);
                    break;

                default:
                    throw new InvalidOperationException("Invalid operation.");
            }

            return result;
        }

        public void Clear()
        {
            CurrentValue = 0;
            PreviousValue = 0;
            CurrentOperation = string.Empty;
        }

        public bool HasOperation()
        {
            return !string.IsNullOrEmpty(CurrentOperation);
        }

    }

    public class FunkyResponceManager
    {
        private static FunkyResponceManager instance = null;
        private static readonly object padlock = new object();
        private Dictionary<int, FunkyResponse> funkyResponses;

        private FunkyResponceManager()
        {
            InializeFunkyResponses();
        }

        public static FunkyResponceManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FunkyResponceManager();
                    }
                    return instance;
                }
            }
        }

        private void InializeFunkyResponses()
        {
            funkyResponses = new Dictionary<int, FunkyResponse>();

            funkyResponses.Add(69, new FunkyResponse(Color.HotPink, Color.White, "bow chicka wow wow ;)"));
            funkyResponses.Add(420, new FunkyResponse(Color.ForestGreen, Color.White, "Woah duudee ▂▂⌇ "));
            funkyResponses.Add(67, new FunkyResponse(Color.Orange, Color.White, "idk tbh... funny brainrot number ¯\\_(ツ)_/¯"));
            funkyResponses.Add(80085, new FunkyResponse(Color.DeepPink, Color.White, "Hehe (.)(.)"));
            funkyResponses.Add(1337, new FunkyResponse(Color.Black, Color.Lime, "LEET HACKER! "));
            funkyResponses.Add(666, new FunkyResponse(Color.DarkRed, Color.White, "You have summoned thee"));
            funkyResponses.Add(777, new FunkyResponse(Color.Gold, Color.Black, "May luck be on your side"));
            funkyResponses.Add(404, new FunkyResponse(Color.Gray, Color.White, "Number not found!"));
            funkyResponses.Add(13, new FunkyResponse(Color.LightGray, Color.Black, "Unlucky number"));
            funkyResponses.Add(21, new FunkyResponse(Color.Brown, Color.White, "What's 9+10? Tweny one"));
            funkyResponses.Add(911, new FunkyResponse(Color.SkyBlue, Color.Gray, "Idk if this one is too far :D ▌ ▌"));


        }

        public bool isFunkyNumber(double number)
        {
            int intValue = (int)Math.Abs(Math.Floor(number));
            return funkyResponses.ContainsKey(intValue);
        }

        public FunkyResponse GetFunkyResponse(double number)
        {
            int intValue = (int)Math.Abs(Math.Floor(number));

            if (funkyResponses.ContainsKey(intValue))
            {
                return funkyResponses[intValue];
            }
            else
            {
                return null;
            }
        }

        public List<int> GetAllFunkyNumbers()
        {
            return new List<int>(funkyResponses.Keys);
        }



    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Converters;
using System.Windows.Data;
using System.Windows.Input;

namespace calc.ModelView
{
    class CalcModelView : INotifyPropertyChanged
    {
        //realizing INotifyPropertyChanged interface 
        private Model.CalcModel calculator = new Model.CalcModel();
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public CalcModelView()
        {
            this._stack = "";
            this._input = "0";
            isCalcEnabled = true;
            isOpEnded = false;
            LastOp = "";
            FirstN = "";
            SecondN = "";
            MaxInpLength = 16;
            InputLength = 0;
            isInScientForm = false;
            ScientInput = "";
    }

        //log for storing all recent events
        private string _stack; 
        public string Stack
        {
            get {return _stack;}
            set
            {
                _stack = value;
                if(_stack.Length>20)
                {
                    int n = _stack.Length - 20;
                    _stack = "<<"+_stack.Substring(n - 1, 20);
                }
                OnPropertyChanged("Stack");}
        }
 
        private string _input; //stores user input 
        public string Input
        {
            get
            {
                if (isInScientForm)
                    return ScientInput;
                else
                    return _input;
            }
            set
            {
                if (value.Length > MaxInpLength)
                {
                    ScientInput = toScientForm(value);
                    isInScientForm = true;
                }
                else
                    isInScientForm = false;    
                _input = value;
                OnPropertyChanged("Input");
            }
        }

        //is calculator ready to work (it's not when there's error mrssage on display)
        private bool isCalcEnabled; 
        //have operation ended
        private bool isOpEnded;
        //first and second operand
        private string FirstN;
        private string SecondN;
        //last operation
        private string LastOp;
        //max length of input on display
        private int MaxInpLength;
        private int InputLength;
        //scientific form (1,167e-3)
        private bool isInScientForm;
        private string ScientInput;

        public ICommand NumberBtnPressed {
            get { return new RelayCommand(param => numberBtnPressed(param)); } }
        public ICommand OneOperandOperationBtnPressed {
            get { return new RelayCommand(param => oneOperandOperationBtnPressed(param)); } }
        public ICommand TwoOperandOperationBtnPressed {
            get { return new RelayCommand(param => twoOperandOperationBtnPressed(param)); } }
        public ICommand EqualsBtnPressed {
            get { return new RelayCommand(param => equalsBtnPressed()); } }
        public ICommand DelBtnPressed {
            get { return new RelayCommand(param => delBtnPressed()); } }
        public ICommand ClearBtnPressed {
            get { return new RelayCommand(param => clearBtnPressed()); } }
        public ICommand ClearEntryBtnPressed {
            get { return new RelayCommand(param => clearEntryBtnPressed()); } }
        public ICommand PointBtnPressed {
            get { return new RelayCommand(param => pointBtnPressed()); } }

        //transforms numbers with too much digits into shorter scientific form
        private string toScientForm(string par)
        {
            double n = Convert.ToDouble(par);
            int e = 0;
            while(n>10 && n>-10)
            {
                n /= 10;
                if (n > 0)
                    e++;
                else
                    e--;
            }
            string res = n.ToString() + "E" + e.ToString();
            return res;
        }

        //0-9
        private void numberBtnPressed(object param)
        {
            string numb = param.ToString();
            if(isCalcEnabled)
            {
                if(InputLength <= MaxInpLength)
                {
                    if (isOpEnded)
                    {
                        Input = numb;
                        isOpEnded = false;
                        InputLength++;
                    }
                    else
                    {
                        if (Input == "0")
                            Input = numb;
                        else
                        {
                            Input += numb;
                            InputLength++;
                        }
                    }
                }
            }
        }

        private bool ValidateOutput(string inp)
        {
            if (inp == "Invalid input" || inp == "Can't divide by zero" || inp=="Type overflow")
                return false;
            return true;
        }

        private bool isPointIn(string x)
        {
            int n = x.IndexOf(',');
            if (n > 0)
                return true;
            return false;
        }

        // sqrt 1/x +-
        private void oneOperandOperationBtnPressed(object param)
        {
            string oper = param.ToString();
            if (isCalcEnabled)
            {
                isOpEnded = true;
                InputLength = 0;
                switch (oper)
                {
                    case ("r"):
                        Stack += "sqrt(" + Input + ")";
                        Input = calculator.root(Input);
                        isCalcEnabled = ValidateOutput(Input);
                        break;
                    case ("-1"):
                        Stack += "reciproc(" + Input + ")";
                        Input = calculator.inv(Input);
                        isCalcEnabled = ValidateOutput(Input);
                        break;
                    case ("+-"):
                        if (Input.Contains("-"))
                            Input = Input.Trim('-');
                        else
                        {
                            if(Input!="0")
                            {
                                string buf = Input;
                                Input = "-";
                                Input += buf;
                            }  
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //calculates result
        private string Calculate(string operation, string input)
        {
            if(LastOp=="")
            {
                LastOp = operation;
                FirstN = input;
                return FirstN;
            }
            else
            {
                SecondN = input;
                switch(LastOp)
                {
                    case ("+"):
                        if(calculator.isPercentUsed)
                        {
                            string buf = calculator.percent(input, FirstN);
                            FirstN = calculator.plus(FirstN, buf);
                            calculator.isPercentUsed = false;
                        }
                        else
                            FirstN = calculator.plus(FirstN, SecondN);
                        SecondN = "";
                        LastOp = operation;
                        return FirstN;
                    case ("-"):
                        if (calculator.isPercentUsed)
                        {
                            string buf = calculator.percent(input, FirstN);
                            FirstN = calculator.minus(FirstN, buf);
                            calculator.isPercentUsed = false;
                        }
                        else
                            FirstN = calculator.minus(FirstN, SecondN);
                        SecondN = "";
                        LastOp = operation;
                        return FirstN;
                    case ("*"):
                        if (calculator.isPercentUsed)
                        {
                            string buf = calculator.percent(input, FirstN);
                            FirstN = calculator.mult(FirstN, buf);
                            calculator.isPercentUsed = false;
                        }
                        else
                            FirstN = calculator.mult(FirstN, SecondN);
                        SecondN = "";
                        LastOp = operation;
                        return FirstN;
                    case ("/"):
                        if (calculator.isPercentUsed)
                        {
                            string buf = calculator.percent(input, FirstN);
                            FirstN = calculator.div(FirstN, buf);
                            calculator.isPercentUsed = false;
                        }
                        else
                            FirstN = calculator.div(FirstN, SecondN);
                        SecondN = "";
                        LastOp = operation;
                        return FirstN;
                    default:
                        return "Invalid input";
                }
            }
        }

        private void twoOperandOperationBtnPressed(object param)
        {
            string oper = param.ToString();
            if (isCalcEnabled)
            {
                isOpEnded = true;
                InputLength = 0;
                isCalcEnabled = ValidateOutput(Input);
                switch (oper)
                {
                    case ("/"):
                        Stack += Input + "/";
                        Input = Calculate("/", Input);
                        isCalcEnabled = ValidateOutput(Input);
                        break;
                    case ("*"):
                        Stack += Input + "*";
                        Input = Calculate("*", Input);
                        break;
                    case ("-"):
                        Stack += Input + "-";
                        Input = Calculate("-", Input);
                        break;
                    case ("+"):
                        Stack += Input + "+";
                        Input = Calculate("+", Input);
                        break;
                    case ("%"):
                        calculator.isPercentUsed = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void equalsBtnPressed()
        {
            if (isCalcEnabled)
            {
                isOpEnded = true;
                Input = Calculate(LastOp, Input);
                Stack = "";
                LastOp = "";
                FirstN = "";
                SecondN = "";
                InputLength = 0;
                isCalcEnabled = ValidateOutput(Input);
            }
        }

        private void delBtnPressed()
        {
            if(isCalcEnabled)
            {
                if (Input.Length > 1)
                    Input = Input.Substring(0, Input.Length - 1); 
                else
                    Input = "0";
                InputLength--;
            }
        }

        private void clearBtnPressed()
        {
            if (!isCalcEnabled)
                isCalcEnabled = true;
            Stack = "";
            Input = "0";
            LastOp = "";
            FirstN = "";
            SecondN = "";
            InputLength = 0;
        }

        private void clearEntryBtnPressed()
        {
            if (!isCalcEnabled)
                isCalcEnabled = true;
            Input = "0";
            InputLength = 0;
        }

        private void pointBtnPressed()
        {
            if (!isPointIn(Input) && isCalcEnabled)
            {
                Input += ",";
                InputLength++;
            }
        }
    }

    //realizing ICommand interface to work with commands from buttons in view
    public class RelayCommand : ICommand
    {
        private Action<object> commandTask;

        public RelayCommand(Action<object> extCommand)
        {
            commandTask = extCommand;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            commandTask(parameter);
        }
    }
}

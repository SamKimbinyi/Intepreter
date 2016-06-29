using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Intepreter
{
    class Cradle
    {
        // Constants
        public const string TAB = "\t";
        public const string NEWLINE = "/n";
        public readonly char[] ADD_OPERATORS = new  char[] { '+', '-' };
        public readonly char[] MULTIPLY_OPERATORS = new char[] { '*', '-' };
        //Charchter stored
        public static char look;


        //Read characters from Input Stream
        public void getChar() {

            look = (char)Console.Read();
        }
        //Report an Error

        public void Error(string s) {
            Console.WriteLine("\n Error{0}", s);


        }

        //Report Error and Halt
        public void Abort(string s) {

            Error(s);
            Environment.Exit(0);
        }

        //Report what was expected
        public void Expected(string s) {
            Abort(s + "Expected");
        }
        //Match a Specific input Char
        public void Match(char c) {

            if (look == c)
            {

                getChar();
            }
            else {
                Expected("'" +c +"'"); 
            }
        }

        //Recognize an aAlphabet Char
        public bool isAlpha(char c) {
            if (Char.IsLetter(c))
            {

                return true;
            }
            else {
                return false;
            }
        }

        //Recognise a Integer
        public bool isDigit(char c) {
            if (Char.IsDigit(c))
            {

                return true;
            }
            else {
                return false;
            }

        }

        //Get an identifier

        public char GetName() {
            try
            {
                if (!isAlpha(look))
                {
                    Expected("Name");
                }

                 return Char.ToUpper(look);
            }
            finally {

                getChar();
            }
        }

        //Get a number
        public char getNum() {
            try
            {
                if (!isDigit(look))
                {
                    Expected("Integer");

                }
                return look;

            }
            finally {
                getChar();
            }
        }

        //output a string with a Tab

        public void Emit(string s) {

            Console.WriteLine(TAB + s);
        }

        //output a string with tab and new line
        public void EmitLn(string s) {
            Emit(s);
            Console.WriteLine();
        }


        //Pase and Translate a Math Expression
        public void Term() {

            Factor();

            while (MULTIPLY_OPERATORS.Contains(look)) {


                switch (look) {
                    case '*': Multiply();
                        break;
                    case '/': Divide();
                        break;
                    default: Expected("Mulop");
                        break;
                        

                }
            }
                }


        //Recognize and Translate an Add
        public void Add()
        {
            Match('+');
            Term();
            EmitLn("ADD D1,D0");
        }
        //Recognize and Translate an Subtract
        public void Subtract()
        {
            Match('-');
            Term();
            EmitLn("SUB D1,D0");
            EmitLn("NEG D0");
        }
        //Recognize and Translate an Multiply
        public void Multiply() {

            Match('*');
            Factor();
            EmitLn("Muls (SP) + D0");
        }

        //Recognize and Translate Divide
        public void Divide() { 
        

            Match('/');
            Factor();
            EmitLn("Muls (SP) + D1");
            EmitLn("Divs D1, D0");
        }

        //Recognize and Translate an AddOP (Subtraction is addition of negative numbers)
        public bool isAddop(char c) {

            if (ADD_OPERATORS.Contains(c))
            {

                return true;
            }
            else {
                return false;
            }
        }

        public void Expression() {
          

            if (isAddop(look)){
                EmitLn("CLR D0");
            }
            else {
                Term();
            }

            while (isAddop(look))
            {
                switch (look)
                {
                    case '+':
                        Add();
                        break;
                    case '-':
                        Subtract();
                        break;
                    default:
                        Expected("Addop");
                        break;

                }
            }
        }

        //Parse and TRanslate a maths fator
        public void Factor() {
            //recognize brackets
            if (look == '(')
            {
                Match('(');
                Expression();
                Match(')');

            }
            else
            {

                EmitLn("MOVE # " + getNum() + ",D0");
            }
        }
     
        //Initilize this class
        public void initialize() {
            getChar();

        }
    }
   

}

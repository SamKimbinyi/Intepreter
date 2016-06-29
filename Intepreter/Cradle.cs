using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intepreter
{
    class Cradle
    {
        public const string Tab = "\t";


        public static char look;


        //Read charachter form input stream
        public void getChar() {

            look = (char)Console.Read();
        }
        //Report an error

        public void Error(string s) {
            Console.WriteLine("\n Error{0}", s);


        }

        //Report Error and halt
        public void Abort(string s) {

            Error(s);
           // Environment.Exit(0);
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

        //Recognize an alphabet char
        public bool isAlpha(char c) {
            if (Char.IsLetter(c))
            {

                return true;
            }
            else {
                return false;
            }
        }

        //Recognise a number
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

            Console.WriteLine(Tab + s);
        }

        //output a string with tab and new line
        public void EmitLn(string s) {
            Emit(s);
            Console.WriteLine();
        }


        //Pase and Translate a Math Expression
        public void Term() {

            Factor();

            while (look == '*' || look == '/') {


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


        //recognize and TRanslate an Add
        public void Add()
        {
            Match('+');
            Term();
            EmitLn("ADD D1,D0");
        }

        public void Subtract()
        {
            Match('-');
            Term();
            EmitLn("SUB D1,D0");
            EmitLn("NEG D0");
        }
        public void Multiply() {

            Match('*');
            Factor();
            EmitLn("Muls (SP) + D0");
        }

        public void Divide() { 
        

            Match('/');
            Factor();
            EmitLn("Muls (SP) + D1");
            EmitLn("Divs D1, D0");
        }


        public void Expression() {
            Term();

            while (look == '+' || look == '-')
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
                        Expected("Add Operation");
                        break;

                }
            }
        }

        //parse and TRanslate a maths fator
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
     
        public void initialize() {
            getChar();

        }
    }
   

}

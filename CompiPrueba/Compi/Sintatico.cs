using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Compi
{

    class Sintatico
    {
        public int brfSum = 0;
        public int briSum = 0;

        Polish lispolish = new Polish();
        public int variableTipo = 0;
        public string nombreVariable;
        public int tipoVariable = 0;
        public int tokenVar = 0;
        public string etiqueta;
        public string apuntador;
        public int contIfBF = 0;
        public int contIfBI = 0;
        public int contWhileBF = 0;
        public int contWhileBI = 0;
        public int iteradorWhileD = 0;
        public string ensambladorPolish;
        public string varTemp;
        public int contVarTemp = 0;
        public string compiEnsamblador;
        public string variables;
        public string finalCompi;


        int p = 0;
        bool errorsintactico = false;
        public List<Token> listaToken;
        public List<Error> listaError;
        public List<Variable> listaVar;
        public List<Variable> listAuxVar;
        public List<int> listaPostfix;
        public List<int> listaAuxPostfix;
        public List<Variable> listaAuxInfix;
        public Stack<int> pilaPostfix = new Stack<int>();
        public Stack<int> pilaPolish = new Stack<int>();
        public List<Polish> listaPolish;
        public Stack<int> auxpilaPolish = new Stack<int>();
        public Stack<Polish> auxpilaPolish2 = new Stack<Polish>();
        public Stack<string> pilavarTempPolish;
        public Stack<string> pilavarPolish;





        public Sintatico(List<Error> errorlis, List<Token> tokenlis)
        {
            listaToken = tokenlis;  // inicializar
            listaError = errorlis;  // inicializar
            listaVar = new List<Variable>();
            listAuxVar = new List<Variable>();
            listaPostfix = new List<int>();
            listaAuxPostfix = new List<int>();
            listaAuxInfix = new List<Variable>();
            listaPolish = new List<Polish>();
            Stack stack = new Stack();
            lispolish = new Polish();
            pilavarTempPolish = new Stack<string>();
            pilavarPolish = new Stack<string>();



        } //constructor
        public void incrementarpunteroSin()
        {
            if (!errorsintactico) { p++; }
        }

        public int[,] mSuma = {
            //   203     204      202        205
         //      int  || real || string ||  bool
            {    203  ,   204  ,   500  ,   500,},
            {    204  ,   204  ,   500  ,   500,},
            {    500  ,   500  ,   202  ,   500,},
            {    500  ,   500  ,   500  ,   500,}
        };

        public int[,] mResta = { 
         //      int  || real || string ||  bool
            {    203  ,   204  ,   500  ,   500,},
            {    204  ,   204  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,}
        };

        public int[,] mMultiplicacion = { 
         //      int  || real || string ||  bool
            {    203  ,   204  ,   500  ,   500,},
            {    204  ,   204  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,}
        };

        public int[,] mDivision = { 
         //      int  || real || string ||  bool
            {    204  ,   204  ,   500  ,   500,},
            {    204  ,   204  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,}
        };

        public int[,] mAsignacion = { 
         //      int  || real || string ||  bool
            {    203  ,   204  ,   500  ,   500,},
            {    204  ,   204  ,   500  ,   500,},
            {    500  ,   500  ,   202  ,   500,},
            {    500  ,   500  ,   500  ,   500,}
        };

        public int[,] mNotequal = { 
         //      int  || real || string ||  bool
            {    205  ,   205  ,   500  ,   500,},
            {    205  ,   205  ,   500  ,   500,},
            {    500  ,   500  ,   205  ,   500,},
            {    500  ,   500  ,   500  ,   500,}
        };

        public int[,] mMayork = { 
         //      int  || real || string ||  bool
            {    205  ,   205  ,   500  ,   500,},
            {    205  ,   205  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,}
        };

        public int[,] mMenork = { 
         //      int  || real || string ||  bool
            {    205  ,   205  ,   500  ,   500,},
            {    205  ,   205  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,}
        };

        public int[,] mMayorigual = { 
         //      int  || real || string ||  bool
            {    205  ,   205  ,   500  ,   500,},
            {    205  ,   205  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,}
        };

        public int[,] mMenorigual = { 
         //      int  || real || string ||  bool
            {    205  ,   205  ,   500  ,   500,},
            {    205  ,   205  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,}
        };

        public int[,] mIgual = { 
         //      int  || real || string ||  bool
            {    205  ,   205  ,   500  ,   500,},
            {    205  ,   205  ,   500  ,   500,},
            {    500  ,   500  ,   205  ,   500,},
            {    500  ,   500  ,   500  ,   500,}
        };

        public int[,] mAND = { 
         //      int  || real || string ||  bool
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   205,}
        };

        public int[,] mOR = { 
         //      int  || real || string ||  bool
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   500,},
            {    500  ,   500  ,   500  ,   205,}
        };

        public int[,] mNOT = { 
         //      int  || real || string ||  bool
            {    500  ,   500  ,   500  ,   205,},

        };


        public void ejecSintactico()
        {

            if (listaToken.ElementAt<Token>(p).ValorToken == 200) //program
            {
                p++;
                if (listaToken.ElementAt<Token>(p).ValorToken == 100) //id
                {

                    p++;
                    if (listaToken.ElementAt<Token>(p).ValorToken == 114) //;
                    {
                        p++;
                        block();
                    }
                    else if (listaToken.ElementAt<Token>(p).ValorToken != 115) //.
                    {
                        listaError.Add(ManejoErrores(507, listaToken.ElementAt<Token>(p).Linea));
                    }
                }
            }
            else
            {
                listaError.Add(ManejoErrores(506, listaToken.ElementAt<Token>(p).Linea));
            }
        }

        public void block()
        {
            variableDeclarationPart();
            if (!errorsintactico) { statementPart(); }

        }

        public void variableDeclarationPart()
        {


            if (listaToken.ElementAt<Token>(p).ValorToken == 201) //var
            {
                p++;
                variableDeclaration();
                int ma = listaToken.ElementAt<Token>(p).ValorToken;
                if (listaToken.ElementAt<Token>(p).ValorToken == 114)//;
                {
                    while (listaToken.ElementAt<Token>(p).ValorToken == 114) //;
                    {
                        p++;
                        variableDeclaration();
                        if (listaToken.ElementAt<Token>(p).ValorToken != 114)//;
                        {
                            if (listaToken.ElementAt<Token>(p).ValorToken == 206 && listaToken.ElementAt<Token>(p - 1).ValorToken == 114) { break; } //begin y ;
                            listaError.Add(ManejoErrores(520, listaToken.ElementAt<Token>(p - 1).Linea));
                            errorsintactico = true;
                            break;
                        }



                    }
                }
                else
                {
                    listaError.Add(ManejoErrores(5201, listaToken.ElementAt<Token>(p - 1).Linea)); errorsintactico = true;

                }


            }
            else
            {
                listaError.Add(ManejoErrores(528, listaToken.ElementAt<Token>(p).Linea)); errorsintactico = true;// se espera var    
            }
        }

        private void variableDeclaration()
        {
            //p++;
            if (listaToken.ElementAt<Token>(p).ValorToken == 100) //id
            {
                agregarVariablePilaAux(listaToken.ElementAt<Token>(p).Lexema);
                p++;

                while (listaToken.ElementAt<Token>(p).ValorToken == 117) //,
                {
                    p++;
                    if (listaToken.ElementAt<Token>(p).ValorToken == 100)//id
                    {
                        agregarVariablePilaAux(listaToken.ElementAt<Token>(p).Lexema);
                        p++;
                    }
                    else
                    {
                        listaError.Add(ManejoErrores(504, listaToken.ElementAt<Token>(p).Linea));
                    }
                }
                int ma = listaToken.ElementAt<Token>(p).ValorToken;
                if (listaToken.ElementAt<Token>(p).ValorToken == 116)//:
                {
                    type();
                }

            }
            else if (listaToken.ElementAt<Token>(p).ValorToken != 206)
            {
                listaError.Add(ManejoErrores(509, listaToken.ElementAt<Token>(p).Linea));
            }
        }

        private void type()
        {
            p++;
            if (listaToken.ElementAt<Token>(p).ValorToken == 202 ||
                listaToken.ElementAt<Token>(p).ValorToken == 203 ||
                listaToken.ElementAt<Token>(p).ValorToken == 204 ||
                listaToken.ElementAt<Token>(p).ValorToken == 205)
            {
                if (listaToken.ElementAt<Token>(p).ValorToken == 202)//string
                {
                    defVar(202);
                    p++;
                }
                else if (listaToken.ElementAt<Token>(p).ValorToken == 203)//integer
                {

                    defVar(203);
                    p++;
                }
                else if (listaToken.ElementAt<Token>(p).ValorToken == 204)//real
                {

                    defVar(204);
                    p++;
                }
                else if (listaToken.ElementAt<Token>(p).ValorToken == 205)//boolean
                {

                    defVar(205);
                    p++;
                }
                listAuxVar.Clear();

            }
            else
            {
                listaError.Add(ManejoErrores(505, listaToken.ElementAt<Token>(p).Linea));
            }
        }

        public void statementPart()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 206)// begin
            {
                agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);
                p++;
                statement();
                while (listaToken.ElementAt<Token>(p).ValorToken == 114) //;
                {
                    agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);
                    
                    p++;

                    if (listaToken.ElementAt<Token>(p).ValorToken == 207) //end
                    {
                        break;
                    }
                        statement();
                    
                }
                if (listaToken.ElementAt<Token>(p).ValorToken == 207) //end
                {

                    p++;
                    if (listaToken.ElementAt<Token>(p).ValorToken == 114) //;
                    {
                        
                        p++;

                    }
                    else if (listaToken.ElementAt<Token>(p).ValorToken == 115) // .final
                    {
                        crearEnsamblador();
                        MessageBox.Show("SINTACTICO COMPLIADO CON EXITO");
                    }
                    else
                    {
                        listaError.Add(ManejoErrores(508, listaToken.ElementAt<Token>(p).Linea));//corregir error
                    }
                }
                else
                {
                    listaError.Add(ManejoErrores(529, listaToken.ElementAt<Token>(p).Linea));
                }
            }
            else
            {
                listaError.Add(ManejoErrores(509, listaToken.ElementAt<Token>(p - 1).Linea));
            }

        }

        private void statement()
        {
            
            if (listaToken.ElementAt<Token>(p).ValorToken == 100 ||//id
                listaToken.ElementAt<Token>(p).ValorToken == 208 ||//read
                listaToken.ElementAt<Token>(p).ValorToken == 209)//write
            {
                simpleStatement();
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 210 ||//if
                     listaToken.ElementAt<Token>(p).ValorToken == 213)//while
            {
                structuredStatement();
            }
            else
            {
                listaError.Add(ManejoErrores(510, listaToken.ElementAt<Token>(p).Linea));
            }
            evaluarPostfijo(listaPostfix, listaPolish);
        }

        private void simpleStatement()
        {

            if (listaToken.ElementAt<Token>(p).ValorToken == 100)// id
            {
                assignmentStatement();
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 208)//read
            {
                agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);
                p++;
                readStatement();
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 209)//write
            {
                agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);
                writeStatement();
            }
            else
            {
                listaError.Add(ManejoErrores(511, listaToken.ElementAt<Token>(p).Linea));
            }

        }

        private void assignmentStatement()
        {
            //p++;
            variable();
            if (listaToken.ElementAt<Token>(p).ValorToken == 113) // :=
            {
                agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);

                p++;
                expression();
            }
            else
            {
                listaError.Add(ManejoErrores(510, listaToken.ElementAt<Token>(p).Linea)); // error de asignaciones if write else while
            }
        }

        private void variable()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 100)//id
            {
                // p++; pormientras xq no se que chucha son estos metodos
                entireVariable();
            }
            else
            {
                listaError.Add(ManejoErrores(504, listaToken.ElementAt<Token>(p).Linea));
            }
        }

        private void entireVariable()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 100) //id
            {
                variableIdentifier();
            }
            else
            {
                listaError.Add(ManejoErrores(504, listaToken.ElementAt<Token>(p).Linea));
            }
        }

        private void variableIdentifier()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 100) //id
            {

                variableNoDeclarada(listaToken.ElementAt<Token>(p).Lexema);
                p++;
            }
            else
            {
                listaError.Add(ManejoErrores(504, listaToken.ElementAt<Token>(p).Linea));
            }
        }

        private void readStatement()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 118) // (
            {
                p++;
                variable();
                while (listaToken.ElementAt<Token>(p).ValorToken == 117) // ,
                {
                    p++;
                    variable();
                }
                if (listaToken.ElementAt<Token>(p).ValorToken == 119)// ))
                {
                    //evaluarPostfijo(listaPostfix, listaPolish);
                    p++;
                }
                else
                {
                    listaError.Add(ManejoErrores(524, listaToken.ElementAt<Token>(p).Linea));
                }
            }
            else
            {
                listaError.Add(ManejoErrores(526, listaToken.ElementAt<Token>(p).Linea));
            }
        }

        private void writeStatement()
        {

            if (listaToken.ElementAt<Token>(p).ValorToken == 209) // write
            {
                p++;
                if (listaToken.ElementAt<Token>(p).ValorToken == 118) // (
                {
                    p++;
                    variable();
                    while (listaToken.ElementAt<Token>(p).ValorToken == 117) //,
                    {
                        p++;
                        variable();
                    }
                }
                else
                {
                    listaError.Add(ManejoErrores(526, listaToken.ElementAt<Token>(p).Linea));
                }
                if (listaToken.ElementAt<Token>(p).ValorToken == 119) // )
                {
                    //evaluarPostfijo(listaPostfix, listaPolish);
                    p++;
                }
                else
                {
                    listaError.Add(ManejoErrores(524, listaToken.ElementAt<Token>(p).Linea));
                }
            }
            else
            {
                listaError.Add(ManejoErrores(500, listaToken.ElementAt<Token>(p).Linea)); //arreglar errores al final
            }
        }

        private void structuredStatement()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 210) //if
            {
                conditionalStatement();
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 213) //while
            {
                repetitiveStatement();
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 219 ||
                listaToken.ElementAt<Token>(p).ValorToken == 220)
            {
                p++;
            }
            else
            {
                listaError.Add(ManejoErrores(512, listaToken.ElementAt<Token>(p).Linea));
            }

        }

        private void conditionalStatement()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 210)//if
            {
                ifStatement();
            }
            else
            {
                listaError.Add(ManejoErrores(516, listaToken.ElementAt<Token>(p).Linea));
            }

        }

        private void ifStatement()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 210)//if
            {
                p++;
                expression();
                if (listaToken.ElementAt<Token>(p).ValorToken == 211)//then
                {
                    p++;

                    while (listaToken.ElementAt<Token>(p).ValorToken == 100 ||//id
                           listaToken.ElementAt<Token>(p).ValorToken == 208 ||//read
                           listaToken.ElementAt<Token>(p).ValorToken == 209 ||//write   
                           listaToken.ElementAt<Token>(p).ValorToken == 210 ||//if
                           listaToken.ElementAt<Token>(p).ValorToken == 213) //while
                    {
                        statement();
                    }
                    if (listaToken.ElementAt<Token>(p).ValorToken == 212)//else
                    {
                        p++;
                        statement();
                    }
                }
                else
                {
                    listaError.Add(ManejoErrores(515, listaToken.ElementAt<Token>(p).Linea));
                }
            }
            else
            {
                listaError.Add(ManejoErrores(516, listaToken.ElementAt<Token>(p).Linea));
            }
        }

        private void repetitiveStatement()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 213)//while
            {
                whileStatement();
            }
            else
            {
                listaError.Add(ManejoErrores(514, listaToken.ElementAt<Token>(p).Linea));
            }

        }

        private void whileStatement()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 213)//while
            {
                p++;

                crearEtiquetaBrinco("while", "BF");
                expression();

                if (listaToken.ElementAt<Token>(p).ValorToken == 214)//do
                {
                    asignarEtiqueta("BF");
                    iteradorWhileD++;
                    p++;
                    if (listaToken.ElementAt<Token>(p).ValorToken == 206)//begin
                    {

                        while (listaToken.ElementAt<Token>(p).ValorToken != 219)//endw
                        {
                            p++;
                            if (listaToken.ElementAt<Token>(p).ValorToken != 219)
                            {
                                statement();
                            }
                            
                        }

                        crearEtiquetaBrinco("while", "BI");
                        asignarEtiqueta("BI");
                        if (listaToken.ElementAt<Token>(p).ValorToken == 219) //endw
                        {
                            contWhileBF--;
                        }
                        p++;
                    }
                    else
                    {
                        listaError.Add(ManejoErrores(533, listaToken.ElementAt<Token>(p).Linea));
                    }

                }
                else
                {
                    listaError.Add(ManejoErrores(513, listaToken.ElementAt<Token>(p).Linea));
                }
            }
            else
            {
                listaError.Add(ManejoErrores(514, listaToken.ElementAt<Token>(p).Linea));
            }
        }

        private void expression()
        {
            simpleExpression();
            if (listaToken.ElementAt<Token>(p).ValorToken == 107 || //Operadores relacionales
                listaToken.ElementAt<Token>(p).ValorToken == 108 ||
                listaToken.ElementAt<Token>(p).ValorToken == 109 ||
                listaToken.ElementAt<Token>(p).ValorToken == 110 ||
                listaToken.ElementAt<Token>(p).ValorToken == 111 ||
                listaToken.ElementAt<Token>(p).ValorToken == 112)
            {
                agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);

                p++;
                simpleExpression();

            }
            evaluarPostfijo(listaPostfix, listaPolish);
        }

        private void simpleExpression()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 118 || //(
                listaToken.ElementAt<Token>(p).ValorToken == 100 || // id
                listaToken.ElementAt<Token>(p).ValorToken == 217 || //not
                listaToken.ElementAt<Token>(p).ValorToken == 101 ||//entero
                listaToken.ElementAt<Token>(p).ValorToken == 102 ||//real
                listaToken.ElementAt<Token>(p).ValorToken == 120) //string
            {

                term();
                if (listaToken.ElementAt<Token>(p).ValorToken == 103 ||// +
                    listaToken.ElementAt<Token>(p).ValorToken == 104 ||// -
                    listaToken.ElementAt<Token>(p).ValorToken == 216)// or
                {
                    addingOperator();
                    term();
                }
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 103 ||//+
                listaToken.ElementAt<Token>(p).ValorToken == 104)//-
            {
                p++;
                sign();
                term();
                if (listaToken.ElementAt<Token>(p).ValorToken == 103 ||// +
                    listaToken.ElementAt<Token>(p).ValorToken == 104 ||// -
                    listaToken.ElementAt<Token>(p).ValorToken == 216)// or
                {
                    addingOperator();
                    term();
                }
            }
            else { listaError.Add(ManejoErrores(525, listaToken.ElementAt<Token>(p).Linea)); }

        }

        private void addingOperator()
        {

            if (listaToken.ElementAt<Token>(p).ValorToken == 103)//+
            {
                agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);

                p++;
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 104)//-
            {
                agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);

                p++;
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 216)//or
            {
                agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);

                p++;
            }
            else
            {
                listaError.Add(ManejoErrores(518, listaToken.ElementAt<Token>(p).Linea));
            }
        }

        private void term()
        {
            factor();
            if (listaToken.ElementAt<Token>(p).ValorToken == 105 ||// *
                    listaToken.ElementAt<Token>(p).ValorToken == 106 ||// /
                    listaToken.ElementAt<Token>(p).ValorToken == 215)// and
            {
                agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);

                p++;
                term();
            }


        }

        private void factor()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 100) //id
            {
                //p++;
                variable();

            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 101 ||//entero
                    listaToken.ElementAt<Token>(p).ValorToken == 102 ||//real
                    listaToken.ElementAt<Token>(p).ValorToken == 120)//string

            {
                //p++;
                unsignedConstant();
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 118)//(
            {
                p++;
                expression();
                if (listaToken.ElementAt<Token>(p).ValorToken == 119)//)
                {
                    //evaluarPostfijo(listaPostfix, listaPolish);
                    p++;
                }
                else { listaError.Add(ManejoErrores(524, listaToken.ElementAt<Token>(p).Linea)); }
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 217) //not
            {
                agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);

                p++;
                factor();
            }
            else
            {
                listaError.Add(ManejoErrores(501, listaToken.ElementAt<Token>(p).Linea));
            }
        }

        private void unsignedConstant()
        {
            int valortoken = listaToken.ElementAt<Token>(p).ValorToken;
            if (listaToken.ElementAt<Token>(p).ValorToken == 101 ||//entero		p	41	int

                listaToken.ElementAt<Token>(p).ValorToken == 102)//real
            {
                unsignedNumber();
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 120)//string
            {
                agregarListaPostfix(202, listaToken.ElementAt<Token>(p).Lexema);

                p++;

            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 100) //id
            {
                p++;
            }

            else if (listaToken.ElementAt<Token>(p).ValorToken != 211) //else
            {
                listaError.Add(ManejoErrores(503, listaToken.ElementAt<Token>(p).Linea));
            }
        }

        private void unsignedNumber()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 101) //int
            {
                agregarListaPostfix(203, listaToken.ElementAt<Token>(p).Lexema);

                p++;

            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 102) //real
            {
                agregarListaPostfix(204, listaToken.ElementAt<Token>(p).Lexema);

                p++;

            }
            else
            {
                listaError.Add(ManejoErrores(501, listaToken.ElementAt<Token>(p).Linea));
            }

        }

        private void sign()
        {
            if (listaToken.ElementAt<Token>(p).ValorToken == 103)//+
            {
                agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);

                p++;

            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 104)//-
            {
                agregarListaPostfix(listaToken.ElementAt<Token>(p).ValorToken, listaToken.ElementAt<Token>(p).Lexema);

                p++;

            }
            else
            {
                listaError.Add(ManejoErrores(500, listaToken.ElementAt<Token>(p).Linea));
            }
        }


        //---------------------------------------------------------- METODOS ---------------------------------------------------//
        public void defVar(int tipo)
        {
            for (int i = 0; i < listAuxVar.Count; i++)
            {
                if (listaVar.Count <= 0)
                {
                    listAuxVar.ElementAt<Variable>(0).TipoVariable = tipo;
                    listaVar.Add(listAuxVar.ElementAt<Variable>(0));
                }
                else
                {
                    if (!variableYaDeclarada(listAuxVar.ElementAt<Variable>(i).Nombre))
                    {
                        listAuxVar.ElementAt<Variable>(i).TipoVariable = tipo;
                        listaVar.Add(listAuxVar.ElementAt<Variable>(i));
                    }
                    else
                    {
                        listaError.Add(ManejoErrores(530, listaToken.ElementAt<Token>(p).Linea));
                        break;
                    }
                }

            }
        }

        private bool variableYaDeclarada(string nombreVar)
        {

            for (int i = 0; i < listaVar.Count; i++)
            {
                if (nombreVar == listaVar.ElementAt<Variable>(i).Nombre)
                {
                    return true;
                }
            }
            return false;

        }

        private bool variableNoDeclarada(string nombreVar)
        {
            for (int i = 0; i < listaVar.Count; i++)
            {
                if (nombreVar == listaVar.ElementAt<Variable>(i).Nombre)
                {
                    agregarListaPostfix(listaVar.ElementAt<Variable>(i).TipoVariable, nombreVar);

                    return true;
                }
            }
            listaError.Add(ManejoErrores(531, listaToken.ElementAt<Token>(p).Linea));
            return false;
        }

        public void agregarVariablePilaAux(string nombreVar)
        {
            Variable nuevaVariable = new Variable() { Nombre = nombreVariable };
            nuevaVariable.Nombre = nombreVar;

            listAuxVar.Add(nuevaVariable);
        }

        public void agregarListaPostfix(int token, string lexema)
        {
            Polish lispolish = new Polish();
            lispolish.Lexema = lexema;

            if (token == 202 ||
                token == 203 ||
                token == 204 ||
                token == 205) // Operandos
            {
                if (listaPolish.Count() != 0)
                {
                    if (listaToken.ElementAt<Token>(p - 1).ValorToken == 213) //while
                    {
                        lispolish.Etiqueta = etiqueta;

                    }
                    else if (listaPolish.ElementAt(listaPolish.Count() - 1).Lexema == "BRI")//endw
                    {
                        lispolish.Etiqueta = "D" + iteradorWhileD;
                        iteradorWhileD--;
                    }
                }

                listaPostfix.Add(token);
                listaPolish.Add(lispolish);

            }
            else if (token == 103 ||
                     token == 104 ||
                     token == 105 ||
                     token == 106 ||
                     token == 107 ||
                     token == 108 ||
                     token == 109 ||
                     token == 110 ||
                     token == 111 ||
                     token == 112 ||
                     token == 113 ||
                     token == 215 ||
                     token == 216 ||
                     token == 217 ||
                     token == 208 ||
                     token == 209)
            {
                if (pilaPostfix.Count == 0)
                {
                    pilaPostfix.Push(token);
                    auxpilaPolish2.Push(lispolish);
                }
                else
                {
                    bool prioOp1 = prioridadOperadores(token, pilaPostfix.Peek());

                    if (prioOp1)
                    {
                        auxpilaPolish2.Push(lispolish);
                        pilaPostfix.Push(token);
                    }
                    else
                    {
                        listaPolish.Add(auxpilaPolish2.Pop());
                        auxpilaPolish2.Push(lispolish);

                        listaPostfix.Add(pilaPostfix.Pop());
                        pilaPostfix.Push(token);

                    }
                }
            }

        }

        public bool prioridadOperadores(int tokenN, int tokenV)
        {

            if (tokenN == 105)// *
            {
                if (tokenV == 106)// /
                {
                    return false;
                }
                return true;
            }

            if (tokenN == 106)// /
            {
                if (tokenV == 105)
                {
                    return false;
                }
                return true;
            }

            if (tokenN == 103)// +
            {
                if (tokenV == 105 ||
                    tokenV == 106)
                    return false;
                else
                    return true;
            }

            if (tokenN == 104) // -
                if (tokenV == 105 ||
                    tokenV == 106)
                    return false;
                else
                    return true;


            if (tokenN == 113)// :=
            {
                if (tokenV == 105 ||
                    tokenV == 106 ||
                    tokenV == 103 ||
                    tokenV == 104)
                    return false;
                else
                    return true;
            }

            if (tokenN == 109)// <
            {
                if (tokenV == 105 ||
                    tokenV == 106 ||
                    tokenV == 103 ||
                    tokenV == 104)
                    return false;
                else
                    return true;
            }

            if (tokenN == 107) // >
            {
                if (tokenV == 105 ||
                    tokenV == 106 ||
                    tokenV == 103 ||
                    tokenV == 104)
                    return false;
                else
                    return true;
            }

            if (tokenN == 112)// <>
            {
                if (tokenV == 105 ||
                    tokenV == 106 ||
                    tokenV == 103 ||
                    tokenV == 104)
                    return false;
                else
                    return true;
            }

            if (tokenN == 110)// <=
            {
                if (tokenV == 105 ||
                    tokenV == 106 ||
                    tokenV == 103 ||
                    tokenV == 104)
                    return false;
                else
                    return true;
            }

            if (tokenN == 108)// >=
            {
                if (tokenV == 105 ||
                    tokenV == 106 ||
                    tokenV == 103 ||
                    tokenV == 104)
                    return false;
                else
                    return true;
            }

            if (tokenN == 111)// ==
            {
                if (tokenV == 105 ||
                    tokenV == 106 ||
                    tokenV == 103 ||
                    tokenV == 104)
                    return false;
                else
                    return true;
            }

            if (tokenN == 217)// not
            {
                if (tokenV == 105 ||
                    tokenV == 106 ||
                    tokenV == 103 ||
                    tokenV == 104 ||
                    tokenV == 113 ||
                    tokenV == 109 ||
                    tokenV == 107 ||
                    tokenV == 112 ||
                    tokenV == 110 ||
                    tokenV == 108 ||
                    tokenV == 111)
                    return false;
                else
                    return true;
            }

            if (tokenN == 215)// and
            {
                if (tokenV == 105 ||
                    tokenV == 106 ||
                    tokenV == 103 ||
                    tokenV == 104 ||
                    tokenV == 113 ||
                    tokenV == 109 ||
                    tokenV == 107 ||
                    tokenV == 112 ||
                    tokenV == 110 ||
                    tokenV == 108 ||
                    tokenV == 111 ||
                    tokenV == 217)
                    return false;
                else
                    return true;
            }

            if (tokenN == 216)// or
            {
                if (tokenV == 105 ||
                    tokenV == 106 ||
                    tokenV == 103 ||
                    tokenV == 104 ||
                    tokenV == 113 ||
                    tokenV == 109 ||
                    tokenV == 107 ||
                    tokenV == 112 ||
                    tokenV == 110 ||
                    tokenV == 108 ||
                    tokenV == 111 ||
                    tokenV == 217 ||
                    tokenV == 215)
                    return false;
                else
                    return true;
            }

            return false;


        }

        public void evaluarPostfijo(List<int> listaPostfix, List<Polish> listaPolish)
        {

            while (pilaPostfix.Count != 0)
            {
                listaPostfix.Add(pilaPostfix.Pop());
                listaPolish.Add(auxpilaPolish2.Pop());
            }

            pilaPolish.Clear();
            auxpilaPolish2.Clear();

            for (int i = 0; i < listaPostfix.Count; i++)
            {

                if (listaPostfix.ElementAt<int>(i) == 202 ||
                    listaPostfix.ElementAt<int>(i) == 203 ||
                    listaPostfix.ElementAt<int>(i) == 204 ||
                    listaPostfix.ElementAt<int>(i) == 205 )//Operando
                {
                    pilaPolish.Push(listaPostfix.ElementAt<int>(i));
                    auxpilaPolish2.Push(listaPolish.ElementAt<Polish>(i));
                }
                else if (listaPostfix.ElementAt<int>(i) != 208 ||
                         listaPostfix.ElementAt<int>(i) != 209)
                {
                    auxpilaPolish2.Push(listaPolish.ElementAt<Polish>(i));
                    if (pilaPolish.Count() >= 2)
                    {
                        pilaPolish.Push(regresarMatriz(listaPostfix.ElementAt<int>(i), pilaPolish.Pop(), pilaPolish.Pop()));
                        if (pilaPolish.Peek() == 500)
                        {
                            listaError.Add(ManejoErrores(532, listaToken.ElementAt<Token>(p).Linea));
                            pilaPolish.Clear();
                        }
                    }
                    
                }
                else//Operadores
                {   
                    pilaPolish.Push(regresarMatriz(listaPostfix.ElementAt<int>(i), pilaPolish.Pop(), pilaPolish.Pop()));
                    if (pilaPolish.Peek() == 500)
                    {
                        listaError.Add(ManejoErrores(532, listaToken.ElementAt<Token>(p).Linea));
                        listaPostfix.Clear();
                    }
                }
            }
            listaPostfix.Clear();
        }

        public int prepararMatriz(int token)
        {
            switch (token)
            {
                case 203:
                    return 0;
                case 204:
                    return 1;
                case 202:
                    return 2;
                case 205:
                    return 3;
                default:
                    return 500;
            }
        }

        public int regresarMatriz(int token, int op1, int op2)
        {

            switch (token)
            {
                case 103:
                    return mSuma[prepararMatriz(op1), prepararMatriz(op2)];
                case 104:
                    return mResta[prepararMatriz(op1), prepararMatriz(op2)];
                case 105:
                    return mMultiplicacion[prepararMatriz(op1), prepararMatriz(op2)];
                case 106:
                    return mDivision[prepararMatriz(op1), prepararMatriz(op2)];
                case 113:
                    return mAsignacion[prepararMatriz(op1), prepararMatriz(op2)];
                case 107:
                    return mMayork[prepararMatriz(op1), prepararMatriz(op2)];
                case 108:
                    return mMayorigual[prepararMatriz(op1), prepararMatriz(op2)];
                case 109:
                    return mMenork[prepararMatriz(op1), prepararMatriz(op2)];
                case 110:
                    return mMenorigual[prepararMatriz(op1), prepararMatriz(op2)];
                case 111:
                    return mIgual[prepararMatriz(op1), prepararMatriz(op2)];
                case 112:
                    return mNotequal[prepararMatriz(op1), prepararMatriz(op2)];
                case 215:
                    return mAND[prepararMatriz(op1), prepararMatriz(op2)];
                case 216:
                    return mOR[prepararMatriz(op1), prepararMatriz(op2)];
                case 217:
                    return mNOT[prepararMatriz(op1), prepararMatriz(op2)];
                default:
                    return 500;
            }
        }

        private Error ManejoErrores(int estado, int linea)
        {
            string mensajeError;

            switch (estado)
            {
                case 500:
                    mensajeError = "Error se esperaba un '+' o '-'"; //sign()
                    break;
                case 501:
                    mensajeError = "Error se esperaba un entero o un real"; //unsignedNumber()
                    break;
                case 502:
                    mensajeError = "Error se esperaba un entero";//unsignedInteger()
                    break;
                case 503:
                    mensajeError = "Error se esperaba un entero | real | string | identificador";//unsignedConstant()
                    break;
                case 504:
                    mensajeError = "Error se esperaba un identificador"; //variableDeclaration();
                    break;
                case 505:
                    mensajeError = "Error se esperabna un string|bool|integer|real";//type()
                    break;
                case 506:
                    mensajeError = "Error se esperaba program";//ejectSintact()
                    break;
                case 507:
                    mensajeError = "Error se esperaba un punto"; //ejectSintact()
                    break;
                case 508:
                    mensajeError = "Error se esperaba un ;"; //statmentPart()
                    break;
                case 509:
                    mensajeError = "Error se esperaba begin";//statmentPart()
                    break;
                case 510:
                    mensajeError = "Error se esperaba id | read | write | if | while";//statment()
                    break;
                case 511:
                    mensajeError = "Error se esperaba id | read | write"; //simpleStatment()
                    break;
                case 512:
                    mensajeError = "Error se esperaba if|while"; //structuredStatment()
                    break;
                case 513:
                    mensajeError = "Error se esperaba do";//whileStatment()
                    break;
                case 514:
                    mensajeError = "Error se esperaba while";//whileStatment()
                    break;
                case 515:
                    mensajeError = "Error se esperaba then";//ifStatment()
                    break;
                case 516:
                    mensajeError = "Error se esperaba if"; //ifStatment()
                    break;
                case 517:
                    mensajeError = "Error se esperabna un operador relacional";//relationalOperator();
                    break;
                case 518:
                    mensajeError = "Error se esperaba + | - | or";//addingOperator()
                    break;
                case 519:
                    mensajeError = "Error se esperaba * | / | and"; //multiplyingOperator()
                    break;
                case 520:
                    mensajeError = "Error se esperaba un ;";
                    break;
                case 5201:
                    mensajeError = "Error se esperaba un ; despues del tipo";
                    break;
                case 521:
                    mensajeError = "Error se esperaba begin";
                    break;
                case 522:
                    mensajeError = "Error se esperaba id | read | write | if | while";
                    break;
                case 523:
                    mensajeError = "Error se esperaba id | read | write";
                    break;
                case 524:
                    mensajeError = "Error se esperaba )";//readstatement()
                    break;
                case 525:
                    mensajeError = "Error se esperaba un factor";
                    break;
                case 526:
                    mensajeError = "Error se esperaba (";//readstatement()
                    break;
                case 527:
                    mensajeError = "Error se esperaba el diablo mamawebo (";//readstatement()
                    break;
                case 528:
                    mensajeError = "se esperaba var";
                    break;
                case 529: //en los statements cuando hay un statement de mas sin ;
                    mensajeError = "se espera un ; para declarar otro statement";
                    break;
                case 530:
                    mensajeError = "Error variable ya definida";
                    break;
                case 531:
                    mensajeError = "Error variable no definida";
                    break;
                case 532:
                    mensajeError = "Error incompatibilidad de tipos";
                    break;
                case 533:
                    mensajeError = "Error se esperaba begin";
                    break;
                default:
                    mensajeError = "Error inesperado";
                    break;
            }
            return new Error() { Codigo = estado, MensajeError = mensajeError, Tipo = tipoError.Lexico, Linea = linea };


        }

        //--------------------------------------------------------- POLISH -------------------------------------------------------//
        public void crearEtiquetaBrinco(string cond, string tipBrinco)
        {
            Polish lispolish = new Polish();

            switch (cond)
            {
                case "if":
                    if (tipBrinco == "BF")
                    {
                        contIfBF = contIfBF + 1;
                        etiqueta = "A" + contIfBF;
                    }
                    else if (tipBrinco == "BI")
                    {
                        contIfBI = contIfBI + 1;
                        apuntador = "B" + contIfBI;

                    }
                    break;
                case "while":
                    if (tipBrinco == "BF")
                    {
                        contWhileBF = contWhileBF + 1;
                        etiqueta = "C" + contWhileBF;
                    }
                    else if (tipBrinco == "BI")
                    {
                        contWhileBI = contWhileBI + 1;
                        apuntador = "D" + contWhileBI;

                    }
                    break;
            }

            lispolish.Etiqueta = etiqueta;
            lispolish.Apuntador = apuntador;
        }

        public void asignarEtiqueta(string tipoBri)
        {
            Polish polish = new Polish();


            if (tipoBri == "BF")
            {
                brfSum++;
                polish.Lexema = "BRF";
                polish.Apuntador = "D" + brfSum;
            }
            else if (tipoBri == "BI")
            {
                briSum++;
                polish.Lexema = "BRI";
                polish.Apuntador = "C" + contWhileBF;
            }
            listaPolish.Add(polish);
        }


        //------------------------------------------------------ ENSAMBLADOR -----------------------------------------------------//

        public void aggvarPolish(string nodo)
        {
            pilavarPolish.Push(nodo);
        }

        public void sumVarTemp()
        {
            pilavarTempPolish.Push("t"+contVarTemp);
            contVarTemp++;
        }

        public void crearEnsamblador()
        {
            string varPolishAct;
            variables = variables + "INCLUDE MACROS.MAC\r\nDOSSEG\r\n.MODEL SMALL\r\nSTACK 100h\r\n.DATA\r\n";

            variables = variables +
               ".CODE\r\n.386\r\nBEGIN:\r\n\t\t\t MOV AX, @DATA\r\n\t\t\tMOV DS, AX\r\nCALL COMPI\r\n\t\t\t MOV AX, 4C00H\r\n\t\t\tINT 21H\r\nCOMPI PROC\r\n\r\n";

            for (int i = 0; i < listaPolish.Count; i++)
            {
                varPolishAct = listaPolish.ElementAt(i).Lexema;

                if (listaPolish.ElementAt(i).Etiqueta != null)
                {
                    ensambladorPolish = ensambladorPolish + listaPolish.ElementAt(i).Etiqueta + ":\r\n";
                }

                switch (listaPolish.ElementAt(i).Lexema)
                {
                    case "read":
                        ensambladorPolish = ensambladorPolish +
                            "READ " + pilavarPolish.ElementAt(0) + "\r\n";
                        pilavarPolish.Pop();
                        break;
                    case "write":
                        ensambladorPolish = ensambladorPolish +
                            "WRITE " + pilavarPolish.ElementAt(0) + "\r\n";
                        pilavarPolish.Pop();
                        break;
                    case "+":
                        sumVarTemp();
                        ensambladorPolish = ensambladorPolish +
                            "SUMAR " + pilavarPolish.ElementAt(1) + " , " + pilavarPolish.ElementAt(0) + " , " + pilavarTempPolish.First() + "\r\n";
                        pilavarPolish.Pop(); pilavarPolish.Pop();
                        pilavarPolish.Push(pilavarTempPolish.Peek());
                        break;
                    case "-":
                        sumVarTemp();
                        ensambladorPolish = ensambladorPolish +
                            "RESTA " + pilavarPolish.ElementAt(1) + " , " + pilavarPolish.ElementAt(0) + " , " + pilavarTempPolish.First() + "\r\n";
                        pilavarPolish.Pop(); pilavarPolish.Pop();
                        pilavarPolish.Push(pilavarTempPolish.Peek());
                        break;
                    case "*":
                        sumVarTemp();
                        ensambladorPolish = ensambladorPolish +
                            "MULTI " + pilavarPolish.ElementAt(1) + " , " + pilavarPolish.ElementAt(0) + " , " + pilavarTempPolish.First() + "\r\n";
                        pilavarPolish.Pop(); pilavarPolish.Pop();
                        pilavarPolish.Push(pilavarTempPolish.Peek());
                        break;
                    case "/":
                        sumVarTemp();
                        ensambladorPolish = ensambladorPolish +
                            "DIVIDE " + pilavarPolish.ElementAt(1) + " , " + pilavarPolish.ElementAt(0) + " , " + pilavarTempPolish.First() + "\r\n";
                        pilavarPolish.Pop(); pilavarPolish.Pop();
                        pilavarPolish.Push(pilavarTempPolish.Peek());
                        break;
                    case "<":
                        sumVarTemp();
                        ensambladorPolish = ensambladorPolish +
                            "I_MENOR " + pilavarPolish.ElementAt(1) + " , " + pilavarPolish.ElementAt(0) + " , " + pilavarTempPolish.First() + "\r\n";
                        pilavarPolish.Pop(); pilavarPolish.Pop();
                        pilavarPolish.Push(pilavarTempPolish.Peek());
                        break;
                    case "<=":
                        sumVarTemp();
                        ensambladorPolish = ensambladorPolish +
                            "I_MENORIGUAL " + pilavarPolish.ElementAt(1) + " , " + pilavarPolish.ElementAt(0) + " , " + pilavarTempPolish.First() + "\r\n";
                        pilavarPolish.Pop(); pilavarPolish.Pop();
                        pilavarPolish.Push(pilavarTempPolish.Peek());
                        break;
                    case ">":
                        sumVarTemp();
                        ensambladorPolish = ensambladorPolish +
                            "I_MAYOR " + pilavarPolish.ElementAt(1) + " , " + pilavarPolish.ElementAt(0) + " , " + pilavarTempPolish.First() + "\r\n";
                        pilavarPolish.Pop(); pilavarPolish.Pop();
                        pilavarPolish.Push(pilavarTempPolish.Peek());
                        break;
                    case ">=":
                        sumVarTemp();
                        ensambladorPolish = ensambladorPolish +
                            "I_MAYORIGUAL " + pilavarPolish.ElementAt(1) + " , " + pilavarPolish.ElementAt(0) + " , " + pilavarTempPolish.First() + "\r\n";
                        pilavarPolish.Pop(); pilavarPolish.Pop();
                        pilavarPolish.Push(pilavarTempPolish.Peek());
                        break;
                    case "==":
                        sumVarTemp();
                        ensambladorPolish = ensambladorPolish +
                            "I_IGUAL " + pilavarPolish.ElementAt(1) + " , " + pilavarPolish.ElementAt(0) + " , " + pilavarTempPolish.First() + "\r\n";
                        pilavarPolish.Pop(); pilavarPolish.Pop();
                        pilavarPolish.Push(pilavarTempPolish.Peek());
                        break;
                    case "<>":
                        sumVarTemp();
                        ensambladorPolish = ensambladorPolish +
                            "I_DIFERENTES " + pilavarPolish.ElementAt(1) + " , " + pilavarPolish.ElementAt(0) + " , " + pilavarTempPolish.First() + "\r\n";
                        pilavarPolish.Push(pilavarTempPolish.Peek());
                        pilavarPolish.Pop(); pilavarPolish.Pop();
                        break;
                    case ":=":
                        ensambladorPolish = ensambladorPolish +
                            "I_ASIGNAR " + pilavarPolish.ElementAt(1) + " , " + pilavarPolish.ElementAt(0) + "\r\n";
                        pilavarPolish.Pop(); pilavarPolish.Pop();
                        pilavarPolish.Push(pilavarTempPolish.Peek());
                        break;
                    case "BRI":
                        ensambladorPolish = ensambladorPolish +
                           "JMP " + listaPolish.ElementAt(i).Apuntador + "\r\n";
                        break;
                    case "BRF":
                        ensambladorPolish = ensambladorPolish +
                          "JF " + pilavarTempPolish.First() + " , " + listaPolish.ElementAt(i).Apuntador + "\r\n";
                        break;
                    default:
                        aggvarPolish(varPolishAct);
                        break;
                }

            }
            for (int i = 0; i < listaVar.Count; i++)
            {
                variables = variables +
                           listaVar.ElementAt(i).Nombre + " DW ' ', '$'\r\n";
            }
            for (int i = 0; i < pilavarTempPolish.Count; i++)
            {
                variables = variables +
                           pilavarTempPolish.ElementAt(i) + " DW , ?\r\n";
            }

            finalCompi = finalCompi +
                "\t\t\tret\r\n\r\nCOMPI ENDP\r\nEND BEGIN";

            compiEnsamblador += variables+"\r\n"+ensambladorPolish+"\r\n"+finalCompi ;
        }
    }

   
    
}

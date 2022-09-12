using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compi
{
    class Sintatico
    {
        int p = 0;
        public List<Token> listaToken;
        public List<Error> listaError;

        public Sintatico(string codigoFuenteInterface)
        {
            listaToken = new List<Token>();  // inicializar
            listaError = new List<Error>();  // inicializar
        } //constructor

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
                    else if(listaToken.ElementAt<Token>(p).ValorToken != 115) //.
                    {
                        listaError.Add(ManejoErrores(500, listaToken.ElementAt<Token>(p).Linea));//corregir error
                    }
                }
            }
        }

        public void block()
        {
            variableDeclarationPart();
            statementPart();
        }

        public void variableDeclarationPart()
        {

            p++;
            if (listaToken.ElementAt<Token>(p).ValorToken == 201) //var
            {
                p++;
                variableDeclaration();
                while (listaToken.ElementAt<Token>(p).ValorToken != 114) //;
                {
                    p++;
                    variableDeclaration();
                }

            }
        }

        public void statementPart()
        {

            p++;
            if (listaToken.ElementAt<Token>(p).ValorToken == 206)// begin
            {
                p++;
                statement();
                while (listaToken.ElementAt<Token>(p).ValorToken == 114 ) //;.
                {
                    statement();
                    if (listaToken.ElementAt<Token>(p).ValorToken == 207) //end
                    {
                        break;
                    }
                }
                if (listaToken.ElementAt<Token>(p).ValorToken == 207) //end
                {
                    p++;
                    if (listaToken.ElementAt<Token>(p).ValorToken == 114) //;
                    {
                        p++;
                    }
                    else
                    {
                        listaError.Add(ManejoErrores(500, listaToken.ElementAt<Token>(p).Linea));//corregir error
                    }
                }
            }
        }

        private void variableDeclaration()
        {
            p++;
            if (listaToken.ElementAt<Token>(p).ValorToken == 100) //id
            {
                p++;

                while (listaToken.ElementAt<Token>(p).ValorToken == 117) //,
                {
                    p++;
                    if (listaToken.ElementAt<Token>(p).ValorToken == 100)//id
                    {
                        p++;
                    }
                    else
                    {
                        listaError.Add(ManejoErrores(500, listaToken.ElementAt<Token>(p).Linea));//corregir error
                    }
                }

                if (listaToken.ElementAt<Token>(p).ValorToken == 116)//:
                {
                    p++;
                    type();
                }
            }
        }

        private void type()
        {
            p++;
            if (listaToken.ElementAt<Token>(p).ValorToken == 202)//string
            {
                p++;
            } else if (listaToken.ElementAt<Token>(p).ValorToken == 203)//integer
            {
                p++;
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 204)//real
            {
                p++;
            }
            else if (listaToken.ElementAt<Token>(p).ValorToken == 205)//boolean
            {
                p++;
            }
            else
            {
                listaError.Add(ManejoErrores(500, listaToken.ElementAt<Token>(p).Linea));//corregir error
            }
        }

        private Error ManejoErrores(int estado, int linea)
        {
            string mensajeError;

            switch (estado)
            {
                case 500:
                    mensajeError = "Se esperaba un digito";
                    break;
                case 501:
                    mensajeError = "Simbolo no valido";
                    break;
                case 502:
                    mensajeError = "Otra cosa";
                    break;
                default:
                    mensajeError = "Error inesperado";
                    break;
            }
            return new Error() { Codigo = estado, MensajeError = mensajeError, Tipo = tipoError.Lexico, Linea = linea };


        }

    }
}

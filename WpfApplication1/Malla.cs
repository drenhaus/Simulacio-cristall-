﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfApplication1
{
    class Malla
    {
        //ATRIBUTOS
        int x = 0; //número de columnas 
        int y = 0; //número de filas

        double cantidadDeFASE; // es la media de la fase en una matriz
        double cantidadDeTEMP; // es la media de la temperatura en una matriz

        Celda[,] matriz_malla; // matriz_malla_Clone =matriz_malla
        Celda[,] matriz_malla_Clone; //matriz espejo

        Normas norma1;
        string condicion;




        // GET 
        public Celda[,] GetMatriz()
        { return this.matriz_malla; }
        public Celda[,] GetClon()
        { return this.matriz_malla_Clone; }
        public int getX()
        { return x; }
        public int getY()
        { return y; }
        public double DameFASEde(int posFILAS, int posCOLUMNAS)
        { return (this.matriz_malla[posFILAS, posCOLUMNAS].GetFase()); }
        public double DameFASEdeClon(int posFILAS, int posCOLUMNAS)
        { return (this.matriz_malla_Clone[posFILAS, posCOLUMNAS].GetFase()); }
        public double DameTEMPERATURAde(int posFILAS, int posCOLUMNAS)
        { return (this.matriz_malla[posFILAS, posCOLUMNAS].GetTemperatura()); }
        public double DameTEMPERATURAdeClon(int posFILAS, int posCOLUMNAS)
        { return (this.matriz_malla_Clone[posFILAS, posCOLUMNAS].GetTemperatura()); }
        public Normas GetNorma()
        { return norma1; }

        // SET
        public void SetNormas(Normas n)
        { this.norma1 = n; }
        public void SetMatriz(Celda[,] matriz_MALLA)
        { this.matriz_malla = matriz_MALLA; }
        public void SetFaseDeCelda(int fila, int columna, double fase)
        { matriz_malla[fila, columna].SetFase(fase); }
        public void SetTemperaturaDeCelda(int fila, int columna, double T)
        { matriz_malla[fila, columna].SetTemperatura(T); }

        //CLONAR PARA LISTA
        //generamos un clon de nuestra matriz para guardar en el historial
        // para ello es importante definir las celdas como nuevas celdas para no sobrescribir 
        //valores en el historial
        public Malla ClonarParaLISTA()
        {
            Celda[,] matriz_malla_Clone_LISTA = new Celda[y, x]; // nueva matriz
            // recorremos toda la matriz para copiar todos los valores
            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    {
                        Celda fill_clone = new Celda(); // rellenamos la matriz con celdas
                        matriz_malla_Clone_LISTA[i, j] = fill_clone;
                        matriz_malla_Clone_LISTA[i, j].SetFase(matriz_malla[i, j].GetFase());
                        matriz_malla_Clone_LISTA[i, j].SetTemperatura(matriz_malla[i, j].GetTemperatura());
                    }
                }
            Malla malla_para_guardar = new Malla(); // generamos la nueva malla

            malla_para_guardar.SetNumeroDeFilasYColumnas(getY() - 2, getX() - 2); // definimos el número  de filas y columnas
            // restamos 2 ya que en Malla x e y es el número  de celdas + contorno
            malla_para_guardar.SetNormas(norma1); // definimos las normas
            malla_para_guardar.SetMatriz(matriz_malla_Clone_LISTA); // definimos la nueva matriz
            return malla_para_guardar;
        }

        //CLONAMOS LA MATRIZ
        //esta función  nos será útil para calcular el estado futuro, al ir actualizando las celdas
        // estas irán variando sus valores, así  que cogeremos de referencia los valores del clon que no 
        // variaran
        public void ClonarMatrix()
        {
            matriz_malla_Clone = new Celda[y, x];
            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    {
                        Celda fill_clone = new Celda(); // rellenamos la matriz con celdas
                        matriz_malla_Clone[i, j] = fill_clone;
                        matriz_malla_Clone[i, j].SetFase(matriz_malla[i, j].GetFase());
                        matriz_malla_Clone[i, j].SetTemperatura(matriz_malla[i, j].GetTemperatura());

                    }
                }
        }

        // SET NÚMERO  DE FILAS Y COLUMNAS
        //dados el número de filas y columnas se generan las dimensiones de la matriz_malla y el clon, considerando
        // un contorno

        public void SetNumeroDeFilasYColumnas(int fila, int columna)
        {
            this.y = fila + 2; // filas + contorno
            this.x = columna + 2; // columnas + contorno

            this.matriz_malla = new Celda[y, x];
            this.matriz_malla_Clone = new Celda[y, x];

            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    {
                        Celda fill = new Celda(); // rellenamos la matriz con celdas
                        Celda fill_clone = new Celda(); // rellenamos la matriz con celdas

                        matriz_malla[i, j] = fill;
                        matriz_malla_Clone[i, j] = fill_clone;
                    }
                }
        }

        public void SetCondicionsContornoFaseTemperatura(string condicion)
        {
            this.condicion = condicion;
        }

        public string GetCondicionsContornoFaseTemperatura()
        {
            return this.condicion;
        }



        // CALCULAMOS EL VALOR MEDIO DE LA FASE
        //será útil  para hacer las gráficas
        // hacemos un recorrido por toda la matriz y vamos sumando la fase. Finalmente dividimos este
        // valor por el número de celdas recorridas
        public double GetcantidadFase()
        {
            cantidadDeFASE = 0;
            int celdasrecorridas = 0;
            for (int i = 1; i < y - 1; i++)
            {
                for (int j = 1; j < x - 1; j++)
                {
                    cantidadDeFASE = cantidadDeFASE + matriz_malla[i, j].GetFase();
                    celdasrecorridas++;
                }
            }
            cantidadDeFASE = cantidadDeFASE / celdasrecorridas; // Así normalizamos la cantidad de celdas
            return cantidadDeFASE;
        }

        // CALCULAMOS EL VALOR MEDIO DE LA TEMPERATURA
        //será útil  para hacer las gráficas
        // hacemos un recorrido por toda la matriz y vamos sumando la temperatura. Finalmente dividimos este 
        // valor por el número de celdas recorridas
        public double GetcantidadTEMP()
        {
            cantidadDeTEMP = 0;
            int celdasrecorridas = 0;
            for (int i = 1; i < y - 1; i++)
            {
                for (int j = 1; j < x - 1; j++)
                {
                    cantidadDeTEMP = cantidadDeTEMP + matriz_malla[i, j].GetTemperatura();
                    celdasrecorridas++;
                }
            }
            cantidadDeTEMP = cantidadDeTEMP / celdasrecorridas; // Así normalizamos la cantidad de celdas
            return cantidadDeTEMP;
        }

        // ACTUALIZAR A LA MALLA FUTURA
        public void MallaFutura()
        {
            ClonarMatrix();
            for (int i = 1; i < y - 1; i++)
            {
                for (int j = 1; j < x - 1; j++)
                {
                    //ponemos los estados de las fases y temperaturas de las celdas proximas
                    matriz_malla[i, j].SetFaseDerecha(DameFASEdeClon(i, j + 1));
                    matriz_malla[i, j].SetFaseIzquierda(DameFASEdeClon(i, j - 1));
                    matriz_malla[i, j].SetFaseAbajo(DameFASEdeClon(i + 1, j));
                    matriz_malla[i, j].SetFaseArriba(DameFASEdeClon(i - 1, j));

                    matriz_malla[i, j].SetTemperaturaDerecha(DameTEMPERATURAdeClon(i, j + 1));
                    matriz_malla[i, j].SetTemperaturaIzquierda(DameTEMPERATURAdeClon(i, j - 1));
                    matriz_malla[i, j].SetTemperaturaAbajo(DameTEMPERATURAdeClon(i + 1, j));
                    matriz_malla[i, j].SetTemperaturaArriba(DameTEMPERATURAdeClon(i - 1, j));

                    //actualizamos

                    matriz_malla[i, j].SetNorma(norma1);
                    matriz_malla[i, j].ActualizarFASEdeCelda();
                }
            }

            if (condicion == "System.Windows.Controls.ComboBoxItem: Fijas")
            {
                for (int i = 0; i < y; i++)
                {
                    matriz_malla[0, i].SetFase(1);
                    matriz_malla[0, i].SetTemperatura(-1);
                    matriz_malla[y - 1, i].SetFase(1);
                    matriz_malla[y - 1, i].SetTemperatura(-1);
                }

                for (int j = 0; j < x; j++)
                {
                    matriz_malla[j, 0].SetFase(1);
                    matriz_malla[j, 0].SetTemperatura(-1);
                    matriz_malla[j, x - 1].SetFase(1);
                    matriz_malla[j, x - 1].SetTemperatura(-1);

                }

            }

            if(condicion == "System.Windows.Controls.ComboBoxItem: Espejo")
                {

                for (int i = 1; i < x; i++)  // filas - y     columnas - x
                {
                    matriz_malla[0, i].SetFase(DameFASEde(1, i));
                    matriz_malla[0, i].SetTemperatura(DameTEMPERATURAde(1, i));
                    matriz_malla[y - 1, i].SetFase(DameFASEde(y - 2, i));
                    matriz_malla[y - 1, i].SetTemperatura(DameTEMPERATURAde(y - 2, i));
                }

                for (int j = 1; j < y; j++)
                {
                    matriz_malla[j, 0].SetFase(DameFASEde(j, 1));
                    matriz_malla[j, 0].SetTemperatura(DameTEMPERATURAde(j, 1));
                    matriz_malla[j, x - 1].SetFase(DameFASEde(j, x - 2));
                    matriz_malla[j, x - 1].SetTemperatura(DameTEMPERATURAde(j, x - 2));

                }
            }


        }

        // GUARDAR SIMULACIÓN
        public int GuardarSimulacion(string nombre)
        {
            try
            {
                StreamWriter w = new StreamWriter(nombre);

                w.Write(this.y + " " + this.x); // guardamos los valores x e y separados en la primera fila
                w.Write('\n');
                w.Write(this.norma1.GetDxDy() + " " + this.norma1.GetEpsilon() + " " + this.norma1.GetBetta() + " " + this.norma1.GetDelta() + " " + this.norma1.GetM() + " " + this.norma1.GetDT() + " " + GetCondicionsContornoFaseTemperatura());
                w.Write('\n');

                for (int j = 0; j < y; j++) // guardamos la matriz
                {
                    for (int i = 0; i < x; i++)
                    {
                        w.Write(this.DameFASEde(j, i) + "&" + this.DameTEMPERATURAde(j, i) + " ");
                    }
                    w.Write('\n');

                }
                w.Close();
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        //CARGAR SIMULACIÓN
        public Malla CargarSimulacion(string name)
        {
            Malla matriz_celdas = new Malla(); // creamos una nueva malla
            Normas norma = new Normas();

            StreamReader sr = new StreamReader(name);
            string linea = sr.ReadLine();
            string[] trozos = linea.Split(' '); // la primera línea  nos indica el número  de filas y columnas
            matriz_celdas.SetNumeroDeFilasYColumnas(Convert.ToInt32(trozos[0]) - 2, Convert.ToInt32(trozos[1]) - 2);

            // Leemos los parámetros y condiciones de contorno
            string lineaParam = sr.ReadLine();
            string[] trozosParam = lineaParam.Split(' ');
            norma.SetDxDy(Convert.ToDouble(trozosParam[0]));
            norma.SetEpsilon(Convert.ToDouble(trozosParam[1]));
            norma.SetBetta(Convert.ToDouble(trozosParam[2]));
            norma.SetDelta(Convert.ToDouble(trozosParam[3]));
            norma.SetM(Convert.ToDouble(trozosParam[4]));
            norma.SetDT(Convert.ToDouble(trozosParam[5]));
            matriz_celdas.SetNormas(norma);
            matriz_celdas.SetCondicionsContornoFaseTemperatura(trozosParam[6] +" "+ trozosParam[7]);

            string line = sr.ReadLine();
            int i = 0;
            while (line != null)
            {
                string[] traces = line.Split(' ');
                for (int j = 0; j < traces.Length - 1; j++)
                {
                    string[] data = traces[j].Split('&');
                    matriz_celdas.SetFaseDeCelda(i, j, Convert.ToDouble(data[0]));
                    matriz_celdas.SetTemperaturaDeCelda(i, j, Convert.ToDouble(data[1]));
                }
                line = sr.ReadLine();
                i++;
            }
            sr.Close();
            return matriz_celdas;
        }


    }

}

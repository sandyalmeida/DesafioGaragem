using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppDesafioGaragem
{
    internal class Persistencia
    {
        /// <summary>
        /// método que a partir da lista persiste os dados no arquivo dos veículos que estão na garagem
        /// </summary>
        /// <param name="lista">lista de veículos</param>
        public static void gravarNoArquivoEntrada(List<Veiculo> lista)
        {
            criarArquivos();
            StreamWriter escritor = new StreamWriter("../../../Arquivos/veiculosEntrada.dat");

            foreach (Veiculo i in lista)
            {
                escritor.WriteLine(i.Placa + ";" + i.DataEntrada + ";" + i.HoraEntrada);
                escritor.Flush();
            }
            escritor.Close();
        }

        /// <summary>
        /// método que a partir da lista persiste os dados no arquivo dos veículos que passaram pela garagem
        /// </summary>
        /// <param name="lista">lsita de veículos</param>
        public static void gravarNoArquivoSaida(List<Veiculo> lista)
        {
            criarArquivos();
            StreamWriter escritor = new StreamWriter("../../../Arquivos/veiculosSaida.dat");

            foreach (Veiculo i in lista)
            {                
                escritor.WriteLine(i.Placa + ";" + i.TempoPermanecia + ";" + i.ValorCobrado);
                escritor.Flush();
            }
            escritor.Close();
        }

        /// <summary>
        /// método que popula a lista de veículos de estão na garagem a partir do arquivo base
        /// </summary>
        /// <param name="lista">lista de veículos</param>
        public static void lerArquivoEntrada(List<Veiculo> lista)
        {
            criarArquivos();
            StreamReader leitor = new StreamReader("../../../Arquivos/veiculosEntrada.dat");
            string linha;
            string[] vetorDados;

            while (!leitor.EndOfStream)
            {
                linha = leitor.ReadLine();
                vetorDados = linha.Split(";");
                lista.Add(new Veiculo(vetorDados[0], vetorDados[1], vetorDados[2]));
            }
            leitor.Close();
        }

        /// <summary>
        /// método que popula a lista de veículos de passaram pela garagem a partir do arquivo base
        /// </summary>
        /// <param name="lista">lista veículos</param>
        public static void lerArquivoSaida(List<Veiculo> lista)
        {
            criarArquivos();
            StreamReader leitor = new StreamReader("../../../Arquivos/veiculosSaida.dat");
            string linha;
            string[] vetorDados;
            string aux = "";

            while (!leitor.EndOfStream)
            {
                linha = leitor.ReadLine();
                vetorDados = linha.Split(";");
                //lista.Add(new Veiculo(vetorDados[0], vetorDados[1], vetorDados[2]));
                lista.Add(new Veiculo(vetorDados[0], aux, aux, aux,
                    aux, int.Parse(vetorDados[1]), double.Parse(vetorDados[2])));
            }
            leitor.Close();
        }

        public static void criarArquivos()
        {
            //criar arquivo de entrada
            using (StreamWriter w = File.AppendText("../../../Arquivos/veiculosEntrada.dat")) { }

            //criar arquivo de saída
            using (StreamWriter w = File.AppendText("../../../Arquivos/veiculosSaida.dat")) { }
        }
    }
}

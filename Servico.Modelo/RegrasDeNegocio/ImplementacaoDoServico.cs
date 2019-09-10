using NLog;
using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Servico.Modelo.RegrasDeNegocio
{
    public class ImplementacaoDoServico
    {
        private string[] _Linha { get; set; }
        private int _Contador { get; set; }

        /// <summary>
        /// Objeto de log centralizado.
        /// </summary>
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Inicializa implementação.
        /// </summary>
        public ImplementacaoDoServico()
        {
            _Contador = 0;
            _Linha = new string[] { "Linha " + _Contador };
        }

        public void ChamarImplementacaoPrincipal()
        {

            while (true)
            {
                try
                {
                    CriarArquivo("arquivo", new string[] { "Linha " + _Contador + " inserida - Implementação Principal" });
                    ChamarImplementacao1();
                    ChamarImplementacao2();
                    ChamarImplementacao3();
                    ChamarImplementacao4();
                    ChamarImplementacao5();

                    _Contador++;

                    //Thread.Sleep(1000);
                }
                catch (Exception excecao)
                {
                    IdentificarETratarExcecao(excecao);
                }
            }
        }

        private void ChamarImplementacao1()
        {
            Log.Debug("Executando: ChamarImplementacao1()");
            CriarArquivo("arquivo", new string[] { "Linha " + _Contador + " inserida - Implementação 1" });
        }

        private void ChamarImplementacao2()
        {
            Log.Debug("Executando: ChamarImplementacao2()");
            CriarArquivo("arquivo", new string[] { "Linha " + _Contador + " inserida - Implementação 2" });
        }

        private void ChamarImplementacao3()
        {
            Log.Debug("Executando: ChamarImplementacao3()");
            CriarArquivo("arquivo", new string[] { "Linha " + _Contador + " inserida - Implementação 3" });
        }

        private void ChamarImplementacao4()
        {
            Log.Debug("Executando: ChamarImplementacao4()");
            CriarArquivo("arquivo", new string[] { "Linha " + _Contador + " inserida - Implementação 4" });
        }

        private void ChamarImplementacao5()
        {
            Log.Debug("Executando: ChamarImplementacao5()");
            CriarArquivo("arquivo", new string[] { "Linha " + _Contador + " inserida - Implementação 5" });
        }

        private void IdentificarETratarExcecao(Exception excecao)
        {
            if (excecao.GetType().IsAssignableFrom(typeof(ArgumentNullException)))
                throw new ArgumentNullException("O serviço foi interrompido porque um argumento nulo foi passado como inválido no método ChamarImplementacao1().");

            if (excecao.GetType().IsAssignableFrom(typeof(DirectoryNotFoundException)))
                throw new DirectoryNotFoundException("O serviço foi interrompido porque o diretório onde o arquivo está localizado não foi encontrado.");

            if (excecao.GetType().IsAssignableFrom(typeof(FileNotFoundException)))
                throw new FileNotFoundException("O serviço foi interrompido porque o arquivo não foi encontrado.");

            if (excecao.GetType().IsAssignableFrom(typeof(IOException)))
                throw new IOException("O serviço foi interrompido porque ocorreu um problema com a entrada / saída de dados no arquivo.");

            if (excecao.GetType().IsAssignableFrom(typeof(PathTooLongException)))
                throw new PathTooLongException("O serviço foi interrompido porque o caminho do arquivo é grande demais para leitura.");

            if (excecao.GetType().IsAssignableFrom(typeof(UnauthorizedAccessException)))
                throw new UnauthorizedAccessException("O serviço foi interrompido porque não possui autorização para modificar o arquivo.");

            if (excecao.GetType().IsAssignableFrom(typeof(UnauthorizedAccessException)))
                throw new UnauthorizedAccessException("O serviço foi interrompido porque não possui autorização para modificar o arquivo");

            if (excecao.GetType().IsAssignableFrom(typeof(ArgumentOutOfRangeException)))
                throw new ArgumentOutOfRangeException("O serviço foi interrompido porque o valor do argumento definido na Thread está fora do intervalo de valores permitidos.");

        }

        private void CriarArquivo(string nome, string[] linha)
        {
            File.AppendAllLines(@"C:\Users\krsantos\Desktop\" + nome + ".txt", linha);
        }

        public void TransferirArquivo()
        {
            string NomeDoArquivoDeOrigem = string.Empty;
            string ArquivoDeTextoRecebido = string.Empty;
            string arquivoDeTexto = string.Empty;

            string CaminhoDeOrigem      = ConfigurationManager.AppSettings["CaminhoDeOrigem"].ToString();
            string CaminhoDeDestino     = ConfigurationManager.AppSettings["CaminhoDeDestino"].ToString();
            string CaminhoDeRecebidos   = ConfigurationManager.AppSettings["CaminhoDeRecebidos"].ToString();
            string CaminhoDeDevolvidos  = ConfigurationManager.AppSettings["CaminhoDeDevolvidos"].ToString();
            string CaminhoDeBackup      = ConfigurationManager.AppSettings["CaminhoDeBackup"].ToString();

            //ResumoDeProcessamentoDoLote ResumoDeProcessamentoDoLote

        }
    }
}

using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using Util.Modelo;

namespace ServicoWindows.Modelo
{
    static class Program
    {
        /// <summary>
        /// Armazena a instância do serviço para execução ou debug.
        /// </summary>
        private static Service1 _Servico;

        /// <summary>
        /// Ponto de entrada do serviço. Inicializa quando for chamado.
        /// </summary>
        static void Main(string[] args)
        {
            // Descomente a linha abaixo caso queira instalar o serviço antes de debuga-lo.
            //InstalarServicoAtravesDaIDE(args);

            // Executa o serviço de acordo com o modo iniciado.
            #if (!DEBUG)
                ExecutarEmModoRelease();
            #else
                ExecutarEmModoDebug();
            #endif
        }

        /// <summary>
        /// Executa o serviço em modo debug.
        /// 
        /// Para debugar o serviço, será necessário 
        /// invocar o(s) método(s) desejado(s).
        /// 
        /// Este método é invocado exclusivamente para debug.
        /// Sua implementação não deve ser replicada, pois 
        /// pode impedir a inicialização correta do serviço
        /// </summary>
        [Conditional("DEBUG")]
        private static void ExecutarEmModoDebug()
        {
            Console.WriteLine("Serviço executando em modo DEBUG");
            Console.WriteLine();
            Console.WriteLine("############################# ATENÇÃO #############################");
            Console.WriteLine();
            Console.WriteLine("Para depurar / debugar os métodos do serviço, invoke o método \"HabilitarDebug\" após os métodos que deseja depurar / debugar.");

            try
            {
                _Servico = new Service1();
                // Abaixo, chame / descomente os métodos que deseja executar / debugar separadamente
                // _Servico._BaixaDePagamentos.CriarArquivo();

                _Servico.HabilitarDebug();
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.StackTrace );
            }
            finally
            {
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Define estrutura que permitirá que o serviço seja
        /// executado normalmente após sua instalação.
        /// 
        /// Observação: Favor não modificar a implementação deste método.
        /// Pois pode ocasionar em falhas na instalação / execução do mesmo.
        /// </summary>
        [Conditional("RELEASE")]
        private static void ExecutarEmModoRelease()
        {
            //var isService = !(Debugger.IsAttached || args.Contains("--console"));

            Service1 servico = new Service1();
            var servicos = new ServiceBase[] { servico };

            ServiceBase.Run(servicos);
        }

        private static void InstalarServicoAtravesDaIDE(string[] args)
        {
            if (args != null && args.Length == 1 && (args[0][0] == '-' || args[0][0] == '/'))
            {
                switch (args[0].Substring(1).ToLower())
                {
                    case "instalar":
                    case "i":
                        if (!UtilitarioDeInstalacao.Instalar())
                            Console.WriteLine("Falha ao instalar serviço");
                        break;
                    case "desinstalar":
                    case "d":
                        if (!UtilitarioDeInstalacao.Desinstalar())
                            Console.WriteLine("Falha ao desinstalar serviço");
                        break;
                    default:
                        Console.WriteLine("Parâmetros não reconhecidos (Permitidos: /instalar e /desinstalar ou i e d)");
                        break;
                }
                //Environment.Exit(0);
            }
        }
    }
}
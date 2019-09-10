using Servico.Modelo.RegrasDeNegocio;
using System.ServiceProcess;
using System.Threading;
using Util.Modelo;

namespace ServicoWindows.Modelo
{
    /// <summary>
    /// Sobrescreve recursos da classe ServiceBase e executa 
    /// ações da classe de negócio do serviço criado.
    /// </summary>
    public partial class Service1 : ServiceBase
    {
        /// <summary>
        /// Contém a classe responsável por gerenciar o agendamento das rotinas.
        /// </summary>
        private AgendadorDeExecucao _Agendador;

        /// <summary>
        /// Contém a classe que representa as regras de negócio do serviço.
        /// </summary>
        public BaixaDePagamentosNegocio _BaixaDePagamentos { get; private set; }

        /// <summary>
        /// Cria uma nova instância do serviço.
        /// </summary>
        public Service1()
        {
            InitializeComponent();

            _Agendador = new AgendadorDeExecucao();
            
            // Instância utilizada para debug
            _BaixaDePagamentos = new BaixaDePagamentosNegocio();
        }

        /// <summary>
        /// Habilita o modo debug no serviço.
        /// 
        /// Este método deve ser chamado apenas após 
        /// invocação dos métodos que deverão ser debugados.
        /// </summary>
        public void HabilitarDebug()
        {
            OnStart(null);
            System.Diagnostics.Debugger.Launch();
            Thread.Sleep(Timeout.Infinite);
        }

        /// <summary>
        /// Este método é responsável por coordenar
        /// o tipo de agendamento de execução do serviço
        /// a ser realizado. 
        /// 
        /// O mesmo utiliza parâmetros do Web.Config que 
        /// obrigatoriamente devem ser informados para 
        /// que o agendamento da execução ocorra com sucesso.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            _Agendador._Scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();
            DefinirTipoDeAgendamento();
        }

        /// <summary>
        /// Executa uma ação ao parar o serviço.
        /// [AS AÇÕES A SEREM EXECUTADAS AO PARAR O SERVIÇO DEVEM SER CHAMADAS AQUI]
        ///
        /// Favor chamar outras implementações antes da chamada existente.
        /// </summary>
        protected override void OnStop()
        {
            _Agendador._Scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Executa uma ação quando o sistema estiver sendo desligado.
        /// 
        /// [AS AÇÕES A SEREM EXECUTADAS ANTES QUE O SISTEMA DESLIGUE DEVEM SER IMPLEMENTADAS AQUI]
        /// 
        /// Favor chamar outras implementações antes da chamada existente.
        /// </summary>
        protected override void OnShutdown()
        {
            OnStop();
        }

        /// <summary>
        /// Define o agendamento de acordo com os parâmetros informados no App.Config .
        /// 
        /// Instruções para criação de uma modalidade de agendamento:
        /// 
        /// Definir uma nova chave no App.Config
        /// Definir metodo para validar / obter valor da chave
        /// Definir metodo para realizar o agendamento
        /// Definir opção case na implementação do switch para chamar o metodo que executa o agendamento
        /// </summary>
        private void DefinirTipoDeAgendamento()
        {
            switch (AppConfig.TipoDeExecucao)
            {
                case "Diaria":
                case "Diária":
                    _Agendador = _Agendador.AgendarExecucaoTodosOsDiasAs(AppConfig.HorarioQueIniciaExecucao);
                    break;
                case "Semanal":
                    _Agendador = _Agendador.AgendarExecucaoSemanal(AppConfig.DiaQueIniciaExecucao, AppConfig.HorarioQueIniciaExecucao);
                    break;
                case "Mensal":
                    _Agendador = _Agendador.AgendarExecucaoMensal(AppConfig.DiaQueIniciaExecucao, AppConfig.HorarioQueIniciaExecucao);
                    break;
                case "Personalizada":
                    _Agendador = _Agendador.AgendarExecucaoPersonalizadaCom(AppConfig.ExpressaoCron);
                    break;
                case "IntervaladaEmHoras":
                    _Agendador = _Agendador.AgendarExecucaoRepetidaComIntervaloEmHoras(AppConfig.DataQueIniciaExecucao, AppConfig.IntervaloDeExecucaoEmHoras, AppConfig.NumeroDeRepeticoesDaExecucao);
                    break;
                case "IntervaladaEmMinutos":
                    _Agendador = _Agendador.AgendarExecucaoRepetidaComIntervaloEmHoras(AppConfig.DataQueIniciaExecucao, AppConfig.IntervaloDeExecucaoEmHoras, AppConfig.NumeroDeRepeticoesDaExecucao);
                    break;
            }
            _Agendador.ConfirmarExecucao();
        }
    }
}

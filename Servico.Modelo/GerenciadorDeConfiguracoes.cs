using System.Collections.Specialized;
using System.Configuration;

namespace Servico.Modelo
{
    /// <summary>
    /// Gerencia as Propriedades de Configuração do App.config.
    /// </summary>
    public class GerenciadorDeConfiguracoes
    {
        /// <summary>
        /// Referência para o gerenciador do arquivo de configuração.
        /// </summary>
        private static NameValueCollection _Config;

        public static void Preparar()
        {
            _Config = ConfigurationManager.AppSettings;
        }

        /// <summary>
        /// Permite usar o arquivo de configuração (web.config / app.config) 
        /// como um objeto dinâmico.
        /// 
        /// No git https://github.com/ChrisMissal/Formo
        /// </summary>
        public static NameValueCollection ObterValorDaChave
        {
            get
            {
                return _Config;
            }
        }

        #region Abaixo, as opções definidas pelo usuário entre as tags <appSettings></appSettings> no App.config / Web.config
        public static int ChaveModelo { get; set; }
        #endregion
    }
}

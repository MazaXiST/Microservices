using System.IO;
using CvLab.Framework.Common.Configuration;

namespace CvLab.MicroserviceTemplate2.Config
{
    /// <summary>
    ///     Service configuration
    /// </summary>
    public class ServiceConfiguration
    {
        /// <summary>
        ///     Путь для хранения саг
        /// </summary>
        public string FineSagaStorage { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "FineSagaStorage");

        /// <summary>
        ///     настройки подключения к шине данных
        /// </summary>
        public ServiceBusConfig ServiceBus { get; set; } = new ServiceBusConfig();

        /// <summary>
        ///     Конфигурация логирования
        /// </summary>
        public string Seq { get; set; } = "http://localhost:5341/";

        /// <summary>
        ///     Конфигурация логирования
        /// </summary>
        public string LogPath { get; set; } = "C:/CvLab/Common/Log";

        /// <summary>
        ///     Конфиг по умолчанию, в нем нужно инициализировать массивы
        ///     Этот метод вызывается при создании конфига по умолчанию автоматически при использовании AddConsulJson
        /// </summary>
        /// <returns></returns>
        private void Init()
        {
            //TODO
        }
    }
}

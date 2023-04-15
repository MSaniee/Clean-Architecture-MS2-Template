namespace $ext_safeprojectname$.Domain.Core.Settings.RabbitMQ
{
    public class RabbitMqOptions
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string HostName { get; set; }

        public int Port { get; set; } = 5672;

        public string VHost { get; set; } = "/";
    }
}

namespace MiniServer.Application.Views
{
    using System.IO;
    using MiniServer.Server.Contracts;

    public class AboutView : IView
    {
        public string View()
        {
            string html = File.ReadAllText(@".\Application\Resources\about.html");

            return html;
        }
    }
}

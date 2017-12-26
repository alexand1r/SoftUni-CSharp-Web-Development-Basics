namespace MiniServer.Application.Controllers
{
    using System.IO;
    using System.Text;
    using MiniServer.Application.Views;
    using MiniServer.Server;
    using MiniServer.Server.Enums;
    using MiniServer.Server.HTTP.Contracts;
    using MiniServer.Server.HTTP.Response;

    public class CakeController
    {
        public IHttpResponse Add()
        {
            return new ViewResponse(HttpStatusCode.OK, new AddCakeView());
        }

        public IHttpResponse AddCakeToCSV(string name, string price)
        {
            var csv = new StringBuilder();
            var newLine = string.Format("{0},{1}", name, price);
            csv.AppendLine(newLine);
            var filePath = "../Resources/database.csv";
            File.AppendAllText(filePath, csv.ToString());
            return new RedirectResponse($"/add/{name}/{price}");
        }

        public IHttpResponse Details(string name, string price)
        {
            Model model = new Model { ["name"] = name, ["price"] = price };
            return new ViewResponse(HttpStatusCode.OK, new AddCakeView(model));
        }
    }
}

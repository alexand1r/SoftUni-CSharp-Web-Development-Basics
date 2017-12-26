namespace MiniServer.Application.Views
{
    using MiniServer.Server;
    using MiniServer.Server.Contracts;

    public class AddCakeView : IView
    {
        private readonly Model model;

        public string View()
        {
            return
                "<body>" +
                "<a href =\"/\">Home</a>" +
                "   <form method=\"POST\">" +
                "       Name" +
                "       <input type=\"text\" name=\"name\" />" +
                "       Price" +
                "       <input type=\"text\" name=\"price\" />" +
                "       <input type=\"submit\" />" +
                "   </form>" +
                "</br>" +
                "name: " + this.model?["name"] + "<br/>" +
                "price:" + this.model?["price"] + 
                "</body>";
        }

        public AddCakeView() { }

        public AddCakeView(Model model)
        {
            this.model = model;
        }
    }
}

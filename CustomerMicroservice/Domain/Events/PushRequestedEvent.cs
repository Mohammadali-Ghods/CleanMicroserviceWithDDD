using MediatR;

namespace Domain.Events
{
    public class PushRequestedEvent: INotification
    {
        public PushRequestedEvent(string title, string content,string firebasetoken,
         string userid,string jsonparameter,string route)
        {
            Title = title;
            Content = content;
            FirebaseToken = firebasetoken;
            UserID = userid;
            JsonParameter = jsonparameter;
            Route = route;
        }
        public string Title { get;private set; }
        public string Content { get; private set; }
        public string FirebaseToken { get; private set; }
        public string UserID { get; private set; }
        public string JsonParameter { get; private set; }
        public string Route { get; private set; }
    }
}

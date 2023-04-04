using BaseDomain;
using Domain.Events;
using Domain.Rules;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class UserFriends : BaseDomain.Entity, IAggregateRoot
    {
        public List<Friend> Friends { get; private set; }
        public UserFriends(string userid)
        {
            ID = userid;
            Friends = new List<Friend>();
        }
        public void SendRequest(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null)
            {
                Friends.Add(new Friend(userid, StatusList.RequestSent));
                this.AddDomainEvent(new SentRequestDomainEvent(ID, userid, DateTime.Now));
                return;
            }
            this.CheckRule(new SendingRequestToUserThatBlockYouNotPossibleRule(friend));
            this.CheckRule(new SendingRequestWhenYouAreWaitingForReponseNotPossibleRule(friend));
            friend.AddStatus(StatusList.RequestSent);
            this.AddDomainEvent(new SentRequestDomainEvent(ID, userid, DateTime.Now));
        }
        public void SendCancel(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null) return;
            this.CheckRule(new CancelRequestJustPossibleWhenActivceRequestExistRule(friend));
            friend.AddStatus(StatusList.CancelSent);
            this.AddDomainEvent(new SentCancelDomainEvent(ID, userid, DateTime.Now));
        }
        public void SendReject(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null) return;
            this.CheckRule(new ThisUserAlsoHaveRequestThatUserWaitingForRule(friend));
            friend.AddStatus(StatusList.RejectSent);
            this.AddDomainEvent(new SentRejectDomainEvent(ID, userid, DateTime.Now));
        }
        public void SendAccept(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null) return;
            this.CheckRule(new ThisUserAlsoHaveRequestThatUserWaitingForRule(friend));
            friend.AddStatus(StatusList.AcceptSent);
            this.AddDomainEvent(new SentAcceptDomainEvent(ID, userid, DateTime.Now));
        }
        public void SendBlock(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null) return;
            friend.AddStatus(StatusList.BlockSent);
        }
        public void SendReport(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null) return;
            friend.AddStatus(StatusList.ReportSent);
        }
        public void SendDisconnect(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null) return;
            this.CheckRule(new ThisUsersMustBeFriendRule(friend));
            friend.AddStatus(StatusList.DisconnectSent);
        }

        public void ReceiveDisconnect(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null) return;
            this.CheckRule(new ThisUsersMustBeFriendRule(friend));
            friend.AddStatus(StatusList.DisconnectReceived);
        }
        public void ReceiveRequest(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null)
            {
                Friends.Add(new Friend(userid, StatusList.RequestReceived));
                return;
            }
            this.CheckRule(new NewRequestRecievedIfNoWaitingRequestFoundedRule(friend));
            friend.AddStatus(StatusList.RequestReceived);
        }
        public void ReceiveCancel(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null) return;
            this.CheckRule(new JustCanReceiveCancelWhenAnyRequestReceivedRule(friend));
            friend.AddStatus(StatusList.CancelReceived);
        }
        public void ReceiveReject(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null) return;
            this.CheckRule(new ThisUserAlsoWaitingForResponseRule(friend));
            friend.AddStatus(StatusList.RejectReceived);
        }
        public void ReceiveAccept(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null) return;
            this.CheckRule(new ThisUserAlsoWaitingForResponseRule(friend));
            friend.AddStatus(StatusList.AcceptReceived);
        }
        public void ReceiveBlock(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null) return;
            friend.AddStatus(StatusList.BlockReceived);
        }
        public void ReceiveReport(string userid)
        {
            var friend = Friends.Where(x => x.UserID == userid).FirstOrDefault();
            if (friend == null) return;
            friend.AddStatus(StatusList.ReportReceived);
        }
    }
}

using Data.Repository.Base;
using Domain.Entities;
using Domain.Interfaces;
using MongoDB.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserFriendsRepository : Repository<UserFriends>, IUserFriendsRepository
    {

    }
}

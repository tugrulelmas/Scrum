using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Data
{
    public interface IBoardOperation : IOperation<Board>
    {
        Board Get(Guid id);

        IEnumerable<Board> GetAll();

        IEnumerable<User> GetBoardUsers(Guid boardId);

        BoardUser AddUser(Guid boardId, Guid userId);

        void RemoveUser(Guid boardId, Guid userId);
    }
}
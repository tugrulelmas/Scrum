using System;
using System.Collections.Generic;
using AbiokaScrum.Api.Entities;
using System.Linq;
using AbiokaScrum.Api.Authentication;
using AbiokaScrum.Api.Entities.DTO;
using MongoDB.Driver;

namespace AbiokaScrum.Api.Data.NoSql.Mongo.Operations
{
    public class BoardOperation : Operation<Board>, IBoardOperation
    {
        public override void Add(Board entity) {
            entity.Users = new List<UserDTO> { new UserDTO { Id = Context.Current.Principal.Id } };
            entity.CreatedUser = Context.Current.Principal.Id;
            repository.Add(entity);
        }

        public BoardUser AddUser(Guid boardId, Guid userId) {
            throw new NotImplementedException();
        }

        public Board Get(Guid id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Board> GetAll() {
            var filter = Builders<Board>.Filter.Where(a => a.Users.Any(c => c.Id == Context.Current.Principal.Id));
            return GetBy(filter);
        }

        public IEnumerable<User> GetBoardUsers(Guid boardId) {
            throw new NotImplementedException();
        }

        public void RemoveUser(Guid boardId, Guid userId) {
            throw new NotImplementedException();
        }
    }
}
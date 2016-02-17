using AbiokaScrum.Api.Authentication;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Entities.DTO;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaScrum.Api.Data.Transactional.Dapper.Operations
{
    public class BoardOperation : Operation<Board>, IBoardOperation
    {
        private readonly IOperation<BoardUser> boardUserOperation;
        private readonly IListOperation listOperation;
        private readonly IUserOperation userOperation;

        public BoardOperation(IOperation<BoardUser> boardUserOperation, IListOperation listOperation, IUserOperation userOperation) {
            this.boardUserOperation = boardUserOperation;
            this.listOperation = listOperation;
            this.userOperation = userOperation;
        }

        public override void Add(Board board) {
            Execute((repository) =>
            {
                board.Users = new List<UserDTO> { new UserDTO { Id = Context.Current.Principal.Id } };
                board.CreatedUser = Context.Current.Principal.Id;
                repository.Add(board);
                var boardUser = new BoardUser
                {
                    BoardId = board.Id,
                    UserId = Context.Current.Principal.Id
                };
                repository.Add(boardUser);
            });
        }

        public Board Get(Guid id) {
            var board = GetByKey(id);
            var predicate = Predicates.Field<List>(l => l.BoardId, Operator.Eq, id);
            board.Lists = ((Operation<List>)listOperation).GetBy(predicate);
            return board;
        }

        public IEnumerable<Board> GetAll() {
            IList<ISort> sort = new List<ISort>
            {
                Predicates.Sort<Board>(b => b.CreateDate, false)
            };

            var boardIds = GetBoardIds();
            if (boardIds == null || boardIds.Count() == 0)
            {
                return new List<Board>();
            }

            var boards = GetBy(Predicates.Field<Board>(b => b.Id, Operator.Eq, boardIds), sort);
            foreach (var board in boards)
            {
                var predicate = Predicates.Field<BoardUser>(l => l.BoardId, Operator.Eq, board.Id);
                board.Users = ((Operation<BoardUser>)boardUserOperation).GetBy(predicate).Select(b => new UserDTO { Id = b.UserId });
            }
            return boards;
        }

        public IEnumerable<User> GetBoardUsers(Guid boardId) {
            var predicate = Predicates.Field<BoardUser>(b => b.BoardId, Operator.Eq, boardId);
            var userIds = ((Operation<BoardUser>)boardUserOperation).GetBy(predicate: predicate).Select(b => b.UserId);

            var userPredicate = Predicates.Field<User>(b => b.Id, Operator.Eq, userIds);
            var sort = new List<ISort>
            {
                Predicates.Sort<User>(b => b.Name)
            };
            var result = ((Operation<User>)userOperation).GetBy(userPredicate, sort);
            return result;
        }

        private IEnumerable<Guid> GetBoardIds() {
            var predicate = Predicates.Field<BoardUser>(b => b.UserId, Operator.Eq, Context.Current.Principal.Id);
            return ((Operation<BoardUser>)boardUserOperation).GetBy(predicate: predicate).Select(b => b.BoardId);
        }

        public override void Remove(Board entity) {
            var board = Get(entity.Id);
            Execute((repository) =>
            {
                if (board.Lists != null)
                {
                    foreach (var listItem in board.Lists)
                    {
                        ((ListOperation)listOperation).Delete(listItem, repository);
                    }
                }

                board.IsDeleted = true;
                repository.Update(board);
            });
        }

        public BoardUser AddUser(Guid boardId, Guid userId) {
            var boardUser = new BoardUser
            {
                BoardId = boardId,
                UserId = userId
            };
            boardUserOperation.Add(boardUser);
            return boardUser;
        }

        public void RemoveUser(Guid boardId, Guid userId) {
            var boardUser = new BoardUser
            {
                BoardId = boardId,
                UserId = userId
            };
            boardUserOperation.Remove(boardUser);
        }
    }
}
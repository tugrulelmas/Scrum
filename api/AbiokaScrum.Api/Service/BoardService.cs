﻿using AbiokaScrum.Api.Authentication;
using AbiokaScrum.Api.Entities;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Service
{
    public class BoardService
    {
        public static void Add(Board board) {
            DBService.Execute((customRepository) =>
            {
                board.Users = new List<User> { new User { Id = Context.Current.Principal.Id } };
                board.CreatedUser = Context.Current.Principal.Id;
                customRepository.Add(board);
                var boardUser = new BoardUser
                {
                    BoardId = board.Id,
                    UserId = Context.Current.Principal.Id
                };
                customRepository.Add(boardUser);
            });
        }

        public static Board Get(Guid id) {
            var board = DBService.GetByKey<Board>(id);
            var predicate = Predicates.Field<List>(l => l.BoardId, Operator.Eq, id);
            board.Lists = DBService.GetBy<List>(predicate);
            return board;
        }

        public static IEnumerable<Board> GetAll() {
            IList<ISort> sort = new List<ISort>
            {
                Predicates.Sort<Board>(b => b.CreateDate, false)
            };

            var pg = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
            var boardIds = GetBoarIds();
            foreach (var boardIdItem in boardIds) {
                pg.Predicates.Add(Predicates.Field<Board>(b => b.Id, Operator.Eq, boardIdItem));
            }

            var boards = DBService.Get<Board>(predicate: pg, sort: sort);
            foreach (var board in boards) {
                var predicate = Predicates.Field<BoardUser>(l => l.BoardId, Operator.Eq, board.Id);
                board.Users = DBService.GetBy<BoardUser>(predicate).Select(b => new User { Id = b.UserId });
            }
            return boards;
        }

        private static IEnumerable<Guid> GetBoarIds() {
            var predicate = Predicates.Field<BoardUser>(b => b.UserId, Operator.Eq, Context.Current.Principal.Id);
            return DBService.Get<BoardUser>(predicate: predicate).Select(b => b.BoardId);
        }

        public static void Delete(Board board) {
            DBService.Execute((customRepository) =>
            {
                if (board.Lists != null) {
                    foreach (var listItem in board.Lists) {
                        listItem.IsDeleted = true;
                        customRepository.Update<List>(listItem);
                    }
                }

                board.IsDeleted = true;
                customRepository.Update<Board>(board);
            });
        }
    }
}
using AbiokaScrum.Api.Authentication;
using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Service
{
    public class BoardService
    {
        public static void Add(Board board) {
            DBService.Execute((customRepository) => {
                //TODO: add current user to board
                board.Users = new List<User> { new User { Id = Context.Current.Principal.Id } };
                customRepository.Add<Board>(board);
            });
        }

        public static void Delete(Board board) {
            DBService.Execute((customRepository) => {
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
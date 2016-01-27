using AbiokaScrum.Api.Entities;
using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Data.Dapper.Map
{
    public class UserMap : ClassMapper<User>
    {
        public UserMap() {
            Map(e => e.Id).Key(KeyType.Guid);
            Map(e => e.ShortName).Ignore();

            AutoMap();
        }
    }

    public class BoardMap : ClassMapper<Board>
    {
        public BoardMap() {
            Map(e => e.Id).Key(KeyType.Guid);
            Map(e => e.Lists).Ignore();
            Map(e => e.Users).Ignore();

            AutoMap();
        }
    }

    public class ListMap : ClassMapper<List>
    {
        public ListMap() {
            Map(e => e.Id).Key(KeyType.Guid);
            Map(e => e.Cards).Ignore();

            AutoMap();
        }
    }

    public class BoardUserMap : ClassMapper<BoardUser>
    {
        public BoardUserMap() {
            Map(e => e.BoardId).Key(KeyType.Assigned);
            Map(e => e.UserId).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}
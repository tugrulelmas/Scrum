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
}
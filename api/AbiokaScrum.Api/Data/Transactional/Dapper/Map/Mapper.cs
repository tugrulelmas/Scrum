using AbiokaScrum.Api.Entities;
using DapperExtensions.Mapper;

namespace AbiokaScrum.Api.Data.Transactional.Dapper.Map
{
    public class UserMap : ClassMapper<User>
    {
        public UserMap() {
            Map(e => e.Id).Key(KeyType.Guid);

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

    public class CardMap : ClassMapper<Card>
    {
        public CardMap() {
            Map(e => e.Id).Key(KeyType.Guid);
            Map(e => e.Labels).Ignore();
            Map(e => e.Users).Ignore();
            Map(e => e.Comments).Ignore();

            AutoMap();
        }
    }

    public class CardUserMap : ClassMapper<CardUser>
    {
        public CardUserMap() {
            Map(e => e.CardId).Key(KeyType.Assigned);
            Map(e => e.UserId).Key(KeyType.Assigned);

            AutoMap();
        }
    }

    public class CardLabelMap : ClassMapper<CardLabel>
    {
        public CardLabelMap() {
            Map(e => e.CardId).Key(KeyType.Assigned);
            Map(e => e.LabelId).Key(KeyType.Assigned);

            AutoMap();
        }
    }

    public class CommentMap : ClassMapper<Comment>
    {
        public CommentMap() {
            Map(e => e.Id).Key(KeyType.Guid);
            Map(e => e.User).Ignore();

            AutoMap();
        }
    }
}
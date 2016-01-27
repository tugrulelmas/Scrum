using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaScrum.Api.Data.Mock
{
    public class BoardCollection : CollectionBase<Board>
    {
        private static ConcurrentDictionary<Guid, Board> values = new ConcurrentDictionary<Guid, Board>();

        static BoardCollection() {
            var lists = ListCollection.Values;

            var board1 = new Board { Id = Guid.NewGuid(), Name = "Board - 1", CreateDate = DateTime.Now, IsDeleted = true, Lists = lists };
            var board2 = new Board { Id = Guid.NewGuid(), Name = "Board - 2", CreateDate = DateTime.Now, IsDeleted = true };
            var board3 = new Board { Id = Guid.NewGuid(), Name = "Board - 3", CreateDate = DateTime.Now, IsDeleted = false };

            values.TryAdd(board1.Id, board1);
            values.TryAdd(board2.Id, board2);
            values.TryAdd(board3.Id, board3);
        }

        public override IEnumerable<Board> GetAll() {
            return values.Values.OrderBy(v => v.CreateDate);
        }

        public override Board GetByKey(object key) {
            var id = new Guid(key.ToString());
            return values[id];
        }

        public override IEnumerator<Board> GetEnumerator() {
            return values.Values.GetEnumerator();
        }
    }
}
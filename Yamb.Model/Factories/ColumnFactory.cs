using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public static class ColumnFactory
    {
        public static Column GetColumn(ColumnTypes columnType)
        {
            switch (columnType)
            {
                case ColumnTypes.DOWN:
                    return new DownColumn();
                case ColumnTypes.UP:
                    return new UpColumn();
                case ColumnTypes.FREE:
                    return new FreeColumn();
                case ColumnTypes.ANNOUNCEMENT:
                    return new AnnouncementColumn();
                default:
                    throw new YambException("");
            }
        }
    }
}

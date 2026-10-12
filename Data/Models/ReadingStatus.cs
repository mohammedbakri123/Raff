using NpgsqlTypes;

namespace Raff.Models;

[PgName("reading_status")]
public enum ReadingStatus
{
    [PgName("WANT_TO_READ")]
    WANT_TO_READ,

    [PgName("READING")]
    READING,

    [PgName("FINISHED")]
    FINISHED
}

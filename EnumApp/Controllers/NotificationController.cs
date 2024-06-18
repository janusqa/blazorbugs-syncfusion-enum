
using System.Text.RegularExpressions;
using EnumApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace EnumApp.Controllers
{
    [ApiController]
    [Route("api/notifications")]  // hard coded route name
    public class NotificationController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public NotificationController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("odata")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<object>> ODataGet()
        {
            var rx = new Regex(@"\(([a-zA-Z0-9_/]+?) ([a-z]{2}) (?!(?:.*?datetime\\.*?\\))(\S+)\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var queryString = Request.Query;
            var whereConditions = string.Empty;
            List<SqliteParameter> sqlParams = [];

            int skip = queryString.TryGetValue("$skip", out StringValues Skip) ? Convert.ToInt32(Skip[0]) : 0;
            int top = queryString.TryGetValue("$top", out StringValues Take) ? Convert.ToInt32(Take[0]) : 20;
            sqlParams.Add(new SqliteParameter("Offset", skip));
            sqlParams.Add(new SqliteParameter("Limit", top));
            string? filterString = queryString.TryGetValue("$filter", out StringValues Filter) && !string.IsNullOrWhiteSpace(Filter[0])
                            ? Convert.ToString(Filter[0]) : null;

            if (filterString is not null)
            {
                MatchCollection matches = rx.Matches(filterString);
                if (matches.Count > 0)
                {
                    var field = matches[0].Groups[1].Value.Split("/", StringSplitOptions.TrimEntries).Last();
                    var param = "P0";
                    var value = matches[0].Groups[3].Value;
                    whereConditions = $"{field} = @{param}";
                    sqlParams.Add(new SqliteParameter(param, value));
                }
            }
            whereConditions = string.IsNullOrWhiteSpace(whereConditions) ? "NULL IS NULL" : whereConditions;

            var recordCount = (await _db.Database.SqlQueryRaw<int>($@"
                SELECT COUNT(Id) FROM Notifications WHERE {whereConditions};
            ", sqlParams.ToArray()).ToListAsync()).FirstOrDefault();

            var data = await _db.Notifications.FromSqlRaw($@"
                SELECT * FROM Notifications WHERE {whereConditions} LIMIT @Limit OFFSET @Offset;
            ", sqlParams.ToArray()).ToListAsync();

            return new { Items = data, Count = recordCount };
        }
    }
}
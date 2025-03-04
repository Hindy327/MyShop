using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposetories
{
    public class RatingRepository : IRatingRepository
    {
        _327725412WebApiContext ConectDb;
        public RatingRepository(_327725412WebApiContext ConectDb)
        {
            this.ConectDb = ConectDb;
        }
        int rowAffected = 0;
        public async void addRating(Rating rating)
        {
            string query = "INSERT INTO RATING(HOST, METHOD,PATH, REFERER, USER_AGENT,Record_Date)" +
            "VALUES(@HOST, @METHOD, @PATH, @REFERER, @USER_AGENT,@Record_Date)";

            using (SqlConnection cn = new SqlConnection("data source=srv2\\pupils;initial catalog=327725412_webApi;Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=true"))

            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.Add("@HOST", SqlDbType.NChar, 50).Value = rating.Host;
                cmd.Parameters.Add("@METHOD", SqlDbType.NChar, 10).Value = rating.Method;
                cmd.Parameters.Add("@PATH", SqlDbType.NVarChar, 50).Value = rating.Path;
                cmd.Parameters.Add("@REFERER", SqlDbType.NVarChar, 100).Value = rating.Referer;
                cmd.Parameters.Add("@USER_AGENT", SqlDbType.NVarChar, int.MaxValue).Value = rating.UserAgent;
                cmd.Parameters.Add("@Record_Date", SqlDbType.DateTime).Value = rating.RecordDate;

                cn.Open();
                rowAffected = await cmd.ExecuteNonQueryAsync();
                cn.Close();

            }

        }

        }
}

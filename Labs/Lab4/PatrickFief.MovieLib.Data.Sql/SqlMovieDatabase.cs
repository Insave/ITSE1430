/*
 * ITSE 1430
 * Lab 4
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickFief.MovieLib.Data.Sql
{
    /// <summary>Provides an implementation of <see cref="IMovieDatabase"/> using SQL Server.</summary>
    public class SqlMovieDatabase : MovieDatabase
    {
        /// <summary>Initializes an instance of the <see cref="SqlMovieDatabase"/> class.</summary>
        /// <param title="connectionString">The connection string.</param>
        /// <exception cref="ArgumentNullException"><paramref title="connectionString"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref title="connectionString"/> is empty.</exception>
        public SqlMovieDatabase ( string connectionString )
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));
            if (connectionString == "")
                throw new ArgumentException("Connection string cannot be empty.",
                                            nameof(connectionString));

            _connectionString = connectionString;
        }

        protected override Movie AddCore( Movie movie )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("AddMovie", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@title", movie.Title);
                cmd.Parameters.AddWithValue("@description", movie.Description);
                cmd.Parameters.AddWithValue("@length", movie.Length);

                var parm = cmd.CreateParameter();
                parm.ParameterName= "@isOwned";
                parm.DbType = System.Data.DbType.Boolean;
                parm.Value = movie.IsOwned;
                cmd.Parameters.Add(parm);

                conn.Open();
                var result = cmd.ExecuteScalar();

                var id = Convert.ToInt32(result);
                movie.Id = id;
            };

            return movie;
        }

        protected override IEnumerable<Movie> GetAllCore()
        {
            var items = new List<Movie>();

            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetAllMovies", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();

                var ds = new DataSet();

                var da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);

                if (ds.Tables.Count == 1)
                {
                    foreach (var row in ds.Tables[0].Rows.OfType<DataRow>())
                    {
                        var movie = new Movie() {
                            Id = Convert.ToInt32(row["Id"]),
                            Title = row.Field<string>("Title"),
                            Description = row.Field<string>("Description"),
                            Length = row.Field<int>("Length"),
                            IsOwned = row.Field<bool>("IsOwned")
                        };

                        items.Add(movie);
                    };
                };
            };

            return items;
        }

        protected override Movie GetCore( int id )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetMovie", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", id));

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return ReadData(reader);
                };
            };

            return null;
        }

        protected override Movie GetMovieByNameCore( string title )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetAllMovies", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var movie = ReadData(reader);
                        if (String.Compare(movie.Title, title, true) == 0)
                            return movie;
                    };
                };
            };

            return null;
        }

        private static Movie ReadData( SqlDataReader reader )
                        => new Movie() {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader.GetFieldValue<string>(1),
                                Description = reader.GetString(2),
                                Length = reader.GetInt32(3),   
                                IsOwned = reader.GetBoolean(4)
                            };

        protected override void RemoveCore( int id )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("RemoveMovie", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", id));

                conn.Open();
                cmd.ExecuteNonQuery();
            };
        }

        protected override Movie UpdateCore( Movie movie )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("UpdateMovie", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@id", movie.Id));
                cmd.Parameters.AddWithValue("@title", movie.Title);
                cmd.Parameters.AddWithValue("@length", movie.Length);
                cmd.Parameters.AddWithValue("@description", movie.Description);

                var parm = cmd.CreateParameter();
                parm.ParameterName= "@IsOwned";
                parm.DbType = System.Data.DbType.Boolean;
                parm.Value = movie.IsOwned;
                cmd.Parameters.Add(parm);

                conn.Open();
                cmd.ExecuteNonQuery();
            };

            return movie;
        }

        #region Private Members

        private readonly string _connectionString;
        #endregion
    }
}

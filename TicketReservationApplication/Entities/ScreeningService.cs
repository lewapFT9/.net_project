namespace TicketReservationApplication.Entities
{
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;

    public class ScreeningService
    {
        private readonly string _connectionString;

        public ScreeningService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task UpdateAttendancesForEndedScreenings()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                SELECT Id 
                FROM Screenings 
                WHERE EndDate <= GETDATE();";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var screeningId = reader.GetInt32(0);

                            await CallStoredProcedureForScreening(screeningId, connection);
                        }
                    }
                }
            }
        }

        private async Task CallStoredProcedureForScreening(int screeningId, SqlConnection connection)
        {
            using (var command = new SqlCommand("UpdateScreeningAttendance", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ScreeningId", screeningId);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}

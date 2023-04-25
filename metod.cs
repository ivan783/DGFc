using System.Data.SqlClient;
using System.Threading.Tasks;

public async Task UpdateQrserClientBachAtmIdAsync(int Id, int QrserId, int bachAtmId)
{
    string connectionString = "Data Source=MiServidor;Initial Catalog=MiBaseDeDatos;User ID=MiUsuario;Password=MiContraseña;";
    string storedProcedureName = "UpdateQrserClientBachAtmId";

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
            idParameter.Value = Id;
            command.Parameters.Add(idParameter);

            SqlParameter qrserIdParameter = new SqlParameter("@QrserId", System.Data.SqlDbType.Int);
            qrserIdParameter.Value = QrserId;
            command.Parameters.Add(qrserIdParameter);

            SqlParameter bachAtmIdParameter = new SqlParameter("@bachAtmId", System.Data.SqlDbType.Int);
            bachAtmIdParameter.Value = bachAtmId;
            command.Parameters.Add(bachAtmIdParameter);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }
    }
}


public async Task UpdateQrserClientBachAtmIdAsync(int Id, int QrserId, int bachAtmId)
{

var value = Context.ExecuteScalar<int>(
    " cw.SpUpdateUser @Id, @QrserId, @bachAtmId",
    new SqlParameter("@Id", System.Data.SqlDbType.Int),
    new SqlParameter("@QrserId", System.Data.SqlDbType.Int),
    new SqlParameter("@bachAtmId", System.Data.SqlDbType.Int),
    operationParameter
);

}

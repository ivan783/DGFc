public int SaveBranchQRPayment(int? id, int businessQRPaymentId, string branchCode, string branchName, string userToken, string userName, string userCreation, 
            string userModification, DateTime dateCreation, DateTime? dateModification, bool isDeleted, string userBranchQR, string qrUserId, string city)
{
    try
    {
        logger.LogInformation("Guardando datos de BranchQRPayments");
        var option = "";
        // Crear el parámetro que indica si es una operación de inserción o actualización
        var operationParameter = new SqlParameter("@Operation", SqlDbType.NVarChar)
        {
            Value = id == 0 ? "Insert" : "Update"
        };
        option = id <= 0 ? option = "INSERT" : option = "UPDATE";
        var value = Context.ExecuteScalar<int>(
            "cw.SpBranchQRPaymentsUpIns @Option,@Id,@BusinessQRPaymentId,@BranchCode,@BranchName,@UserToken,@UserName," +
            "@UserCreation,@UserModification,@DateCreation,@DateModification,@IsDeleted,"+
            "@UserBranchQR,@QrUserId,@City",
            new SqlParameter("@Option", option),
            id == 0 ? new SqlParameter("@Id", DBNull.Value) : new SqlParameter("@Id", id),
            new SqlParameter("@BusinessQRPaymentId", businessQRPaymentId),
            new SqlParameter("@BranchCode", branchCode),
            new SqlParameter("@BranchName", branchName),
            string.IsNullOrEmpty(userToken) ? new SqlParameter("@UserToken", DBNull.Value) : new SqlParameter("@UserToken", userToken),
            string.IsNullOrEmpty(userName) ? new SqlParameter("@UserName", DBNull.Value) : new SqlParameter("@UserName", userName),
            new SqlParameter("@UserCreation", userCreation),
            string.IsNullOrEmpty(userModification) ? new SqlParameter("@UserModification", DBNull.Value) : new SqlParameter("@UserModification", userModification),
            new SqlParameter("@DateCreation", dateCreation),
            !dateModification.HasValue ? new SqlParameter("@DateModification", DBNull.Value) : new SqlParameter("@DateModification", dateModification),
            new SqlParameter("@IsDeleted", isDeleted),
            new SqlParameter("@UserBranchQR", userBranchQR),
            new SqlParameter("@QrUserId", qrUserId),
            new SqlParameter("@City", city),
            operationParameter
        );
        return value;
    }
    catch(Exception ex)
    {
        // manejar la excepción aquí
    }
}

int branchQRPaymentId = SaveBranchQRPayment(id, businessQRPaymentId, branchCode, branchName, userToken, userName, userCreation, userModification, dateCreation, dateModification, isDeleted, userBranchQR, qrUserId, city);

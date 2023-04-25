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


public async Task<Result<GetRegBranchQRResponse>> SaveRegBranch(BusinesBranchId bisinessId)
        {
            GetRegBranchQRResponse response = new GetRegBranchQRResponse();
            GetRegBranchQRRequest request = new GetRegBranchQRRequest();
            var appUserId = configuration.GetSection("Connectors").GetSection("ApiQR")["AppUserId"];
            var publicToken = configuration.GetSection("Connectors").GetSection("ApiQR")["PublicToken"];
            var channel = configuration.GetSection("Connectors").GetSection("ApiQR")["Channel"];
            var url = configuration.GetSection("Connectors").GetSection("ApiQR")["AddresRegBusiness"];
            var businessQr = Context.BusinessQRPayments.Where(x => x.CompanyId == this.companyId).FirstOrDefault();
            request.PublicToken = publicToken;
            request.AppUserId = appUserId;
            request.BusinessCode = 1;
            request.BranchName = "GF" + this.companyId;
            request.City = "";
            request.Channel = channel;
            var aux = JsonConvert.SerializeObject(request);
            logger.LogError($"Request: {aux}");
            response = this.manager.SaveRegBranchQR(request, url);
            if (response.State=="00")
            {
                int branchQRPaymentId = SaveBranchQRPayment(0, bisinessId.businessId, response.Data.BranchCode.ToString(), "", "Not", "Not",
                                    "Migra", "", DateTime.Now, DateTime.Now, false, "Not", 34, "City");
            }


            return Result<GetRegBranchQRResponse>.SetOk(response);
        }

public async Task<int> SaveBranchQRPaymentAsync(int? id, int businessQRPaymentId, string branchCode, string branchName, string userToken, string userName, string userCreation, 
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
        var value = await Context.ExecuteScalarAsync<int>(
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
        logger.LogError(ex, "Error al guardar los datos de BranchQRPayments");
        // manejar la excepción aquí
        throw;
    }
}



public async Task<Result<GetRegBranchQRResponse>> SaveRegBranch(BusinesBranchId bisinessId)
{
    GetRegBranchQRResponse response = new GetRegBranchQRResponse();
    GetRegBranchQRRequest request = new GetRegBranchQRRequest();
    var appUserId = configuration.GetSection("Connectors").GetSection("ApiQR")["AppUserId"];
    var publicToken = configuration.GetSection("Connectors").GetSection("ApiQR")["PublicToken"];
    var channel = configuration.GetSection("Connectors").GetSection("ApiQR")["Channel"];
    var url = configuration.GetSection("Connectors").GetSection("ApiQR")["AddresRegBusiness"];
    var businessQr = Context.BusinessQRPayments.Where(x => x.CompanyId == this.companyId).FirstOrDefault();
    request.PublicToken = publicToken;
    request.AppUserId = appUserId;
    request.BusinessCode = 1;
    request.BranchName = "GF" + this.companyId;
    request.City = "";
    request.Channel = channel;
    var aux = JsonConvert.SerializeObject(request);
    logger.LogError($"Request: {aux}");
    response = await this.manager.SaveRegBranchQRAsync(request, url); // Se espera la respuesta del método asincrónico
    if (response.State=="00")
    {
        // Se llama al método SaveBranchQRPaymentAsync y se espera su ejecución
        int branchQRPaymentId = await SaveBranchQRPaymentAsync(0, bisinessId.businessId, response.Data.BranchCode.ToString(), "", "Not", "Not",
                                "Migra", "", DateTime.Now, DateTime.Now, false, "Not", 34, "City");
    }

    return Result<GetRegBranchQRResponse>.SetOk(response);
}


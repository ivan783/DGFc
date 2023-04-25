public static string EncryptDecrypt(bool encrypt, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            try
            {
                int size = text.Length * 2 + 1;
                StringBuilder outString = new (size);
                _ = EncryptDecryptReader(encrypt, text, outString, ref size);
                return outString.ToString();
            }
            catch (Exception ex)
            {
                //Logger.Error("Message: {0}; Exception: {1}", ex.Message, Json.ToObject(ex));
                return string.Empty;
            }
        }


public async Task<Result<GetRegUserQRResponse>> RegUsers(ReqRegUserDto dto)
        {
            try
            {
                GetRegUserQRRequest request = new GetRegUserQRRequest();
                GetRegUserQRResponse response = new GetRegUserQRResponse();
                var appUserId = configuration.GetSection("Connectors").GetSection("ApiQR")["AppUserId"];
                var publicToken = configuration.GetSection("Connectors").GetSection("ApiQR")["PublicToken"];
                var channel = configuration.GetSection("Connectors").GetSection("ApiQR")["Channel"];
                var url = configuration.GetSection("Connectors").GetSection("ApiQR")["AddressRegUsers"];

                request.AppUserId = appUserId;
                request.Channel = channel;
                request.PublicToken = publicToken;
               // request.RoleCode = dto.roleCode;
                request.BusinessCode = dto.businessCode;
                request.TypeUser = "CW";

                var result = Result<GetRegUserQRResponse>.SetOk(responseRes);
                foreach (var addUser in dto.ClientResponseUser)
                {
                    request.Client= new Client();
                    request.Client.Name = addUser.name;
                    request.Client.Password = EncryptDecrypt(true, addUser.password);
                    request.Client.UserName = addUser.userName;
                    request.Client.Email = addUser.email;
                    request.Client.DocumentComplement = addUser.documentComplement=="" ? "00": addUser.documentComplement;
                    request.Client.DocumentExtension = addUser.documentExtension;
                    request.Client.DocumentNumber = addUser.documentNumber;
                    if (addUser.documentType == "C.I.")
                    {
                        request.Client.DocumentType = "Q";
                    }else if (addUser.documentType == "RUC")
                    {
                        request.Client.DocumentType = "R";
                    }
                    else if (addUser.documentType == "NIT")
                    {
                        request.Client.DocumentType = "R";
                    }
                    else if (addUser.documentType == "PASAPORTE")
                    {
                        request.Client.DocumentType = "P";
                    }else
                    {
                        request.Client.DocumentType = "O";
                    }
                    request.Client.Cellphone = addUser.cellphone;
                    request.RoleCode = addUser.roleCode;
                    request.Client.PasswordExpirationDate = DateTime.Now.ToString();
                    

                    request.Device = new Device();
                    request.Device.Id = "01";
                    request.Device.Type = "PC";
                    request.Device.Name = "PC T";
                    request.Device.Os = "Windows 10";
                    var aux = JsonConvert.SerializeObject(request);
                    logger.LogError($"Request: {aux}");
                    response = this.manager.SaveRegUserQR(request, url);
                    if (response?.State=="00")
                    {
                        logger.LogError($"UserIdQR:-----{response.Data.Client.Id}----");
                        InsertarRegistro(null, companyId, 0, response.Data.Client.Id, request.RoleCode, addUser.typeUser, request.Client.Name,
                        response?.Data?.Client?.UserName, response?.Data?.Client?.Token.ToString(), request.Client.Password, request.Client.Email, request.Client.Cellphone, request.Client.DocumentNumber,
                        request.Client.DocumentType, request.Client.DocumentExtension, request.Client.DocumentComplement, "Migra",
                        "migra", DateTime.Now, DateTime.Now, DateTime.Now, false);
                    }                   
                }
                 return response.State != "00" ? Result<GetRegUserQRResponse>.SetError($"Mensaje de error: {response.Message}") :
                result;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error al obtener monitoreo: {ex.Message}");
                return Result<GetRegUserQRResponse>.SetError("Error al Registrar user");
            }
        }



public async Task<Result<GetRegUserQRResponse>> RegUsers(ReqRegUserDto dto) 
         { 
             try 
             { 
                 GetRegUserQRRequest request = new GetRegUserQRRequest(); 
                 GetRegUserQRResponse response = new GetRegUserQRResponse(); 
                 var appUserId = configuration.GetSection("Connectors").GetSection("ApiQR")["AppUserId"]; 
                 var publicToken = configuration.GetSection("Connectors").GetSection("ApiQR")["PublicToken"]; 
                 var channel = configuration.GetSection("Connectors").GetSection("ApiQR")["Channel"]; 
                 var url = configuration.GetSection("Connectors").GetSection("ApiQR")["AddressRegUsers"]; 
  
                 request.AppUserId = appUserId; 
                 request.Channel = channel; 
                 request.PublicToken = publicToken; 
                // request.RoleCode = dto.roleCode; 
                 request.BusinessCode = dto.businessCode; 
                 request.TypeUser = "CW"; 
  
                 var result = Result<GetRegUserQRResponse>.SetOk(responseRes); 
                 foreach (var addUser in dto.ClientResponseUser) 
                 { 
                     request.Client= new Client(); 
                     request.Client.Name = addUser.name; 
                     request.Client.Password = EncryptDecrypt(true, addUser.password); 
                     request.Client.UserName = addUser.userName; 
                     request.Client.Email = addUser.email; 
                     request.Client.DocumentComplement = addUser.documentComplement=="" ? "00": addUser.documentComplement; 
                     request.Client.DocumentExtension = addUser.documentExtension; 
                     request.Client.DocumentNumber = addUser.documentNumber; 
                     if (addUser.documentType == "C.I.") 
                     { 
                         request.Client.DocumentType = "Q"; 
                     }else if (addUser.documentType == "RUC") 
                     { 
                         request.Client.DocumentType = "R"; 
                     } 
                     else if (addUser.documentType == "NIT") 
                     { 
                         request.Client.DocumentType = "R"; 
                     } 
                     else if (addUser.documentType == "PASAPORTE") 
                     { 
                         request.Client.DocumentType = "P"; 
                     }else 
                     { 
                         request.Client.DocumentType = "O"; 
                     } 
                     request.Client.Cellphone = addUser.cellphone; 
                     request.RoleCode = addUser.roleCode; 
                     request.Client.PasswordExpirationDate = DateTime.Now.ToString(); 
                      
  
                     request.Device = new Device(); 
                     request.Device.Id = "01"; 
                     request.Device.Type = "PC"; 
                     request.Device.Name = "PC T"; 
                     request.Device.Os = "Windows 10"; 
                     var aux = JsonConvert.SerializeObject(request); 
                     logger.LogError($"Request: {aux}"); 
                     response = this.manager.SaveRegUserQR(request, url); 
                     if (response?.State=="00") 
                     { 
                         logger.LogError($"UserIdQR:-----{response.Data.Client.Id}----"); 
                         InsertarRegistro(null, companyId, 0, response.Data.Client.Id, request.RoleCode, addUser.typeUser, request.Client.Name, 
                         response?.Data?.Client?.UserName, response?.Data?.Client?.Token.ToString(), request.Client.Password, request.Client.Email, request.Client.Cellphone, request.Client.DocumentNumber, 
                         request.Client.DocumentType, request.Client.DocumentExtension, request.Client.DocumentComplement, "Migra", 
                         "migra", DateTime.Now, DateTime.Now, DateTime.Now, false); 
                     }                    
                 } 
                  return response.State != "00" ? Result<GetRegUserQRResponse>.SetError($"Mensaje de error: {response.Message}") : 
                 result; 
             } 
             catch (Exception ex) 
             { 
                 logger.LogError($"Error al obtener monitoreo: {ex.Message}"); 
                 return Result<GetRegUserQRResponse>.SetError("Error al Registrar user"); 
             } 
         }

CREATE PROCEDURE UpdateQrserClientBachAtmId
    @Id int,
    @QrserId int,
    @bachAtmId int
AS
BEGIN
    UPDATE cw.QrserClient
    SET bachAtmId = @bachAtmId
    WHERE Id = @Id AND QrserId = @QrserId;
END


CREATE PROCEDURE [cw].[SpBranchQRPaymentsUpIns]
	@Option VARCHAR(10) = NULL,-- Parámetro opcional que determina la opción (por defecto es SELECT)
	@Id INT = NULL, -- Si el Id es NULL, se insertará un nuevo registro
	@BusinessQRPaymentId INT,
	@BranchCode NVARCHAR(20),
	@BranchName NVARCHAR(100),
	@UserToken NVARCHAR(120)=NULL,
	@UserName NVARCHAR(80)=NULL,
	@UserCreation NVARCHAR(6),
	@UserModification NVARCHAR(6)=NULL,
	@DateCreation DATETIME,
	@DateModification DATETIME=NULL,
	@IsDeleted BIT,
	@UserBranchQR NVARCHAR(100)=NULL,
	@QrUserId NVARCHAR(60)=NULL,
	@City NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	IF @Option='SELECT'
    BEGIN
        BEGIN
            SELECT *
            FROM [cw].[BranchQRPayments]
			WHERE [BusinessQRPaymentId]=@BusinessQRPaymentId;
        END
    END
    ELSE IF @Option='UPDATE'
	BEGIN
		IF @Id IS NOT NULL
		BEGIN
			-- Actualizar registro existente
			UPDATE [cw].[BranchQRPayments]
			SET [BranchName] = @BranchName,
				[UserToken] = @UserToken,
				[UserName] = @UserName,
				[UserModification] = @UserModification,
				[DateModification] = @DateModification,
				[IsDeleted] = @IsDeleted,
				[UserBranchQR] = @UserBranchQR,
				[QrUserId] = @QrUserId,
				[City] = @City
			WHERE [Id] = @Id;
		END
	END
	ELSE IF @Option='INSERT'
	BEGIN
		BEGIN
			-- Insertar nuevo registro
			INSERT INTO [cw].[BranchQRPayments] ([BusinessQRPaymentId], [BranchCode], [BranchName], [UserToken], [UserName], [UserCreation], [DateCreation], [IsDeleted], [UserBranchQR], [QrUserId], [City])
			VALUES (@BusinessQRPaymentId, @BranchCode, @BranchName, @UserToken, @UserName, @UserCreation, @DateCreation, @IsDeleted, @UserBranchQR, @QrUserId, @City);
			SELECT @Id = SCOPE_IDENTITY();
		END
	END
	ELSE
	BEGIN
		SELECT [Id], [BusinessQRPaymentId], [BranchCode], [BranchName], [UserToken], [UserName], [UserCreation], [UserModification], [DateCreation], [DateModification], [IsDeleted], [UserBranchQR], [QrUserId], [City]
		FROM [cw].[BranchQRPayments]
		WHERE Id = @Id;
	END
	select @@ROWCOUNT;
END
GO


public void SaveBranchQRPayment(int? id, int businessQRPaymentId, string branchCode, string branchName, string userToken, string userName, string userCreation, 
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
            }

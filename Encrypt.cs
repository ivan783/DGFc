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

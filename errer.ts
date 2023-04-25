src/app/Services/qr/models/RequestSucursal.ts:8:3
    8   businessId: any;
        ~~~~~~~~~~
    'businessId' is declared here.

    public async Task<Result<GetRegBranchQRResponse>> SaveRegBranch(RegRequestBranchDto Dto)
        {
            try
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
                request.Channel = channel;
                foreach (var item in Dto.RequestSucursal)
                {
                    request.BusinessCode = item.BusinessCode;
                    request.BranchName = item.BranchName;
                    request.City = item.City;
                    request.UserId = item.UserId;

                    var aux = JsonConvert.SerializeObject(request);
                    logger.LogError($"Request: {aux}");
                    response = this.manager.SaveRegBranchQR(request, url);
                    if (response.State == "00")
                    {
                        int branchQRPaymentId =
                            SaveBranchQRPayment(0, item.BusinesId, response.Data.BranchCode.ToString(), request.BranchName, "Not", "Not",
                                            "Migra", "", DateTime.Now, DateTime.Now, false, "Not", item.UserId, item.City);
                    }
                }

                return Result<GetRegBranchQRResponse>.SetOk(response);
            }catch(Exception ex)
            {
                logger.LogError($"Error al guardar Branch: {ex.Message}");
                return Result<GetRegBranchQRResponse>.SetError("Error al guardar branch");
            }
        }
public async Task<Result<List<GetRegBranchQRResponse>>> SaveRegBranch(RegRequestBranchDto Dto)
{
    try
    {
        var appUserId = configuration.GetSection("Connectors").GetSection("ApiQR")["AppUserId"];
        var publicToken = configuration.GetSection("Connectors").GetSection("ApiQR")["PublicToken"];
        var channel = configuration.GetSection("Connectors").GetSection("ApiQR")["Channel"];
        var url = configuration.GetSection("Connectors").GetSection("ApiQR")["AddresRegBusiness"];

        var responses = new List<GetRegBranchQRResponse>();
        foreach (var item in Dto.RequestSucursal)
        {
            var request = new GetRegBranchQRRequest
            {
                PublicToken = publicToken,
                AppUserId = appUserId,
                Channel = channel,
                BusinessCode = item.BusinessCode,
                BranchName = item.BranchName,
                City = item.City,
                UserId = item.UserId
            };

            var aux = JsonConvert.SerializeObject(request);
            logger.LogError($"Request: {aux}");
            var response = await this.manager.SaveRegBranchQR(request, url);
            responses.Add(response);

            if (response.State == "00")
            {
                int branchQRPaymentId =
                    SaveBranchQRPayment(0, item.BusinesId, response.Data.BranchCode.ToString(), request.BranchName, "Not", "Not",
                                    "Migra", "", DateTime.Now, DateTime.Now, false, "Not", item.UserId, item.City);
            }
        }

        return Result<List<GetRegBranchQRResponse>>.SetOk(responses);
    }
    catch (Exception ex)
    {
        logger.LogError($"Error al guardar Branch: {ex.Message}");
        return Result<List<GetRegBranchQRResponse>>.SetError("Error al guardar branch");
    }
}

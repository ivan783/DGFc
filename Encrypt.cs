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

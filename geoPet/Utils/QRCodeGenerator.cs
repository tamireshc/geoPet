using QRCoder;

namespace geoPet.Utils
{
    //https://balta.io/blog/aspnet-qrcode
    public class QRCodeGenerator
    {
        public byte[] GenerateQrCode(string type, string id)
        {
            try
            {
                QRCoder.QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode("https://localhost:7225/" + type + "/" + id, QRCoder.QRCodeGenerator.ECCLevel.Q);

                // Crie um objeto QRCode com base nos dados e personalize-o
                var qrCode = new QRCoder.BitmapByteQRCode(qrCodeData);

                var qrCodeImage = qrCode.GetGraphic(20); // 20 é o tamanho do módulo (pixel)

                return qrCodeImage;
            }
            catch (Exception e)
            {
                // Lidar com erros 
                throw new Exception("Erro gennereted QRCode");
            }
        }
    }
}


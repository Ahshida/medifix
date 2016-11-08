using System;
using System.Text;
using System.Web;
using DBO.Data.Objects;

namespace DBO.Data.Managers
{
    public static class AttachmentManager
    {
        public static bool HasJpgHeader(this HttpPostedFileBase postedFile)
        {
            if (!postedFile.ContentType.ToLower().Contains("image/jpg") &&
                !postedFile.ContentType.ToLower().Contains("image/jpeg"))
                return false;
            using (var br = new NoCloseBinaryReader(postedFile.InputStream))
            {
                UInt16 soi = br.ReadUInt16();  // Start of Image (SOI) marker (FFD8)
                UInt16 jfif = br.ReadUInt16(); // JFIF marker (FFE0)
                return soi == 0xd8ff && jfif == 0xe0ff;
            }
        }
        public static bool HasPdfHeader(this HttpPostedFileBase postedFile)
        {
            if (!postedFile.ContentType.ToLower().Contains("application/pdf"))
                return false;
            using (var br = new NoCloseBinaryReader(postedFile.InputStream))
            {
                var buffer = br.ReadBytes(5);

                var enc = new ASCIIEncoding();
                var header = enc.GetString(buffer);
                return header.StartsWith("%PDF-");
            }
        }
    }
}

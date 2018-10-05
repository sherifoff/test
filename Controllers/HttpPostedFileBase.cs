using System;

namespace RealEstate.Controllers
{
    public class HttpPostedFileBase
    {
        public int ContentLength { get; internal set; }
        public object FileName { get; internal set; }

        internal void SaveAs(string path)
        {
            throw new NotImplementedException();
        }
    }
}